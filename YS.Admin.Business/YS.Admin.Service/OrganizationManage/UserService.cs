using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data;
using System.Data.Common;
using YS.Admin.Data.Repository;
using YS.Admin.Entity.OrganizationManage;
using YS.Admin.Enum.OrganizationManage;
using YS.Admin.Model.Param.OrganizationManage;
using YS.Admin.Util;
using YS.Admin.Util.Model;
using YS.Admin.Util.Extension;
using YS.Admin.Enum;
using YS.Admin.Entity;
using YS.Admin.Data.EF;
using YS.Admin.Service.SystemManage;
using YS.Admin.Entity.SystemManage;
using YS.Admin.Web.Code;
using YS.Admin.Cache.Factory;

namespace YS.Admin.Service.OrganizationManage
{
    public class UserService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<UserEntity>> GetList(UserListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<UserEntity>> GetPageList(UserListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<UserEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<UserEntity>(id);
        }

        public async Task<UserEntity> GetEntity(string userName)
        {
            return await this.BaseRepository().FindEntity<UserEntity>(p => p.UserName == userName);
        }
        public async Task<UserEntity> GetEntityByMobile(string Mobile)
        {
            return await this.BaseRepository().FindEntity<UserEntity>(p => p.Mobile == Mobile);
        }
        public async Task<UserEntity> CheckLogin(string userName)
        {
            var expression = LinqExtensions.True<UserEntity>();
            expression = expression.And(t => t.UserName == userName);
            expression = expression.Or(t => t.Mobile == userName);
            expression = expression.Or(t => t.Email == userName);
            return await this.BaseRepository().FindEntity(expression);
        }

        public bool ExistUserName(UserEntity entity)
        {
            var expression = LinqExtensions.True<UserEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.UserName == entity.UserName);
            }
            else
            {
                expression = expression.And(t => t.UserName == entity.UserName && t.Id != entity.Id);
            }
            return this.BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        public bool ExistUserMobile(UserEntity entity)
        {
            var expression = LinqExtensions.True<UserEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.Mobile == entity.Mobile);
            }
            else
            {
                expression = expression.And(t => t.Mobile == entity.Mobile && t.Id != entity.Id);
            }
            return this.BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region 提交数据
        public async Task UpdateUser(UserEntity entity)
        {
            await this.BaseRepository().Update(entity);
        }

        public async Task SaveForm(UserEntity entity)
        {
            var db = await this.BaseRepository().BeginTrans();
            try
            {
                if (entity.Id.IsNullOrZero())
                {
                    await entity.Create();
                    await db.Insert(entity);
                }
                else
                {
                    await db.Delete<UserBelongEntity>(t => t.UserId == entity.Id);

                    // 密码不进行更新，有单独的方法更新密码
                    entity.Password = null;
                    await entity.Modify();
                    await db.Update(entity);
                }
                // 职位
                if (!string.IsNullOrEmpty(entity.PositionIds))
                {
                    foreach (long positionId in TextHelper.SplitToArray<long>(entity.PositionIds, ','))
                    {
                        UserBelongEntity positionBelongEntity = new UserBelongEntity();
                        positionBelongEntity.UserId = entity.Id;
                        positionBelongEntity.BelongId = positionId;
                        positionBelongEntity.BelongType = UserBelongTypeEnum.Position.ParseToInt();
                        await positionBelongEntity.Create();
                        await db.Insert(positionBelongEntity);
                    }
                }
                // 角色
                if (!string.IsNullOrEmpty(entity.RoleIds))
                {
                    foreach (long roleId in TextHelper.SplitToArray<long>(entity.RoleIds, ','))
                    {
                        UserBelongEntity departmentBelongEntity = new UserBelongEntity();
                        departmentBelongEntity.UserId = entity.Id;
                        departmentBelongEntity.BelongId = roleId;
                        departmentBelongEntity.BelongType = UserBelongTypeEnum.Role.ParseToInt();
                        await departmentBelongEntity.Create();
                        await db.Insert(departmentBelongEntity);
                    }
                }
                await db.CommitTrans();
            }
            catch
            {
                await db.RollbackTrans();
                throw;
            }
        }

        public async Task DeleteForm(string ids)
        {
            var db = await this.BaseRepository().BeginTrans();
            try
            {
                long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
                await db.Delete<UserEntity>(idArr);
                await db.Delete<UserBelongEntity>(t => idArr.Contains(t.UserId.Value));
                await db.CommitTrans();
            }
            catch
            {
                await db.RollbackTrans();
                throw;
            }
        }

        public async Task ResetPassword(UserEntity entity)
        {
            await entity.Modify();
            await this.BaseRepository().Update(entity);
        }

        public async Task ChangeUser(UserEntity entity)
        {
            await entity.Modify();
            await this.BaseRepository().Update(entity);
        }
        public async Task<UserEntity> LockJson()
        {
            OperatorInfo user = await Operator.Instance.Current();
            //查询当前用户信息
            UserEntity data = await this.BaseRepository().FindEntity<UserEntity>(user.UserId.ParseToLong());
            data.LockStatus = 1;
            await this.BaseRepository().Update(data);
            return data;
        }
        public async Task<bool> UnLockJson(string entity)
        {
            OperatorInfo user = await Operator.Instance.Current();
            //查询当前用户信息
            UserEntity data = await this.BaseRepository().FindEntity<UserEntity>(user.UserId.ParseToLong());
            if (data.LockPassword == entity)
            {
                data.LockStatus = 0;

                await data.Modify();

                await this.BaseRepository().Update(data);
                await RemoveCacheById(data.Id.Value);
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<bool> UnLockByIdJson(string id)
        {
            //查询当前用户信息
            UserEntity data = await this.BaseRepository().FindEntity<UserEntity>(id.ParseToLong());

            data.LockStatus = 0;

            await data.Modify();

            await this.BaseRepository().Update(data);
            await RemoveCacheById(data.Id.Value);
            return true;
        }
        #endregion

        private async Task RemoveCacheById(long id)
        {
            var dbEntity = await this.BaseRepository().FindEntity<UserEntity>(id);
            if (dbEntity != null)
            {
                CacheFactory.Cache.RemoveCache(dbEntity.WebToken);
            }
        }

        #region 私有方法
        private Expression<Func<UserEntity, bool>> ListFilter(UserListParam param)
        {
            var expression = LinqExtensions.True<UserEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.UserName))
                {
                    expression = expression.And(t => t.UserName.Contains(param.UserName));
                }
                if (!string.IsNullOrEmpty(param.UserIds))
                {
                    long[] userIdList = TextHelper.SplitToArray<long>(param.UserIds, ',');
                    expression = expression.And(t => userIdList.Contains(t.Id.Value));
                }
                if (!string.IsNullOrEmpty(param.Mobile))
                {
                    expression = expression.And(t => t.Mobile.Contains(param.Mobile));
                }
                if (param.UserStatus > -1)
                {
                    expression = expression.And(t => t.UserStatus == param.UserStatus);
                }
                if (!string.IsNullOrEmpty(param.StartTime.ToString()))
                {
                    expression = expression.And(t => t.BaseModifyTime >= param.StartTime);
                }
                if (!string.IsNullOrEmpty(param.EndTime.ToString()))
                {
                    param.EndTime = param.EndTime.Value.Date.Add(new TimeSpan(23, 59, 59));
                    expression = expression.And(t => t.BaseModifyTime <= param.EndTime);
                }
                if (param.ChildrenDepartmentIdList != null && param.ChildrenDepartmentIdList.Count > 0)
                {
                    expression = expression.And(t => param.ChildrenDepartmentIdList.Contains(t.DepartmentId.Value));
                }
            }
            return expression;
        }
        #endregion
    }
}

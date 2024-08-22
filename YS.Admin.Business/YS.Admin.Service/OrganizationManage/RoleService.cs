using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using YS.Admin.Data.Repository;
using YS.Admin.Entity.SystemManage;
using YS.Admin.Enum.SystemManage;
using YS.Admin.Model.Param.SystemManage;
using YS.Admin.Util.Extension;
using YS.Admin.Util.Model;
using YS.Admin.Util;
using System.Data.Common;
using System.Text;
using YS.Admin.Data;
using YS.Admin.Model.Param.OrganizationManage;
using YS.Admin.Entity.OrganizationManage;
using YS.Admin.Model.Result.SystemManage;

namespace YS.Admin.Service.OrganizationManage
{
    public class RoleService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<RoleEntity>> GetList(RoleListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<RoleEntity>> GetPageList(RoleListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<RoleEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<RoleEntity>(id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await BaseRepository().FindObject("SELECT MAX(RoleSort) FROM base_role");
            int sort = result.ParseToInt();
            sort++;
            return sort;
        }

        public bool ExistRoleName(RoleEntity entity)
        {
            var expression = LinqExtensions.True<RoleEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.RoleName == entity.RoleName);
            }
            else
            {
                expression = expression.And(t => t.RoleName == entity.RoleName && t.Id != entity.Id);
            }
            return BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }


        public async Task<List<UserRoleInfo>> GetUserRoleList(RoleListParam param)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await BaseRepository().FindList<UserRoleInfo>(strSql.ToString(), filter.ToArray());
            return list.ToList();
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(RoleEntity entity)
        {
            var db = await BaseRepository().BeginTrans();
            try
            {
                if (entity.Id.IsNullOrZero())
                {
                    await entity.Create();
                    await db.Insert(entity);
                }
                else
                {
                    await db.Delete<MenuAuthorizeEntity>(t => t.AuthorizeId == entity.Id);
                    await entity.Modify();
                    await db.Update(entity);
                }
                // 角色对应的菜单、页面和按钮权限
                if (!string.IsNullOrEmpty(entity.MenuIds))
                {
                    foreach (long menuId in TextHelper.SplitToArray<long>(entity.MenuIds, ','))
                    {
                        MenuAuthorizeEntity menuAuthorizeEntity = new MenuAuthorizeEntity();
                        menuAuthorizeEntity.AuthorizeId = entity.Id;
                        menuAuthorizeEntity.MenuId = menuId;
                        menuAuthorizeEntity.AuthorizeType = AuthorizeTypeEnum.Role.ParseToInt();
                        await menuAuthorizeEntity.Create();
                        await db.Insert(menuAuthorizeEntity);
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
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<RoleEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private Expression<Func<RoleEntity, bool>> ListFilter(RoleListParam param)
        {
            var expression = LinqExtensions.True<RoleEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.RoleName))
                {
                    expression = expression.And(t => t.RoleName.Contains(param.RoleName));
                }
                if (!string.IsNullOrEmpty(param.RoleIds))
                {
                    long[] roleIdArr = TextHelper.SplitToArray<long>(param.RoleIds, ',');
                    expression = expression.And(t => roleIdArr.Contains(t.Id.Value));
                }
                if (!string.IsNullOrEmpty(param.RoleName))
                {
                    expression = expression.And(t => t.RoleName.Contains(param.RoleName));
                }
                if (param.RoleStatus > -1)
                {
                    expression = expression.And(t => t.RoleStatus == param.RoleStatus);
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
            }
            return expression;
        }

        private List<DbParameter> ListFilter(RoleListParam param, StringBuilder strSql)
        {
            strSql.Append(@"SELECT
                            	a.BelongId AS RoleId,
                            	a.UserId,
                            	r.RoleName 
                            FROM
                            	sys_userbelong a
                            	LEFT JOIN base_role r ON r.Id = a.BelongId 
                            WHERE
                            	1 = 1 
                            	AND a.BelongType = 2");
            var parameter = new List<DbParameter>();
            if (param != null)
            {

            }
            return parameter;
        }

        #endregion
    }
}

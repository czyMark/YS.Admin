using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using YS.Admin.Util;
using YS.Admin.Util.Extension;
using YS.Admin.Util.Model;
using YS.Admin.Entity.BlogManage;
using YS.Admin.Model.Param.BlogManage;
using YS.Admin.Service.BlogManage;
using System.Linq.Expressions;
using YS.Admin.Entity.OrganizationManage;
using YS.Admin.Enum;
using YS.Admin.Service.OrganizationManage;
using YS.Admin.Cache.Factory;

namespace YS.Admin.Business.BlogManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-16 22:09
    /// 描 述：第三方登录用户业务类
    /// </summary>
    public class QqUserBLL
    {
        private QqUserService userService = new QqUserService();

        #region 获取数据
        public async Task<TData<List<QqUserEntity>>> GetList(QqUserListParam param)
        {
            TData<List<QqUserEntity>> obj = new TData<List<QqUserEntity>>();
            obj.Data = await userService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<QqUserEntity>>> GetPageList(QqUserListParam param, Pagination pagination)
        {
            TData<List<QqUserEntity>> obj = new TData<List<QqUserEntity>>();
            obj.Data = await userService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<QqUserEntity>> GetEntity(long id)
        {
            TData<QqUserEntity> obj = new TData<QqUserEntity>();
            obj.Data = await userService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion
        public async Task<TData<QqUserEntity>> GetQQUserByOpenId(string id)
        {
            TData<QqUserEntity> obj = new TData<QqUserEntity>();
            obj.Data = await userService.GetQQUserByOpenId(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        public async Task<TData<int>> GetTodayCommentNum(string id)
        {
            TData<int> obj = new TData<int>();
            obj.Data = await userService.GetTodayCommentNum(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }

        /// <summary>
        /// 密码MD5处理
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        private string EncryptUserPassword(string password, string salt)
        {
            string md5Password = SecurityHelper.MD5ToHex(password);
            string encryptPassword = SecurityHelper.MD5ToHex(md5Password.ToLower() + salt).ToLower();
            return encryptPassword;
        }
        public async Task<TData<QqUserEntity>> CheckLogin(string userName, string password, int platform)
        {
            TData<QqUserEntity> obj = new TData<QqUserEntity>();
            if (userName.IsEmpty() || password.IsEmpty())
            {
                obj.Message = "用户名或密码不能为空";
                return obj;
            }
            QqUserEntity user = await userService.CheckLogin(userName);
            if (user != null)
            {
                if (user.Status == 0)
                {
                    if (user.Password == EncryptUserPassword(password, "79669"))
                    {
                        //user.LoginCount++;
                        //user.IsOnline = 1;

                        //#region 设置日期
                        //if (user.FirstVisit == GlobalConstant.DefaultTime)
                        //{
                        //    user.FirstVisit = DateTime.Now;
                        //}
                        //if (user.PreviousVisit == GlobalConstant.DefaultTime)
                        //{
                        //    user.PreviousVisit = DateTime.Now;
                        //}
                        //if (user.LastVisit != GlobalConstant.DefaultTime)
                        //{
                        //    user.PreviousVisit = user.LastVisit;
                        //}
                        //user.LastVisit = DateTime.Now;
                        //#endregion

                        //switch (platform)
                        //{
                        //    case (int)PlatformEnum.Web:
                        //        if (GlobalContext.SystemConfig.LoginMultiple)
                        //        {
                        //            #region 多次登录用同一个token
                        //            if (string.IsNullOrEmpty(user.WebToken))
                        //            {
                        //                user.WebToken = SecurityHelper.GetGuid(true);
                        //            }
                        //            #endregion
                        //        }
                        //        else
                        //        {
                        //            user.WebToken = SecurityHelper.GetGuid(true);
                        //        }
                        //        break;

                        //    case (int)PlatformEnum.WebApi:
                        //        user.ApiToken = SecurityHelper.GetGuid(true);
                        //        break;
                        //}
                        obj.Data = user;
                        obj.Message = "登录成功";
                        obj.Tag = 1;
                    }
                    else
                    {
                        obj.Message = "密码不正确，请重新输入";
                    }
                }
                else
                {
                    obj.Message = "账号被禁用，请联系管理员";
                }
            }
            else
            {
                obj.Message = "账号不存在，请重新输入";
            }
            return obj;
        }



        public async Task<TData<QqUserEntity>> CheckLogin(string Crypto)
        {
            TData<QqUserEntity> obj = new TData<QqUserEntity>();
            if (Crypto.IsEmpty())
            {
                obj.Message = "浏览器指纹错误";
                return obj;
            }
            QqUserEntity user = await userService.cryptoCheckLogin(Crypto);
            if (user != null)
            {
                if (user.Status == 0)
                {
                    if (user.Password == EncryptUserPassword("123456", "79669"))
                    {
                        //user.LoginCount++;
                        //user.IsOnline = 1;

                        //#region 设置日期
                        //if (user.FirstVisit == GlobalConstant.DefaultTime)
                        //{
                        //    user.FirstVisit = DateTime.Now;
                        //}
                        //if (user.PreviousVisit == GlobalConstant.DefaultTime)
                        //{
                        //    user.PreviousVisit = DateTime.Now;
                        //}
                        //if (user.LastVisit != GlobalConstant.DefaultTime)
                        //{
                        //    user.PreviousVisit = user.LastVisit;
                        //}
                        //user.LastVisit = DateTime.Now;
                        //#endregion

                        //switch (platform)
                        //{
                        //    case (int)PlatformEnum.Web:
                        //        if (GlobalContext.SystemConfig.LoginMultiple)
                        //        {
                        //            #region 多次登录用同一个token
                        //            if (string.IsNullOrEmpty(user.WebToken))
                        //            {
                        //                user.WebToken = SecurityHelper.GetGuid(true);
                        //            }
                        //            #endregion
                        //        }
                        //        else
                        //        {
                        //            user.WebToken = SecurityHelper.GetGuid(true);
                        //        }
                        //        break;

                        //    case (int)PlatformEnum.WebApi:
                        //        user.ApiToken = SecurityHelper.GetGuid(true);
                        //        break;
                        //}
                        obj.Data = user;
                        obj.Message = "登录成功";
                        obj.Tag = 1;
                    }
                    else
                    {
                        obj.Message = "密码不正确，请重新输入";
                    }
                }
                else
                {
                    obj.Message = "账号被禁用，请联系管理员";
                }
            }
            else
            {
                //直接注册
                QqUserEntity entity = new QqUserEntity();
                entity.Crypto = Crypto; 
                entity.NickName = RandomData.Instance.ChineseName();
                entity.HeadShot = entity.NickName;
                entity.Password = EncryptUserPassword("123456", "79669");
                await userService.SaveForm(entity);

                obj.Data = entity;
                obj.Message = "登录成功";
                obj.Tag = 1;
            }
            return obj;
        }
        #region 提交数据
        public async Task<TData<string>> SaveForm(QqUserEntity entity, bool verfiy)
        {
            TData<string> obj = new TData<string>();
            if (userService.ExistUserName(entity))
            {
                obj.Message = "用户名已经存在！";
                return obj;
            }
            //if (!string.IsNullOrEmpty(entity.Mobile))
            //{
            //    if (userService.ExistUserMobile(entity))
            //    {
            //        obj.Message = "手机号已经存在！";
            //        return obj;
            //    }
            //}
            if (entity.Id.IsNullOrZero())
            {
                entity.Password = EncryptUserPassword(entity.Password, "79669");
            }
            //if (!entity.Birthday.IsEmpty())
            //{
            //    entity.Birthday = entity.Birthday.ParseToDateTime().ToString("yyyy-MM-dd");
            //}

            await userService.SaveForm(entity);

            await RemoveCacheById(entity.Id.Value);

            obj.Message = "注册成功";
            obj.Tag = 1;
            return obj;
        }

        private async Task RemoveCacheById(long id)
        {
            CacheFactory.Cache.RemoveCache(id.ToString());
        }
        public async Task<TData<string>> SaveForm(QqUserEntity entity)
        {
            TData<string> obj = new TData<string>();
            await userService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> SaveForms(List<QqUserEntity> entities)
        {
            TData<string> obj = new TData<string>();
            await userService.SaveForms(entities);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await userService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<string>> UpdateForms(QqUserEntity entity, Expression<Func<QqUserEntity, QqUserEntity>> updateExpression)
        {
            TData<string> obj = new TData<string>();
            long[] IdsList = TextHelper.SplitToArray<long>(entity.ids, ',');
            if (IdsList.Length.Equals(0))
            {
                obj.Tag = 0;
                obj.Message = "请选择操作数据";
                return obj;
            }

            await userService.Update(t => IdsList.Contains(t.Id.Value), updateExpression);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}

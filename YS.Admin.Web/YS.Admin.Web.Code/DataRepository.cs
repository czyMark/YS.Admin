using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YS.Admin.Data.Repository;
using YS.Admin.Enum.OrganizationManage;
using YS.Admin.Util;
using YS.Admin.Util.Extension;

namespace YS.Admin.Web.Code
{
    public class DataRepository : RepositoryFactory
    {
        public async Task<OperatorInfo> GetUserByToken(string token)
        {
            if (!SecurityHelper.IsSafeSqlParam(token))
            {
                return null;
            }
            token = token.ToString().Trim();

            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  a.Id as UserId,
                                    a.UserStatus,
                                    a.IsOnline,
                                    a.UserName,
                                    a.RealName,
                                    a.Portrait,
                                    a.DepartmentId,
                                    a.WebToken,
                                    a.ApiToken,
                                    a.IsSystem,
                                    a.LockStatus
                            FROM    Base_User a
                            WHERE   WebToken = '" + token + "' or ApiToken = '" + token + "'  ");
            var operatorInfo = await BaseRepository().FindObject<OperatorInfo>(strSql.ToString());
            if (operatorInfo != null)
            {
                #region 角色
                strSql.Clear();
                strSql.Append(@"SELECT  a.BelongId as RoleId
                                FROM    sys_userbelong a
                                WHERE   a.UserId = " + operatorInfo.UserId + " AND ");
                strSql.Append("         a.BelongType = " + UserBelongTypeEnum.Role.ParseToInt());
                IEnumerable<RoleInfo> roleList = await BaseRepository().FindList<RoleInfo>(strSql.ToString());
                if (roleList != null)
                {
                    operatorInfo.RoleIds = string.Join(",", roleList.Select(p => p.RoleId).ToArray());
                }
                #endregion

                #region 部门名称
                strSql.Clear();
                strSql.Append(@"SELECT  a.DepartmentName
                                FROM    base_department a
                                WHERE   a.Id = " + operatorInfo.DepartmentId);
                object departmentName = await BaseRepository().FindObject(strSql.ToString());
                if (departmentName != null)
                {
                    operatorInfo.DepartmentName = departmentName.ToString();
                }
                #endregion
            }
            return operatorInfo;
        }
        public async Task<UserInfo> GetUserByUToken(string token)
        {
            if (!SecurityHelper.IsSafeSqlParam(token))
            {
                return null;
            }
            token = token.ToString().Trim();

            var strSql = new StringBuilder();
            strSql.Append(@"SELECT
                            	Id AS UserId,
                            	ApiToken AS UToken,
                            	Mobile AS Mobile,
                            	0 AS Mid,
                            	'赛尼尔法务智库' AS CompanyName,
                            	'系统管理人员' AS UserPost ,
                            	1 as RoleType,
                            	case 
                            	when RealName!='' THEN RealName
                            	ELSE UserName
                            	END
                            	UserName
                            	
                            FROM
                            	Base_User 
                            WHERE
                            	ApiToken = '" + token + "'  ");
            var operatorInfo = await BaseRepository().FindObject<UserInfo>(strSql.ToString());
            if (operatorInfo != null)
            {
                return operatorInfo;
            }
            strSql.Clear();

            strSql.Append(@"SELECT
						srm.Id AS UserId,
						srm.ApiToken AS UToken,
						Mobile AS Mobile,
						IFNULL(socr.Mid,0) AS Mid,
						socr.CompanyName AS CompanyName,
						srm.UserPost AS UserPost,
						2 AS RoleType,
						srm.RealName AS UserName 
					FROM
						sys_reg_member srm
						LEFT JOIN sys_open_class_register socr ON socr.UserTel = srm.Mobile 
					WHERE
						srm.ApiToken = '" + token + "'  ");
            operatorInfo = await BaseRepository().FindObject<UserInfo>(strSql.ToString());
            if (operatorInfo != null)
            {
                return operatorInfo;
            }
            return null;
        }


        public async Task<WeChatOperatorInfo> GetWeChatUserByToken(string token)
        {
            if (!SecurityHelper.IsSafeSqlParam(token))
            {
                return null;
            }
            token = token.ToString().Trim();

            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  a.Openid,
                                    a.NickName,
                                    a.Sex,
                                    a.Province,
                                    a.City,
                                    a.Country,
                                    a.HeadImgurl,
                                    a.Privilege,
                                    a.Unionid
                            FROM    sys_wechat_userinfo a
                            WHERE   Openid = '" + token + "'  ");
            var operatorInfo = await BaseRepository().FindObject<WeChatOperatorInfo>(strSql.ToString());

            return operatorInfo;
        }
    }
}

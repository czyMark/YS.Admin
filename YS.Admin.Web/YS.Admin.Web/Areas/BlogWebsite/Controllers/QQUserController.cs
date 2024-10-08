﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;
using YS.Admin.Web.Controllers;


namespace YS.Admin.Web.Areas.BlogWebsite.Controllers
{
    [Area("BlogWebsite")]
    public class QQUserController : BaseController
    {
        public IQQUserService service { get; set; }
        // GET: QQUser
        public ActionResult QQLogin()
        {
            string state = DateTime.Now.ToString("yyyyMMddHHmmssffff");  //client端的状态值。用于第三方应用防止CSRF攻击，成功授权后回调时会原样带回。
            string url = QQLoginHelper.CreateAuthorizeUrl(state);
            RouteValueDictionary routeValue = RouteData.Route.GetRouteData(this.HttpContext).Values;
            Session["QQLoginState"] = state;    //记录client端状态值
            Session["BeforeLoginUrl"] = Request.UrlReferrer;    //记录登陆之前的URL，登陆成功后返回
            return Redirect(url);
            //Response.Cookies["openid"].Value = "5DB22905E4DA3582E221CE85737059C1";
            //Response.Cookies["openid"].Expires = DateTime.Now.AddDays(3);
            //Response.Cookies["nickname"].Value = "蚯蚓";
            //Response.Cookies["nickname"].Expires = DateTime.Now.AddDays(3);
            //Response.Cookies["figureurl_qq"].Value = "http://thirdqq.qlogo.cn/g?b=oidb&k=q8BmhyWUTZEpo7Us6QTqhA&s=140&t=1557712926";
            //Response.Cookies["figureurl_qq"].Expires = DateTime.Now.AddDays(3);
            //return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult CallBack()
        {
            string backState = Request.QueryString["state"];
            string code = Request.QueryString["code"];
            if (null != backState && null != code && null != Session["QQLoginState"] && backState == Session["QQLoginState"].ToString())
            {
                Session["QQLoginState"] = null;
                //调获取AccessToken的接口
                string access_token = QQLoginHelper.GetAccessToken(code);
                //调获取OpenID的接口
                string openid = QQLoginHelper.GetOpenId(access_token);
                //调获取用户信息的接口
                QQUserInfo qqUserInfo = QQLoginHelper.GetQQUserInfo(access_token, openid);
                if (qqUserInfo.ret == 0 && string.IsNullOrEmpty(qqUserInfo.msg))
                {
                    QQUserModel userModel = service.GetByWhere("WHERE OpenId='" + openid + "'").FirstOrDefault();
                    if (userModel != null)
                    {
                        //更新QQ用户信息
                        userModel.NickName = qqUserInfo.nickname;
                        userModel.Gender = qqUserInfo.gender == "男" ? 1 : 0;
                        userModel.HeadShot = qqUserInfo.figureurl_qq;
                        userModel.LastLogin = DateTime.Now;
                        service.UpdateModel(userModel);
                    }
                    else
                    {
                        userModel = new QQUserModel();
                        //添加QQ用户
                        userModel.OpenId = openid;
                        userModel.NickName = qqUserInfo.nickname;
                        userModel.Gender = qqUserInfo.gender=="男"?1:0;
                        userModel.HeadShot = qqUserInfo.figureurl_qq;
                        userModel.Email = "";
                        userModel.Status = true;
                        userModel.LastLogin = DateTime.Now;
                        userModel.CreateOn = DateTime.Now;
                        service.CreateModel(userModel);
                    }
                    Response.Cookies["openid"].Value = openid;
                    Response.Cookies["openid"].Expires = DateTime.Now.AddDays(3);
                    Response.Cookies["nickname"].Value = qqUserInfo.nickname;
                    Response.Cookies["nickname"].Expires = DateTime.Now.AddDays(3);
                    Response.Cookies["figureurl_qq"].Value = qqUserInfo.figureurl_qq;
                    Response.Cookies["figureurl_qq"].Expires = DateTime.Now.AddDays(3);
                }
                else
                {
                    //获取用户失败
                    throw new ApplicationException(qqUserInfo.msg);
                }
                if (Session["BeforeLoginUrl"] != null)
                {
                    return Redirect(Session["BeforeLoginUrl"].ToString()); //返回登陆之前的URL
                }
                else
                {
                    throw new Exception("登录之前的路由丢失");
                }
            }
            else
            {
                throw new Exception("登录State已丢失或不正确");
            }
        }
        public ActionResult QQLogOut()
        {
            Response.Cookies["openid"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["nickname"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["figureurl_qq"].Expires = DateTime.Now.AddDays(-1);
            //return Redirect(Request.UrlReferrer.ToString());
        }
    }
}
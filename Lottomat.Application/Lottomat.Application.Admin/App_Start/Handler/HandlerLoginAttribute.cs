using System.Web.Mvc;
using Lottomat.Application.Code;
using Lottomat.Util;
using System.Web;
using Lottomat.Utils;
using Lottomat.Utils.Web;

namespace Lottomat.Application.Admin
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2015.11.9 10:45
    /// 描 述：登录认证（会话验证组件）
    /// </summary>
    public class HandlerLoginAttribute : AuthorizeAttribute
    {
        private readonly LoginMode _customMode;
        /// <summary>默认构造</summary>
        /// <param name="mode">认证模式</param>
        public HandlerLoginAttribute(LoginMode mode)
        {
            _customMode = mode;
        }
        /// <summary>
        /// 响应前执行登录验证,查看当前用户是否有效 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //登录拦截是否忽略
            if (_customMode == LoginMode.Ignore)
            {
                return;
            }
            //登录是否过期
            if (OperatorProvider.Provider.IsOverdue())
            {
                WebHelper.WriteCookie("exsoft_login_error", "Overdue");//登录已超时,请重新登录
                filterContext.Result = new RedirectResult("~/Login/Default");
                return;
            }
            //是否已登录
            var OnLine = OperatorProvider.Provider.IsOnLine();
            if (OnLine == 0)
            {
                WebHelper.WriteCookie("exsoft_login_error", "OnLine");//您的帐号已在其它地方登录,请重新登录
                filterContext.Result = new RedirectResult("~/Login/Default");
                return;
            }
            else if (OnLine == -1)
            {
                WebHelper.WriteCookie("exsoft_login_error", "-1");//缓存已超时,请重新登录
                //filterContext.Result = new RedirectResult("~/Login/Default");
                return;
            }
        }
    }
}
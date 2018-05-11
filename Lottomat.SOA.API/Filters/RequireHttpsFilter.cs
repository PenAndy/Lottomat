using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.CommonEntity;
using Lottomat.Util.Extension;

namespace Lottomat.SOA.API.Filters
{
    /// <summary>
    /// Https过滤器，判断请求是否来自HTTPS
    /// </summary>
    public class RequireHttpsFilter : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                BaseJson<string> resultMsg = new BaseJson<string>
                {
                    Status = (int)JsonObjectStatus.HttpRequestError,
                    Message = "请求不合法，请求地址：" + actionContext.Request.RequestUri + "必须是Https请求。",
                    Data = ""
                };

                actionContext.Response = resultMsg.ToJson().ToHttpResponseMessage();
                base.OnAuthorization(actionContext);
            }
            else
            {
                base.OnAuthorization(actionContext);
            }
        }
    }
}
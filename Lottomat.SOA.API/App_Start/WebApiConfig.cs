using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Batch;
using System.Web.Http.Cors;
using System.Web.Http.Dispatcher;
using System.Web.Routing;
using Lottomat.SOA.API.Caching;
using Lottomat.SOA.API.Filters;
using Lottomat.SOA.API.Handlers;
using Lottomat.SOA.API.Selector;
using Lottomat.Util;
using Lottomat.Util.Extension;

namespace Lottomat.SOA.API
{
    public static class WebApiConfig
    {
        /// <summary>
        ///  Web API 配置和服务
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultVersionApi",
                routeTemplate: "api/{version}/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { HttpMethod = new HttpMethodConstraint("GET", "POST", "OPTIONS") }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { HttpMethod = new HttpMethodConstraint("GET", "POST", "OPTIONS") }
            );

            //注册自定义API过滤器
            config.Filters.Add(new ApiSecurityFilter());

            //添加版本控制
            config.Services.Replace(typeof(IHttpControllerSelector), new WebApiControllerSelector(config));

            //注册请求频率限制
            RegisterRequestLimitHandlers(config);

            //启用跨域
            //EnableCorsAttribute cors = new EnableCorsAttribute(ConfigHelper.GetValue("Origins"), "*", "GET,POST,OPTIONS,DELETE,PUT");
            //config.EnableCors(cors);
        }

        /// <summary>
        /// 注册请求频率限制
        /// </summary>
        /// <param name="config"></param>
        private static void RegisterRequestLimitHandlers(HttpConfiguration config)
        {
            //请求频率限制，默认一分钟60次
            int times = ConfigHelper.GetValue("RequestFrequencyLimit").TryToInt32();
            times = times == 0 ? 60 : times;

            config.MessageHandlers.Add(new RequestFrequencyLimitHandlers(
                new InMemoryThrottleStore(),
                no => times,
                TimeSpan.FromMinutes(1)));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Lottomat.SOA.API.Handlers;

namespace Lottomat.SOA.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            GlobalConfiguration.Configuration.MessageHandlers.Add(new CrosHandler());
        }

        protected void Application_BeginRequest()
        {
            //if (Request.Headers.AllKeys.Contains("Origin") && Request.HttpMethod == "OPTIONS")
            //{
            //    Response.End();
            //}
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Lottomat.Application.Entity.CommonEntity;
using Lottomat.Application.SystemAutoJob;

namespace Lottomat.Application.Admin
{
    /// <summary>
    /// 应用程序全局设置
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        //线程
        private static System.Threading.Thread _schedulerThread = null;

        /// <summary>
        /// 启动应用程序
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleTable.EnableOptimizations = true;
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            //=============启动定时检测任务=============
            //List<TaskList> taskLists = new List<TaskList>
            //{
            //    new TaskList
            //    {
            //        Jobs = new ArrayList
            //        {
            //            new AutoAddNewestLotteryManager(),
            //        },
            //        SleepInterval = 1000
            //    },
            //    new TaskList
            //    {
            //        Jobs = new ArrayList
            //        {
            //            new AutoRemindingTheForthcomingLotteryManager(),
            //        },
            //        SleepInterval = 1500
            //    }
            //};

            //SchedulerConfiguration config = new SchedulerConfiguration(taskLists);

            //Scheduler scheduler = new Scheduler(config);
            //System.Threading.ThreadStart myThreadStart = new System.Threading.ThreadStart(scheduler.Start);
            //if (_schedulerThread == null)
            //{
            //    _schedulerThread = new System.Threading.Thread(myThreadStart);
            //}
            //_schedulerThread.IsBackground = true;
            //_schedulerThread.Start();
            //=============定时检测任务=============
        }

        /// <summary>
        /// 应用程序错误处理
        /// </summary>
        protected void Application_Error(object sender, EventArgs e)
        {
            var lastError = Server.GetLastError();
        }
    }
}
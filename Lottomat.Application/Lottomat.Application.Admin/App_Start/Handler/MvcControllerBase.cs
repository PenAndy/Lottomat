using System;
using System.Reflection;
using Lottomat.Application.Code;
using Lottomat.Util;
using Lottomat.Util.Log;
using Lottomat.Util.WebControl;
using System.Web.Mvc;
using Lottomat.Util.Extension;
using WebGrease;

namespace Lottomat.Application.Admin
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2015.11.9 10:45
    /// 描 述：控制器基类
    /// </summary>
    [HandlerLogin(LoginMode.Enforce)]
    public abstract class MvcControllerBase : Controller, ILogger
    {
        #region 系统日志
        /// <summary>
        /// 系统日志 主动调用
        /// </summary>
        private readonly LogHelper _logHelper = new LogHelper(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 系统日志 对try块进行了封装
        /// </summary>
        /// <param name="type"></param>
        /// <param name="function">方法名称</param>
        /// <param name="errorHandel">异常处理方式</param>
        /// <param name="tryHandel">调试代码</param>
        /// <param name="catchHandel">异常处理方式</param>
        /// <param name="finallHandel">最终处理方式</param>
        public void Logger(Type type, string argJsonString, string function, Action tryHandel, Action<Exception> catchHandel = null,
            Action finallHandel = null, ErrorHandel errorHandel = ErrorHandel.Throw)
        {
            LogHelper.Logger(type, argJsonString, function, errorHandel, tryHandel, catchHandel, finallHandel);
        }
        #endregion

        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected virtual ActionResult ToJsonResult(object data)
        {
            return Content(data.ToJson());
        }
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        protected virtual ActionResult Success(string message)
        {
            return Content(new AjaxResult<string> { type = ResultType.Success, message = message }.ToJson());
        }
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected virtual ActionResult Success<T>(string message, T data)
        {
            return Content(new AjaxResult<T> { type = ResultType.Success, message = message, resultdata = data }.ToJson());
        }
        /// <summary>
        /// 返回失败消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        protected virtual ActionResult Error(string message)
        {
            return Content(new AjaxResult<string> { type = ResultType.Error, message = message }.ToJson());
        }
    }
}

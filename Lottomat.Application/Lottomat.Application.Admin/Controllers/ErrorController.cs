﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lottomat.Application.Admin.Controllers
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2015.09.01 13:32
    /// 描 述：错误处理
    /// </summary>
    public class ErrorController : Controller
    {
        #region 视图功能
        /// <summary>
        /// 错误页面（异常页面）
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public ActionResult ErrorMessage(string message)
        {
            Dictionary<string, string> modulesError = (Dictionary<string, string>)HttpContext.Application["error"];
            ViewData["Message"] = modulesError;
            return View();
        }
        /// <summary>
        /// 错误页面（错误路径404）
        /// </summary>
        /// <returns></returns>
        public ActionResult ErrorPath404()
        {
            return View();
        }
        /// <summary>
        /// 错误页面（升级浏览器软件）
        /// </summary>
        /// <returns></returns>
        public ActionResult ErrorBrowser()
        {
            return View();
        }
        #endregion

        #region 提交数据
        #endregion
    }
}

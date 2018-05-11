using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lottomat.Application.Code;
using Lottomat.Util;

namespace Lottomat.Application.Admin.Areas.SystemManage.Controllers
{
    public class SystemConfigController : MvcControllerBase
    {
        //
        // GET: /SystemManage/SystemConfig/

        #region 视图功能

        /// <summary>
        /// 系统配置管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 抓取彩票开奖号地址配置管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult LotteryIndex()
        {
            return View();
        }

        /// <summary>
        /// 接口缓存管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult ApiCacheIndex()
        {
            return View();
        }

        #endregion

        #region
        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [AjaxOnly(true)]
        [HttpPost]
        public ActionResult SaveConfig(string key, string value)
        {
            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
            {
                ConfigHelper.SetValue(key, value);
            }
            else
            {
                return Error("值不能为空");
            }
            return Success("保存成功");
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [AjaxOnly(true)]
        [HttpPost]
        public ActionResult GetConfigByKey(string key)
        {
            string res = String.Empty;
            if (!string.IsNullOrEmpty(key))
            {
                res = ConfigHelper.GetValue("__" + key + "__URL__") + "^"+ ConfigHelper.GetValue("__" + key + "__XPATH__");
            }
            else
            {
                return Error("Key值不能为空");
            }
            return Success("提取成功", res);
        }

        #endregion
    }
}

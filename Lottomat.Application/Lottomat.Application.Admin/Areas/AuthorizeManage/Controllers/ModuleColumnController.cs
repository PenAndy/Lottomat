﻿using Lottomat.Application.Busines.AuthorizeManage;
using Lottomat.Application.Entity.AuthorizeManage;
using Lottomat.Util;
using Lottomat.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Lottomat.Util.Extension;

namespace Lottomat.Application.Admin.Areas.AuthorizeManage.Controllers
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2015.10.29 15:13
    /// 描 述：系统视图
    /// </summary>
    public class ModuleColumnController : MvcControllerBase
    {
        private ModuleColumnBLL moduleColumnBLL = new ModuleColumnBLL();

        #region 视图功能
        /// <summary>
        /// 视图表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BatchAdd()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 视图列表 
        /// </summary>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string moduleId)
        {
            var data = moduleColumnBLL.GetList(moduleId);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 视图列表 
        /// </summary>
        /// <returns>返回树形列表Json</returns>
        [HttpGet]
        public ActionResult GetTreeListJson(string moduleId)
        {
            var data = moduleColumnBLL.GetList(moduleId);
            if (data != null)
            {
                return Content(data.ToJson());
            }
            return null;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 视图列表Json转换视图树形Json 
        /// </summary>
        /// <param name="moduleColumnJson">视图列表</param>
        /// <returns>返回树形列表Json</returns>
        [HttpPost]
        public ActionResult ListToListTreeJson(string moduleColumnJson)
        {
            var data = from items in moduleColumnJson.ToList<ModuleColumnEntity>() orderby items.SortCode select items;
            return Content(data.ToJson());
        }
        #endregion
    }
}

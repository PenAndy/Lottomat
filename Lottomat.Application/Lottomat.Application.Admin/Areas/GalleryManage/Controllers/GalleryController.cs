using Lottomat.Application.Entity.GalleryManage;
using Lottomat.Application.Busines.GalleryManage;
using Lottomat.Util;
using Lottomat.Util.WebControl;
using System.Web.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Lottomat.Application.Admin.Areas.GalleryManage.Controllers
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创 建：开发者账号
    /// 日 期：2017-10-25 09:38
    /// 描 述：Tk_Gallery
    /// </summary>
    public class GalleryController : MvcControllerBase
    {
        private Tk_GalleryBLL gallerybll = new Tk_GalleryBLL();

        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = gallerybll.GetPageList(pagination, queryJson);
            var jsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = gallerybll.GetList(queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = gallerybll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            gallerybll.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, Tk_Gallery entity)
        {
           // entity.ID = Guid.NewGuid().ToString().Replace("-", "");
            gallerybll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        #endregion


        /// <summary>
        /// 查询图库列表
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        public ActionResult QueryGalleryComList(string CategoryId)
        {
           IEnumerator<Tk_Gallery>  enumerator= gallerybll.GetList(t => t.IsDelete == false).GetEnumerator();
            List<Dictionary<string,string>> list = new List<Dictionary<string,string>>();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            while (enumerator.MoveNext())
            {
                dict[enumerator.Current.GalleryName] = enumerator.Current.ID;
            }
            return Json(new JsonResult { Data=dict});
        }
    }
}

using Lottomat.Application.Entity.BaseManage;
using Lottomat.Application.Busines.BaseManage;
using Lottomat.Util;
using Lottomat.Util.WebControl;
using System.Web.Mvc;
using Lottomat.Application.Busines.GalleryManage;
using Lottomat.Application.Entity.GalleryManage;

namespace Lottomat.Application.Admin.Areas.BaseManage.Controllers
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创 建：开发者账号
    /// 日 期：2017-10-19 14:24
    /// 描 述：图库标题表
    /// </summary>
    public class Tk_GalleryController : MvcControllerBase
    {
        private Tk_GalleryBLL tk_gallerybll = new Tk_GalleryBLL();

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
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = tk_gallerybll.GetList(queryJson);
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
            var data = tk_gallerybll.GetEntity(keyValue);
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
            tk_gallerybll.RemoveForm(keyValue);
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
            tk_gallerybll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        #endregion
    }
}

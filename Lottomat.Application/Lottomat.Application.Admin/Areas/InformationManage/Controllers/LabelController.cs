using System.Collections.Generic;
using System.Linq;
using Lottomat.Application.Entity.InformationManage;
using Lottomat.Application.Busines.InformationManage;
using Lottomat.Util;
using Lottomat.Util.WebControl;
using System.Web.Mvc;
using Lottomat.Application.Entity.SystemManage.ViewModel;

namespace Lottomat.Application.Admin.Areas.InformationManage.Controllers
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-10-27 10:34
    /// 描 述：Zx_Label
    /// </summary>
    public class LabelController : MvcControllerBase
    {
        private LabelBLL zx_labelbll = new LabelBLL();

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
            var data = zx_labelbll.GetPageList(pagination, queryJson);
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
            var data = zx_labelbll.GetList(queryJson);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 根据分类ID获取标签列表
        /// </summary>
        /// <param name="cId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetListByCId(string cId)
        {
            List<DataItemModel> res = new List<DataItemModel>();
            if (!string.IsNullOrEmpty(cId))
            {
                var data = zx_labelbll.GetList(l => l.CategoryId.Equals(cId)).ToList();
                res = data.Select(d => new DataItemModel
                {
                    ItemDetailId = d.ID,
                    ItemName = d.LabelName,
                    SortCode = d.SortCode,
                    ItemValue = d.LabelName
                }).ToList();
            }
            return ToJsonResult(res);
        }

        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = zx_labelbll.GetEntity(keyValue);
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
            zx_labelbll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, LabelEntity entity)
        {
            zx_labelbll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        #endregion
    }
}

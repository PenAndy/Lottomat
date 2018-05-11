using System.Collections.Generic;
using System.Linq;
using Lottomat.Application.Busines.SystemManage;
using Lottomat.Application.Code;
using Lottomat.Util;
using Lottomat.Util.WebControl;
using System.Web.Mvc;
using Lottomat.Application.Entity.SystemManage;
using Lottomat.Util.Extension;

namespace Lottomat.Application.Admin.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2015.11.18 9:56
    /// 描 述：系统日志
    /// </summary>
    public class LogController : MvcControllerBase
    {
        #region 视图功能
        /// <summary>
        /// 日志管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 清空日志
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveLog()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 日志列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = new List<LogEntity>();
            string thisUser = OperatorProvider.Provider.Current().Account;
            if (thisUser != "System")
            {
                data = LogBLL.GetPageList(pagination, queryJson).Where(l => l.OperateAccount.Equals(thisUser)).ToList();
            }
            else
            {
                data = LogBLL.GetPageList(pagination, queryJson).ToList();
            }

            var JsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return Content(JsonData.ToJson());
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 清空日志
        /// </summary>
        /// <param name="categoryId">日志分类Id</param>
        /// <param name="keepTime">保留时间段内</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveLog(int categoryId, string keepTime)
        {
            LogBLL.RemoveLog(categoryId, keepTime);
            return Success("清空成功。");
        }
        #endregion
    }
}

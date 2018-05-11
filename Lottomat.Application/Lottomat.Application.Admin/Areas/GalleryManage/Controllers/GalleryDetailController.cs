using Lottomat.Application.Entity.GalleryManage;
using Lottomat.Application.Busines.GalleryManage;
using Lottomat.Util;
using Lottomat.Util.WebControl;
using System.Web.Mvc;
using System;
using System.Collections.Generic;

namespace Lottomat.Application.Admin.Areas.GalleryManage.Controllers
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创 建：开发者账号
    /// 日 期：2017-10-25 10:56
    /// 描 述：GalleryDetail
    /// </summary>
    public class GalleryDetailController : MvcControllerBase
    {
        private Tk_GalleryDetailBLL gallerydetailbll = new Tk_GalleryDetailBLL();
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
            
            var data = gallerydetailbll.GetPageList(pagination, queryJson);
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
            var data = gallerydetailbll.GetList(queryJson);
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
           
            var data = gallerydetailbll.GetEntity(keyValue);
            Tk_Gallery tktmp = gallerybll.GetEntity(data.GalleryId);
            string tmp = tktmp.GalleryName;
            return ToJsonResult(new {data=data, GalleryName=tmp });
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
            gallerydetailbll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, Tk_GalleryDetail entity)
        {
            gallerydetailbll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        /// <summary>
        /// 根据GalleryId 获取期数 (默认两期根据查询数据库排除)
        /// </summary>
        /// <param name="GalleryId"></param>
        /// <returns></returns>
        public ActionResult QueryPeriodsNumber(string GalleryId)
        {
            string periodsNumber = getTimeSpan(DateTime.Now.ToString(System.Globalization.CultureInfo.InvariantCulture));
            string[] pnArray = periodsNumber.Split('|');
            List<string> list=new List<string>( Array.ConvertAll(pnArray, new Converter<string, string>(s=>s)));
            IEnumerator<Tk_GalleryDetail> enumerator = gallerydetailbll.GetList(w => list.Contains(w.PeriodsNumber) && w.GalleryId == GalleryId).GetEnumerator();
            while (enumerator.MoveNext())
            {
                list.Remove(enumerator.Current.PeriodsNumber);
            }
            return Json(new JsonResult { Data = list });


        }
        protected string getTimeSpan(string timeStr)
        {
            //判断当前时间是否在工作时间段内
            string _strWorkingDayPM = "21:00";
            //string _strWorkingDayPM = "17:30";
            TimeSpan dspWorkingDayPM = DateTime.Parse(_strWorkingDayPM).TimeOfDay;
            //TimeSpan dspWorkingDayPM = DateTime.Parse(_strWorkingDayPM).TimeOfDay;

            //string time1 = "2017-2-17 8:10:00";
            DateTime t1 = Convert.ToDateTime(timeStr);

            TimeSpan dspNow = t1.TimeOfDay;
            //返回期数形式 192|193
            string periodsNumber = "";
            if (dspNow < dspWorkingDayPM)
            {
                periodsNumber = (int.Parse(DateTime.Now.DayOfYear.ToString()) - 8 + 1).ToString() + "|";
                periodsNumber += (int.Parse(DateTime.Now.DayOfYear.ToString()) - 8 + 1 + 1);
                return periodsNumber;
            }
            else
            {
                periodsNumber = (int.Parse(DateTime.Now.DayOfYear.ToString()) - 8 + 1 + 1).ToString() + "|";
                periodsNumber += (int.Parse(DateTime.Now.DayOfYear.ToString()) - 8 + 1 + 1 + 1).ToString();
                return periodsNumber;
            }
        }
        #endregion
    }
}

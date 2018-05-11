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
    /// �� �� 1.0
    /// Copyright (c) 2016-2017
    /// �� �����������˺�
    /// �� �ڣ�2017-10-25 10:56
    /// �� ����GalleryDetail
    /// </summary>
    public class GalleryDetailController : MvcControllerBase
    {
        private Tk_GalleryDetailBLL gallerydetailbll = new Tk_GalleryDetailBLL();
        private Tk_GalleryBLL gallerybll = new Tk_GalleryBLL();
        #region ��ͼ����
        /// <summary>
        /// �б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        #endregion

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ����</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�Json</returns>
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
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = gallerydetailbll.GetList(queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// ��ȡʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
           
            var data = gallerydetailbll.GetEntity(keyValue);
            Tk_Gallery tktmp = gallerybll.GetEntity(data.GalleryId);
            string tmp = tktmp.GalleryName;
            return ToJsonResult(new {data=data, GalleryName=tmp });
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            gallerydetailbll.RemoveForm(keyValue);
            return Success("ɾ���ɹ���");
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, Tk_GalleryDetail entity)
        {
            gallerydetailbll.SaveForm(keyValue, entity);
            return Success("�����ɹ���");
        }
        /// <summary>
        /// ����GalleryId ��ȡ���� (Ĭ�����ڸ��ݲ�ѯ���ݿ��ų�)
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
            //�жϵ�ǰʱ���Ƿ��ڹ���ʱ�����
            string _strWorkingDayPM = "21:00";
            //string _strWorkingDayPM = "17:30";
            TimeSpan dspWorkingDayPM = DateTime.Parse(_strWorkingDayPM).TimeOfDay;
            //TimeSpan dspWorkingDayPM = DateTime.Parse(_strWorkingDayPM).TimeOfDay;

            //string time1 = "2017-2-17 8:10:00";
            DateTime t1 = Convert.ToDateTime(timeStr);

            TimeSpan dspNow = t1.TimeOfDay;
            //����������ʽ 192|193
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

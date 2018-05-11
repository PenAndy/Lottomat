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
    /// �� �� 1.0
    /// Copyright (c) 2016-2017
    /// �� �����������˺�
    /// �� �ڣ�2017-10-25 09:38
    /// �� ����Tk_Gallery
    /// </summary>
    public class GalleryController : MvcControllerBase
    {
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
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = gallerybll.GetList(queryJson);
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
            var data = gallerybll.GetEntity(keyValue);
            return ToJsonResult(data);
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
            gallerybll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, Tk_Gallery entity)
        {
           // entity.ID = Guid.NewGuid().ToString().Replace("-", "");
            gallerybll.SaveForm(keyValue, entity);
            return Success("�����ɹ���");
        }
        #endregion


        /// <summary>
        /// ��ѯͼ���б�
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

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
    /// �� �� 1.0
    /// Copyright (c) 2016-2017
    /// �� �����������˺�
    /// �� �ڣ�2017-10-19 14:24
    /// �� ����ͼ������
    /// </summary>
    public class Tk_GalleryController : MvcControllerBase
    {
        private Tk_GalleryBLL tk_gallerybll = new Tk_GalleryBLL();

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
        /// ����ҳ��
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
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = tk_gallerybll.GetList(queryJson);
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
            var data = tk_gallerybll.GetEntity(keyValue);
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
            tk_gallerybll.RemoveForm(keyValue);
            return Success("ɾ���ɹ���");
        }
        /// <summary>
        /// ����������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, Tk_Gallery entity)
        {
            tk_gallerybll.SaveForm(keyValue, entity);
            return Success("�����ɹ���");
        }
        #endregion
    }
}
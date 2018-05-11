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
    /// �� �� 1.0
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-10-27 10:34
    /// �� ����Zx_Label
    /// </summary>
    public class LabelController : MvcControllerBase
    {
        private LabelBLL zx_labelbll = new LabelBLL();

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
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = zx_labelbll.GetList(queryJson);
            return ToJsonResult(data);
        }

        /// <summary>
        /// ���ݷ���ID��ȡ��ǩ�б�
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
        /// ��ȡʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = zx_labelbll.GetEntity(keyValue);
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
            zx_labelbll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, LabelEntity entity)
        {
            zx_labelbll.SaveForm(keyValue, entity);
            return Success("�����ɹ���");
        }
        #endregion
    }
}

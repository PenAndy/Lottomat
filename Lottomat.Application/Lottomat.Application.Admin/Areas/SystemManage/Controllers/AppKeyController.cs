using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Lottomat.Application.Entity.SystemManage;
using Lottomat.Application.Busines.SystemManage;
using Lottomat.Util;
using Lottomat.Util.WebControl;
using System.Web.Mvc;
using Lottomat.Utils.Security;

namespace Lottomat.Application.Admin.Areas.SystemManage.Controllers
{
    /// <summary>
    /// �� �� 1.0
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-10-25 16:17
    /// �� ����ϵͳ�ӿ���Կ����
    /// </summary>
    public class AppKeyController : MvcControllerBase
    {
        private AppKeyBLL appkeybll = new AppKeyBLL();

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
            var data = appkeybll.GetPageList(pagination, queryJson);
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
            var data = appkeybll.GetList(queryJson);
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
            var data = appkeybll.GetEntity(keyValue);
            return ToJsonResult(data);
        }

        /// <summary>
        /// ��ȡAppKey��У����Կ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAppKey()
        {
            string[] res = GetSignToken();

            var obj = new
            {
                AppKey = res[0],
                AppSecret = res[1]
            };

            return ToJsonResult(obj);
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
            appkeybll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, AppKeyEntity entity)
        {
            appkeybll.SaveForm(keyValue, entity);
            return Success("�����ɹ���");
        }
        #endregion

        #region ˽�з���
        /// <summary>
        /// ����AppKey
        /// </summary>
        /// <returns></returns>
        private string[] GetSignToken()
        {
            //ǩ����Ϣ
            string tokenStr = CommonHelper.GetGuid();
            //��Կ
            string tokenKey = CommonHelper.GetGuid();

            //���ܴ���
            string first = ToBase64Hmac(tokenStr, tokenKey);
            //AppKey
            string last = DESEncrypt.Encrypt(Md5Helper.MD5(first, 32)).ToUpper();

            //����У����Կ
            string check = CommonHelper.GetGuid();
            //��������
            string o = (last + check).ToUpper();
            string temp = string.Concat(o.OrderByDescending(c => c));
            //�õ���Կ
            string sec = DESEncrypt.Encrypt(Md5Helper.MD5(temp, 16)).ToUpper();

            return new[] { last, sec };
        }

        /// <summary>
        /// HMACSHA1�㷨���ܲ�����ToBase64String
        /// </summary>
        /// <param name="strText">ǩ�������ַ���</param>
        /// <param name="strKey">��Կ����</param>
        /// <returns>����һ��ǩ��ֵ(����ϣֵ)</returns>
        private static string ToBase64Hmac(string strText, string strKey)
        {
            HMACSHA1 hmacsha1 = new HMACSHA1(Encoding.UTF8.GetBytes(strKey), true);
            byte[] byteText = hmacsha1.ComputeHash(Encoding.UTF8.GetBytes(strText));
            //ES+TPCa+UT+Sb8PORoIT36M63fs=
            string res = System.Convert.ToBase64String(byteText, Base64FormattingOptions.None).ToUpper();
            return res;
        }

        #endregion
    }
}

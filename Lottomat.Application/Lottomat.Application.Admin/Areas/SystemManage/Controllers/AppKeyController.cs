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
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-10-25 16:17
    /// 描 述：系统接口密钥管理
    /// </summary>
    public class AppKeyController : MvcControllerBase
    {
        private AppKeyBLL appkeybll = new AppKeyBLL();

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
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = appkeybll.GetList(queryJson);
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
            var data = appkeybll.GetEntity(keyValue);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取AppKey和校验密钥
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
            appkeybll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, AppKeyEntity entity)
        {
            appkeybll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 生成AppKey
        /// </summary>
        /// <returns></returns>
        private string[] GetSignToken()
        {
            //签名信息
            string tokenStr = CommonHelper.GetGuid();
            //密钥
            string tokenKey = CommonHelper.GetGuid();

            //加密处理
            string first = ToBase64Hmac(tokenStr, tokenKey);
            //AppKey
            string last = DESEncrypt.Encrypt(Md5Helper.MD5(first, 32)).ToUpper();

            //生成校验密钥
            string check = CommonHelper.GetGuid();
            //降序排序
            string o = (last + check).ToUpper();
            string temp = string.Concat(o.OrderByDescending(c => c));
            //得到密钥
            string sec = DESEncrypt.Encrypt(Md5Helper.MD5(temp, 16)).ToUpper();

            return new[] { last, sec };
        }

        /// <summary>
        /// HMACSHA1算法加密并返回ToBase64String
        /// </summary>
        /// <param name="strText">签名参数字符串</param>
        /// <param name="strKey">密钥参数</param>
        /// <returns>返回一个签名值(即哈希值)</returns>
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

using System;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using Lottomat.Application.Busines.SystemManage;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.CommonEntity;
using Lottomat.Application.Entity.SystemManage;
using Lottomat.Application.Entity.ViewModel;
using Lottomat.Cache.Factory;
using Lottomat.SOA.API.Controllers.Base;
using Lottomat.Util;
using Lottomat.Util.Extension;
using Lottomat.Util.Log;
using Lottomat.Utils;
using Lottomat.Utils.Date;
using Lottomat.Utils.Security;
using System.Web.Http.Cors;

namespace Lottomat.SOA.API.Controllers.V1
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OAuthController : BaseApiController
    {
       
        private readonly AppKeyBLL _appKeyBll = new AppKeyBLL();

        #region 根据AppKey获取Token
        /// <summary>
        /// 根据AppKey获取Token
        /// </summary>
        /// <param name="appkey">AppKey</param>
        /// <param name="appsecret">appsecret</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage GetToken(string appkey, string appsecret)
        {
            BaseJson<Token_Preview> resultMsg = new BaseJson<Token_Preview> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };

            Logger(typeof(OAuthController), "", "根据AppKey获取Token-GetToken", () =>
             {
                //判断参数是否合法
                if (string.IsNullOrEmpty(appkey) && string.IsNullOrEmpty(appsecret))
                 {
                     resultMsg = new BaseJson<Token_Preview>
                     {
                         Status = (int)JsonObjectStatus.ParameterError,
                         Message = JsonObjectStatus.ParameterError.GetEnumText(),
                         Data = null
                     };
                 }
                 else
                 {
                     string exp = GlobalStaticConstant.REGRXP_APP_KEY;
                     bool validate = StringHelper.QuickValidate(exp, appkey);
                     if (!validate)
                     {
                         resultMsg = new BaseJson<Token_Preview>
                         {
                             Status = (int)JsonObjectStatus.ParameterError,
                             Data = null,
                             Message = JsonObjectStatus.ParameterError.GetEnumText(),
                             BackUrl = ""
                         };
                     }
                     else
                     {
                        //TODO 核对是否存在appkey以及校验appsecret是否正确
                        AppKeyEntity appKeyEntity = _appKeyBll.GetEntity(a => a.AppKey.Equals(appkey));
                         if (appKeyEntity != null)
                         {
                            //比对密钥
                            if (appKeyEntity.AppSecret.Equals(appsecret))
                             {
                                //获取缓存Token信息
                                Token_Preview token = CacheFactory.Cache().GetCache<Token_Preview>(appkey);
                                 if (token == null)
                                 {
                                    //过期时间
                                    DateTime time = DateTimeHelper.Now.AddHours(GlobalStaticConstant.TOKEN_EXPIRE_TIME);

                                     string accessToken = GetSignToken(appkey);

                                     token = new Token_Preview
                                     {
                                         AppKey = appkey,
                                         AccessToken = accessToken,
                                         ExpireTime = time.ToString("yyyy-MM-dd HH:mm:ss")
                                     };
                                    //插入缓存
                                    CacheFactory.Cache().WriteCache(token, token.AppKey, time);
                                 }

                                //返回token信息
                                resultMsg = new BaseJson<Token_Preview>
                                 {
                                     Status = (int)JsonObjectStatus.Success,
                                     Message = JsonObjectStatus.Success.GetEnumText(),
                                     Data = token
                                 };
                             }
                             else
                             {
                                 resultMsg = new BaseJson<Token_Preview>
                                 {
                                     Status = (int)JsonObjectStatus.Fail,
                                     Message = "AppSecret无效。",
                                     Data = null
                                 };
                             }
                         }
                         else
                         {
                             resultMsg = new BaseJson<Token_Preview>
                             {
                                 Status = (int)JsonObjectStatus.Fail,
                                 Message = "AppKey不存在。",
                                 Data = null
                             };
                         }
                     }
                 }
             }, e =>
             {
                 resultMsg = new BaseJson<Token_Preview>
                 {
                     Status = (int)JsonObjectStatus.Exception,
                     Message = JsonObjectStatus.Exception.GetEnumText() + "，异常信息：" + e.Message,
                     Data = null
                 };
             }, null, ErrorHandel.Continue);

            return resultMsg.ToJson().ToHttpResponseMessage();
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 生成Token
        /// </summary>
        /// <returns></returns>
        private string GetSignToken(string appkey)
        {
            //签名信息
            string tokenStr = CommonHelper.GetGuid();
            //密钥
            string tokenKey = CommonHelper.GetGuid().ToLower().Replace("-", "");
            
            string first = ToBase64Hmac(tokenStr, tokenKey);

            string last = DESEncrypt.Encrypt(Md5Helper.MD5(first, 32), appkey);

            return last;
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

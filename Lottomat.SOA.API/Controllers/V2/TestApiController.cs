using System.Net.Http;
using System.Web.Http;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.CommonEntity;
using Lottomat.Application.Entity.SystemManage;
using Lottomat.SOA.API.Controllers.Base;
using Lottomat.Util.Extension;

namespace Lottomat.SOA.API.Controllers.V2
{
    public class TestApiController : BaseApiController
    {
        /// <summary>
        /// 测试是否连接成功
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage HelloWorld(string s)
        {
            BaseJson<string> resultMsg = new BaseJson<string> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };

            Logger(typeof(TestApiController), "", "测试是否连接成功-HelloWorld", () =>
             {
                 if (!string.IsNullOrEmpty(s))
                 {
                    //TODO Dosomething
                }
                 else
                 {
                     resultMsg = new BaseJson<string>
                     {
                         Status = (int)JsonObjectStatus.Fail,
                         Data = null,
                         Message = JsonObjectStatus.Fail.GetEnumText() + "，请求参数有误。",
                         BackUrl = null
                     };
                 }
             }, e =>
             {
                 resultMsg = new BaseJson<string>
                 {
                     Status = (int)JsonObjectStatus.Exception,
                     Data = null,
                     Message = JsonObjectStatus.Exception.GetEnumText() + "，异常信息：" + e.Message,
                     BackUrl = null
                 };
             });

            return resultMsg.TryToJson().ToHttpResponseMessage();
        }
    }
}

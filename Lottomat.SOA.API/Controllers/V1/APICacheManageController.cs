using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.CommonEntity;
using Lottomat.Application.Entity.LotteryNumberManage.Parameter;
using Lottomat.Cache.Factory;
using Lottomat.SOA.API.Controllers.Base;
using Lottomat.Util.Extension;

namespace Lottomat.SOA.API.Controllers.V1
{
    /// <summary>
    /// API缓存管理
    /// </summary>
    public class APICacheManageController : BaseApiController
    {
        /// <summary>
        /// API缓存管理
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage RemoveCacheByKey(RemoveCacheArgEntity arg)
        {
            BaseJson<string> resultMsg = new BaseJson<string> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };

            Logger(typeof(APICacheManageController), arg.TryToJson(), "API缓存管理-RemoveCacheByKey", () =>
            {
                if (!string.IsNullOrEmpty(arg.t))
                {
                    if (arg.t.CheckTimeStamp())
                    {
                        if (!string.IsNullOrEmpty(arg.CacheKey))
                        {
                            string[] cacheKeys = arg.CacheKey.Split("|".ToCharArray());
                            foreach (string key in cacheKeys)
                            {
                                CacheFactory.Cache().RemoveCache(key);
                            }

                            resultMsg = new BaseJson<string>
                            {
                                Status = (int)JsonObjectStatus.Success,
                                Data = null,
                                Message = JsonObjectStatus.Success.GetEnumText(),
                                BackUrl = null
                            };
                        }
                    }
                    else
                    {
                        resultMsg = new BaseJson<string>
                        {
                            Status = (int)JsonObjectStatus.Fail,
                            Data = null,
                            Message = JsonObjectStatus.Fail.GetEnumText() + "，无效参数。",
                            BackUrl = null
                        };
                    }
                }
                else
                {
                    resultMsg = new BaseJson<string>
                    {
                        Status = (int)JsonObjectStatus.Fail,
                        Data = null,
                        Message = JsonObjectStatus.Fail.GetEnumText() + "，请求参数为空。",
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

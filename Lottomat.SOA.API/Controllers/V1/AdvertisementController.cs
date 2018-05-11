using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Lottomat.Application.Busines.InformationManage;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.CommonEntity;
using Lottomat.Application.Entity.InformationManage;
using Lottomat.Application.Entity.LotteryNumberManage.Parameter;
using Lottomat.Application.Entity.LotteryNumberManage.ViewModel;
using Lottomat.Application.Entity.ViewModel;
using Lottomat.Cache.Factory;
using Lottomat.SOA.API.Controllers.Base;
using Lottomat.Util.Extension;
using Lottomat.Utils.Date;
using System.Web.Http.Cors;

namespace Lottomat.SOA.API.Controllers.V1
{
    /// <summary>
    /// 广告接口
    /// </summary>
    public class AdvertisementController : BaseApiController
    {
        /// <summary>
        /// 广告BLL
        /// </summary>
        private static AdvertisementBLL advertisementBll = new AdvertisementBLL();

        //        for (int i = 1; i <= 32; i++)
        //{
        //    AdvertisementEntity entity = new AdvertisementEntity
        //    {
        //        Category = "开奖网",
        //        CategoryId = "1",
        //        Position = i
        //    };

        //    advertisementBll.SaveForm("", entity);
        //}

        //for (int i = 1; i <= 6; i++)
        //{
        //AdvertisementEntity entity = new AdvertisementEntity
        //{
        //    Category = "手机站",
        //    CategoryId = "2",
        //    Position = i
        //};

        //advertisementBll.SaveForm("", entity);
        //}

        /// <summary>
        /// 保存广告
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage SaveAdvertisement(SaveAdvertisementArgEntity arg)
        {
            BaseJson<string> resultMsg = new BaseJson<string> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };

            Logger(typeof(AdvertisementController), arg.TryToJson(), "保存广告-SaveAdvertisement", () =>
            {
                if (!string.IsNullOrEmpty(arg.t) && !string.IsNullOrEmpty(arg.Appkey) && !string.IsNullOrEmpty(arg.AccessToken))
                {
                    if (arg.t.CheckTimeStamp())
                    {
                        //获取缓存Token信息
                        Token_Preview token = CacheFactory.Cache().GetCache<Token_Preview>(arg.Appkey);
                        if (token != null)
                        {
                            //校验授权码
                            string tokenStr = token.AccessToken;
                            if (!string.IsNullOrEmpty(tokenStr) && tokenStr.Equals(arg.AccessToken))
                            {
                                AdvertisementEntity entity = new AdvertisementEntity
                                {
                                    Title = arg.Title,
                                    Category = arg.Which == "0" ? "主站" : arg.Which == "1" ? "开奖网" : arg.Which == "2" ? "手机站" : "",
                                    CategoryId = arg.Which,
                                    Position = arg.Position,
                                    Href = arg.Href,
                                    TermOfValidity = arg.OverTime
                                };

                                if (!string.IsNullOrEmpty(arg.Id))
                                {
                                    entity.IsEnable = true;
                                    advertisementBll.SaveForm(arg.Id, entity);
                                }
                                else
                                {
                                    AdvertisementEntity temp = advertisementBll.GetEntity(a => a.CategoryId.Equals(arg.Which) && a.Position == arg.Position);
                                    if (temp != null)
                                    {
                                        entity.IsEnable = true;
                                        advertisementBll.SaveForm(temp.ID, entity);
                                    }
                                }

                                //清理缓存
                                Cache.Factory.CacheFactory.Cache().RemoveCache("Advertisement_Html_" + arg.Which);

                                resultMsg = new BaseJson<string>
                                {
                                    Status = (int)JsonObjectStatus.Success,
                                    Data = null,
                                    Message = JsonObjectStatus.Success.GetEnumText(),
                                    BackUrl = null
                                };
                            }
                            else
                            {
                                resultMsg = new BaseJson<string>
                                {
                                    Status = (int)JsonObjectStatus.TokenInvalid,
                                    Data = null,
                                    Message = JsonObjectStatus.TokenInvalid.GetEnumText(),
                                    BackUrl = null
                                };
                            }
                        }
                        else
                        {
                            resultMsg = new BaseJson<string>
                            {
                                Status = (int)JsonObjectStatus.TokenInvalid,
                                Data = null,
                                Message = JsonObjectStatus.TokenInvalid.GetEnumText(),
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

        /// <summary>
        /// 获取广告列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetAdvertisementList(GetAdvertisementArgEntity arg)
        {
            BaseJson<List<AdvertisementViewEntity>> resultMsg = new BaseJson<List<AdvertisementViewEntity>> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };

            Logger(typeof(AdvertisementController), arg.TryToJson(), "获取广告列表-GetAdvertisementList", () =>
            {
                if (!string.IsNullOrEmpty(arg.t))
                {
                    if (arg.t.CheckTimeStamp())
                    {
                        List<AdvertisementEntity> list = advertisementBll.GetList(a => a.CategoryId.Equals(arg.Which) && a.Title != "" && a.TermOfValidity > DateTimeHelper.Now).ToList();

                        List<AdvertisementViewEntity> res = list.Select(n => new AdvertisementViewEntity
                        {
                            Id = n.ID,
                            Title = n.Title ?? "",
                            Href = n.Href != null ? n.Href.ToLower().Contains("http://") || n.Href.ToLower().Contains("https://") ? n.Href : "http://" + n.Href : "",
                            Position = n.Position,
                            Which = n.CategoryId,
                            OverTime = n.TermOfValidity.TryToDateTimeToString("yyyy-MM-dd"),
                            IsEnable = n.IsEnable ?? false,
                            IsExpire = n.TermOfValidity != null && n.TermOfValidity < DateTimeHelper.Now
                        }).ToList();

                        res = res.OrderBy(r => r.Position).ToList();

                        resultMsg = new BaseJson<List<AdvertisementViewEntity>>
                        {
                            Status = (int)JsonObjectStatus.Success,
                            Data = res,
                            Message = JsonObjectStatus.Success.GetEnumText(),
                            BackUrl = null
                        };
                    }
                    else
                    {
                        resultMsg = new BaseJson<List<AdvertisementViewEntity>>
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
                    resultMsg = new BaseJson<List<AdvertisementViewEntity>>
                    {
                        Status = (int)JsonObjectStatus.Fail,
                        Data = null,
                        Message = JsonObjectStatus.Fail.GetEnumText() + "，请求参数为空。",
                        BackUrl = null
                    };
                }
            }, e =>
            {
                resultMsg = new BaseJson<List<AdvertisementViewEntity>>
                {
                    Status = (int)JsonObjectStatus.Exception,
                    Data = null,
                    Message = JsonObjectStatus.Exception.GetEnumText() + "，异常信息：" + e.Message,
                    BackUrl = null
                };
            });

            return resultMsg.TryToJson().ToHttpResponseMessage();
        }

        /// <summary>
        /// 获取广告Html
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetAdvertisementHtml(GetAdvertisementArgEntity arg)
        {
            BaseJson<string> resultMsg = new BaseJson<string> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };

            Logger(typeof(AdvertisementController), arg.TryToJson(), "获取广告Html-GetAdvertisementHtml", () =>
            {
                if (!string.IsNullOrEmpty(arg.t))
                {
                    if (arg.t.CheckTimeStamp())
                    {
                        string html = Cache.Factory.CacheFactory.Cache().GetCache<string>("Advertisement_Html_" + arg.Which);
                        if (string.IsNullOrEmpty(html))
                        {
                            List<AdvertisementEntity> list = advertisementBll.GetList(a => a.CategoryId.Equals(arg.Which)).ToList();

                            //List<AdvertisementViewEntity> res = list.Select(n => new AdvertisementViewEntity
                            //{
                            //    Id = n.ID,
                            //    Title = n.Title,
                            //    Href = n.Href.ToLower().Contains("http://") || n.Href.ToLower().Contains("https://") ? n.Href : "http://" + n.Href,
                            //    Position = n.Position,
                            //    Which = n.CategoryId,
                            //    OverTime = n.TermOfValidity.TryToDateTimeToString("yyyy-MM-dd"),
                            //    IsEnable = n.IsEnable ?? false
                            //}).ToList();

                            //按位置排序
                            list = list.OrderBy(r => r.Position).ToList();

                            html = GetHtml(list, arg.Which);

                            Cache.Factory.CacheFactory.Cache().WriteCache(html, "Advertisement_Html_" + arg.Which, DateTimeHelper.Now.AddDays(10));
                        }
                        resultMsg = new BaseJson<string>
                        {
                            Status = (int)JsonObjectStatus.Success,
                            Data = html,
                            Message = JsonObjectStatus.Success.GetEnumText(),
                            BackUrl = null
                        };
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

        private string GetHtml(List<AdvertisementEntity> res, string which)
        {
            StringBuilder builderRes = new StringBuilder();
            StringBuilder builder = new StringBuilder();
            for (int i = 1; i <= res.Count; i++)
            {
                if (res[i - 1].IsEnable ?? false)
                {
                    //检查是否过期
                    bool isExpire = res[i - 1].TermOfValidity != null && res[i - 1].TermOfValidity < DateTimeHelper.Now;

                    builder.Append(isExpire
                        ? NoSoldTemplate
                        : string.Format(BeSoldTemplate, res[i - 1].Href,
                            string.Format("{0}({1})", res[i - 1].Title, res[i - 1].Href), res[i - 1].Title));
                }
                else
                {
                    builder.Append(NoSoldTemplate);
                }
                int j = which.TryToInt32() == 0 || which.TryToInt32() == 1 ? 8 : which.TryToInt32() == 2 ? 3 : 8;
                if (i % j == 0)
                {
                    builderRes.Append("<tr>");
                    builderRes.Append(builder.ToString());
                    builderRes.Append("</tr>");

                    //清空
                    builder.Clear();
                }
            }
            return which.TryToInt32() == 0 || which.TryToInt32() == 1 ? string.Format(TableTemplate_5, builderRes) : which.TryToInt32() == 2 ? string.Format(TableTemplate_3, builderRes) : "";
        }

        private string TableTemplate_5 = @"<table style='width: 100%;'><tr><td rowspan = '5' style='line-height: 25px;text-align: center; width: 60px;border:1px solid #ddd;'>站<br> 外<br>推<br> 广</td></tr>{0}</table>";
        private string TableTemplate_3 = @"<table style='width: 100%;'><tr><td rowspan = '3' style='line-height: 25px;text-align: center; width: 60px;border:1px solid #ddd;'>站<br> 外<br>推<br> 广</td></tr>{0}</table>";

        /// <summary>
        /// 已经销售了的广告样式模板
        /// </summary>
        private string BeSoldTemplate = "<td style='padding:7px 0;border:1px solid #ddd;text-align: center;white-space:nowrap;overflow: hidden;text-overflow: ellipsis;'><a href='{0}' target='_blank' title='{1}'>{2}</a></td>";
        /// <summary>
        /// 招商
        /// </summary>
        private string NoSoldTemplate = "<td style='padding:7px 0;border:1px solid #ddd;text-align: center;white-space:nowrap;overflow: hidden;text-overflow: ellipsis;'><i class='layui-icon' style='font-size: 10px; color: #FF5722;'>&#xe64d;招商中...</i></td>";
    }
}

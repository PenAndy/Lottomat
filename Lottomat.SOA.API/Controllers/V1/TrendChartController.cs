using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lottomat.Application.Busines.InformationManage;
using Lottomat.Application.Busines.SystemManage;
using Lottomat.Application.Cache;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.PublicInfoManage;
using Lottomat.Application.Entity.SystemManage;
using Lottomat.Application.Entity.ViewModel.ConsultationMangerModel;
using Lottomat.Application.Entity.ViewModel.TrendChartModel;
using Lottomat.SOA.API.Controllers.Base;
using Lottomat.Util.Extension;
using Lottomat.Util.WebControl;
using Lottomat.Application.Entity.CommonEntity;
using Lottomat.Application.Entity.InformationManage;
using Lottomat.Application.Entity.SystemManage.ViewModel;

namespace Lottomat.SOA.API.Controllers.V1
{
    public class TrendChartController : BaseApiController
    {
        private static readonly TrendChartBLL TrendChartBll = new TrendChartBLL();
        //private static readonly string[] types = { "QGC", "DFC", "GPC11X5", "GPCK3", "GPCKL12", "GPCKLSF" , "GPCQTC" };
        private static readonly DataItemCache dataItemCache = new DataItemCache();

        #region  查询走势图链接
        /// <summary>
        /// 查询走势图链接
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetTrendChartNewsList(TrendChart arg)
        {
            BaseJson<List<TrendChart_Preview>> resultMsg = new BaseJson<List<TrendChart_Preview>> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };
            Logger(typeof(TrendChartController), arg.TryToJson(), "查询走势图链接-GetTrendChartNewsList", () =>
            {
                if (!string.IsNullOrEmpty(arg.Category))
                {
                    List<TrendChart_Preview> res = Cache.Factory.CacheFactory.Cache().GetCache<List<TrendChart_Preview>>("__" + arg.Category + "__");
                    if (res == null)
                    {
                        res = new List<TrendChart_Preview>();
                        //如果传入为 GPC 的话，就默认返回高频彩11选5相关数据
                        List<DataItemModel> data = dataItemCache.GetDataItemList(arg.Category.Equals("GPC") ? "GPC11X5" : arg.Category).OrderBy(n => n.SortCode).ToList();
                        string[] ids = data.Select(d => d.ItemDetailId).ToArray();
                        //获取该分类下符合要求的数据
                        List<TrendChartEntity> trendChartEntities = TrendChartBll.GetPageList(n => n.IsDelete == false && n.IsStick == true && ids.Contains(n.CategoryId)).OrderBy(n => n.SortCode).ToList();

                        if (trendChartEntities.Count > 0)
                        {
                            //组装父级相关属性
                            TrendChart_Preview previewItem = new TrendChart_Preview
                            {
                                TrendChartType = arg.Category,
                                ItemName = dataItemCache.GetDataItemEntityByCode(arg.Category).ItemName
                            };

                            //根据分类ID进行分组
                            IEnumerable<IGrouping<string, TrendChartEntity>> group = trendChartEntities.GroupBy(t => t.CategoryId);
                            List<TempTrendChartPreviewItem> temp = new List<TempTrendChartPreviewItem>();
                            foreach (IGrouping<string, TrendChartEntity> chartEntities in group)
                            {
                                //分类ID
                                string key = chartEntities.Key;
                                //获取分类信息
                                DataItemDetailEntity dataItemEntity = dataItemCache.GetDataItemEntityById(key);

                                List<TrendChartPreviewItem> trendChartPreviewItems = new List<TrendChartPreviewItem>();
                                //组装项目相关属性
                                TempTrendChartPreviewItem item = new TempTrendChartPreviewItem
                                {
                                    TrendChartChildType = dataItemEntity.ItemValue,
                                    TrendChartChildName = dataItemEntity.ItemName
                                };
                                //组装具体信息
                                foreach (TrendChartEntity entity in chartEntities)
                                {
                                    TrendChartPreviewItem p = new TrendChartPreviewItem
                                    {
                                        Title = entity.Title,
                                        TrendChartUrl = entity.TrendChartUrl
                                    };
                                    trendChartPreviewItems.Add(p);
                                }

                                item.TrendChartPreviewItem = trendChartPreviewItems;

                                temp.Add(item);
                            }
                            previewItem.TrendChartPreviewItems = temp;
                            res.Add(previewItem);
                        }

                        Cache.Factory.CacheFactory.Cache().WriteCache<List<TrendChart_Preview>>(res, "__" + arg.Category + "__");
                    }

                    resultMsg = new BaseJson<List<TrendChart_Preview>>
                    {
                        Status = (int)JsonObjectStatus.Success,
                        Data = res,
                        Message = JsonObjectStatus.Success.GetEnumText(),
                        BackUrl = null
                    };
                }
                else
                {
                    resultMsg = new BaseJson<List<TrendChart_Preview>>
                    {
                        Status = (int)JsonObjectStatus.Fail,
                        Data = null,
                        Message = JsonObjectStatus.Fail.GetEnumText() + "，请求参数Category为空。",
                        BackUrl = null
                    };
                }
            }, e =>
            {
                resultMsg = new BaseJson<List<TrendChart_Preview>>
                {
                    Status = (int)JsonObjectStatus.Exception,
                    Data = null,
                    Message = JsonObjectStatus.Exception.GetEnumText() + "，异常信息：" + e.Message,
                    BackUrl = null
                };
            });
            return resultMsg.TryToJson().ToHttpResponseMessage();
        }
        #endregion
    }
}

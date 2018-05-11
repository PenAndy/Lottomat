using Lottomat.SOA.API.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lottomat.Application.Entity.GalleryManage;
using Lottomat.Application.Entity.CommonEntity;
using Lottomat.Application.Entity.ViewModel.GallerMangerModel;
using Lottomat.Application.Code;
using Lottomat.Util.Extension;
using Lottomat.Application.Busines.GalleryManage;
using System.IO;
using Lottomat.Application.Entity.ViewModel.ConsultationMangerModel;
using Lottomat.Utils.Date;
using Lottomat.Application.Busines.InformationManage;
using Lottomat.Application.Cache;
using Lottomat.Application.Entity.SystemManage;
using Lottomat.Application.Entity.InformationManage;
using Lottomat.Application.Entity.SystemManage.ViewModel;

namespace Lottomat.SOA.API.Controllers.V1
{
    public class GallerMangerApiController : BaseApiController
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly Tk_GalleryDetailBLL tkDetailBll = new Tk_GalleryDetailBLL();
        /// <summary>
        /// 
        /// </summary>
        private static readonly Tk_GalleryBLL tkBll = new Tk_GalleryBLL();
        private static readonly DataItemCache dataItemCache = new DataItemCache();
        private DataItemCache itemCache = new DataItemCache();
        /// <summary>
        /// 查询图库全部页面
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage QuerAll(ArtListParams param)
        {
            BaseJson4Page<QueryAllDataModel> resultMsg = new BaseJson4Page<QueryAllDataModel> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };
            Logger(typeof(GallerMangerApiController), param.TryToJson(), "GallerMangerApi/QuerAll链接成功", () =>
            {
                int totalrows = 0, totalpage = 0;
                List<QueryAllDataModel> list = TKApiBLL.QueryAll(param.PageIndex == 0 ? 1 : param.PageIndex, param.PageSize == 0 ? 10 : param.PageSize, param.TryToJson(), out totalrows, out totalpage,param.PeriodsNumber);
                PageData<QueryAllDataModel> pageData = new PageData<QueryAllDataModel>
                {
                    TotalRow = totalrows,
                    TotalPage = totalpage,
                    PageIndex = param.PageIndex,
                    Rows = list
                };
                resultMsg = new BaseJson4Page<QueryAllDataModel>
                {
                    Status = (int)JsonObjectStatus.Success,
                    Data = pageData,
                    Message = JsonObjectStatus.Success.GetEnumText(),
                    BackUrl = null
                };
            }, e =>
            {
                resultMsg = new BaseJson4Page<QueryAllDataModel>
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
        /// 图库文章详细查询（公用）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage QueryArtInfo(ArtInfoParams artInfoParams)
        {
            BaseJson<ImageInfoModel> resultMsg = new BaseJson<ImageInfoModel> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };
            Logger(typeof(GallerMangerApiController), artInfoParams.TryToJson(), "GallerMangerApi/QueryArtInfo链接成功", () =>
            {

                if (string.IsNullOrEmpty(artInfoParams.GalleryId) && string.IsNullOrEmpty(artInfoParams.ID) && string.IsNullOrEmpty(artInfoParams.PeriodsNumber))
                {
                    resultMsg = new BaseJson<ImageInfoModel>
                    {
                        Status = (int)JsonObjectStatus.Fail,
                        Data = null,
                        Message = JsonObjectStatus.Fail.GetEnumText() + "GallerMangerApi/QueryArtInfo参数错误: ",
                        BackUrl = null
                    };
                }
                ImageInfoModel data = TKApiBLL.QueryArtInfo(artInfoParams);
                resultMsg = new BaseJson<ImageInfoModel>
                {
                    Status = (int)JsonObjectStatus.Success,
                    Data = data,
                    Message = JsonObjectStatus.Success.GetEnumText(),
                    BackUrl = null
                };
            }, e =>
            {
                resultMsg = new BaseJson<ImageInfoModel>
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
        /// 查询热门分类栏目列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage QueryArtHotList(ArtHotListParams artHotListParams)
        {
            BaseJson4Page<ImageMenuInfoModel> resultMsg = new BaseJson4Page<ImageMenuInfoModel> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };
            Logger(typeof(GallerMangerApiController), artHotListParams.TryToJson(), "GallerMangerApi/QueryArtHotList链接成功", () =>
            {
                int totalrows = 0, totalpage = 0;
                if (string.IsNullOrEmpty(artHotListParams.menuName))
                {
                    resultMsg = new BaseJson4Page<ImageMenuInfoModel>
                    {
                        Status = (int)JsonObjectStatus.Fail,
                        Data = null,
                        Message = JsonObjectStatus.Fail.GetEnumText() + "GallerMangerApi/QueryArtHotList参数错误: ",
                        BackUrl = null
                    };
                }
                List<ImageMenuInfoModel> list = TKApiBLL.QueryHotArtList(artHotListParams.PageIndex == 0 ? 1 : artHotListParams.PageIndex, artHotListParams.PageSize == 0 ? 10 : artHotListParams.PageSize, artHotListParams.menuName, out totalrows, out totalpage,artHotListParams.PeriodsNumber);
                PageData<ImageMenuInfoModel> pageData = new PageData<ImageMenuInfoModel>
                {
                    TotalRow = totalrows,
                    TotalPage = totalpage,
                    PageIndex = artHotListParams.PageIndex,
                    Rows = list
                };
                resultMsg = new BaseJson4Page<ImageMenuInfoModel>
                {
                    Status = (int)JsonObjectStatus.Success,
                    Data = pageData,
                    Message = JsonObjectStatus.Success.GetEnumText(),
                    BackUrl = null
                };
            }, e =>
            {
                resultMsg = new BaseJson4Page<ImageMenuInfoModel>
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
        /// 热门分类菜单查询
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage QueryArtHotMenu(ArtHotMenuParams artHotMenuParams)
        {
            BaseJson<List<HotMenuModel>> resultMsg = new BaseJson<List<HotMenuModel>> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };
            Logger(typeof(GallerMangerApiController), artHotMenuParams.TryToJson(), "GallerMangerApi/QueryArtHotMenu链接成功", () =>
            {
                string[] itemName = { "A", "B", "C" };
                if (!string.IsNullOrEmpty(artHotMenuParams.ItemName))
                {
                    itemName = new string[] { artHotMenuParams.ItemName };
                }
                List<HotMenuModel> list = TKApiBLL.QueryHotMenu(itemName);
                resultMsg = new BaseJson<List<HotMenuModel>>
                {
                    Status = (int)JsonObjectStatus.Success,
                    Data = list,
                    Message = JsonObjectStatus.Success.GetEnumText(),
                    BackUrl = null
                };
            }, e =>
            {
                resultMsg = new BaseJson<List<HotMenuModel>>
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
        /// 图库最新文章查询(分页)
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        public HttpResponseMessage QueryNewArtList(ArtNewParams artNewParams)
        {
            BaseJson4Page<ImageMenuInfoModel> resultMsg = new BaseJson4Page<ImageMenuInfoModel> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };
            Logger(typeof(GallerMangerApiController), artNewParams.TryToJson(), "GallerMangerApi/QueryNewArtList链接成功", () =>
            {
                int totalrows = 0, totalpage = 0;
                List<ImageMenuInfoModel> list = TKApiBLL.QueryNew(artNewParams.PageIndex == 0 ? 1 : artNewParams.PageIndex, artNewParams.PageSize == 0 ? 10 : artNewParams.PageSize, out totalrows, out totalpage,artNewParams.PeriodsNumber);
                PageData<ImageMenuInfoModel> pageData = new PageData<ImageMenuInfoModel>
                {
                    TotalRow = totalrows,
                    TotalPage = totalpage,
                    PageIndex = artNewParams.PageIndex,
                    Rows = list
                };
                resultMsg = new BaseJson4Page<ImageMenuInfoModel>
                {
                    Status = (int)JsonObjectStatus.Success,
                    Data = pageData,
                    Message = JsonObjectStatus.Success.GetEnumText(),
                    BackUrl = null
                };
            }, e =>
            {
                resultMsg = new BaseJson4Page<ImageMenuInfoModel>
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
        /// 图库期数列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetTkPeriodsNumberList()
        {
            BaseJson<List<GalleryNumberList>> resultMsg = new BaseJson<List<GalleryNumberList>> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };
            Logger(typeof(GallerMangerApiController),"", "GetTkPeriodsNumberList - 图库期数列表", () =>
            {
                #region 第一种方式，不可取
                //List<Tk_GalleryDetail> tkGalleries = tkDetailBll.GetList(n => n.IsDelete == false).OrderBy(n => n.PeriodsNumber).ToList();
                ////通过分组获取期数
                //IEnumerable<IGrouping<string, Tk_GalleryDetail>> group = tkGalleries.GroupBy(t => t.PeriodsNumber);

                ////开始遍历
                //foreach (IGrouping<string, Tk_GalleryDetail> grouping in group)
                //{
                //    string key = grouping.Key;

                //    GalleryNumberList preItem = new GalleryNumberList
                //    {
                //        PeriodsNumber = key,
                //        Title = "2017" + key
                //    };
                //    newsPreviewItem.Add(preItem);
                //}
                #endregion

                #region 第二种方式
                //List<String> tkGalleries = tkDetailBll.GetList(n => n.IsDelete == false).Select(n => n.PeriodsNumber).ToList();
                //TODO 
                //foreach (string s in tkGalleries)
                //{
                //    GalleryNumberList preItem = new GalleryNumberList
                //    {
                //        PeriodsNumber = s,
                //        Title = "2017" + s
                //    };
                //    newsPreviewItem.Add(preItem);
                //}
                #endregion

                #region 第三种
                //获取缓存
                List<GalleryNumberList> newsPreviewItem = Cache.Factory.CacheFactory.Cache().GetCache<List<GalleryNumberList>>("__TK_PERIODS__NUMBER__");
                if (newsPreviewItem == null)
                {
                    newsPreviewItem = new List<GalleryNumberList>();

                    //查询出所有的分类
                    List<Tk_Gallery> list = tkBll.GetList(t => t.IsDelete == false).ToList();
                    //取出所有的分类ID
                    List<string> ids = list.Select(n => n.ID).ToList();
                    //再从详细里面取出去期数
                    List<Tk_GalleryDetail> detailList = tkDetailBll.GetList(t => ids.Contains(t.GalleryId)).ToList();
                    //取出期数
                    List<string> preNumbers = detailList.Select(n => n.PeriodsNumber).OrderByDescending(n => n).ToList();
                    //年份
                    string year = DateTimeHelper.Now.Year.ToString();
                    //分组
                    IEnumerable<IGrouping<string, string>> group = preNumbers.GroupBy(p => p);
                    foreach (IGrouping<string, string> grouping in group)
                    {
                        string key = grouping.Key;

                        GalleryNumberList preItem = new GalleryNumberList
                        {
                            PeriodsNumber = key,
                            Title = year + key
                        };
                        newsPreviewItem.Add(preItem);
                    }

                    newsPreviewItem = newsPreviewItem.Take(50).ToList();

                    Cache.Factory.CacheFactory.Cache().WriteCache<List<GalleryNumberList>>(newsPreviewItem, "__TK_PERIODS__NUMBER__", DateTimeHelper.Now.AddHours(12));
                }
                #endregion

                resultMsg = new BaseJson<List<GalleryNumberList>>
                {
                    Status = (int)JsonObjectStatus.Success,
                    Data = newsPreviewItem,
                    Message = JsonObjectStatus.Success.GetEnumText(),
                    BackUrl = null
                };
            }, e =>
            {
                resultMsg = new BaseJson<List<GalleryNumberList>>
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
        /// 查询
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetTrendChartSRC(TrendChartParams param)
        {
            BaseJson<TrendChartModel> resultMsg = new BaseJson<TrendChartModel> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };
            Logger(typeof(GallerMangerApiController), param.TryToJson(), "GetTkPeriodsNumberList - 图库期数列表", () =>
            {
                if (string.IsNullOrEmpty(param.LotteryType))
                {
                    resultMsg = new BaseJson<TrendChartModel>
                    {
                        Status = (int)JsonObjectStatus.Exception,
                        Data = null,
                        Message = JsonObjectStatus.Exception.GetEnumText() + "，参数有误请确认：" ,
                        BackUrl = null
                    };
                }
                TrendChartBLL trendChartBLL = new TrendChartBLL();
                DataItemModel itemdata = itemCache.GetItemDetailByItemValue(param.LotteryType);//.GetEntityByCode(param.CategoryId);
                List<TrendChartEntity> list = trendChartBLL.GetPageList(s => s.CategoryId == itemdata.ItemDetailId&&s.Title.Contains("基本走势图")).ToList();
                TrendChartModel data = new TrendChartModel();
                if (list.Count==0)//继续采用走势图匹配
                {
                    list = trendChartBLL.GetPageList(s => s.CategoryId == itemdata.ItemDetailId && s.Title=="走势图").ToList();
                    
                }
                if (list.Count==0)//继续采用走势图带连线 匹配
                {
                    list = trendChartBLL.GetPageList(s => s.CategoryId == itemdata.ItemDetailId && s.Title == "走势图带连线").ToList();
                }
                if (list.Count==0)//还是就返回失败
                {
                    resultMsg = new BaseJson<TrendChartModel>
                    {
                        Status = (int)JsonObjectStatus.Fail,
                        Data = null,
                        Message = JsonObjectStatus.Exception.GetEnumText() + "，查询不到结果：",
                        BackUrl = null
                    };
                }
                else
                {
                     data = new TrendChartModel()
                    {
                        Name = list[0].Title,
                        Url = list[0].TrendChartUrl
                    };
                }
                
                resultMsg = new BaseJson<TrendChartModel>()
                {
                    Status = (int)JsonObjectStatus.Success,
                    Data = data,
                    Message = JsonObjectStatus.Success.GetEnumText(),
                    BackUrl = null
                };
            }, e =>
            {
                resultMsg = new BaseJson<TrendChartModel>
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

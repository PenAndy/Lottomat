using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Lottomat.Application.Busines.CommonManage;
using Lottomat.Application.Busines.PublicInfoManage;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.CommonEntity;
using Lottomat.Application.Entity.PublicInfoManage;
using Lottomat.SOA.API.Controllers.Base;
using Lottomat.Util.Extension;
using Lottomat.Application.Cache;
using Lottomat.Util.WebControl;
using Lottomat.Application.Entity.ViewModel.ConsultationMangerModel;
using Lottomat.Application.Entity.InformationManage;
using Lottomat.Application.Busines.SystemManage;
using Lottomat.Application.Entity.SystemManage;
using Lottomat.Application.Entity.SystemManage.ViewModel;
using Lottomat.Util;

namespace Lottomat.SOA.API.Controllers.V1
{
    public class ZxNewsController : BaseApiController
    {
        #region 实例
        /// <summary>
        /// 新闻bll
        /// </summary>
        public static readonly NewsBLL newsBll = new NewsBLL();
        /// <summary>
        /// 字典缓存bll
        /// </summary>
        private static readonly DataItemCache dataItemCache = new DataItemCache();

        private static readonly CommonBLL commonBll = new CommonBLL();
        /// <summary>
        /// 彩种编码
        /// </summary>

        //主分类名称
        private static readonly string[] types = { "FC3D", "SSQ", "PL3", "QT" };

        //主分类下面的预测名称
        private static readonly string[] itemValue = { "3DYC", "P3YC", "SSQYC", "DLT" };
        /// <summary>
        /// 锁
        /// </summary>
        private static readonly object _lock = new Object();
        /// <summary>
        /// 获取文章详情
        /// </summary>
        private static string GetNewsDetailSql = "SELECT [PK],[FullHead],[CreateDate],[CreateUserName],[NewsContent],[PeriodsNumber],[IsRecommend],[IsHot],[PV],[TitleElement],[DescriptionElement],[KeywordElement]  FROM [dbo].[Base_News] WHERE [PK] = {0}";
        /// <summary>
        /// 获取新闻摘要
        /// </summary>
        private static string GetNewsAbstractSql = "SELECT [PK],[FullHead] FROM [dbo].[Base_News] WHERE [PK] = {0}";
        #endregion

        #region 查询咨询首页的分类下面最新的5条数据

        /// <summary>
        /// 查询咨询首页的分类下面最新的5条数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetHomeNewsList()
        {
            BaseJson<List<News_Preview>> resultMsg = new BaseJson<List<News_Preview>> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };
            Logger(typeof(ZxNewsController), "", "查询咨询首页的分类下面最新的5条数据-GetHomeNewsList", () =>
             {
                 List<News_Preview> res = Cache.Factory.CacheFactory.Cache().GetCache<List<News_Preview>>("HttpResponseMessage_GetHomeNewsList");
                 if (res == null || res.Count == 0)
                 {
                     res = new List<News_Preview>();
                     var data = dataItemCache.GetDataItemList("FC3D|SSQ|PL3|QT").Where(n => itemValue.Contains(n.ItemValue));
                     foreach (string code in types)
                     {
                         #region 不用
                         //foreach (string value in itemValue)
                         //{
                         //    var data = dataItemCache.GetDataItemList(code).Where(n => n.ItemValue.Equals(value));
                         //    string[] ids = data.Select(d => d.ItemDetailId).ToArray();

                         //    List<NewsEntity> news = newsBll.FindList<NewsEntity>(n => n.IsDelete == false && n.TypeId == 1 && ids.Contains(n.CategoryId), "CreateDate", false, 5, 1, out int records).ToList();

                         //    //newsBll.GetPageList(n => n.IsDelete == false && n.TypeId == 1 && ids.Contains(n.CategoryId), page).ToList();
                         //    if (news.Count > 0)
                         //    {
                         //        News_Preview preview = new News_Preview
                         //        {
                         //            NewsType = code
                         //        };

                         //        DataItemEntity list = dataItemCache.GetDataItemEntityByCode(code);
                         //        preview.ItemName = list.ItemName;

                         //        List<NewsPreviewItem> newsPreviewItem = new List<NewsPreviewItem>();

                         //        foreach (NewsEntity n in news)
                         //        {
                         //            NewsPreviewItem preItem = new NewsPreviewItem
                         //            {
                         //                AddTime = n.CreateDate.TryToDateTimeToString("yyyy-MM-dd"),
                         //                NewsId = n.NewsId,
                         //                Title = n.FullHead
                         //            };
                         //            newsPreviewItem.Add(preItem);
                         //        }
                         //        preview.NewsPreviewItem = newsPreviewItem;
                         //        res.Add(preview);
                         //    }
                         //} 
                         #endregion

                         List<NewsEntity> news = GetHomeNewsList(code);
                         if (news.Count > 0)
                         {
                             News_Preview preview = new News_Preview
                             {
                                 NewsType = code
                             };

                             var tmpbaseitme = data.Single(w => w.EnCode == code);
                             preview.ItemName = tmpbaseitme.ItemName;

                             List<NewsPreviewItem> newsPreviewItem = new List<NewsPreviewItem>();

                             foreach (NewsEntity n in news)
                             {
                                 NewsPreviewItem preItem = new NewsPreviewItem
                                 {
                                     AddTime = n.CreateDate.TryToDateTimeToString("yyyy-MM-dd"),
                                     NewsId = n.PK.ToString(),
                                     Title = n.FullHead
                                 };
                                 newsPreviewItem.Add(preItem);
                             }
                             preview.NewsPreviewItem = newsPreviewItem;
                             res.Add(preview);
                         }
                     }

                     Cache.Factory.CacheFactory.Cache().WriteCache(res, "HttpResponseMessage_GetHomeNewsList");
                 }

                 resultMsg = new BaseJson<List<News_Preview>>
                 {
                     Status = (int)JsonObjectStatus.Success,
                     Data = res,
                     Message = JsonObjectStatus.Success.GetEnumText(),
                     BackUrl = null
                 };
             }, e =>
             {
                 resultMsg = new BaseJson<List<News_Preview>>
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
        /// 查询咨询首页的分类下面最新的5条数据 详情看sql语句
        /// </summary>
        /// <returns></returns>
        private static List<NewsEntity> GetHomeNewsList(string code)
        {
            string strSql = @"select top 5 * from Base_News where CategoryId in( SELECT   
                                    d.ItemDetailId  
                            FROM    Base_DataItemDetail d
                                    LEFT JOIN Base_DataItem i ON i.ItemId = d.ItemId
                            WHERE   1 = 1
                                    AND d.EnabledMark = 1
                                    AND d.DeleteMark = 0
                                    AND i.ItemCode ='{0}'
									AND d.ItemValue in('3DYC','P3YC','SSQYC','DLT')
                            )AND IsDelete = 0 AND TypeId = 1 order by CreateDate desc";

            string sql = string.Format(strSql, code);
            return commonBll.ExcuteSqlDataTable(sql, DatabaseLinksEnum.Base, null).DataTableToList<NewsEntity>();
        }

        #endregion

        #region  彩种下面的文章列表
        /// <summary>
        /// 彩种下面的文章列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetColorNewsList(ColorNews arg)
        {
            BaseJson4Page<News_Preview> resultMsg = new BaseJson4Page<News_Preview> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };
            Logger(typeof(ZxNewsController), arg.TryToJson(), "彩种下面的文章列表-GetColorNewsList", () =>
             {
                 if (!string.IsNullOrEmpty(arg.Category))
                 {
                     Pagination page = new Pagination
                     {
                         rows = arg.PageSize,
                         page = arg.PageIndex,
                         sidx = "CreateDate",
                         sord = "desc",
                         records = 0,
                         conditionJson = ""
                     };
                     //获取分类信息
                     List<DataItemModel> data = dataItemCache.GetDataItemList(arg.Category);
                     //分类Ids
                     string[] ids = data.Select(d => d.ItemDetailId).ToArray();

                     //获取数据
                     List<NewsEntity> news = newsBll.GetPageList(n => n.IsDelete == false && n.TypeId == 1 && ids.Contains(n.CategoryId), page).ToList();
                     if (news.Count > 0 || news.Any())
                     {
                         //分类名称
                         News_Preview preview = new News_Preview
                         {
                             NewsType = arg.Category
                         };

                         //项目名称
                         DataItemEntity entity = dataItemCache.GetDataItemEntityByCode(arg.Category);
                         preview.ItemName = entity.ItemName;

                         List<NewsPreviewItem> newsPreviewItem = news.Select(n => new NewsPreviewItem
                         {
                             AddTime = n.CreateDate.TryToDateTimeToString("yyyy-MM-dd"),
                             NewsId = n.NewsId,
                             Title = n.FullHead
                         }).ToList();

                         preview.NewsPreviewItem = newsPreviewItem;

                         PageData<News_Preview> pageData = new PageData<News_Preview>
                         {
                             TotalRow = page.records,
                             TotalPage = Math.Ceiling(page.records * 1.0 / arg.PageSize).TryToInt32(),
                             PageIndex = arg.PageIndex,
                             Rows = new List<News_Preview> { preview }
                         };

                         resultMsg = new BaseJson4Page<News_Preview>
                         {
                             Status = (int)JsonObjectStatus.Success,
                             Data = pageData,
                             Message = JsonObjectStatus.Success.GetEnumText(),
                             BackUrl = null
                         };
                     }
                     else
                     {
                         resultMsg = new BaseJson4Page<News_Preview>
                         {
                             Status = (int)JsonObjectStatus.Fail,
                             Data = null,
                             Message = JsonObjectStatus.Fail.GetEnumText(),
                             BackUrl = null
                         };
                     }
                 }
                 else
                 {
                     resultMsg = new BaseJson4Page<News_Preview>
                     {
                         Status = (int)JsonObjectStatus.Fail,
                         Data = null,
                         Message = JsonObjectStatus.Fail.GetEnumText() + "，请求参数Category为空。",
                         BackUrl = null
                     };
                 }
             }, e =>
             {
                 resultMsg = new BaseJson4Page<News_Preview>
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

        #region 新闻详情
        /// <summary>
        /// 新闻详情
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetNewsDetails(NewsDetail arg)
        {
            BaseJson<GetZX_NewsDetails> resultMsg = new BaseJson<GetZX_NewsDetails> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };
            Logger(typeof(ZxNewsController), arg.TryToJson(), "新闻详情-GetNewsDetails", () =>
            {
                if (!string.IsNullOrEmpty(arg.NewsId))
                {
                    #region 不用
                    //NewsEntity data = newsBll.GetEntity(arg.NewsId);
                    ////获取数据
                    //GetZX_NewsDetails res = new GetZX_NewsDetails();
                    //if (data != null)
                    //{
                    //    res = new GetZX_NewsDetails
                    //    {
                    //        NewsId = data.NewsId,
                    //        PK = data.PK,
                    //        FullHead = data.FullHead,
                    //        CreateDate = data.CreateDate.TryToDateTimeToString("yyyy-MM-dd"),
                    //        CreateUserName = data.CreateUserName,
                    //        NewsContent = data.NewsContent,
                    //        PeriodsNumber = data.PeriodsNumber,
                    //        IsRecommend = data.IsRecommend ?? false,
                    //        IsHot = data.IsHot ?? false,
                    //        PV = data.PV ?? 0,
                    //        TitleElement = data.TitleElement,
                    //        DescriptionElement = data.DescriptionElement,
                    //        KeywordElement = data.KeywordElement,
                    //    };

                    //    #region 新增上一期、下一期预告
                    //    List<OnThene> preAndNext = GetNewsListByPk(res.PK ?? 1);
                    //    res.PreAndNextNewsList = preAndNext;
                    //    #endregion

                    //} 
                    #endregion

                    DataTable data = commonBll.ExcuteSqlDataTable(string.Format(GetNewsDetailSql, arg.NewsId), DatabaseLinksEnum.Base);
                    if (data != null && data.Rows.Count > 0)
                    {
                        NewsEntity entity = data.DataTableToObject<NewsEntity>();

                        GetZX_NewsDetails res = new GetZX_NewsDetails
                        {
                            NewsId = entity.NewsId,
                            PK = entity.PK,
                            FullHead = entity.FullHead,
                            CreateDate = entity.CreateDate.TryToDateTimeToString("yyyy-MM-dd"),
                            CreateUserName = entity.CreateUserName,
                            NewsContent = entity.NewsContent,
                            PeriodsNumber = entity.PeriodsNumber,
                            IsRecommend = entity.IsRecommend ?? false,
                            IsHot = entity.IsHot ?? false,
                            PV = entity.PV ?? 0,
                            TitleElement = entity.TitleElement,
                            DescriptionElement = entity.DescriptionElement,
                            KeywordElement = entity.KeywordElement,
                        };

                        #region 新增上一期、下一期预告
                        List<PreAndNextNews> preAndNext = GetNewsListByPk(res.PK ?? 1);
                        res.PreAndNextNewsList = preAndNext;
                        #endregion

                        resultMsg = new BaseJson<GetZX_NewsDetails>
                        {
                            Status = (int)JsonObjectStatus.Success,
                            Data = res,
                            Message = JsonObjectStatus.Success.GetEnumText(),
                            BackUrl = null
                        };
                    }
                }
                else
                {
                    resultMsg = new BaseJson<GetZX_NewsDetails>
                    {
                        Status = (int)JsonObjectStatus.Fail,
                        Data = null,
                        Message = JsonObjectStatus.Fail.GetEnumText() + "，请求参数有误。",
                        BackUrl = null
                    };
                }
            }, e =>
            {
                resultMsg = new BaseJson<GetZX_NewsDetails>
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
        /// 获取上一期、下一期文章集合
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        private List<PreAndNextNews> GetNewsListByPk(int pk)
        {
            List<PreAndNextNews> list = new List<PreAndNextNews>();
            try
            {
                #region 上一期
                NewsEntity dataPre = GetPreNewsByPk(pk);
                if (dataPre != null)
                {
                    //上一期
                    PreAndNextNews onThene = new PreAndNextNews
                    {
                        Which = 1,
                        Title = dataPre.FullHead,
                        NewsId = dataPre.PK.ToString()
                    };
                    list.Add(onThene);
                }
                else
                {
                    //上一期
                    PreAndNextNews onThene = new PreAndNextNews
                    {
                        Which = 1,
                        Title = "已经是最新一期啦",
                        NewsId = ""
                    };
                    list.Add(onThene);
                }
                #endregion

                #region 下一期
                NewsEntity dataNext = GetNextNewsByPk(pk);
                if (dataNext != null)
                {
                    //下一期
                    PreAndNextNews onThene = new PreAndNextNews
                    {
                        Which = 2,
                        Title = dataNext.FullHead,
                        NewsId = dataNext.PK.ToString()
                    };
                    list.Add(onThene);
                }
                else
                {
                    //下一期
                    PreAndNextNews onThene = new PreAndNextNews
                    {
                        Which = 2,
                        Title = "已经是最后一期啦",
                        NewsId = ""
                    };
                    list.Add(onThene);
                }
                #endregion
            }
            catch (Exception e)
            {
                return new List<PreAndNextNews>();
            }
            return list;
        }

        /// <summary>
        /// 递归获取上一期文章
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        private NewsEntity GetPreNewsByPk(int pk)
        {
            NewsEntity entity = null;
            int tempPrePk = pk;
            lock (_lock)
            {
                do
                {
                    //数据库45524为第一条
                    tempPrePk = (tempPrePk - 1) <= 0 ? 45524 : (tempPrePk - 1);
                    //entity = newsBll.GetEntity(n => n.PK == tempPrePk);
                    DataTable table = commonBll.ExcuteSqlDataTable(string.Format(GetNewsAbstractSql, tempPrePk.ToString()), DatabaseLinksEnum.Base);
                    entity = table.DataTableToObject<NewsEntity>();
                } while (entity == null);
            }
            return entity;
        }
        /// <summary>
        /// 获取下一期
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        private NewsEntity GetNextNewsByPk(int pk)
        {
            NewsEntity entity = null;
            int tempPrePk = pk;
            lock (_lock)
            {
                do
                {
                    tempPrePk = (tempPrePk + 1) <= 0 ? 45524 : (tempPrePk + 1);

                    DataTable table = commonBll.ExcuteSqlDataTable(string.Format(GetNewsAbstractSql, tempPrePk.ToString()), DatabaseLinksEnum.Base);
                    entity = table.DataTableToObject<NewsEntity>();
                } while (entity == null);
                //return newsBll.GetEntity(n => n.PK == tempPrePk);
            }
            return entity;
        }
        #endregion

        #region  彩种下面的分类名称和文章列表
        /// <summary>
        /// 彩种下面的分类名称和文章列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetClassiFicationNewsList(GetClassiFicationNewsList arg)
        {
            BaseJson<ClassiFication> resultMsg = new BaseJson<ClassiFication> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };
            Logger(typeof(ZxNewsController), arg.TryToJson(), "彩种下面的分类名称和文章列表-GetClassiFicationNewsList", () =>
             {
                 if (!string.IsNullOrEmpty(arg.Category))
                 {
                     //获取分类信息
                     List<DataItemModel> data = dataItemCache.GetDataItemList(arg.Category);
                     List<ItemList> res = data.Select(d => new ItemList
                     {
                         ItemId = d.ItemDetailId,
                         SimpleSpelling = d.SimpleSpelling.ToUpper(),
                         ItemName = d.ItemName
                     }).ToList();

                     //文章Ids
                     string firstId = res[0].ItemId;
                     List<NewsEntity> news = newsBll.GetList(t => t.CategoryId.Equals(firstId)).Take(GlobalStaticConstant.SYSTEM_DEFAULT_PAGE_SIZE).ToList();
                     List<NewsPreviewItem> newsPreviewItem = new List<NewsPreviewItem>();
                     if (news.Count > 0)
                     {
                         foreach (NewsEntity n in news)
                         {
                             NewsPreviewItem preItem = new NewsPreviewItem
                             {
                                 AddTime = n.CreateDate.TryToDateTimeToString("yyyy-MM-dd"),
                                 NewsId = n.NewsId,
                                 Title = n.FullHead
                             };
                             newsPreviewItem.Add(preItem);
                         }
                     }
                     ClassiFication result = new ClassiFication
                     {
                         ItemList = res,
                         NewsList = newsPreviewItem
                     };
                     resultMsg = new BaseJson<ClassiFication>
                     {
                         Status = (int)JsonObjectStatus.Success,
                         Data = result,
                         Message = JsonObjectStatus.Success.GetEnumText(),
                         BackUrl = null
                     };
                 }
                 else
                 {
                     resultMsg = new BaseJson<ClassiFication>
                     {
                         Status = (int)JsonObjectStatus.Fail,
                         Data = null,
                         Message = JsonObjectStatus.Fail.GetEnumText() + "，请求参数Category为空。",
                         BackUrl = null
                     };
                 }
             }, e =>
             {
                 resultMsg = new BaseJson<ClassiFication>
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

        #region  彩种下面的文章列表

        /// <summary>
        /// 彩种下面的文章列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetNewsList(GetNewsList arg)
        {
            BaseJson4Page<NewsPreviewItem> resultMsg = new BaseJson4Page<NewsPreviewItem> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };

            Logger(typeof(ZxNewsController), arg.TryToJson(), "彩种下面的文章列表-GetNewsList", () =>
            {
                if (!string.IsNullOrEmpty(arg.CategoryId))
                {
                    //Pagination page = new Pagination
                    //{
                    //    rows = arg.PageSize,
                    //    page = arg.PageIndex,
                    //    sidx = "CreateDate",
                    //    sord = "desc",
                    //    records = 0,
                    //    conditionJson = ""
                    //};

                    //分类Ids
                    string id = arg.CategoryId;

                    List<NewsPreviewItem> newsPreviewItem = new List<NewsPreviewItem>();
                    int records = 0;

                    lock (_lock)
                    {
                        Trace.WriteLine("========加锁时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));

                        Task task = Task.Factory.StartNew(() =>
                        {
                            //获取数据
                            List<NewsEntity> news = newsBll.FindList<NewsEntity>(t => t.CategoryId.Equals(id), "CreateDate", false, arg.PageSize, arg.PageIndex, out records).ToList();//.GetPageList(t => t.CategoryId.Equals(id), page).ToList();

                            Trace.WriteLine("========请求到数据：" + news.Count);

                            if (news.Count > 0)
                            {
                                newsPreviewItem = news.Select(n => new NewsPreviewItem
                                {
                                    AddTime = n.CreateDate.TryToDateTimeToString("yyyy-MM-dd"),
                                    NewsId = n.NewsId,
                                    Title = n.FullHead
                                }).ToList();
                            }
                            else
                            {
                                //如果为空，则默认查询太湖字谜和彩神通字谜下面子分类咨询
                                List<DataItemDetailEntity> list = dataItemCache.GetDataItemListById(arg.CategoryId);
                                if (list != null)
                                {
                                    List<string> itemDetailIds = list.Select(d => d.ItemDetailId).ToList();

                                    //获取数据
                                    List<NewsEntity> temp = newsBll.FindList<NewsEntity>(t => itemDetailIds.Contains(t.CategoryId), "CreateDate", false, arg.PageSize, arg.PageIndex, out records).ToList();//.GetPageList(t => t.CategoryId.Equals(id), page).ToList();

                                    //newsBll.GetPageList(t => itemDetailIds.Contains(t.CategoryId), page).ToList();

                                    newsPreviewItem = temp.Select(n => new NewsPreviewItem
                                    {
                                        AddTime = n.CreateDate.TryToDateTimeToString("yyyy-MM-dd"),
                                        NewsId = n.NewsId,
                                        Title = n.FullHead
                                    }).ToList();
                                }
                            }
                        });
                        task.Wait();
                    }
                    Trace.WriteLine("当前线程ID：" + Thread.CurrentThread.ManagedThreadId);
                    Trace.WriteLine("========离开时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));

                    PageData<NewsPreviewItem> pageData = new PageData<NewsPreviewItem>
                    {
                        TotalRow = records,
                        TotalPage = Math.Ceiling(records * 1.0 / arg.PageSize).TryToInt32(),
                        PageIndex = arg.PageIndex,
                        Rows = newsPreviewItem
                    };

                    resultMsg = new BaseJson4Page<NewsPreviewItem>
                    {
                        Status = (int)JsonObjectStatus.Success,
                        Data = pageData,
                        Message = JsonObjectStatus.Success.GetEnumText(),
                        BackUrl = null
                    };
                }
                else
                {
                    resultMsg = new BaseJson4Page<NewsPreviewItem>
                    {
                        Status = (int)JsonObjectStatus.Fail,
                        Data = null,
                        Message = JsonObjectStatus.Fail.GetEnumText() + "，请求参数CategoryId为空。",
                        BackUrl = null
                    };
                }
            }, e =>
            {
                resultMsg = new BaseJson4Page<NewsPreviewItem>
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
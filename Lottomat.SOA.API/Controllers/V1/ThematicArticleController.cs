using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lottomat.Application.Busines.CommonManage;
using Lottomat.Application.Cache;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.CommonEntity;
using Lottomat.Application.Entity.InformationManage;
using Lottomat.Application.Entity.LotteryNumberManage.Parameter;
using Lottomat.Application.Entity.ViewModel;
using Lottomat.Cache.Factory;
using Lottomat.SOA.API.Controllers.Base;
using Lottomat.Util;
using Lottomat.Util.Extension;

namespace Lottomat.SOA.API.Controllers.V1
{
    /// <summary>
    /// 专题文章
    /// </summary>
    public class ThematicArticleController : BaseApiController
    {
        #region 实例
        /// <summary>
        /// 公共BLL
        /// </summary>
        private static readonly CommonBLL commonBll = new CommonBLL();

        #endregion

        /// <summary>
        /// 获取专题文章分类
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetThematicArticleClassify(BaseParameterEntity arg)
        {
            BaseJson<List<ZTColumnViewModel>> resultMsg = new BaseJson<List<ZTColumnViewModel>> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };

            Logger(typeof(ThematicArticleController), arg.TryToJson(), "获取专题文章分类-GetThematicArticleClassify", () =>
            {
                if (!string.IsNullOrEmpty(arg.t))
                {
                    if (arg.t.CheckTimeStamp())
                    {
                        List<ZTColumnViewModel> res = Cache.Factory.CacheFactory.Cache().GetCache<List<ZTColumnViewModel>>("__ThematicArticleClassify");
                        if (res == null || res.Count == 0)
                        {
                            res = new List<ZTColumnViewModel>();

                            DataTable data = commonBll.ExcuteSqlDataTable(QueryThematicArticleClassify, DatabaseLinksEnum.CB55128);
                            if (data.IsExistRows())
                            {
                                List<ZTColumnEntity> list = data.DataTableToList<ZTColumnEntity>();
                                foreach (ZTColumnEntity entity in list)
                                {
                                    ZTColumnViewModel model = new ZTColumnViewModel
                                    {
                                        Name = entity.Name.Trim(),
                                        CId = entity.CId,
                                        TagId = entity.TagId,
                                        Logo = entity.Logo.ToLower(),
                                        Lottery = entity.Lottery.Trim(),
                                        About = entity.About,
                                        hTitle = entity.hTitle,
                                        hKeywords = entity.hKeywords,
                                        hDescription = entity.hDescription,
                                        RewriteUrl = entity.RewriteUrl.Trim()
                                    };

                                    res.Add(model);
                                }

                                Cache.Factory.CacheFactory.Cache().WriteCache(res, "__ThematicArticleClassify", DateTime.Now.AddHours(12));
                            }
                        }

                        resultMsg = new BaseJson<List<ZTColumnViewModel>>
                        {
                            Status = (int)JsonObjectStatus.Success,
                            Data = res,
                            Message = JsonObjectStatus.Success.GetEnumText(),
                            BackUrl = null
                        };
                    }
                    else
                    {
                        resultMsg = new BaseJson<List<ZTColumnViewModel>>
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
                    resultMsg = new BaseJson<List<ZTColumnViewModel>>
                    {
                        Status = (int)JsonObjectStatus.Fail,
                        Data = null,
                        Message = JsonObjectStatus.Fail.GetEnumText() + "，请求参数为空。",
                        BackUrl = null
                    };
                }
            }, e =>
            {
                resultMsg = new BaseJson<List<ZTColumnViewModel>>
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
        /// 获取专题文章列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetThematicArticleList(ThematicArticleListArg arg)
        {
            BaseJson4Page<ZTArticleViewModel> resultMsg = new BaseJson4Page<ZTArticleViewModel> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };

            Logger(typeof(ThematicArticleController), arg.TryToJson(), "获取专题文章列表-GetThematicArticleList", () =>
            {
                if (!string.IsNullOrEmpty(arg.t))
                {
                    if (arg.t.CheckTimeStamp())
                    {
                        string sql = $"SELECT [Id],[Title],[ShortDetail],[Addtime] FROM [dbo].[ZT_Article] WHERE Cid = {arg.CId}";
                        
                        DataTable data = commonBll.FindPageDataTable(sql, "Addtime", false, arg.PageSize, arg.PageIndex, out int total, DatabaseLinksEnum.CB55128);
                        if (data.IsExistRows())
                        {
                            List<ZTArticleViewModel> res = data.DataTableToList<ZTArticleViewModel>();

                            PageData<ZTArticleViewModel> pageData = new PageData<ZTArticleViewModel>
                            {
                                TotalRow = total,
                                TotalPage = (int)Math.Floor(total * 1.0 / arg.PageSize) + 1,
                                PageIndex = arg.PageIndex,
                                Rows = res
                            };

                            resultMsg = new BaseJson4Page<ZTArticleViewModel>
                            {
                                Status = (int)JsonObjectStatus.Success,
                                Data = pageData,
                                Message = JsonObjectStatus.Success.GetEnumText(),
                                BackUrl = null
                            };
                        }
                        else
                        {
                            resultMsg = new BaseJson4Page<ZTArticleViewModel>
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
                        resultMsg = new BaseJson4Page<ZTArticleViewModel>
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
                    resultMsg = new BaseJson4Page<ZTArticleViewModel>
                    {
                        Status = (int)JsonObjectStatus.Fail,
                        Data = null,
                        Message = JsonObjectStatus.Fail.GetEnumText() + "，请求参数为空。",
                        BackUrl = null
                    };
                }
            }, e =>
            {
                resultMsg = new BaseJson4Page<ZTArticleViewModel>
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
        /// 获取专题文章详细
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetThematicArticleById(ThematicArticleInfoArg arg)
        {
            BaseJson<ZTArticleDetailViewModel> resultMsg = new BaseJson<ZTArticleDetailViewModel> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };

            Logger(typeof(ThematicArticleController), arg.TryToJson(), "获取专题文章详细-GetThematicArticleById", () =>
            {
                if (!string.IsNullOrEmpty(arg.t))
                {
                    if (arg.t.CheckTimeStamp())
                    {
                        //string sql = $"SELECT [Id],[Title],[Detail],[Editor],[Addtime] FROM [dbo].[ZT_ArticleDetail] WHERE Id = {arg.Id}";
                        string sql = $"SELECT ad.[Id],ad.[Title],ad.[Detail],ad.[Editor],ad.[Addtime],c.[Name] AS TypeName,c.[Lottery] AS EnumCode FROM [dbo].[ZT_ArticleDetail] AS ad JOIN [dbo].[ZT_Column] AS c ON ad.Id = {arg.Id} AND c.CId = {arg.CId}";

                        DataTable data = commonBll.ExcuteSqlDataTable(sql, DatabaseLinksEnum.CB55128);
                        if (data.IsExistRows())
                        {
                            ZTArticleDetailViewModel detail = data.DataTableToObject<ZTArticleDetailViewModel>();
                            if (detail != null)
                            {
                                detail.Editor = detail.Editor.Trim();
                                detail.TypeName = detail.TypeName.Trim();
                                //校正彩种枚举码
                                switch (detail.EnumCode.Trim())
                                {
                                    case "ssq":
                                        detail.EnumCode = SCCLottery.SSQ.ToString();
                                        break;
                                    case "p3":
                                        detail.EnumCode = SCCLottery.PL3.ToString();
                                        break;
                                    case "3d":
                                        detail.EnumCode = SCCLottery.FC3D.ToString();
                                        break;
                                    case "dlt":
                                        detail.EnumCode = SCCLottery.DLT.ToString();
                                        break;
                                }

                                resultMsg = new BaseJson<ZTArticleDetailViewModel>
                                {
                                    Status = (int)JsonObjectStatus.Success,
                                    Data = detail,
                                    Message = JsonObjectStatus.Success.GetEnumText(),
                                    BackUrl = null
                                };
                            }
                        }
                    }
                    else
                    {
                        resultMsg = new BaseJson<ZTArticleDetailViewModel>
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
                    resultMsg = new BaseJson<ZTArticleDetailViewModel>
                    {
                        Status = (int)JsonObjectStatus.Fail,
                        Data = null,
                        Message = JsonObjectStatus.Fail.GetEnumText() + "，请求参数为空。",
                        BackUrl = null
                    };
                }
            }, e =>
            {
                resultMsg = new BaseJson<ZTArticleDetailViewModel>
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
        /// 获取系统推荐专题文章列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetRecommendThematicArticleList(RecommendThematicArticleArg arg)
        {
            BaseJson<List<ZTArticleViewModel>> resultMsg = new BaseJson<List<ZTArticleViewModel>> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };

            Logger(typeof(ThematicArticleController), arg.TryToJson(), "获取系统推荐专题文章列表-GetRecommendThematicArticleList", () =>
            {
                if (!string.IsNullOrEmpty(arg.t))
                {
                    if (arg.t.CheckTimeStamp())
                    {
                        string[] codeArr = {"SSQ", "PL3", "FC3D", "DLT" };
                        arg.EnumCode = string.IsNullOrEmpty(arg.EnumCode) ? "Default" : codeArr.Contains(arg.EnumCode) ? arg.EnumCode : "Default";

                        List<ZTArticleViewModel> list = CacheFactory.Cache().GetCache<List<ZTArticleViewModel>>("__RecommendThematicArticle_" + arg.TotalRecord + "_" + arg.EnumCode.Trim().ToUpper());
                        if (list == null || list.Count == 0)
                        {
                            list = new List<ZTArticleViewModel>();

                            //string sql = $"SELECT TOP {arg.TotalRecord} [Id],[Title],[AddTime],[Cid] FROM [dbo].[ZT_Article] ORDER BY Addtime DESC";

                            string code = String.Empty;
                            //校正彩种编码
                            switch (arg.EnumCode.Trim())
                            {
                                case "SSQ":
                                    code = "ssq";
                                    break;
                                case "PL3":
                                    code = "p3";
                                    break;
                                case "FC3D":
                                    code = "3d";
                                    break;
                                case "DLT":
                                    code = "dlt";
                                    break;
                                default:
                                    code = "";
                                    break;
                            }

                            string sql = "";
                            if (string.IsNullOrEmpty(code))
                            {
                                sql = $"SELECT TOP {arg.TotalRecord} [Id],[Title],[AddTime],[Cid] FROM [dbo].[ZT_Article] WHERE Cid IN(SELECT [CId] FROM [dbo].[ZT_Column] WHERE TypeName = 'article') ORDER BY Addtime DESC";
                            }
                            else
                            {
                                sql = $"SELECT TOP {arg.TotalRecord} [Id],[Title],[AddTime],[Cid] FROM [dbo].[ZT_Article] WHERE Cid IN(SELECT [CId] FROM [dbo].[ZT_Column] WHERE Lottery = '{code}' AND TypeName = 'article') ORDER BY Addtime DESC";
                            }

                            DataTable data = commonBll.ExcuteSqlDataTable(sql, DatabaseLinksEnum.CB55128);
                            if (data.IsExistRows())
                            {
                                for (int i = 0; i < data.Rows.Count; i++)
                                {
                                    ZTArticleViewModel article = new ZTArticleViewModel
                                    {
                                        Id = data.Rows[i]["Id"].TryToInt32(),
                                        Title = data.Rows[i]["Title"].ToStringEx(),
                                        Addtime = data.Rows[i]["AddTime"].TryToDateTime(),
                                        Cid = data.Rows[i]["Cid"].TryToInt32()
                                    };
                                    list.Add(article);
                                }
                                CacheFactory.Cache().WriteCache(list, "__RecommendThematicArticle_" + arg.TotalRecord + "_" + arg.EnumCode.Trim().ToUpper(), DateTime.Now.AddHours(6));
                            }
                            else
                            {
                                resultMsg = new BaseJson<List<ZTArticleViewModel>>
                                {
                                    Status = (int)JsonObjectStatus.Success,
                                    Data = null,
                                    Message = JsonObjectStatus.Success.GetEnumText(),
                                    BackUrl = null
                                }; 
                            }
                        }

                        resultMsg = new BaseJson<List<ZTArticleViewModel>>
                        {
                            Status = (int)JsonObjectStatus.Success,
                            Data = list,
                            Message = JsonObjectStatus.Success.GetEnumText(),
                            BackUrl = null
                        };
                    }
                    else
                    {
                        resultMsg = new BaseJson<List<ZTArticleViewModel>>
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
                    resultMsg = new BaseJson<List<ZTArticleViewModel>>
                    {
                        Status = (int)JsonObjectStatus.Fail,
                        Data = null,
                        Message = JsonObjectStatus.Fail.GetEnumText() + "，请求参数为空。",
                        BackUrl = null
                    };
                }
            }, e =>
            {
                resultMsg = new BaseJson<List<ZTArticleViewModel>>
                {
                    Status = (int)JsonObjectStatus.Exception,
                    Data = null,
                    Message = JsonObjectStatus.Exception.GetEnumText() + "，异常信息：" + e.Message,
                    BackUrl = null
                };
            });

            return resultMsg.TryToJson().ToHttpResponseMessage();
        }

        #region SQL语句
        /// <summary>
        /// 查询专题文章分类
        /// </summary>
        private string QueryThematicArticleClassify =
                "SELECT [Id],[Name],[CId],[TagId],[RewriteUrl],[Logo],[Lottery],[TypeName],[Status],[Words],[About],[hTitle],[hKeywords],[hDescription],[Addtime] FROM [dbo].[ZT_Column] WHERE TypeName  = 'article'";

        #endregion
    }
}

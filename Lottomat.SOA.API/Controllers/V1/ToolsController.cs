using Lottomat.SOA.API.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lottomat.Application.Entity.ViewModel.ToolsMangerModer;
using Lottomat.Application.Entity.CommonEntity;
using Lottomat.Application.Code;
using Lottomat.Util.WebControl;
using Lottomat.Application.Entity.InformationManage;
using Lottomat.Util.Extension;
using Lottomat.Application.Cache;
using Lottomat.Application.Entity.SystemManage;
using Lottomat.Application.Busines.InformationManage;
using Lottomat.Application.Entity.SystemManage.ViewModel;
using Lottomat.Application.Entity.ViewModel.ConsultationMangerModel;

namespace Lottomat.SOA.API.Controllers.V1
{
    /// <summary>
    /// 彩吧工具
    /// </summary>
    public class ToolsController : BaseApiController
    {
        private static readonly ToolsBLL toolsBll = new ToolsBLL();

        private static readonly DataItemCache dataItemCache = new DataItemCache();

        #region 查询工具页面下的信息
        /// <summary>
        /// 获取彩吧工具分类列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetToolsList()
        {
            BaseJson<List<Tools_Preview>> resultMsg = new BaseJson<List<Tools_Preview>> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };
            Logger(typeof(ToolsController),"", "获取彩吧工具分类列表-GetToolsList", () =>
            {
                List<Tools_Preview> res = Cache.Factory.CacheFactory.Cache().GetCache<List<Tools_Preview>>("__ColorBarTools__");
                if (res == null)
                {
                    res = new List<Tools_Preview>();

                    List<DataItemModel> data = dataItemCache.GetDataItemList("ColorBarTools").OrderBy(n=>n.SortCode).ToList();

                    //工具类下面的彩种名称
                    List<ItemList> toolsType = data.Select(d => new ItemList
                    {
                        ItemId = d.ItemDetailId,
                        SimpleSpelling = d.SimpleSpelling.ToUpper(),
                        ItemName = d.ItemName
                    }).ToList();

                    foreach (ItemList list in toolsType)
                    {
                        //获取数据
                        List<ToolsEntity> toolsnews = toolsBll.GetList(n => n.IsDelete == false && n.IsStick == true && n.CategoryId.Equals(list.ItemId)).OrderBy(n => n.SortCode).ToList();
                        //从数据中
                        List<ToolsItem> toolsItems = toolsnews.Select(n => new ToolsItem
                        {
                            Title = n.Title,
                            ToolsUrl = n.ToolsUrl
                            }).ToList();

                        Tools_Preview preview = new Tools_Preview
                        {
                            ToolsName = list.ItemName,
                            SimpleSpelling = list.SimpleSpelling,
                            ToolsPreviewItem = toolsItems
                        };
                        res.Add(preview);
                    }
                    Cache.Factory.CacheFactory.Cache().WriteCache<List<Tools_Preview>>(res, "__ColorBarTools__");
                }

                resultMsg = new BaseJson<List<Tools_Preview>>
                {
                    Status = (int)JsonObjectStatus.Success,
                    Data = res,
                    Message = JsonObjectStatus.Success.GetEnumText(),
                    BackUrl = null
                };
            }, e =>
            {
                resultMsg = new BaseJson<List<Tools_Preview>>
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

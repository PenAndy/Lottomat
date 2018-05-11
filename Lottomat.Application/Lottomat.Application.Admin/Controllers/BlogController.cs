using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lottomat.Application.Busines.PublicInfoManage;
using Lottomat.Application.Cache;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.PublicInfoManage;
using Lottomat.Application.Entity.SystemManage.ViewModel;
using Lottomat.Util;
using Lottomat.Util.Extension;
using Lottomat.Util.WebControl;

namespace Lottomat.Application.Admin.Controllers
{
    /// <summary>
    /// 门户主页控制器
    /// </summary>
    [HandlerLogin(LoginMode.Ignore)]
    public class BlogController : MvcControllerBase
    {
        //
        // GET: /Blog/
        /// <summary>
        /// 新闻中心
        /// </summary>
        private NewsBLL newsBll = new NewsBLL();
        private NoticeBLL noticeBll = new NoticeBLL();
        private DataItemCache itemCache = new DataItemCache();
        private Object _lock = new Object();

        #region 视图功能
        /// <summary>
        /// 门户主页视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 文章详细
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail()
        {
            return View();
        }

        /// <summary>
        /// 分类详细
        /// </summary>
        /// <returns></returns>
        public ActionResult Category()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取首页文章数据
        /// </summary>
        /// <param name="pageIndex">索引</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(int pageIndex, string queryJson)
        {
            Stopwatch watch = CommonHelper.TimerStart();

            Pagination pagination = new Pagination
            {
                rows = 6,
                page = pageIndex,
                sidx = "CreateDate DESC",
                sord = "",
                records = 0,
                conditionJson = ""
            };

            List<NewsEntity> data = newsBll.GetPageList(pagination, queryJson).ToList();
            //置顶
            data = data.OrderByDescending(d => d.IsStick).ThenByDescending(d => d.CreateDate).ToList();
            //CommentingNum

            var jsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = data.Count,
                costtime = CommonHelper.TimerEnd(watch)
            };

            return Content(jsonData.ToJson());
        }

        /// <summary>
        /// 获取公告数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LoadAnnouncement()
        {
            List<NoticeEntity> data =
                noticeBll.GetList(
                    n =>
                        n.TypeId == 2 && n.DeleteMark == (int)DeleteMarkEnum.NotDelete &&
                        n.EnabledMark == (int)EnabledMarkEnum.Enabled).ToList();
            return Success("操作成功", data);
        }

        /// <summary>
        /// 获取文章详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Detail(string id)
        {
            NewsEntity data;
            if (!string.IsNullOrEmpty(id))
            {
                data = newsBll.GetEntity(id);
                lock (_lock)
                {
                    //更新浏览量+1
                    int old = data.PV ?? 0;
                    NewsEntity temp = new NewsEntity
                    {
                        NewsId = id,
                        PV = old + 1
                    };

                    newsBll.UpdateForm(temp);
                }
            }
            else
            {
                return View();
            }

            return View(data);
        }

        /// <summary>
        /// 获取所有文章分类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetArticleClassify()
        {
            List<DataItemModel> itemModels = itemCache.GetDataItemList("NewsCategory").ToList();

            return Success("操作成功", itemModels);
        }
        #endregion
    }
}

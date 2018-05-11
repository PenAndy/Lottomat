using Lottomat.Application.Entity.PublicInfoManage;
using Lottomat.Application.IService.PublicInfoManage;
using Lottomat.Application.Service.PublicInfoManage;
using Lottomat.Util;
using Lottomat.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lottomat.Utils;
using Lottomat.Utils.Web;

namespace Lottomat.Application.Busines.PublicInfoManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2015.12.7 16:40
    /// 描 述：新闻中心
    /// </summary>
    public class NewsBLL
    {
        private INewsService service = new NewsService();

        #region 获取数据
        /// <summary>
        /// 新闻列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<NewsEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public IEnumerable<NewsEntity> GetPageList(Expression<Func<NewsEntity, bool>> condition, Pagination pagination)
        {
            return service.GetPageList(condition, pagination);
        }
        /// <summary>
        /// 新闻实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public NewsEntity GetEntity(string keyValue)
        {
            NewsEntity newsEntity = service.GetEntity(keyValue);
            if (newsEntity != null)
                newsEntity.NewsContent = WebHelper.HtmlDecode(newsEntity.NewsContent);
            return newsEntity;
        }
        public NewsEntity GetEntity(Expression<Func<NewsEntity, bool>> condition)
        {
            NewsEntity newsEntity = service.GetEntity(condition);
            if (newsEntity != null)
                newsEntity.NewsContent = WebHelper.HtmlDecode(newsEntity.NewsContent);
            return newsEntity;
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<NewsEntity> GetList(Expression<Func<NewsEntity, bool>> condition)
        {
            return service.GetList(condition).ToList();
        }
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, string orderField, bool isAsc, int pageSize, int pageIndex,
            out int total) where T : class, new()
        {
            return service.FindList(condition, orderField, isAsc, pageSize, pageIndex, out total);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存新闻表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="newsEntity">新闻实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, NewsEntity newsEntity)
        {
            try
            {
                newsEntity.NewsContent = WebHelper.HtmlEncode(newsEntity.NewsContent);
                service.SaveForm(keyValue, newsEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="newsEntity">新闻实体</param>
        /// <returns></returns>
        public void UpdateForm(NewsEntity newsEntity)
        {
            service.UpdateForm(newsEntity);
        }
        #endregion

        #region 修改数据
        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="modelModifyProps">要修改的列及修改后列的值集合</param>
        /// <param name="where">修改的条件</param>
        /// <param name="paramModifyStrings">修改列的名称的集合</param>
        /// <returns>返回受影响行数</returns>
        public int Modify(NewsEntity modelModifyProps, Expression<Func<NewsEntity, bool>> where, params string[] paramModifyStrings)
        {
            return service.Modify(modelModifyProps, where, paramModifyStrings);
        }
        #endregion
    }
}

using System;
using Lottomat.Application.Entity.PublicInfoManage;
using Lottomat.Application.IService.PublicInfoManage;
using Lottomat.Data.Repository;
using Lottomat.Util;
using Lottomat.Util.Extension;
using Lottomat.Util.WebControl;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;

namespace Lottomat.Application.Service.PublicInfoManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2015.12.7 16:40
    /// 描 述：新闻中心
    /// </summary>
    public class NewsService : RepositoryFactory<NewsEntity>, INewsService
    {
        #region 获取数据
        /// <summary>
        /// 新闻列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<NewsEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<NewsEntity>();
            JObject queryParam = queryJson.ToJObject();
            if (queryParam != null)
            {
                if (!queryParam["FullHead"].IsEmpty())
                {
                    string FullHead = queryParam["FullHead"].ToString();
                    expression = expression.And(t => t.FullHead.Contains(FullHead));
                }
                if (!queryParam["Category"].IsEmpty())
                {
                    string Category = queryParam["Category"].ToString();
                    expression = expression.And(t => t.Category == Category);
                }
                if (!queryParam["CategoryId"].IsEmpty())
                {
                    string CategoryId = queryParam["CategoryId"].ToString();
                    expression = expression.And(t => t.CategoryId == CategoryId);
                }
            }
            
            expression = expression.And(t => t.TypeId == 1 && t.IsDelete == false);
            return this.BaseRepository().FindList(expression, pagination);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public IEnumerable<NewsEntity> GetPageList(Expression<Func<NewsEntity, bool>> condition, Pagination pagination)
        {
            //var expression = LinqExtensions.True<NewsEntity>();

            //expression = expression.And(t => t.TypeId == 1 && t.IsDelete == false);
            //expression = expression.And(condition);
            return this.BaseRepository().FindList(condition, pagination);
        }
        /// <summary>
        /// 新闻实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public NewsEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        public NewsEntity GetEntity(Expression<Func<NewsEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IEnumerable<NewsEntity> GetList(Expression<Func<NewsEntity, bool>> condition)
        {
            return this.BaseRepository().FindList(condition);
        }

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="modelModifyProps">要修改的列及修改后列的值集合</param>
        /// <param name="where">修改的条件</param>
        /// <param name="paramModifyStrings">修改列的名称的集合</param>
        /// <returns>返回受影响行数</returns>
        public int Modify(NewsEntity modelModifyProps, Expression<Func<NewsEntity, bool>> where, params string[] paramModifyStrings)
        {
            return this.BaseRepository().Modify(modelModifyProps, where, paramModifyStrings);
        }

        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, string orderField, bool isAsc, int pageSize, int pageIndex,
            out int total) where T : class, new()
        {
            return this.BaseRepository().FindList(condition, orderField, isAsc, pageSize, pageIndex, out total);
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存新闻表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="newsEntity">新闻实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, NewsEntity newsEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                newsEntity.Modify(keyValue);
                newsEntity.TypeId = 1;
                this.BaseRepository().Update(newsEntity);
            }
            else
            {
                newsEntity.Create();
                newsEntity.TypeId = 1;
                this.BaseRepository().Insert(newsEntity);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="newsEntity">新闻实体</param>
        /// <returns></returns>
        public void UpdateForm(NewsEntity newsEntity)
        {
            newsEntity.TypeId = 1;
            this.BaseRepository().Update(newsEntity);
        }

        #endregion
    }
}

using System;
using Lottomat.Application.Entity.PublicInfoManage;
using Lottomat.Util.WebControl;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Lottomat.Application.IService.PublicInfoManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2015.12.7 16:40
    /// 描 述：新闻中心
    /// </summary>
    public interface INewsService
    {
        #region 获取数据
        /// <summary>
        /// 新闻列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<NewsEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 新闻列表
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        IEnumerable<NewsEntity> GetPageList(Expression<Func<NewsEntity, bool>> condition, Pagination pagination);
        /// <summary>
        /// 新闻实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        NewsEntity GetEntity(string keyValue);

        NewsEntity GetEntity(Expression<Func<NewsEntity, bool>> condition);

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IEnumerable<NewsEntity> GetList(Expression<Func<NewsEntity, bool>> condition);

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="modelModifyProps">要修改的列及修改后列的值集合</param>
        /// <param name="where">修改的条件</param>
        /// <param name="paramModifyNames">修改列的名称的集合</param>
        /// <returns>返回受影响行数</returns>
        int Modify(NewsEntity modelModifyProps, Expression<Func<NewsEntity, bool>> where, params string[] paramModifyNames);

        IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class, new();
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存新闻表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="newsEntity">新闻实体</param>
        /// <returns></returns>
        void SaveForm(string keyValue, NewsEntity newsEntity);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="newsEntity">新闻实体</param>
        /// <returns></returns>
        void UpdateForm(NewsEntity newsEntity);
        #endregion
    }
}

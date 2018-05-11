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
    /// 描 述：电子公告
    /// </summary>
    public interface INoticeService
    {
        #region 获取数据
        /// <summary>
        /// 公告列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<NewsEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 公告实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        NewsEntity GetEntity(string keyValue);

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IEnumerable<NewsEntity> GetList(Expression<Func<NewsEntity, bool>> condition);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存公告表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="newsEntity">公告实体</param>
        /// <returns></returns>
        void SaveForm(string keyValue, NewsEntity newsEntity);
        #endregion
    }
}

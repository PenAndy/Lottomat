using Lottomat.Application.Entity.BaseManage;
using Lottomat.Util.WebControl;
using System.Collections.Generic;

namespace Lottomat.Application.IService.BaseManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2018-04-09 14:48
    /// 描 述：站点TDK管理
    /// </summary>
    public interface ISiteTDKService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<SiteTDKEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<SiteTDKEntity> GetList(string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        SiteTDKEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        void SaveForm(string keyValue, SiteTDKEntity entity);
        #endregion
    }
}

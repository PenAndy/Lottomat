using System;
using Lottomat.Application.Entity.InformationManage;
using Lottomat.Util.WebControl;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Lottomat.Application.IService.InformationManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2018-01-05 14:54
    /// 描 述：广告管理
    /// </summary>
    public interface IAdvertisementService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<AdvertisementEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="query">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<AdvertisementEntity> GetList(Expression<Func<AdvertisementEntity, bool>> query);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        AdvertisementEntity GetEntity(string keyValue);

        AdvertisementEntity GetEntity(Expression<Func<AdvertisementEntity, bool>> query);
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
        void SaveForm(string keyValue, AdvertisementEntity entity);
        #endregion
    }
}

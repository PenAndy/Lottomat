using Lottomat.Application.Entity.BaseManage;
using Lottomat.Application.Entity.GalleryManage;
using Lottomat.Application.Entity.PublicInfoManage;
using Lottomat.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Lottomat.Application.IService.GalleryManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创 建：开发者账号
    /// 日 期：2017-10-19 14:29
    /// 描 述：图库详情表
    /// </summary>
    public interface ITk_GalleryDetailIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<Tk_GalleryDetail> GetList(string queryJson);
      
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        Tk_GalleryDetail GetEntity(string keyValue);
        #endregion
        IEnumerable<Tk_GalleryDetail> GetList(Expression<Func<Tk_GalleryDetail, bool>> condition);
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
        void SaveForm(string keyValue, Tk_GalleryDetail entity);
        IEnumerable<Tk_GalleryDetail> GetPageList(Pagination pagination, string queryJson);
        int MenuNewPeriodsNumber(string menuname);
        List<Tk_GalleryDetail> QueryDetailByGalleryId(List<string> galleryIds, int periodsNumber);
         int NewPeriodsNumber();
        #endregion
    }
}

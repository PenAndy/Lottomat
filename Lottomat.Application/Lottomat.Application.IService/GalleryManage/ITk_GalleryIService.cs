using System;
using Lottomat.Application.Entity.BaseManage;
using Lottomat.Application.Entity.GalleryManage;
using Lottomat.Util.WebControl;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Lottomat.Application.IService.GalleryManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创 建：开发者账号
    /// 日 期：2017-10-19 14:24
    /// 描 述：图库标题表
    /// </summary>
    public interface ITk_GalleryIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<Tk_Gallery> GetList(string queryJson);
        IEnumerable<Tk_Gallery> GetList(Expression<Func<Tk_Gallery, bool>> condition);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        Tk_Gallery GetEntity(string keyValue);
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
        void SaveForm(string keyValue, Tk_Gallery entity);
        IEnumerable<Tk_Gallery> GetPageList(Pagination pagination, string queryJson);
         List<Tk_Gallery> QueryAll(int count);
        #endregion
    }
}

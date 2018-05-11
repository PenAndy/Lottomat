using Lottomat.Application.Entity.SystemManage;
using Lottomat.Application.IService.SystemManage;
using Lottomat.Application.Service.SystemManage;
using Lottomat.Util.WebControl;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;
using Lottomat.Application.Code;

namespace Lottomat.Application.Busines.SystemManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-12-06 13:49
    /// 描 述：友情链接
    /// </summary>
    public class BaseFriendLinksBLL
    {
        private IBaseFriendLinksService service = new BaseFriendLinksService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<BaseFriendLinksEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<BaseFriendLinksEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<BaseFriendLinksEntity> GetList(Expression<Func<BaseFriendLinksEntity, bool>> condition)
        {
            return service.GetList(condition);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public BaseFriendLinksEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
                Cache.Factory.CacheFactory.Cache().RemoveCache(CacheKeyEnum.FriendLink.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, BaseFriendLinksEntity entity,string code)
        {
            try
            {
                service.SaveForm(keyValue, entity, code);
                Cache.Factory.CacheFactory.Cache().RemoveCache(CacheKeyEnum.FriendLink.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}

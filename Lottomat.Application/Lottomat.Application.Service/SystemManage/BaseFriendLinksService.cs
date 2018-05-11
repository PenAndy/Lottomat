using System;
using Lottomat.Application.Entity.SystemManage;
using Lottomat.Application.IService.SystemManage;
using Lottomat.Data.Repository;
using Lottomat.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.LotteryNumberManage;
using Lottomat.Util.Extension;
using Newtonsoft.Json.Linq;

namespace Lottomat.Application.Service.SystemManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-12-06 13:49
    /// 描 述：友情链接
    /// </summary>
    public class BaseFriendLinksService : RepositoryFactory<BaseFriendLinksEntity>, IBaseFriendLinksService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<BaseFriendLinksEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<BaseFriendLinksEntity>();
            JObject queryParam = queryJson.ToJObject();
            if (queryParam != null)
            {
                if (!queryParam["Name"].IsEmpty())
                {
                    string Name = queryParam["Name"].ToStringEx();
                    expression = expression.And(t => t.Name.Equals(Name));
                }
                if (!queryParam["Type"].IsEmpty())
                {
                    string Type = queryParam["Type"].ToStringEx();
                    expression = expression.And(t => t.Type.Equals(Type));
                }
            }

            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<BaseFriendLinksEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<BaseFriendLinksEntity> GetList(Expression<Func<BaseFriendLinksEntity, bool>> condition)
        {
            return this.BaseRepository().FindList(condition);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public BaseFriendLinksEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, BaseFriendLinksEntity entity,string code)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (code == "1")
                {
                    entity.IsEnable = true;
                }
                else if (code == "0")
                {
                    entity.IsEnable = false;
                }

                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }
        #endregion
    }
}

using System;
using Lottomat.Application.Entity.InformationManage;
using Lottomat.Application.IService.InformationManage;
using Lottomat.Data.Repository;
using Lottomat.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lottomat.Util.Extension;
using Newtonsoft.Json.Linq;

namespace Lottomat.Application.Service.InformationManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2018-01-05 14:54
    /// 描 述：广告管理
    /// </summary>
    public class AdvertisementService : RepositoryFactory<AdvertisementEntity>, IAdvertisementService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<AdvertisementEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<AdvertisementEntity>();
            JObject queryParam = queryJson.ToJObject();
            if (queryParam != null)
            {
                if (!queryParam["CategoryId"].IsEmpty())
                {
                    string CategoryId = queryParam["CategoryId"].ToString();
                    expression = expression.And(t => t.CategoryId.Contains(CategoryId));
                }
            }

            expression = expression.And(t => t.IsDelete == false);
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="query">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<AdvertisementEntity> GetList(Expression<Func<AdvertisementEntity, bool>> query)
        {
            return this.BaseRepository().FindList(query);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public AdvertisementEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        public AdvertisementEntity GetEntity(Expression<Func<AdvertisementEntity, bool>> query)
        {
            return this.BaseRepository().FindEntity(query);
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
        public void SaveForm(string keyValue, AdvertisementEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
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

using Lottomat.Application.Entity.PublicInfoManage;
using Lottomat.Application.IService.PublicInfoManage;
using Lottomat.Data.Repository;
using Lottomat.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using Lottomat.Util.Extension;
using Newtonsoft.Json.Linq;

namespace Lottomat.Application.Service.PublicInfoManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-08-02 12:20
    /// 描 述：资源分享
    /// </summary>
    public class ResourcesService : RepositoryFactory<ResourcesEntity>, IResourcesService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<ResourcesEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<ResourcesEntity>();
            JObject queryParam = queryJson.ToJObject();
            if (queryParam != null)
            {
                if (!queryParam["Name"].IsEmpty())
                {
                    string Name = queryParam["Name"].ToString();
                    expression = expression.And(t => t.Name.Contains(Name));
                }
                if (!queryParam["TypeNme"].IsEmpty())
                {
                    string TypeName = queryParam["TypeName"].ToString();
                    expression = expression.And(t => t.TypeName == TypeName);
                }
                if (!queryParam["TypeId"].IsEmpty())
                {
                    string TypeId = queryParam["TypeId"].ToString();
                    expression = expression.And(t => t.TypeId == TypeId);
                }
            }
            
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<ResourcesEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ResourcesEntity GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, ResourcesEntity entity)
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

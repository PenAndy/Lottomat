using System;
using Lottomat.Application.Entity.InformationManage;
using Lottomat.Application.IService.InformationManage;
using Lottomat.Data.Repository;
using Lottomat.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.PublicInfoManage;
using Lottomat.Util.Extension;
using Newtonsoft.Json.Linq;

namespace Lottomat.Application.Service.InformationManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-10-17 09:58
    /// 描 述：55128资讯内容
    /// </summary>
    public class ZxNewsService : RepositoryFactory<ZxNewsEntity>, IZxNewsService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<ZxNewsEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<ZxNewsEntity>();
            JObject queryParam = queryJson.ToJObject();
            if (queryParam != null)
            {
                if (!queryParam["title"].IsEmpty())
                {
                    string title = queryParam["title"].ToString();
                    expression = expression.And(t => t.title.Contains(title));
                }
            }

            expression = expression.And(t => t.isDelete == false);
            return this.BaseRepository(DatabaseLinksEnum.InformationBase).FindList(expression, pagination);

            //return this.BaseRepository(DatabaseLinksEnum.InformationBase).FindList(pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<ZxNewsEntity> GetList(string queryJson)
        {
            return this.BaseRepository(DatabaseLinksEnum.InformationBase).IQueryable().ToList();
        }
        
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IEnumerable<ZxNewsEntity> GetList(Expression<Func<ZxNewsEntity, bool>> condition)
        {
            return this.BaseRepository(DatabaseLinksEnum.InformationBase).FindList(condition);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ZxNewsEntity GetEntity(string keyValue)
        {
            return this.BaseRepository(DatabaseLinksEnum.InformationBase).FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository(DatabaseLinksEnum.InformationBase).Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, ZxNewsEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository(DatabaseLinksEnum.InformationBase).Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository(DatabaseLinksEnum.InformationBase).Insert(entity);
            }
        }
        #endregion
    }
}

using System;
using Lottomat.Application.Entity.InformationManage;
using Lottomat.Application.IService.InformationManage;
using Lottomat.Data.Repository;
using Lottomat.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lottomat.Application.Entity.PublicInfoManage;
using Lottomat.Util.Extension;
using Newtonsoft.Json.Linq;

namespace Lottomat.Application.Service.InformationManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-10-27 10:34
    /// 描 述：Zx_Label
    /// </summary>
    public class LabelService : RepositoryFactory<LabelEntity>, ILabelService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<LabelEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<LabelEntity>();
            JObject queryParam = queryJson.ToJObject();
            if (queryParam != null)
            {
                if (!queryParam["LabelName"].IsEmpty())
                {
                    string LabelName = queryParam["LabelName"].ToString();
                    expression = expression.And(t => t.LabelName.Contains(LabelName));
                }
                if (!queryParam["TitleElement"].IsEmpty())
                {
                    string TitleElement = queryParam["TitleElement"].ToString();
                    expression = expression.And(t => t.TitleElement == TitleElement);
                }
                if (!queryParam["DescriptionElement"].IsEmpty())
                {
                    string DescriptionElement = queryParam["DescriptionElement"].ToString();
                    expression = expression.And(t => t.DescriptionElement == DescriptionElement);
                }
                if (!queryParam["KeywordElement"].IsEmpty())
                {
                    string KeywordElement = queryParam["KeywordElement"].ToString();
                    expression = expression.And(t => t.KeywordElement == KeywordElement);
                }
                if (!queryParam["CategoryId"].IsEmpty())
                {
                    string CategoryId = queryParam["CategoryId"].ToString();
                    expression = expression.And(t => t.CategoryId == CategoryId);
                }
            }

            expression = expression.And(t => t.IsDelete == false);
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<LabelEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public LabelEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IEnumerable<LabelEntity> GetList(Expression<Func<LabelEntity, bool>> condition)
        {
            return this.BaseRepository().FindList(condition);
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
        public void SaveForm(string keyValue, LabelEntity entity)
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

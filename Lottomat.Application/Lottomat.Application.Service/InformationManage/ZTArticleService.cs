using Lottomat.Application.Entity.InformationManage;
using Lottomat.Application.IService.InformationManage;
using Lottomat.Data.Repository;
using Lottomat.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using Lottomat.Application.Code;
using Lottomat.Util.Extension;
using Newtonsoft.Json.Linq;

namespace Lottomat.Application.Service.InformationManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2018-01-02 15:17
    /// 描 述：专题文章
    /// </summary>
    public class ZTArticleService : RepositoryFactory<ZTArticleEntity>, IZTArticleService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<ZTArticleEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<ZTArticleEntity>();
            JObject queryParam = queryJson.ToJObject();
            if (queryParam != null)
            {
                if (!queryParam["Cid"].IsEmpty())
                {
                    int Cid = queryParam["Cid"].TryToInt32();
                    expression = expression.And(t => t.Cid == Cid);
                }
            }

            return this.BaseRepository(DatabaseLinksEnum.CB55128).FindList(expression, pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<ZTArticleEntity> GetList(string queryJson)
        {
            return this.BaseRepository(DatabaseLinksEnum.CB55128).IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ZTArticleEntity GetEntity(string keyValue)
        {
            return this.BaseRepository(DatabaseLinksEnum.CB55128).FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository(DatabaseLinksEnum.CB55128).Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, ZTArticleEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository(DatabaseLinksEnum.CB55128).Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository(DatabaseLinksEnum.CB55128).Insert(entity);
            }
        }
        #endregion
    }
}

using System;
using Lottomat.Application.Entity.PublicInfoManage;
using Lottomat.Application.IService.PublicInfoManage;
using Lottomat.Data.Repository;
using Lottomat.Util;
using Lottomat.Util.Extension;
using Lottomat.Util.WebControl;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;

namespace Lottomat.Application.Service.PublicInfoManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2015.12.7 16:40
    /// 描 述：电子公告
    /// </summary>
    public class NoticeService : RepositoryFactory<NewsEntity>, INoticeService
    {
        #region 获取数据
        /// <summary>
        /// 公告列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<NewsEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<NewsEntity>();
            JObject queryParam = queryJson.ToJObject();
            if (queryParam != null)
            {
                if (!queryParam["FullHead"].IsEmpty())
                {
                    string FullHead = queryParam["FullHead"].ToString();
                    expression = expression.And(t => t.FullHead.Contains(FullHead));
                }
                if (!queryParam["Category"].IsEmpty())
                {
                    string Category = queryParam["Category"].ToString();
                    expression = expression.And(t => t.Category == Category);
                }
            }
            
            expression = expression.And(t => t.TypeId == 2);
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 公告实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public NewsEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IEnumerable<NewsEntity> GetList(Expression<Func<NewsEntity, bool>> condition)
        {
            return this.BaseRepository().FindList(condition);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存公告表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="newsEntity">公告实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, NewsEntity newsEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                newsEntity.Modify(keyValue);
                newsEntity.TypeId = 2;
                this.BaseRepository().Update(newsEntity);
            }
            else
            {
                newsEntity.Create();
                newsEntity.TypeId = 2;
                //new Repository().Insert()
            }
        }
        #endregion
    }
}

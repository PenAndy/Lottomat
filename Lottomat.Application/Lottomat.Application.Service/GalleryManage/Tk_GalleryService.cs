using System;
using Lottomat.Application.Entity.BaseManage;
using Lottomat.Application.Entity.GalleryManage;
using Lottomat.Application.IService.BaseManage;
using Lottomat.Application.IService.GalleryManage;
using Lottomat.Data.Repository;
using Lottomat.Util.Extension;
using Lottomat.Util.WebControl;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Lottomat.Application.Service.GalleryManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创 建：开发者账号
    /// 日 期：2017-10-19 14:24
    /// 描 述：图库标题表
    /// </summary>
    public class Tk_GalleryService : RepositoryFactory<Tk_Gallery>, ITk_GalleryIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Tk_Gallery> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }

        public IEnumerable<Tk_Gallery> GetList(Expression<Func<Tk_Gallery, bool>> condition)
        {
            return this.BaseRepository().FindList(condition);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Tk_Gallery GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, Tk_Gallery entity)
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

        public IEnumerable<Tk_Gallery> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Tk_Gallery>();
            JObject queryParam = queryJson.ToJObject();
            if (queryParam != null)
            {
                if (!queryParam["ID"].IsEmpty())
                {
                    string ID = queryParam["ID"].ToString();
                    expression = expression.And(t => t.ID == ID);
                }
                if (!queryParam["GalleryNumber"].IsEmpty())
                {
                    string GalleryNumber = queryParam["GalleryNumber"].ToString();
                    expression = expression.And(t => t.GalleryNumber.Value == GalleryNumber.TryToInt32());
                }
                if (!queryParam["GalleryName"].IsEmpty())
                {
                    string GalleryName = queryParam["GalleryName"].ToString();
                    expression = expression.And(t => t.GalleryName.Contains(GalleryName));
                }
                if (!queryParam["CategoryId"].IsEmpty())
                {
                    string CategoryId = queryParam["CategoryId"].ToString();
                    expression = expression.And(t => t.CategoryId == CategoryId);
                }
                if (!queryParam["SortCode"].IsEmpty())
                {
                    string SortCode = queryParam["SortCode"].ToString();
                    expression = expression.And(t => t.SortCode.Value == SortCode.TryToInt32());
                }
                if (!queryParam["IsPicZip"].IsEmpty())
                {
                    string IsPicZip = queryParam["IsPicZip"].ToString();
                    expression = expression.And(t => IsPicZip == "1" ? true : false);
                }
                if (!queryParam["Reamrk"].IsEmpty())
                {
                    string Reamrk = queryParam["Reamrk"].ToString();
                    expression = expression.And(t => t.Reamrk.Contains(Reamrk));
                }
                if (!queryParam["SeoKey"].IsEmpty())
                {
                    string SeoKey = queryParam["SeoKey"].ToString();
                    expression = expression.And(t => t.SeoKey.Contains(SeoKey));
                }
                if (!queryParam["CreateTime"].IsEmpty())
                {
                    string CreateTime = queryParam["CreateTime"].ToString();
                    expression = expression.And(t => t.CreateTime.Value > System.DateTime.Parse(CreateTime));
                }
                if (!queryParam["HotNumber"].IsEmpty())
                {
                    string HotNumber = queryParam["HotNumber"].ToString();
                    expression = expression.And(t => t.HotNumber.Value > HotNumber.TryToInt32());
                }
                if (!queryParam["AreaCode"].IsEmpty())
                {
                    string AreaCode = queryParam["AreaCode"].ToString();
                    expression = expression.And(t => t.AreaCode == AreaCode);
                }
            }

            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// sql查询全部图库
        /// </summary>
        /// <returns></returns>
        public List<Tk_Gallery> QueryAll(int count)
        {
            string sql = string.Format(@"   select  top {0} * from Tk_Gallery  where AreaCode='A' 
          UNION 
         select  top {0} * from Tk_Gallery  where AreaCode='B' 
         UNION 
	     select  top {0} * from Tk_Gallery  where AreaCode='C'
	       order by  HotNumber desc", count);
            return this.BaseRepository().FindList(sql).ToList();
        }


        #endregion
    }
}

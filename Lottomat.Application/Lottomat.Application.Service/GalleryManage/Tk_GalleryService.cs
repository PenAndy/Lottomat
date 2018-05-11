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
    /// �� �� 1.0
    /// Copyright (c) 2016-2017
    /// �� �����������˺�
    /// �� �ڣ�2017-10-19 14:24
    /// �� ����ͼ������
    /// </summary>
    public class Tk_GalleryService : RepositoryFactory<Tk_Gallery>, ITk_GalleryIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<Tk_Gallery> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }

        public IEnumerable<Tk_Gallery> GetList(Expression<Func<Tk_Gallery, bool>> condition)
        {
            return this.BaseRepository().FindList(condition);
        }

        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public Tk_Gallery GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
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
        /// sql��ѯȫ��ͼ��
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

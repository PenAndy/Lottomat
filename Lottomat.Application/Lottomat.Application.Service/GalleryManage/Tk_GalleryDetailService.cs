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
    /// �� �ڣ�2017-10-19 14:29
    /// �� ����ͼ�������
    /// </summary>
    public class Tk_GalleryDetailService : RepositoryFactory<Tk_GalleryDetail>, ITk_GalleryDetailIService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<Tk_GalleryDetail> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
     
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public Tk_GalleryDetail GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        public IEnumerable<Tk_GalleryDetail> GetList(Expression<Func<Tk_GalleryDetail, bool>> condition)
        {
            return this.BaseRepository().FindList(condition);
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
        public void SaveForm(string keyValue, Tk_GalleryDetail entity)
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

        public IEnumerable<Tk_GalleryDetail> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Tk_GalleryDetail>();
            JObject queryParam = queryJson.ToJObject();
            if (queryParam != null)
            {
                if (!queryParam["ID"].IsEmpty())
                {
                    string ID = queryParam["ID"].ToString();
                    expression = expression.And(t => t.ID== ID);
                }
                if (!queryParam["GalleryId"].IsEmpty())
                {
                    string GalleryId = queryParam["GalleryId"].ToString();
                    expression = expression.And(t => t.GalleryId == GalleryId);
                }
                if (!queryParam["PeriodsNumber"].IsEmpty())
                {
                    string PeriodsNumber = queryParam["PeriodsNumber"].ToString();
                    expression = expression.And(t => t.PeriodsNumber == PeriodsNumber);
                }
                if (!queryParam["SortCode"].IsEmpty())
                {
                    string SortCode = queryParam["SortCode"].ToString();
                    expression = expression.And(t => t.SortCode.Value == int.Parse(SortCode));
                }
                if (!queryParam["CreateUserId"].IsEmpty())
                {
                    string CreateUserId = queryParam["CreateUserId"].ToString();
                    expression = expression.And(t => t.CreateUserId == CreateUserId);
                }
                if (!queryParam["IsDelete"].IsEmpty())
                {
                    string IsDelete = queryParam["IsDelete"].ToString();
                    expression = expression.And(t => IsDelete == "1" ? true : false);
                }


            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// ��ѯabc�����������
        /// </summary>
        /// <param name="menuname">abc</param>
        /// <returns></returns>
        public int MenuNewPeriodsNumber(string menuname)
        {
            string sql = string.Format(@" select  top 1 * from  Tk_GalleryDetail   where
    GalleryId in (select ID from Tk_Gallery where AreaCode ='{0}')  order by PeriodsNumber desc", menuname); List<Tk_GalleryDetail> list = this.BaseRepository().FindList(sql).ToList();
            if (list != null)
            {
                return int.Parse(list[0].PeriodsNumber);
            }
            return 0;

        }
        /// <summary>
        /// ��ȡ���������ں�
        /// </summary>
        /// <returns></returns>
        public int NewPeriodsNumber()
        {
            string sql = string.Format(@" select  top 1 * from Tk_GalleryDetail   where
    GalleryId in (select ID from Tk_Gallery )  order by PeriodsNumber desc");
            List<Tk_GalleryDetail> list = this.BaseRepository().FindList(sql).ToList();
            if (list != null)
            {
                return int.Parse(list[0].PeriodsNumber);
            }
            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="galleryIds"></param>
        /// <param name="periodsNumber"></param>
        /// <returns></returns>

        public List<Tk_GalleryDetail> QueryDetailByGalleryId(List<string> galleryIds, int periodsNumber)
        {
            string galls = galleryIds.ExpandAndToString("','");
            galls = "'" + galls + "'";
            string sql = string.Format(@" select * from  Tk_GalleryDetail 	 where periodsNumber={1} and isDelete=0 and galleryId in ({0})", galls, periodsNumber);
            List<Tk_GalleryDetail> list = this.BaseRepository().FindList(sql).ToList();
            return list;
            
        }

        #endregion
    }
}

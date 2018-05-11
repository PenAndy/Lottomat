using Lottomat.Application.Entity.BaseManage;
using Lottomat.Application.IService.BaseManage;
using Lottomat.Application.Service.BaseManage;
using Lottomat.Util.WebControl;
using System.Collections.Generic;
using System;
using Lottomat.Application.IService.GalleryManage;
using Lottomat.Application.Service.GalleryManage;
using Lottomat.Application.Entity.GalleryManage;
using System.Linq.Expressions;
using Lottomat.Application.Entity.PublicInfoManage;
using System.Linq;
using Newtonsoft.Json;
namespace Lottomat.Application.Busines.GalleryManage
{
    /// <summary>
    /// �� �� 1.0
    /// Copyright (c) 2016-2017
    /// �� �����������˺�
    /// �� �ڣ�2017-10-19 14:29
    /// �� ����ͼ�������
    /// </summary>
    public class Tk_GalleryDetailBLL
    {
        private ITk_GalleryDetailIService service = new Tk_GalleryDetailService();
        private Tk_GalleryBLL  tk_GalleryBLL = new Tk_GalleryBLL();
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<Tk_GalleryDetail> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        public IEnumerable<Tk_GalleryDetail> GetList(Expression<Func<Tk_GalleryDetail, bool>> condition)
        {
            return service.GetList(condition);
        }


        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public Tk_GalleryDetail GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, Tk_GalleryDetail entity)
        {
            try
            {
                
                service.SaveForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Tk_GalleryDetail> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                if (!String.IsNullOrEmpty(queryJson))
                {
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(queryJson);
                    List<Tk_Gallery> glist = new List<Tk_Gallery>();
                    if (dict.Keys.Contains("GalleryName"))
                    {
                        string gname = dict["GalleryName"];
                        List<Tk_Gallery> tlist = tk_GalleryBLL.GetList(w => w.GalleryName == gname).ToList();
                        if (tlist.Count > 0)
                        {
                            dict["GalleryId"] = tlist[0].ID;
                            queryJson = JsonConvert.SerializeObject(dict);
                        }
                    }
                }
                
                List<Tk_GalleryDetail> list= service.GetPageList(pagination, queryJson).ToList();
                List<string> GalleryIdlist = list.Select(s => s.GalleryId).ToList();
                List<Tk_Gallery> gallerylist = tk_GalleryBLL.GetList(w => GalleryIdlist.Contains(w.ID)).ToList() ;
                for (int i = 0; i < list.Count; i++)
                {
                    Tk_Gallery tmp = gallerylist.SingleOrDefault(w=>w.ID==list[i].GalleryId);
                    if (tmp!=null)
                    {
                        list[i].GalleryId = tmp.GalleryName;
                    }
                    else
                    {
                        list[i].GalleryId = "";
                    }
                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int MenuNewPeriodsNumber(string menuname)
        {
            try
            {
                return service.MenuNewPeriodsNumber(menuname);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int NewPeriodsNumber()
        {
            try
            {
                return service.NewPeriodsNumber();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<Tk_GalleryDetail> QueryDetailByGalleryId(List<string> galleryIds, int periodsNumber)
        {
            try
            {
                return service.QueryDetailByGalleryId(galleryIds, periodsNumber);
            }
            catch (Exception)
            {

                throw;
            }
        }




        #endregion
    }
}

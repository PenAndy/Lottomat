using Lottomat.Application.Entity.BaseManage;
using Lottomat.Application.IService.BaseManage;
using Lottomat.Application.Service.BaseManage;
using Lottomat.Util.WebControl;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;
using Lottomat.Application.IService.GalleryManage;
using Lottomat.Application.Entity.GalleryManage;
using Lottomat.Application.Entity.PublicInfoManage;
using Lottomat.Application.Service.GalleryManage;
using Lottomat.Application.Entity.SystemManage;
using Lottomat.Application.Busines.SystemManage;
using Newtonsoft.Json;
namespace Lottomat.Application.Busines.GalleryManage
{
    /// <summary>
    /// �� �� 1.0
    /// Copyright (c) 2016-2017
    /// �� �����������˺�
    /// �� �ڣ�2017-10-19 14:24
    /// �� ����ͼ������
    /// </summary>
    public class Tk_GalleryBLL
    {
        private ITk_GalleryIService service = new Tk_GalleryService();

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<Tk_Gallery> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        public IEnumerable<Tk_Gallery> GetList(Expression<Func<Tk_Gallery, bool>> condition)
        {
            return service.GetList(condition);
        }
        public IEnumerable<Tk_Gallery> GetPageList(Pagination pagination, string queryJson)
        {
            
            return service.GetPageList(pagination, queryJson);
        }

        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public Tk_Gallery GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, Tk_Gallery entity)
        {
            try
            {
                //entity.CategoryId=
                DataItemEntity dataItemEntit = QueryDataItemEntityByItemName(entity.AreaCode);
                if (dataItemEntit!=null)
                {
                    entity.CategoryId = dataItemEntit.ItemId;  
                }
                service.SaveForm(keyValue, entity);
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        /// <summary>
        /// ����ItemName ��ѯ��Ŀʵ��
        /// </summary>
        /// <param name="ItemName"></param>
        /// <returns></returns>
        public  DataItemEntity QueryDataItemEntityByItemName(string ItemName)
        {
            DataItemBLL dataItemBLL = new DataItemBLL();
            //JsonConvert.SerializeObject(new { ItemName = ItemName }
            return dataItemBLL.GetByItemName(ItemName);
            
        }

      
        public List<Tk_Gallery> QueryAll(int count)
        {
            try
            {
                return service.QueryAll( count);
            }
            catch (Exception)
            {

                throw;
            }
        }
      
             

        #endregion
    }
}

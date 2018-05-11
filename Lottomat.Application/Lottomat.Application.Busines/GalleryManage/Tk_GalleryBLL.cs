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
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创 建：开发者账号
    /// 日 期：2017-10-19 14:24
    /// 描 述：图库标题表
    /// </summary>
    public class Tk_GalleryBLL
    {
        private ITk_GalleryIService service = new Tk_GalleryService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
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
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Tk_Gallery GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
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
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
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
        /// 根据ItemName 查询栏目实体
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

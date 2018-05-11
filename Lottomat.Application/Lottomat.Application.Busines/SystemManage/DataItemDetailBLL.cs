using Lottomat.Application.Entity.SystemManage;
using Lottomat.Application.Entity.SystemManage.ViewModel;
using Lottomat.Application.IService.SystemManage;
using Lottomat.Application.Service.SystemManage;
using Lottomat.Cache.Factory;
using Lottomat.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using Lottomat.Utils;

namespace Lottomat.Application.Busines.SystemManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2015.11.17 9:56
    /// 描 述：数据字典明细
    /// </summary>
    public class DataItemDetailBLL
    {
        private IDataItemDetailService service = new DataItemDetailService();
        /// <summary>
        /// 缓存key
        /// </summary>
        public string cacheKey = "dataItemCache";

        #region 获取数据
        /// <summary>
        /// 明细列表
        /// </summary>
        /// <param name="itemId">分类Id</param>
        /// <returns></returns>
        public IEnumerable<DataItemDetailEntity> GetList(string itemId)
        {
            return service.GetList(itemId);
        }
        /// <summary>
        /// 明细实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DataItemDetailEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// 数据字典列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DataItemModel> GetDataItemList()
        {
            return service.GetDataItemList();
        }
        /// <summary>
        /// 根据分类ID获取实体对象
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public DataItemDetailEntity GetEntityById(string id)
        {
            DataItemDetailEntity old = service.GetEntityById(id);
            return old;
        }

        /// <summary>
        /// 根据分类ID获取实体对象
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public List<DataItemDetailEntity> GetDataItemListById(string id)
        {
            List<DataItemDetailEntity> old = service.GetDataItemListById(id).ToList();
            return old;
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 项目值不能重复
        /// </summary>
        /// <param name="itemValue">项目值</param>
        /// <param name="keyValue">主键</param>
        /// <param name="itemId">分类Id</param>
        /// <returns></returns>
        public bool ExistItemValue(string itemValue, string keyValue, string itemId)
        {
            return service.ExistItemValue(itemValue, keyValue, itemId);
        }
        /// <summary>
        /// 项目名不能重复
        /// </summary>
        /// <param name="itemName">项目名</param>
        /// <param name="keyValue">主键</param>
        /// <param name="itemId">分类Id</param>
        /// <returns></returns>
        public bool ExistItemName(string itemName, string keyValue, string itemId)
        {
            return service.ExistItemName(itemName, keyValue, itemId);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除明细
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
                CacheFactory.Cache().RemoveCache(cacheKey);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存明细表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="dataItemDetailEntity">明细实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, DataItemDetailEntity dataItemDetailEntity)
        {
            try
            {
                service.SaveForm(keyValue, dataItemDetailEntity);
                CacheFactory.Cache().RemoveCache(cacheKey);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}

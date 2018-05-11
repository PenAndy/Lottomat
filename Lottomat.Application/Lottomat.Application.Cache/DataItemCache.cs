using Lottomat.Application.Busines;
using Lottomat.Application.Busines.SystemManage;
using Lottomat.Application.Entity.SystemManage.ViewModel;
using Lottomat.Cache.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.SystemManage;

namespace Lottomat.Application.Cache
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2015.12.29 9:56
    /// 描 述：数据字典缓存
    /// </summary>
    public class DataItemCache
    {
        private DataItemDetailBLL busines = new DataItemDetailBLL();
        /// <summary>
        /// 字典分类bll
        /// </summary>
        public static DataItemBLL dataItemBll = new DataItemBLL();

        /// <summary>
        /// 数据字典列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DataItemModel> GetDataItemList()
        {
            IEnumerable<DataItemModel> cacheList = CacheFactory.Cache().GetCache<IEnumerable<DataItemModel>>(busines.cacheKey);
            if (cacheList == null)
            {
                cacheList = busines.GetDataItemList();
                CacheFactory.Cache().WriteCache(cacheList, busines.cacheKey);
            }
            return cacheList;
        }

        /// <summary>
        /// 根据编码获取项目
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public DataItemEntity GetDataItemEntityByCode(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                DataItemEntity entity = CacheFactory.Cache().GetCache<DataItemEntity>("__" + code + "__");
                if (entity == null)
                {
                    entity = dataItemBll.GetEntityByCode(code);
                    CacheFactory.Cache().WriteCache(entity, "__" + code + "__");
                }
                return entity;
            }
            return default(DataItemEntity);
        }

        /// <summary>
        /// 根据字典详细信息ID获取字典实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataItemDetailEntity GetDataItemEntityById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                DataItemDetailEntity entity = CacheFactory.Cache().GetCache<DataItemDetailEntity>("__" + id + "__");
                if (entity == null)
                {
                    entity = busines.GetEntityById(id);
                    CacheFactory.Cache().WriteCache(entity, "__" + id + "__");
                }
                return entity;
            }
            return default(DataItemDetailEntity);
        }

        /// <summary>
        /// 根据字典详细信息ID获取字典实体集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<DataItemDetailEntity> GetDataItemListById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                List<DataItemDetailEntity> entity = CacheFactory.Cache().GetCache<List<DataItemDetailEntity>>("__" + id + "__DATA__ITEM__LIST__");
                if (entity == null)
                {
                    entity = busines.GetDataItemListById(id);
                    CacheFactory.Cache().WriteCache(entity, "__" + id + "__DATA__ITEM__LIST__");
                }
                return entity;
            }
            return default(List<DataItemDetailEntity>);
        }

        /// <summary>
        /// 数据字典列表
        /// </summary>
        /// <param name="EnCode">分类代码</param>
        /// <returns></returns>
        public List<DataItemModel> GetDataItemList(string EnCode)
        {
            string[] codeArr = EnCode.Split("|".ToArray());

            return this.GetDataItemList().Where(t => codeArr.Contains(t.EnCode) && t.EnabledMark == (int)EnabledMarkEnum.Enabled).ToList();
        }
        /// <summary>
        /// 数据字典列表
        /// </summary>
        /// <param name="EnCode">分类代码</param>
        /// <param name="ItemValue">项目值</param>
        /// <returns></returns>
        public IEnumerable<DataItemModel> GetSubDataItemList(string EnCode, string ItemValue)
        {
            IEnumerable<DataItemModel> data = this.GetDataItemList().Where(t => t.EnCode == EnCode);
            IEnumerable<DataItemModel> dataItemModels = data as DataItemModel[] ?? data.ToArray();
            string itemDetailId = dataItemModels.First(t => t.ItemValue == ItemValue).ItemDetailId;
            return dataItemModels.Where(t => t.ParentId == itemDetailId);
        }
        /// <summary>
        /// 根据itemvalue 获取实体
        /// </summary>
        /// <param name="ItemValue"></param>
        /// <returns></returns>
        public DataItemModel GetItemDetailByItemValue(string ItemValue)
        {
            DataItemModel data = this.GetDataItemList().First(t => t.ItemValue == ItemValue);
            return data;
        }
        /// <summary>
        /// 项目值转换名称
        /// </summary>
        /// <param name="EnCode">分类代码</param>
        /// <param name="ItemValue">项目值</param>
        /// <returns></returns>
        public string ToItemName(string EnCode, string ItemValue)
        {
            IEnumerable<DataItemModel> data = this.GetDataItemList().Where(t => t.EnCode == EnCode);
            return data.First(t => t.ItemValue == ItemValue).ItemName;
        }
    }
}

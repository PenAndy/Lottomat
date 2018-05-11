using Lottomat.Application.Entity.SystemManage;
using Lottomat.Application.Entity.SystemManage.ViewModel;
using Lottomat.Application.IService.SystemManage;
using Lottomat.Data.Repository;
using Lottomat.Util.Extension;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lottomat.Utils;

namespace Lottomat.Application.Service.SystemManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2015.11.17 9:56
    /// 描 述：数据字典明细
    /// </summary>
    public class DataItemDetailService : RepositoryFactory<DataItemDetailEntity>, IDataItemDetailService
    {
        #region 获取数据
        /// <summary>
        /// 明细列表
        /// </summary>
        /// <param name="itemId">分类Id</param>
        /// <returns></returns>
        public IEnumerable<DataItemDetailEntity> GetList(string itemId)
        {
            return this.BaseRepository().IQueryable(t => t.ItemId == itemId).OrderBy(t => t.SortCode).ToList();
        }
        /// <summary>
        /// 明细实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DataItemDetailEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// 获取数据字典列表（给绑定下拉框提供的）
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DataItemModel> GetDataItemList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  i.ItemId ,
                                    i.ItemCode AS EnCode ,
                                    d.ItemDetailId ,
                                    d.ParentId ,
                                    d.ItemCode ,
                                    d.ItemName ,
                                    d.ItemValue ,
                                    d.QuickQuery ,
                                    d.SimpleSpelling ,
                                    d.IsDefault ,
                                    d.SortCode ,
                                    d.EnabledMark,
                                    d.IsRecommend,
                                    d.IsHot,
                                    d.IsShowHomePage 
                            FROM    Base_DataItemDetail d
                                    LEFT JOIN Base_DataItem i ON i.ItemId = d.ItemId
                            WHERE   1 = 1
                                    AND d.EnabledMark = 1
                                    AND d.DeleteMark = 0
                            ORDER BY d.SortCode ASC");
            return new RepositoryFactory().BaseRepository().FindList<DataItemModel>(strSql.ToString());
        }

        /// <summary>
        /// 根据分类ID获取实体对象
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public DataItemDetailEntity GetEntityById(string id)
        {
            var expression = LinqExtensions.True<DataItemDetailEntity>();
            if (!string.IsNullOrEmpty(id))
            {
                expression = expression.And(t => t.ItemDetailId.Equals(id));
            }
            return new RepositoryFactory().BaseRepository().FindEntity(expression);
        }

        /// <summary>
        /// 根据分类ID获取实体对象集合
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public IEnumerable<DataItemDetailEntity> GetDataItemListById(string id)
        {
            var expression = LinqExtensions.True<DataItemDetailEntity>();
            if (!string.IsNullOrEmpty(id))
            {
                expression = expression.And(t => t.ItemId.Equals(id));
            }
            return new RepositoryFactory().BaseRepository().FindList<DataItemDetailEntity>(expression);
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
            var expression = LinqExtensions.True<DataItemDetailEntity>();
            expression = expression.And(t => t.ItemValue == itemValue).And(t => t.ItemId == itemId);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.ItemDetailId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
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
            var expression = LinqExtensions.True<DataItemDetailEntity>();
            expression = expression.And(t => t.ItemName == itemName).And(t => t.ItemId == itemId);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.ItemDetailId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除明细
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存明细表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="dataItemDetailEntity">明细实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, DataItemDetailEntity dataItemDetailEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                dataItemDetailEntity.Modify(keyValue);
                this.BaseRepository().Update(dataItemDetailEntity);
            }
            else
            {
                dataItemDetailEntity.Create();
                if (string.IsNullOrEmpty(dataItemDetailEntity.SimpleSpelling))
                    dataItemDetailEntity.SimpleSpelling = StringHelper.PinYin(dataItemDetailEntity.ItemName);
                this.BaseRepository().Insert(dataItemDetailEntity);
            }
        }
        #endregion
    }
}

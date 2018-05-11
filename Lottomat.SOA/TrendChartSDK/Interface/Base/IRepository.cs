using System.Collections.Generic;
using TrendChartSDK.Entity.Base;
using System.ServiceModel;

namespace TrendChartSDK.Interface.Base
{
    /// <summary>
    /// 基础数据操作接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    [ServiceContract]
    public interface IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        /// <summary>
        /// 数据新增操作
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        [OperationContract]
        bool Save(TEntity entity);
        /// <summary>
        /// 数据更新操作
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        [OperationContract]
        bool Update(TEntity entity);
        /// <summary>
        /// 数据删除操作
        /// </summary>
        /// <param name="id"></param>
        bool Delete(int id);
        /// <summary>
        /// 数据获取
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        TEntity GetEntity<TKey>(TKey key);
        /// <summary>
        /// 数据列表
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IList<TEntity> GetList();
        /// <summary>
        /// 数据查询
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        IList<TEntity> GetList(TEntity entity);
        /// <summary>
        /// 翻页查询数据列表
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList<TEntity> ToPaging(TEntity entity, int pageSize, int pageIndex, out int recordCount);
    }
}
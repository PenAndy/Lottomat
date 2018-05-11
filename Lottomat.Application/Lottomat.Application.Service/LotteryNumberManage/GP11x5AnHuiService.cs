using Lottomat.Application.Entity.LotteryNumberManage;
using Lottomat.Application.IService.LotteryNumberManage;
using Lottomat.Data.Repository;
using Lottomat.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using Lottomat.Application.Code;
using Lottomat.Util.Extension;
using Newtonsoft.Json.Linq;

namespace Lottomat.Application.Service.LotteryNumberManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-12-05 10:15
    /// 描 述：高频彩-安徽11选5
    /// </summary>
    public class GP11x5AnHuiService : RepositoryFactory<GP11x5AnHuiEntity>, IGP11x5AnHuiService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<GP11x5AnHuiEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<GP11x5AnHuiEntity>();
            JObject queryParam = queryJson.ToJObject();
            if (queryParam != null)
            {
                if (!queryParam["Term"].IsEmpty())
                {
                    int Term = queryParam["Term"].TryToInt32();
                    expression = expression.And(t => t.Term == Term);
                }
            }

            return this.BaseRepository(DatabaseLinksEnum.LotteryNumber).FindList(expression, pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<GP11x5AnHuiEntity> GetList(string queryJson)
        {
            return this.BaseRepository(DatabaseLinksEnum.LotteryNumber).IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public GP11x5AnHuiEntity GetEntity(string keyValue)
        {
            return this.BaseRepository(DatabaseLinksEnum.LotteryNumber).FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository(DatabaseLinksEnum.LotteryNumber).Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, GP11x5AnHuiEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository(DatabaseLinksEnum.LotteryNumber).Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository(DatabaseLinksEnum.LotteryNumber).Insert(entity);
            }
        }
        #endregion
    }
}

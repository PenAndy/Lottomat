using Lottomat.Application.Entity.SystemManage;
using Lottomat.Application.IService.SystemManage;
using Lottomat.Data.Repository;
using Lottomat.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.LotteryNumberManage;
using Lottomat.Util.Extension;
using Lottomat.Utils.Date;
using Newtonsoft.Json.Linq;

namespace Lottomat.Application.Service.SystemManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-12-18 10:55
    /// 描 述：意见反馈
    /// </summary>
    public class FeedbackService : RepositoryFactory<FeedbackEntity>, IFeedbackService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<FeedbackEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<FeedbackEntity>();
            JObject queryParam = queryJson.ToJObject();
            if (queryParam != null)
            {
                if (!queryParam["NickName"].IsEmpty())
                {
                    string NickName = queryParam["NickName"].ToString();
                    expression = expression.And(t => t.NickName.Contains(NickName));
                }
                if (!queryParam["Contact"].IsEmpty())
                {
                    string Contact = queryParam["Contact"].ToString();
                    expression = expression.And(t => t.Contact.Contains(Contact));
                }
            }

            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<FeedbackEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FeedbackEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public int SaveForm(string keyValue, FeedbackEntity entity, string which)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (which == "1")
                {
                    entity.IsReply = true;
                    entity.ReplyTime = DateTimeHelper.Now;
                    entity.ReplyUserName = entity.ReplyUserName == ""
                        ? OperatorProvider.Provider.Current().UserName
                        : entity.ReplyUserName;
                }
                entity.Modify(keyValue);
                return this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                return this.BaseRepository().Insert(entity);
            }
        }
        #endregion
    }
}

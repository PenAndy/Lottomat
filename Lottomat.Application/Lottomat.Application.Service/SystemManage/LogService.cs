using Lottomat.Application.Entity.SystemManage;
using Lottomat.Application.IService.SystemManage;
using Lottomat.Data.Repository;
using Lottomat.Util;
using Lottomat.Util.Extension;
using Lottomat.Util.WebControl;
using System;
using System.Collections.Generic;
using Lottomat.Application.Code;
using Lottomat.Utils.Date;
using Lottomat.Utils.Web;
using Newtonsoft.Json.Linq;

namespace Lottomat.Application.Service.SystemManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2016.1.8 9:56
    /// 描 述：系统日志
    /// </summary>
    public class LogService : RepositoryFactory<LogEntity>, ILogService
    {
        #region 获取数据
        /// <summary>
        /// 日志列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<LogEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<LogEntity>();
            JObject queryParam = queryJson.ToJObject();
            if (queryParam != null)
            {
                //日志分类
                if (!queryParam["Category"].IsEmpty())
                {
                    int categoryId = queryParam["CategoryId"].ToInt();
                    expression = expression.And(t => t.CategoryId == categoryId);
                }
                //操作时间
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    DateTime startTime = queryParam["StartTime"].ToDate();
                    DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                    expression = expression.And(t => t.OperateTime >= startTime && t.OperateTime <= endTime);
                }
                //操作用户Id
                if (!queryParam["OperateUserId"].IsEmpty())
                {
                    string OperateUserId = queryParam["OperateUserId"].ToString();
                    expression = expression.And(t => t.OperateUserId == OperateUserId);
                }
                //操作用户账户
                if (!queryParam["OperateAccount"].IsEmpty())
                {
                    string OperateAccount = queryParam["OperateAccount"].ToString();
                    expression = expression.And(t => t.OperateAccount.Contains(OperateAccount));
                }
                //操作类型
                if (!queryParam["OperateType"].IsEmpty())
                {
                    string operateType = queryParam["OperateType"].ToString();
                    expression = expression.And(t => t.OperateType == operateType);
                }
                //功能模块
                if (!queryParam["Module"].IsEmpty())
                {
                    string module = queryParam["Module"].ToString();
                    expression = expression.And(t => t.Module.Contains(module));
                }
            }

            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 日志实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public LogEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 清空日志
        /// </summary>
        /// <param name="categoryId">日志分类Id</param>
        /// <param name="keepTime">保留时间段内</param>
        public void RemoveLog(int categoryId, string keepTime)
        {
            DateTime operateTime = DateTimeHelper.Now;
            if (keepTime == "7")//保留近一周
            {
                operateTime = DateTimeHelper.Now.AddDays(-7);
            }
            else if (keepTime == "1")//保留近一个月
            {
                operateTime = DateTimeHelper.Now.AddMonths(-1);
            }
            else if (keepTime == "3")//保留近三个月
            {
                operateTime = DateTimeHelper.Now.AddMonths(-3);
            }
            var expression = LinqExtensions.True<LogEntity>();
            expression = expression.And(t => t.OperateTime <= operateTime);
            expression = expression.And(t => t.CategoryId == categoryId);
            this.BaseRepository().Delete(expression);
        }
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="logEntity">对象</param>
        public void WriteLog(LogEntity logEntity)
        {
            logEntity.LogId = CommonHelper.GetGuid().ToString();
            logEntity.OperateTime = DateTimeHelper.Now;
            logEntity.DeleteMark = (int)DeleteMarkEnum.NotDelete;
            logEntity.EnabledMark = (int)EnabledMarkEnum.Enabled;
            logEntity.IPAddress = NetHelper.Ip;
            logEntity.Host = NetHelper.Host;
            logEntity.Browser = NetHelper.Browser;
            this.BaseRepository().Insert(logEntity);
        }
        #endregion
    }
}

﻿using Lottomat.Application.Entity.SystemManage;
using Lottomat.Application.IService.SystemManage;
using Lottomat.Application.Service.SystemManage;
using Lottomat.Util.WebControl;
using System;
using System.Collections.Generic;
using Lottomat.Application.Code;
using Lottomat.Util;
using Lottomat.Util.Extension;

namespace Lottomat.Application.Busines.SystemManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2016.1.8 9:56
    /// 描 述：系统日志
    /// </summary>
    public static class LogBLL
    {
        private static ILogService service = new LogService();

        #region 获取数据
        /// <summary>
        /// 日志列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public static IEnumerable<LogEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 日志实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public static LogEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 清空日志
        /// </summary>
        /// <param name="categoryId">日志分类Id</param>
        /// <param name="keepTime">保留时间段内</param>
        public static void RemoveLog(int categoryId, string keepTime)
        {
            try
            {
                service.RemoveLog(categoryId, keepTime);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="logEntity">对象</param>
        public static void WriteLog(this LogEntity logEntity)
        {
            try
            {
                service.WriteLog(logEntity);
            }
            catch (Exception)
            {
                LogEntity log = new LogEntity
                {
                    CategoryId = (int)CategoryType.Exception,
                    OperateTypeId = ((int)OperationType.Exception).ToString(),
                    OperateType = OperationType.Exception.GetEnumDescription(),
                    OperateAccount = OperatorProvider.Provider.Current().Account,
                    OperateUserId = OperatorProvider.Provider.Current().UserId,
                    ExecuteResult = 1,
                    ExecuteResultJson = "写日志",
                    Module = ConfigHelper.GetValue("SoftName")
                };

                service.WriteLog(log);
            }
        }
        #endregion
    }
}

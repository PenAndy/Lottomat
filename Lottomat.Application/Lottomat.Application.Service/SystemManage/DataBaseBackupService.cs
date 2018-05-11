﻿using Lottomat.Application.Entity.SystemManage;
using Lottomat.Application.IService.SystemManage;
using Lottomat.Data.Repository;
using Lottomat.Util.Extension;
using Lottomat.Util;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Lottomat.Application.Service.SystemManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2015.11.25 11:02
    /// 描 述：数据库备份
    /// </summary>
    public class DataBaseBackupService : RepositoryFactory<DataBaseBackupEntity>, IDataBaseBackupService
    {
        #region 获取数据
        /// <summary>
        /// 库备份列表
        /// </summary>
        /// <param name="dataBaseLinkId">连接库Id</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<DataBaseBackupEntity> GetList(string dataBaseLinkId, string queryJson)
        {
            var expression = LinqExtensions.True<DataBaseBackupEntity>();
            if (!string.IsNullOrEmpty(dataBaseLinkId))
            {
                expression = expression.And(t => t.DatabaseLinkId == dataBaseLinkId);
            }
            JObject queryParam = queryJson.ToJObject();
            if (queryParam != null)
            {
                //查询条件
                if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
                {
                    string condition = queryParam["condition"].ToString();
                    string keyword = queryParam["keyword"].ToString();
                    switch (condition)
                    {
                        case "EnCode":            //计划编号
                            expression = expression.And(t => t.EnCode.Contains(keyword));
                            break;
                        case "FullName":          //计划名称
                            expression = expression.And(t => t.FullName.Contains(keyword));
                            break;
                        default:
                            break;
                    }
                }
            }
            
            return this.BaseRepository().IQueryable(expression).OrderByDescending(t => t.CreateDate).ToList();
        }
        /// <summary>
        /// 库备份文件路径列表
        /// </summary>
        /// <param name="databaseBackupId">计划Id</param>
        /// <returns></returns>
        public IEnumerable<DataBaseBackupEntity> GetPathList(string databaseBackupId)
        {
            return this.BaseRepository().IQueryable(t => t.ParentId == databaseBackupId).OrderByDescending(t => t.CreateDate).ToList();
        }
        /// <summary>
        /// 库备份实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DataBaseBackupEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除库备份
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存库备份表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="dataBaseBackupEntity">库备份实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, DataBaseBackupEntity dataBaseBackupEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                dataBaseBackupEntity.Modify(keyValue);
                this.BaseRepository().Update(dataBaseBackupEntity);
            }
            else
            {
                dataBaseBackupEntity.Create();
                this.BaseRepository().Insert(dataBaseBackupEntity);
            }
        }
        #endregion
    }
}

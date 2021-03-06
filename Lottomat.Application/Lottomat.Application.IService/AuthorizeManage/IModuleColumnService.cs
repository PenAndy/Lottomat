﻿using Lottomat.Application.Entity.AuthorizeManage;
using System.Collections.Generic;

namespace Lottomat.Application.IService.AuthorizeManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2015.10.29 15:13
    /// 描 述：系统视图
    /// </summary>
    public interface IModuleColumnService
    {
        #region 获取数据
        /// <summary>
        /// 视图列表
        /// </summary>
        /// <returns></returns>
        List<ModuleColumnEntity> GetList();
        /// <summary>
        /// 视图列表
        /// </summary>
        /// <param name="moduleId">功能Id</param>
        /// <returns></returns>
        List<ModuleColumnEntity> GetList(string moduleId);
        /// <summary>
        /// 视图实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        ModuleColumnEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 添加视图
        /// </summary>
        /// <param name="moduleButtonEntity">视图实体</param>
        void AddEntity(ModuleColumnEntity moduleColumnEntity);
        #endregion
    }
}

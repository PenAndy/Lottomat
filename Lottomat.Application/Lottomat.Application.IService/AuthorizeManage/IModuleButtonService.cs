﻿using Lottomat.Application.Entity.AuthorizeManage;
using System.Collections.Generic;

namespace Lottomat.Application.IService.AuthorizeManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2015.08.01 14:00
    /// 描 述：系统按钮
    /// </summary>
    public interface IModuleButtonService
    {
        #region 获取数据
        /// <summary>
        /// 按钮列表
        /// </summary>
        /// <returns></returns>
        List<ModuleButtonEntity> GetList();
        /// <summary>
        /// 按钮列表
        /// </summary>
        /// <param name="moduleId">功能Id</param>
        /// <returns></returns>
        List<ModuleButtonEntity> GetList(string moduleId);
        /// <summary>
        /// 按钮实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        ModuleButtonEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="moduleButtonEntity">按钮实体</param>
        void AddEntity(ModuleButtonEntity moduleButtonEntity);
        #endregion
    }
}

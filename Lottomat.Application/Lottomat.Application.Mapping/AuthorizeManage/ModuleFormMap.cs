﻿using Lottomat.Application.Entity.AuthorizeManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.AuthorizeManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2016.04.14 09:16
    /// 描 述：系统表单
    /// </summary>
    public class ModuleFormMap : EntityTypeConfiguration<ModuleFormEntity>
    {
        public ModuleFormMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_ModuleForm");
            //主键
            this.HasKey(t => t.FormId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

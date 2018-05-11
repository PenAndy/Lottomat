using Lottomat.Application.Entity.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.SystemManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2015.11.25 11:02
    /// 描 述：数据库备份
    /// </summary>
    public class DataBaseBackupMap : EntityTypeConfiguration<DataBaseBackupEntity>
    {
        public DataBaseBackupMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_DatabaseBackup");
            //主键
            this.HasKey(t => t.DatabaseBackupId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

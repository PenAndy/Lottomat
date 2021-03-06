using Lottomat.Application.Entity.PublicInfoManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.PublicInfoManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：XXX
    /// 日 期：2016-04-25 10:45
    /// 描 述：日程管理
    /// </summary>
    public class ScheduleMap : EntityTypeConfiguration<ScheduleEntity>
    {
        public ScheduleMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_Schedule");
            //主键
            this.HasKey(t => t.ScheduleId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

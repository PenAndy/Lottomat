using Lottomat.Application.Entity.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace Lottomat.Application.Mapping.SystemManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2015.11.17 9:56
    /// 描 述：数据字典明细
    /// </summary>
    public class DataItemDetailMap : EntityTypeConfiguration<DataItemDetailEntity>
    {
        public DataItemDetailMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_DataItemDetail");
            //主键
            this.HasKey(t => t.ItemDetailId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

using System;
using TrendChartSDK.Entity.Base;

namespace TrendChartSDK.Entity.Template
{
    /// <summary>
    /// 模板实体类
    /// </summary>
    public class TemplateInfo : BaseEntity
    {
        /// <summary>
        /// 模板名称
        /// </summary>		
        public string Name { get; set; }
        /// <summary>
        /// 模板路径
        /// </summary>		
        public string FilePath { get; set; }
        /// <summary>
        /// 模板类型
        /// </summary>
        public int TemplateType { get; set; }
        /// <summary>
        /// 模板解释类型
        /// </summary>
        public TemplateMethodType MethodType { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Descript { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateDate { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }
    }

    /// <summary>
    /// 模板解释类型 纯文本型、替换型、解释型
    /// </summary>
    public enum TemplateMethodType
    {
        /// <summary>
        /// 纯文本型
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 替换型
        /// </summary>
        Replace = 1,
        /// <summary>
        /// 解释型
        /// </summary>
        Explanation = 2
    }
}
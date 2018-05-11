using System;
using TrendChartSDK.Entity.TrendChart;
using TrendChartSDK.Entity.TrendMiss;

namespace TrendChartSDK.TrendMiss
{
    /// <summary>
    /// 走势图项值计算函数描述类
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class MissFunction : Attribute
    {
        /// <summary>
        /// 项值计算方法描述
        /// </summary>
        public string Name;
        /// <summary>
        /// 项值计算方法对应枚举类型
        /// </summary>
        public MissItemType ItemType;
        /// <summary>
        /// 处理类型
        /// </summary>
        public ChartItemClassName ClassName;

        public MissFunction(string name, MissItemType itemType, ChartItemClassName className)
        {
            this.Name = name;
            this.ItemType = itemType;
            this.ClassName = className;
        }
    }
}

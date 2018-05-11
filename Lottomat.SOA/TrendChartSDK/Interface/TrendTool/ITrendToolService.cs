using System.Collections.Generic;
using TrendChartSDK.Entity.TrendTool;
using TrendChartSDK.Interface.Base;

namespace TrendChartSDK.Interface.TrendTool
{
    /// <summary>
    /// 工具
    /// </summary>
    public interface ITrendToolService : IRepository<TrendToolInfo>
    {
        IList<TrendToolInfo> ChildToList(TrendToolInfo entity);
    }
}
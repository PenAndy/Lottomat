using System;
using System.ComponentModel;
using TrendChartSDK.Entity.Base;

namespace TrendChartSDK.Entity.TrendTool
{
    /// <summary>
    /// 工具
    /// </summary>
    public class TrendToolInfo : BaseEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string ToolName { set; get; }
        /// <summary>
        /// 项类型,是判断类型,取值类型,空
        /// </summary>
        //public ToolType ToolType { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { set; get; }
        /// <summary>
        /// 工具页面ID
        /// </summary>
        public int PageID { set; get; }
        /// <summary>
        /// 父节点ID
        /// </summary>
        public int ParentId { set; get; }
        /// <summary>
        /// html元素值,如果多个请用英文状态下的逗号隔开，例：0,1,2,3,4,5,6,7,8,9
        /// </summary>
        public string ItemValue { set; get; }
        /// <summary>
        /// 过滤项类型,0表示缺省无类型,1是包含不包含过滤,2值类型
        /// </summary>
        public FilterCodeType FilterCodeType { set; get; }
        /// <summary>
        /// 总数
        /// </summary>
        public int ItemCount { set; get; }
        /// <summary>
        /// 号码开始位置,1为开始位置
        /// </summary>
        public int IndexStart { set; get; }
        /// <summary>
        /// 号码结束位置,1为开始位置
        /// </summary>
        public int IndexEnd { set; get; }
        /// <summary>
        /// HTML模版信息
        /// </summary>
        public string TemplateHtml { get; set; }
        /// <summary>
        /// 生成的html内容
        /// </summary>
        public string Html { set; get; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? ToolOrder { set; get; }
        /// <summary>
        /// 有效状态
        /// </summary>
        public FilterStatus Status { set; get; }
        /// <summary>
        /// 枚举方法
        /// </summary>
        public FilterType FilterFunEnum { set; get; }
        /// <summary>
        /// 是否保存用户过滤数据
        /// </summary>
        public bool IsSaveData { set; get; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { set; get; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastUpdate { set; get; }
    }
    
    /// <summary>
    /// 过滤类型
    /// </summary>
    public enum FilterCodeType
    {
        /// <summary>
        /// 无类型
        /// </summary>
        None = 0,
        /// <summary>
        ///判断取值（包含选中，排除选中）
        /// </summary>
        Include = 1,
        /// <summary>
        /// 项取值
        /// </summary>
        Content = 2,
    }
    
    /// <summary>
    /// 工具枚举
    /// </summary>
    public enum FilterType
    {
        /// <summary>
        /// 缺省
        /// </summary>
        None = 0,
        /// <summary>
        /// 3D-杀号过滤(通用)
        /// </summary>
        Filter_ShaHao = 3,

        /// <summary>
        /// 3D-胆码过滤(不通用)
        /// </summary>
        Filter_DanMa = 4,

        /// <summary>
        /// 3D-二码和过滤(通用)
        /// </summary>
        Filter_ErMaHe = 5,

        /// <summary>
        /// 3D-二码差过滤(通用)
        /// </summary>
        Filter_ErMaCha = 6,

        /// <summary>
        /// 3D-二码过滤(通用)
        /// </summary>
        Filter_ErMa = 7,

        /// <summary>
        /// 3D-012路过滤(通用)
        /// </summary>
        Filter_Filter012 = 8,

        /// <summary>
        /// 3D-和值过滤(通用)
        /// </summary>
        Filter_HeZhi = 9,

        /// <summary>
        /// 3D-和尾过滤(通用)
        /// </summary>
        Filter_HeWei = 10,

        /// <summary>
        /// 3D-跨度过滤(通用)
        /// </summary>
        Filter_KuaDu = 11,

        /// <summary>
        /// 3D-大中小过滤(通用)
        /// </summary>
        Filter_DaZhongXiao = 12,

        /// <summary>
        /// 3D-大小过滤(通用)
        /// </summary>
        Filter_DaXiao = 13,

        /// <summary>
        /// 3D-奇偶过滤(通用)
        /// </summary>
        Filter_JiOu = 14,

        /// <summary>
        /// 3D-质合过滤(通用)
        /// </summary>
        Filter_ZhiHe = 15,

        /// <summary>
        /// 3D-顺子过滤(不通用)
        /// </summary>
        Filter_ShunZi = 16,

        /// <summary>
        /// 3D-组合选项(不通用)
        /// </summary>
        Filter_ZuHe = 17,
        /// <summary>
        /// 首位奇偶(通用)
        /// </summary>
        Filter_FirstJiOu = 18,
        /// <summary>
        /// 末位奇偶(通用)
        /// </summary>
        Filter_LastJiOu = 19,
        /// <summary>
        /// 奇偶比例过滤(通用)
        /// </summary>
        Filter_ProportionOfJiOu = 20,
        /// <summary>
        /// 大小比例过滤(不通用，临界值无法确定)
        /// </summary>
        Filter_ProportionOfDaXiao = 21,
        /// <summary>
        /// 质和比例过滤(通用)
        /// </summary>
        Filter_ProportionOfZhiHe = 22,
        /// <summary>
        /// 连号组数(通用)
        /// </summary>
        Filter_LianHaoCount = 23,
        /// <summary>
        /// 每注中尾数不相同的投注号码
        /// </summary>
        Filter_TzCount = 24,
        /// <summary>
        /// AC值过滤（通用）
        /// </summary>
        Filter_Ac = 25,
        /// <summary>
        /// 和值过滤(区间段，通用)
        /// </summary>
        Filter_hzSpan = 26,
        /// <summary>
        /// 3D不定位杀号(不通用)
        /// </summary>
        Filter_NoFixShaHao = 27,
        /// <summary>
        /// 0路组合式形态过滤
        /// </summary>
        Filte_P5ZhuHe0 = 28,
        /// <summary>
        /// 双色球中六保五
        /// </summary>
        Filter_ZlBw = 29,
        /// <summary>
        /// 012路组选过滤
        /// </summary>
        Filter_ZuXuan_Filter012 = 30,
        /// <summary>
        /// 组选-质合过滤
        /// </summary>
        Filter_ZuXuan_ZhiHe = 31,
        /// <summary>
        /// 组选-奇偶过滤
        /// </summary>
        Filter_ZuXuan_JiOu = 32,
        /// <summary>
        /// 组选-大小过滤
        /// </summary>
        Filter_ZuXuan_DaXiao = 33,
        /// <summary>
        /// 1路组合式形态过滤
        /// </summary>
        Filte_P5ZhuHe1 = 34,
        /// <summary>
        /// 2路组合式形态过滤
        /// </summary>
        Filte_P5ZhuHe2 = 35,
        /// <summary>
        /// 和值012路(通用)
        /// </summary>
        Filter_HeZhi012 = 51,
        ///// <summary>
        ///// 奇偶数比(数字类型的比例，通用)
        ///// </summary>
        //Filter_NumberJiOu = 52,
        ///// <summary>
        ///// 质合比(数字类型的比例，通用)
        ///// </summary>
        //Filter_NumberZhiHe = 54,
        /// <summary>
        /// 大乐透5保4旋转矩阵
        /// </summary>
        Filter_Dlt5b4 = 55,
        /// <summary>
        /// 七乐彩中7保6
        /// </summary>
        Filter_ZqBl = 56,
    }

    /// <summary>
    /// 工具状态
    /// </summary>
    public enum FilterStatus
    {
        /// <summary>
        ///  缺省
        /// </summary>
        [Description("缺省")]
        None = 0,
        /// <summary>
        ///  失效状态
        /// </summary>
        [Description("失效状态")]
        Cancel = -1,
        /// <summary>
        /// 正常状态
        /// </summary>
        [Description("正常状态")]
        Default = 1,
        /// <summary>
        /// 测试状态
        /// </summary>
        [Description("测试状态")]
        Test = 2

    }
}
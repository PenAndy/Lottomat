using TrendChartSDK.Entity.Base;
using TrendChartSDK.Entity.TrendChartData;

namespace TrendChartSDK.Entity.TrendChart
{
    /// <summary>
    /// 走势图每项具体配置信息
    /// </summary>
    public class TrendChartItemInfo : BaseEntity
    {
        /// <summary>
        /// 彩种
        /// </summary>
        public int Cid { get; set; }
        /// <summary>
        /// 走势图ID
        /// </summary>
        public int ChartId { get; set; }
        /// <summary>
        /// 走势图类型
        /// </summary>
        public TrendChartType ChartType { get; set; }
        /// <summary>
        /// 项类类型
        /// </summary>
        public ChartItemClassName ClassName { get; set; }
        /// <summary>
        /// 自定义项名称
        /// </summary>
        public string ChartItemName { get; set; }
        /// <summary>
        /// 列最小周期
        /// 即该项所有列周期内出现次数最小的列的出现次数
        /// </summary>
        public int Cycle { get; set; }
        /// <summary>
        /// 项最小值
        /// </summary>
        public int ItemMinValue { get; set; }
        /// <summary>
        /// 项最大值
        /// </summary>
        public int ItemMaxValue { get; set; }
        /// <summary>
        /// 中间值以区别大小
        /// 大于等于splitNumber算大数
        /// </summary>
        public int SplitNumberOfDX { get; set; }
        /// <summary>
        /// 项中列的个数
        /// </summary>
        public int ItemCount { get; set; }
        /// <summary>
        /// 项字符串数据
        /// </summary>
        public string[] ItemString { get; set; }
        /// <summary>
        /// 起始索引
        /// 计算项值时的起始索引号
        /// 特殊值-1表示IndexStart无效
        /// 注：单值项时仅IndexStart有效
        /// </summary>
        public int IndexStart { get; set; }
        /// <summary>
        /// 结束索引
        /// 计算项值时的结束索引号
        /// 特殊值-1表示IndexEnd无效
        /// </summary>
        public int IndexEnd { get; set; }
        /// <summary>
        /// 是否画连接线
        /// </summary>
        public bool DrawLine { get; set; }
        /// <summary>
        /// 项值函数类型(决定项值的计算方式)
        /// </summary>
        public ChartItemType FuntionType { get; set; }
        /// <summary>
        /// CSS配置ID
        /// </summary>
        public int ChartCssId { get; set; }
        /// <summary>
        /// 排序序号
        /// </summary>
        public int OrderBy { get; set; }
    }
    
    /// <summary>
    /// 项值函数类型(决定项值的计算方式)
    /// </summary>
    public enum ChartItemType
    {
        /// <summary>
        /// 单列期数项
        /// </summary>
        Term_TermItem = 1,
        /// <summary>
        /// 012值(单值)
        /// </summary>
        SingleCell_012StatusItem = 2,
        /// <summary>
        /// 大小状态项(单值)
        /// </summary>
        SingleValue_DaXiaoStatusItem = 3,
        /// <summary>
        /// 和值尾数项(单值)
        /// </summary>
        SingleValue_HeWeiItem = 4,
        /// <summary>
        /// 奇偶状态项(单值)
        /// </summary>
        SingleValue_JiOuStatusItem = 5,
        /// <summary>
        /// 单个号码012形态项(单值)
        /// </summary>
        SingleValue_Number012StatusItem = 6,
        /// <summary>
        /// 单个号码数字项
        /// </summary>
        SingleValue_NumberItem = 7,
        /// <summary>
        /// 多于两个号码跨度项
        /// </summary>
        SingleValue_SpanItem = 8,
        /// <summary>
        /// 两个号码跨度项
        /// </summary>
        SingleValue_SpanNumberItem = 9,
        /// <summary>
        /// 和值项
        /// </summary>
        SingleValue_SumItem = 10,
        /// <summary>
        /// 质合状态项
        /// </summary>
        SingleValue_ZhiHeStatusItem = 11,
        /// <summary>
        /// 组三组六项
        /// </summary>
        SingleValue_ZuHeStatusItem = 12,
        /// <summary>
        /// 单列和尾项
        /// </summary>
        SingleCell_HeWeiItem = 13,
        /// <summary>
        /// 单列开奖号码展示项
        /// </summary>
        SingleCell_OpenCodeItem = 14,
        /// <summary>
        /// 单列012比例项
        /// </summary>
        SingleCell_ProportionOf012Item = 15,
        /// <summary>
        /// 单列大小比例项
        /// </summary>
        SingleCell_ProportionOfDxItem = 16,
        /// <summary>
        /// 单列奇偶比例项
        /// </summary>
        SingleCell_ProportionOfJoItem = 17,
        /// <summary>
        /// 单列质合比例项
        /// </summary>
        SingleCell_ProportionOfZhItem = 18,
        /// <summary>
        /// 单列跨度值项
        /// </summary>
        SingleCell_SpanItem = 19,
        /// <summary>
        /// 单列和值项
        /// </summary>
        SingleCell_SumItem = 20,
        /// <summary>
        /// 多值开奖号码展示项
        /// </summary>
        MultiValue_OpenCodeItem = 21,
        /// <summary>
        /// 单列试机号项
        /// </summary>
        SingleCell_ShiJiHao = 22,
        /// <summary>
        /// 和值奇偶状态
        /// </summary>
        SingleValue_HzJoStatusItem = 23,
        /// <summary>
        /// 和值大小状态
        /// </summary>
        SingleValue_HzDxStatusItem = 24,
        /// <summary>
        /// （多值）大小形态
        /// </summary>
        SingleValue_DxStatusItem = 25,
        /// <summary>
        /// (多值)奇偶形态
        /// </summary>
        SingleValue_JoStatusItem = 26,
        /// <summary>
        /// 单值试机号
        /// </summary>
        SingleValue_ShiJiHao = 27,
        /// <summary>
        /// 单列试机号和值项
        /// </summary>
        SingleCell_ShiJiHaoHzItem = 28,
        /// <summary>
        /// 单列试机号跨度项
        /// </summary>
        SingleCell_ShiJiHaoSpanItem = 29,
        /// <summary>
        /// 单列试机号奇偶比例
        /// </summary>
        SingleCell_ProportionOfShiJiHaoJoItem = 30,
        /// <summary>
        /// 单列试机号大小比例
        /// </summary>
        SingleCell_ProportionOfShiJiHaoDxItem = 31,
        /// <summary>
        /// 单列试机号类型项
        /// </summary>
        SingleValue_ShiJiHaoTypeItem = 32,
        /// <summary>
        /// 组三形态
        /// </summary>
        SingleValue_ZsStatusItem = 33,
        /// <summary>
        /// 单列组三遗漏项
        /// </summary>
        SingleCell_ZsMissItem = 34,
        /// <summary>
        /// 组三号码
        /// </summary>
        SingleCell_ZsHaoMaItem = 35,
        /// <summary>
        /// 单值AC值
        /// </summary>
        SingleCell_Ac = 36,
        /// <summary>
        /// 三区比(只适用于双色球)
        /// </summary>
        SingleCell_SanQu = 38,
        /// <summary>
        /// 单列ac值奇偶状态
        /// </summary>
        SingleCell_AcJiOu = 39,
        /// <summary>
        /// 单列ac值质合状态
        /// </summary>
        SingleCell_AcZhiHe = 40,
        /// <summary>
        /// 单列ac值012路
        /// </summary>
        SingleCell_Ac012Lu = 41,
        /// <summary>
        /// 单个号码的区间分布
        /// </summary>
        SingleValue_QuJianFenBu = 42,
        /// <summary>
        /// 和尾奇偶状态
        /// </summary>
        SingleValue_HeWeiJiOu = 43,
        /// <summary>
        /// 单列重号
        /// </summary>
        SingleCell_RepeatedNumber = 50,
        /// <summary>
        /// 单列连号
        /// </summary>
        SingleCell_LinkNumber = 51,
        /// <summary>
        /// 和值(区间)分布
        /// </summary>
        SingleValue_SumItemGroup = 52,
        /// <summary>
        /// 组三奇偶形态
        /// </summary>
        SingleValue_ZsJoStatusItem = 60,
        /// <summary>
        /// 组三大小形态
        /// </summary> 
        SingleValue_ZsDxStatusItem = 61,
        /// <summary>
        /// 组三012形态
        /// </summary>
        SingleValue_Zs012StatusItem = 62,
        /// <summary>
        /// 后区号码
        /// </summary>
        SingleCell_HqItem = 63,
        /// <summary>
        /// 多值多列连号分布
        /// </summary>
        MultiValue_LinkNumber = 65,
        /// <summary>
        /// 单列组三跨度值项
        /// </summary>
        SingleCell_ZSSpanItem = 66,
        /// <summary>
        /// 质合状态项
        /// </summary>
        SingleCell_ZhiHeStatusItem = 67,
        /// <summary>
        /// 多值多列重号分布
        /// </summary>
        MultiValue_RepeatNumber = 68,
        /// <summary>
        /// 多值多列折号分布
        /// </summary>
        MultiValue_ZheHaoNumber = 69,
        /// <summary>
        /// 多值多列斜连号分布
        /// </summary>
        MultiValue_XieLianHaoNumber = 70,
        /// <summary>
        /// 多值多列斜跳号分布
        /// </summary>
        MultiValue_XieTiaoHaoNumber = 71,
        /// <summary>
        /// 多值多列竖三连分布
        /// </summary>
        MultiValue_ShuSanLianHaoNumber = 72,
        /// <summary>
        /// 多值多列竖跳号分布
        /// </summary>
        MultiValue_ShuTiaoHaoNumber = 73,

        /// <summary>
        /// 福彩3D 012路走势图4
        /// </summary>
        SpecialValue_FC3D012_4 = 74,
        /// <summary>
        /// 福彩 双色球出号频率
        /// </summary>
        SpecialValue_FCSSQ_ChuHaoPL = 75,
        /// <summary>
        /// 体彩PD 012路走势图4
        /// </summary>
        SpecialValue_TCP3012_4 = 76,
        /// <summary>
        /// 体彩 大乐透出号频率
        /// </summary>
        SpecialValue_TCDLT_ChuHaoPL = 77,

        /// <summary>
        /// 多值多列快乐12号码分布
        /// </summary>
        MultiValue_KL12 = 78,
        /// <summary>
        /// 生肖分布
        /// </summary>
        SingleValue_SX = 79,
        /// <summary>
        /// 季节分布
        /// </summary>
        SingleValue_JJ = 80,
        /// <summary>
        /// 方位分布
        /// </summary>
        SingleValue_FW = 81,
        /// <summary>
        /// 回摆
        /// </summary>
        SingleValue_HB = 82,
        /// <summary>
        /// 振幅
        /// </summary>
        SingleCell_ZF = 83,
        /// <summary>
        /// 福建31选7三区比
        /// </summary>
        SingleCell_FJ31X7SanQu = 84,
        /// <summary>
        /// 福建36选7三区比
        /// </summary>
        SingleCell_FJ36X7SanQu = 85,
        /// <summary>
        /// 和尾大小形态
        /// </summary>
        SingleValue_HeWeiDx = 86,
        /// <summary>
        /// 生肖
        /// </summary>
        SingleValue_ShengXiao = 87,
        /// <summary>
        /// 华东15选5三区比
        /// </summary>
        SingleCell_Hd15x5SanQU = 88,
        /// <summary>
        /// 华东1区个数
        /// </summary>
        SingleValue_Hd11x5Yq = 89,
        /// <summary>
        /// 华东2区个数
        /// </summary>
        SingleValue_Hd11x5Eq = 90,
        /// <summary>
        /// 华东3区个数
        /// </summary>
        SingleValue_Hd11x5Sq = 91,
        /// <summary>
        /// 南粤三区比
        /// </summary>
        SingleCell_NY36x7Sanqu = 92,
        /// <summary>
        /// 和值012比
        /// </summary>
        SingleCell_Hz012 = 93,
        /// <summary>
        /// 快3三连号走势
        /// </summary>
        SingleValue_K3sbt = 94,
        /// <summary>
        /// 快3二不同单选走势
        /// </summary>
        MultiValue_K3ebt = 95,
        /// <summary>
        /// 快3二同号（单值）
        /// </summary>
        SingleCell_K3ebt = 96,
        /// <summary>
        /// 奇偶个数
        /// </summary>
        SingleValue_JoValue = 97,
        /// <summary>
        /// 大小个数
        /// </summary>
        SingleValue_DxValue = 98,
        /// <summary>
        /// 质合个数
        /// </summary>
        SingleValue_ZhValue = 99,
        /// <summary>
        /// 三不同形态
        /// </summary>
        MultiValue_Sbtxt = 100,
        /// <summary>
        /// 二不同形态
        /// </summary>
        MultiValue_Ebtxt = 101,
        /// <summary>
        /// 大小奇偶
        /// </summary>
        SingleValue_DxjoValue = 102,

        /// <summary>
        /// 小数个数
        /// </summary>
        SingleValue_XsValue = 103,
        /// <summary>
        /// 合数个数
        /// </summary>
        SingleValue_HsValue = 104,
        /// <summary>
        /// 偶数个数
        /// </summary>
        SingleValue_OsValue = 105,
        /// <summary>
        /// 快乐扑克3开奖号
        /// </summary>
        SingleValue_KLPKKJValue = 106,
        /// <summary>
        /// 快乐扑克3形态
        /// </summary>
        SingleValue_KLPKXTValue = 107,
        /// <summary>
        /// 快乐扑克3号码分布
        /// </summary>
        MultiValue_KLPKHMFBValue = 108,
        /// <summary>
        /// 快乐扑克3形态分布
        /// </summary>
        SingleValue_KLPKXTFBValue = 109,
        /// <summary>
        /// 快乐扑克3和值
        /// </summary>
        SingleValue_KLPKHZValue = 110,
        /// <summary>
        /// 快乐扑克3和值分布
        /// </summary>
        SingleValue_KLPKHZFBValue = 111,
        /// <summary>
        /// 快乐扑克3花色分布
        /// </summary>
        SingleValue_KLPKHSValue = 112,
        /// <summary>
        /// 幸运赛车颜色分布
        /// </summary>
        SingleValue_XYSCColor = 113,
        /// <summary>
        /// 幸运赛车余数012分布
        /// </summary>
        SingleValue_XYSC012 = 114,
        /// <summary>
        /// 泳坛夺金组选
        /// </summary>
        SingleValue_YTDJZuXuan = 115,
        /// <summary>
        /// 升降平
        /// </summary>
        SingleValue_SJP = 120,
        /// <summary>
        /// 和尾升降平
        /// </summary>
        SingleValue_HwSjp = 121,
        /// <summary>
        /// 和尾012路
        /// </summary>
        SingleValue_Hw012 = 122,
        /// <summary>
        /// 最大值
        /// </summary>
        SingleValue_Max = 123,
        /// <summary>
        /// 最小值
        /// </summary>
        SingleValue_Min = 124,
        /// <summary>
        /// 平均值
        /// </summary>
        SingleValue_Avg = 125,
        /// <summary>
        /// 和跨和
        /// </summary>
        SingleValue_Hkh = 126,
        /// <summary>
        /// 和跨差
        /// </summary>
        SingleValue_Hkc = 127,
        /// <summary>
        /// 和尾和值
        /// </summary>
        SingleValue_Whz = 128
    }

    /// <summary>
    /// 项处理类类型(处理项的类类型) 
    /// </summary>
    public enum ChartItemClassName
    {
        /// <summary>
        /// 单值项类型
        /// </summary>
        SingleValue = 1,
        /// <summary>
        /// 多值项类型
        /// </summary>
        MultiValue = 2,
        /// <summary>
        /// 特殊项
        /// </summary>
        SpecialValue = 3
    }

    /// <summary>
    /// 遗漏数据类型
    /// </summary>
    public enum MissDataType
    {
        /// <summary>
        /// 本期遗漏
        /// </summary>
        LocalMiss = 1,
        /// <summary>
        /// 上期遗漏
        /// </summary>
        LastMiss = 2,
        /// <summary>
        /// 最大遗漏
        /// </summary>
        AllMaxMiss = 3,
        /// <summary>
        /// 平均遗漏
        /// </summary>
        AllAvgMiss = 4,
        /// <summary>
        /// 出现次数
        /// </summary>
        AllTimes = 5
    }
}
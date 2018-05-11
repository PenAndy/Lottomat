using System.ComponentModel;

namespace TrendChartSDK
{
    /// <summary>
    /// 球的类型枚举
    /// </summary>
    public enum LotteryBallType
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 蓝球
        /// </summary>
        Blue = 2,
        /// <summary>
        /// 生肖
        /// </summary>
        Zodiac = 3,
        /// <summary>
        /// 季节
        /// </summary>
        Season = 4,
        /// <summary>
        /// 方位
        /// </summary>
        Position = 5,
        /// <summary>
        /// 扑克，如山东快乐扑克
        /// </summary>
        Poker = 6,
    }

    /// <summary>
    /// 彩票定义
    /// </summary>
    public enum LotteryDefine
    {
        /// <summary>
        /// 全国彩种
        /// </summary>
        National = 1,
        /// <summary>
        /// 地方彩种
        /// </summary>
        Locality = 2,
        /// <summary>
        /// 高频彩
        /// </summary>
        HighRate = 3
    }

    /// <summary>
    /// 彩种枚举
    /// </summary>
    public enum LotteryType
    {
        #region 全国彩
        /// <summary>
        /// 福彩3D
        /// </summary>
        [TableName("QG_FC3D")]
        [Text("3")]
        [Description("福彩3D")]
        [LotteryCId(1)]
        FC3D,
        /// <summary>
        /// 福彩3D走势图
        /// </summary>
        [TableName("QG_FC3DTrendChartData")]
        FC3DTrendChart,

        /// <summary>
        /// 双色球
        /// </summary>
        [TableName("QG_FCSSQ")]
        [Text("7")]
        [Description("双色球")]
        [LotteryCId(4)]
        SSQ,
        /// <summary>
        /// 双色球走势图
        /// </summary>
        [TableName("QG_FCSSQTrendChartData")]
        SSQTrendChart,

        /// <summary>
        /// 七乐彩
        /// </summary>
        [TableName("QG_FCQLC")]
        [Text("8")]
        [Description("七乐彩")]
        [LotteryCId(5)]
        QLC,
        /// <summary>
        /// 七乐彩走势图
        /// </summary>
        [TableName("QG_FCQLCTrendChartData")]
        QLCTrendChart,

        /// <summary>
        /// 排列三
        /// </summary>
        [TableName("QG_TCP3")]
        [Text("3")]
        [Description("排列三")]
        [LotteryCId(2)]
        PL3,
        /// <summary>
        /// 排列五
        /// </summary>
        [TableName("QG_TCP5")]
        [Text("5")]
        [Description("排列五")]
        [LotteryCId(2)]
        PL5,
        /// <summary>
        /// 排列三、排列五走势图
        /// </summary>
        [TableName("QG_TCP3TrendChartData")]
        PL3TrendChart,

        /// <summary>
        /// 大乐透
        /// </summary>
        [TableName("QG_TCDLT")]
        [Text("7")]
        [Description("大乐透")]
        [LotteryCId(12)]
        DLT,
        /// <summary>
        /// 大乐透走势图
        /// </summary>
        [TableName("QG_TCDLTTrendChartData")]
        DLTTrendChart,

        /// <summary>
        /// 七星彩
        /// </summary>
        [TableName("QG_TCQXC")]
        [Text("7")]
        [Description("七星彩")]
        [LotteryCId(19)]
        QXC,
        /// <summary>
        /// 七星彩走势图
        /// </summary>
        [TableName("QG_TCQXCTrendChartData")]
        QXCTrendChart,
        #endregion

        #region 地方彩

        #region 华东六省

        /// <summary>
        /// 东方6+1
        /// </summary>
        [TableName("DF_DF6J1")]
        [Description("东方6+1")]
        [Text("7")]
        [LotteryCId(64)]
        DF6J1,
        /// <summary>
        /// 华东15选5
        /// </summary>
        [TableName("DF_HD15x5")]
        [Description("华东15选5")]
        [Text("5")]
        [LotteryCId(65)]
        HD15X5,

        #endregion

        #region 北京



        #endregion

        #region 上海

        /// <summary>
        /// 上海天天彩选4
        /// </summary>
        [TableName("DF_TTCx4_ShangHai")]
        [Description("上海天天彩选4")]
        [Text("4")]
        ShangHaiTTCX4,

        #endregion

        #region 天津



        #endregion

        #region 江苏

        /// <summary>
        /// 江苏7位数
        /// </summary>
        [TableName("DF_TC7WS_JiangSu")]
        [Description("江苏体彩七位数")]
        [Text("7")]
        [LotteryCId(62)]
        JiangSuTC7WS,

        #endregion

        #region 浙江

        /// <summary>
        /// 浙江20选5
        /// </summary>
        [TableName("DF_20x5_ZheJiang")]
        [Description("浙江20选5")]
        [Text("5")]
        ZheJiang20x5,
        /// <summary>
        /// 浙江6+1
        /// </summary>
        [TableName("DF_6J1_ZheJiang")]
        [Description("浙江体彩6+1")]
        [Text("7")]
        [LotteryCId(63)]
        ZheJiang6J1,

        #endregion

        #region 福建

        /// <summary>
        /// 福建体彩22选5
        /// </summary>
        [TableName("DF_TC22x5_FuJian")]
        [Description("福建体彩22选5")]
        [Text("5")]
        FuJian22x5,
        /// <summary>
        /// 福建31选7
        /// </summary>
        [TableName("DF_31x7_FuJian")]
        [Description("福建31选7")]
        [Text("8")]
        [LotteryCId(60)]
        FuJian31x7,
        /// <summary>
        /// 福建体彩36选7
        /// </summary>
        [TableName("DF_TC36x7_FuJian")]
        [Description("福建体彩36选7")]
        [Text("8")]
        [LotteryCId(61)]
        FuJianTC36x7,

        #endregion

        #region 广东

        /// <summary>
        /// 广东南粤风采26选5
        /// </summary>
        [TableName("DF_26x5_GuangDong")]
        [Description("广东26选5")]
        [Text("5")]
        [LotteryCId(69)]
        GuangDong26X5,
        /// <summary>
        /// 广东36选7
        /// </summary>
        [TableName("DF_36x7_GuangDong")]
        [Description("广东36选7")]
        [Text("7")]
        GuangDong36x7,
        /// <summary>
        /// 广东好彩1
        /// </summary>
        [TableName("DF_HC1_GuangDong")]
        [Description("广东好彩一")]
        [Text("4")]
        [LotteryCId(29)]
        GuangDongHC1,

        #endregion

        #region 深圳

        /// <summary>
        /// 广东深圳风采
        /// </summary>
        [TableName("DF_SZFC_GuangDong")]
        [Description("深圳风采")]
        [Text("8")]
        GuangDongSZFC,

        #endregion

        #region 黑龙江

        /// <summary>
        /// 黑龙江龙江风彩22选5
        /// </summary>
        [TableName("DF_LJFC22x5_HeiLongJiang")]
        [Description("黑龙江22选5")]
        [Text("5")]
        HeiLongJiangLJFC22x5,
        /// <summary>
        /// 黑龙江体彩6+1
        /// </summary>
        [TableName("DF_TC6J1_HeiLongJiang")]
        [Description("黑龙江体彩6+1")]
        [Text("7")]
        HeiLongJiangTC6J1,
        /// <summary>
        /// 黑龙江36选7
        /// </summary>
        [TableName("DF_36x7_HeiLongJiang")]
        [Description("黑龙江36选7")]
        [Text("8")]
        HeiLongJiang36x7,
        /// <summary>
        /// 黑龙江P62
        /// </summary>
        [TableName("DF_P62_HeiLongJiang")]
        [Description("黑龙江P62")]
        [Text("7")]
        HeiLongJiangP62,

        #endregion

        #region 四川



        #endregion

        #region 辽宁

        /// <summary>
        /// 辽宁风采35选7
        /// </summary>
        [TableName("DF_35x7_LiaoNing")]
        [Description("辽宁风采35选7")]
        [Text("8")]
        LiaoNingFC35X7,

        #endregion

        #region 河北

        /// <summary>
        /// 河北燕赵风采20选5
        /// </summary>
        [TableName("DF_20x5_HeBei")]
        [Description("河北20选5")]
        [Text("5")]
        HeBei20X5,
        /// <summary>
        /// 河北燕赵风采好运彩2
        /// </summary>
        [TableName("DF_HYC2_HeBei")]
        [Description("河北好运彩2")]
        [Text("2")]
        HeBeiHYC2,
        /// <summary>
        /// 河北燕赵风采好运彩3
        /// </summary>
        [TableName("DF_HYC3_HeBei")]
        [Description("河北好运彩3")]
        [Text("3")]
        HeBeiHYC3,
        /// <summary>
        /// 河北燕赵风采排列5
        /// </summary>
        [TableName("DF_PL5_HeBei")]
        [Description("河北排列5")]
        [Text("5")]
        HeBeiPL5,
        /// <summary>
        /// 河北燕赵风采排列7
        /// </summary>
        [TableName("DF_PL7_HeBei")]
        [Description("河北排列7")]
        [Text("7")]
        HeBeiPL7,

        #endregion

        #region 河南

        /// <summary>
        /// 河南中原风采22选5
        /// </summary>
        [TableName("DF_22x5_HeNan")]
        [Description("河南22选5")]
        [Text("5")]
        [LotteryCId(67)]
        HeNan22X5,

        #endregion

        #region 湖北

        /// <summary>
        /// 湖北30选5
        /// </summary>
        [TableName("DF_30x5_HuBei")]
        [Description("湖北30选5")]
        [Text("5")]
        [LotteryCId(101)]
        HuBei30X5,

        #endregion

        #region 山西



        #endregion

        #region 云南



        #endregion

        #region 新疆

        /// <summary>
        /// 新疆风采18选7
        /// </summary>
        [TableName("DF_18x7_XinJiang")]
        [Description("新疆风采18选7")]
        [Text("7")]
        XinJiangFC18X7,
        /// <summary>
        /// 新疆风采25选7
        /// </summary>
        [TableName("DF_25x7_XinJiang")]
        [Description("新疆风采25选7")]
        [Text("8")]
        XinJiangFC25X7,
        /// <summary>
        /// 新疆风采35选7
        /// </summary>
        [TableName("DF_35x7_XinJiang")]
        [Description("新疆风采35选7")]
        [Text("8")]
        [LotteryCId(68)]
        XinJiangFC35X7,

        #endregion

        #region 广西

        /// <summary>
        /// 广西快乐双彩
        /// </summary>
        [TableName("DF_KLSC_GuangXi")]
        [Description("广西快乐双彩")]
        [Text("7")]
        GuangXiKLSC,

        #endregion

        #region 江西



        #endregion

        #region 安徽

        /// <summary>
        /// 安徽福彩25选5
        /// </summary>
        [TableName("DF_FC25x5_AnHui")]
        [Description("安徽福彩25选5")]
        [Text("5")]
        AnHui25x5,

        #endregion

        #region 贵州



        #endregion

        #region 海南

        /// <summary>
        /// 海南4+1
        /// </summary>
        [TableName("DF_4J1_HaiNan")]
        [Description("海南4+1")]
        [Text("5")]
        HaiNan4J1,

        #endregion

        #region 香港

        /// <summary>
        /// 香港赛马会六合彩
        /// </summary>
        [TableName("DF_SMHLHC_HongKong")]
        [Description("香港赛马会六合彩")]
        [Text("7")]
        HongKongSMHLHC,

        #endregion

        #endregion

        #region 高频彩

        #region 华东六省



        #endregion

        #region 北京

        /// <summary>
        /// 北京11选5
        /// </summary>
        [TableName("GP_11x5_BeiJing")]
        [Text("5")]
        [Description("北京11选5")]
        BeiJing11x5,
        /// <summary>
        /// 北京快乐8
        /// </summary>
        [TableName("GP_KL8_BeiJing")]
        [Text("21")]
        [Description("北京快乐8")]
        [LotteryCId(114)]
        BeiJingKL8,
        /// <summary>
        /// 北京快3
        /// </summary>
        [TableName("GP_K3_BeiJing")]
        [Text("3")]
        [Description("北京快3")]
        BeiJingK3,
        /// <summary>
        /// 北京快中彩
        /// </summary>
        [TableName("GP_KZC_BeiJing")]
        [Text("9")]
        [Description("北京快中彩")]
        BeiJingKZC,
        /// <summary>
        /// 北京pk10(北京赛车)
        /// </summary>
        [TableName("GP_PK10_BeiJing")]
        [Text("10")]
        [Description("北京PK10(北京赛车)")]
        [LotteryCId(115)]
        BeiJingPK10,

        #endregion

        #region 上海

        /// <summary>
        /// 上海时时乐
        /// </summary>
        [TableName("GP_SSL_ShangHai")]
        [Text("3")]
        [Description("上海时时乐")]
        [LotteryCId(102)]
        ShangHaiSSL,
        /// <summary>
        /// 上海快3
        /// </summary>
        [TableName("GP_K3_ShangHai")]
        [Text("3")]
        [Description("上海快3")]
        ShangHaiK3,
        /// <summary>
        /// 上海11选5
        /// </summary>
        [TableName("GP_11x5_ShangHai")]
        [Text("5")]
        [Description("上海11选5")]
        [LotteryCId(81)]
        ShangHai11x5,

        #endregion

        #region 天津

        /// <summary>
        /// 天津快乐十分
        /// </summary>
        [TableName("GP_KL10F_TianJin")]
        [Text("8")]
        [Description("天津快乐十分")]
        [LotteryCId(94)]
        TianJinKL10F,
        /// <summary>
        /// 天津时时彩
        /// </summary>
        [TableName("GP_SSC_TianJin")]
        [Text("5")]
        [Description("天津时时彩")]
        TianJinSSC,
        /// <summary>
        /// 天津11选5
        /// </summary>
        [TableName("GP_11x5_TianJin")]
        [Text("5")]
        [Description("天津11选5")]
        TianJin11x5,

        #endregion

        #region 江苏

        /// <summary>
        /// 江苏11选5
        /// </summary>
        [TableName("GP_11x5_JiangSu")]
        [Text("5")]
        [Description("江苏11选5")]
        [LotteryCId(76)]
        JiangSu11X5,
        /// <summary>
        /// 江苏快3
        /// </summary>
        [TableName("GP_K3_JiangSu")]
        [Text("3")]
        [Description("江苏快3")]
        [LotteryCId(88)]
        JiangSuK3,

        #endregion

        #region 浙江

        /// <summary>
        /// 浙江11选5
        /// </summary>
        [TableName("GP_11x5_ZheJiang")]
        [Text("5")]
        [Description("浙江11选5")]
        [LotteryCId(85)]
        ZheJiang11X5,
        /// <summary>
        /// 浙江快乐12
        /// </summary>
        [TableName("GP_KL12_ZheJiang")]
        [Text("5")]
        [Description("浙江快乐12")]
        [LotteryCId(98)]
        ZheJiangKL12,

        #endregion

        #region 福建

        /// <summary>
        /// 福建11选5(即乐彩)
        /// </summary>
        [TableName("GP_11x5_FuJian")]
        [Text("5")]
        [Description("福建11选5(即乐彩)")]
        [LotteryCId(108)]
        FuJian11x5,
        /// <summary>
        /// 福建快3
        /// </summary>
        [TableName("GP_K3_FuJian")]
        [Text("3")]
        [Description("福建快3")]
        FuJianK3,

        #endregion

        #region 广东

        /// <summary>
        /// 广东11选5
        /// </summary>
        [TableName("GP_11x5_GuangDong")]
        [Text("5")]
        [Description("广东11选5")]
        [LotteryCId(72)]
        GuangDong11X5,
        /// <summary>
        /// 广东快乐十分
        /// </summary>
        [TableName("GP_KL10F_GuangDong")]
        [Text("8")]
        [Description("广东快乐十分")]
        [LotteryCId(15)]
        GuangDongKL10F,

        #endregion

        #region 深圳



        #endregion

        #region 黑龙江

        /// <summary>
        /// 黑龙江11选5
        /// </summary>
        [TableName("GP_11x5_HeiLongJiang")]
        [Text("5")]
        [Description("黑龙江11选5")]
        [LotteryCId(74)]
        HeiLongJiang11X5,
        /// <summary>
        /// 黑龙江快乐十分
        /// </summary>
        [TableName("GP_KL10F_HeiLongJiang")]
        [Text("8")]
        [Description("黑龙江快乐十分")]
        HeiLongJiangKL10F,
        /// <summary>
        /// 黑龙江时时彩
        /// </summary>
        [TableName("GP_SSC_HeiLongJiang")]
        [Text("5")]
        [Description("黑龙江时时彩")]
        HeiLongJiangSSC,

        #endregion

        #region 四川

        /// <summary>
        /// 四川快乐12
        /// </summary>
        [TableName("GP_KL12_SiChuan")]
        [Text("5")]
        [Description("四川快乐12")]
        [LotteryCId(59)]
        SiChuanKL12,

        #endregion

        #region 辽宁

        /// <summary>
        /// 辽宁11选5(全运彩)
        /// </summary>
        [TableName("GP_11x5_LiaoNing")]
        [Text("5")]
        [Description("辽宁11选5(全运彩)")]
        [LotteryCId(79)]
        LiaoNing11X5,
        /// <summary>
        /// 辽宁快乐12
        /// </summary>
        [TableName("GP_KL12_LiaoNing")]
        [Text("5")]
        [Description("辽宁快乐12")]
        [LotteryCId(90)]
        LiaoNingKL12,

        #endregion

        #region 吉林

        /// <summary>
        /// 吉林11选5
        /// </summary>
        [TableName("GP_11x5_JiLin")]
        [Text("5")]
        [Description("吉林11选5")]
        [LotteryCId(78)]
        JiLin11X5,
        /// <summary>
        /// 吉林快3
        /// </summary>
        [TableName("GP_K3_JiLin")]
        [Text("3")]
        [Description("吉林快3")]
        [LotteryCId(89)]
        JiLinK3,

        #endregion

        #region 河北

        /// <summary>
        /// 河北11选5
        /// </summary>
        [TableName("GP_11x5_HeBei")]
        [Text("5")]
        [Description("河北11选5")]
        [LotteryCId(100)]
        HeBei11X5,
        /// <summary>
        /// 河北快3
        /// </summary>
        [TableName("GP_K3_HeBei")]
        [Text("3")]
        [Description("河北快3")]
        [LotteryCId(96)]
        HeBeiK3,

        #endregion

        #region 河南

        /// <summary>
        /// 河南快赢481(泳坛夺金)
        /// </summary>
        [TableName("GP_KY481_HeNan")]
        [Text("4")]
        [Description("河南快赢481(泳坛夺金)")]
        [LotteryCId(105)]
        HeNanKY481,
        /// <summary>
        /// 河南快3
        /// </summary>
        [TableName("GP_K3_HeNan")]
        [Text("3")]
        [Description("河南快3")]
        HeNanK3,

        #endregion

        #region 湖北

        /// <summary>
        /// 湖北快3
        /// </summary>
        [TableName("GP_K3_HuBei")]
        [Text("3")]
        [Description("湖北快3")]
        [LotteryCId(87)]
        HuBeiK3,
        /// <summary>
        /// 湖北11选5
        /// </summary>
        [TableName("GP_11x5_HuBei")]
        [Text("5")]
        [Description("湖北11选5")]
        [LotteryCId(75)]
        HuBei11x5,

        #endregion

        #region 山西

        /// <summary>
        /// 山西快乐十分
        /// </summary>
        [TableName("GP_KL10F_ShanXiTaiYuan")]
        [Text("8")]
        [Description("山西快乐十分")]
        [LotteryCId(99)]
        ShanXiTaiYuanKL10F,
        /// <summary>
        /// 山西泳坛夺金(481)
        /// </summary>
        [TableName("GP_YTDJ_ShanXiTaiYuan")]
        [Text("4")]
        [Description("山西泳坛夺金(481)")]
        ShanXiTaiYuan481,
        /// <summary>
        /// 山西11选5
        /// </summary>
        [TableName("GP_11x5_ShanXiTaiYuan")]
        [Text("5")]
        [Description("山西11选5")]
        [LotteryCId(110)]
        ShanXiTaiYuan11x5,

        #endregion

        #region 云南

        /// <summary>
        /// 云南11选5
        /// </summary>
        [TableName("GP_11x5_YunNan")]
        [Text("5")]
        [Description("云南11选5")]
        [LotteryCId(84)]
        YunNan11X5,
        /// <summary>
        /// 云南快乐十分
        /// </summary>
        [TableName("GP_KL10F_YunNan")]
        [Text("8")]
        [Description("云南快乐十分")]
        YunNanKL10F,
        /// <summary>
        /// 云南时时彩
        /// </summary>
        [TableName("GP_SSC_YunNan")]
        [Text("5")]
        [Description("云南时时彩")]
        YunNanSSC,

        #endregion

        #region 新疆

        /// <summary>
        /// 新疆时时彩
        /// </summary>
        [TableName("GP_SSC_XinJiang")]
        [Text("5")]
        [Description("新疆时时彩")]
        XinJiangSSC,
        /// <summary>
        /// 新疆喜乐彩
        /// </summary>
        [TableName("GP_XLC_XinJiang")]
        [Text("7")]
        [Description("新疆喜乐彩")]
        XinJiangXLC,
        /// <summary>
        /// 新疆11选5(新乐彩)
        /// </summary>
        [TableName("GP_11x5_XinJiang")]
        [Text("5")]
        [Description("新疆11选5(新乐彩)")]
        [LotteryCId(109)]
        XinJiang11x5,

        #endregion

        #region 重庆

        /// <summary>
        /// 重庆时时彩
        /// </summary>
        [TableName("GP_SSC_ChongQing")]
        [Text("5")]
        [Description("重庆时时彩")]
        ChongQingSSC,
        /// <summary>
        /// 重庆快乐十分(幸运农场)
        /// </summary>
        [TableName("GP_KL10F_ChongQing")]
        [Text("8")]
        [Description("重庆快乐十分(幸运农场)")]
        [LotteryCId(91)]
        ChongQingKL10F,

        #endregion

        #region 广西

        /// <summary>
        /// 广西快乐十分
        /// </summary>
        [TableName("GP_KL10F_GuangXi")]
        [Text("5")]
        [Description("广西快乐十分")]
        [LotteryCId(92)]
        GuangXiKL10F,
        /// <summary>
        /// 广西11选5
        /// </summary>
        [TableName("GP_11x5_GuangXi")]
        [Text("5")]
        [Description("广西11选5")]
        GuangXi11x5,
        /// <summary>
        /// 广西快3
        /// </summary>
        [TableName("GP_K3_GuangXi")]
        [Text("3")]
        [Description("广西快3")]
        GuangXiK3,

        #endregion

        #region 江西

        /// <summary>
        /// 江西11选5
        /// </summary>
        [TableName("GP_11x5_JiangXi")]
        [Text("5")]
        [Description("江西11选5")]
        [LotteryCId(77)]
        JiangXi11X5,
        /// <summary>
        /// 江西快3
        /// </summary>
        [TableName("GP_K3_JiangXi")]
        [Text("3")]
        [Description("江西快3")]
        JiangXiK3,

        #endregion

        #region 安徽

        /// <summary>
        /// 安徽快3
        /// </summary>
        [TableName("GP_K3_AnHui")]
        [Text("3")]
        [Description("安徽快3")]
        [LotteryCId(86)]
        AnHuiK3,
        /// <summary>
        /// 安徽11选5
        /// </summary>
        [TableName("GP_11x5_AnHui")]
        [Text("5")]
        [Description("安徽11选5")]
        [LotteryCId(70)]
        AnHui11x5,

        #endregion

        #region 贵州

        /// <summary>
        /// 贵州11选5
        /// </summary>
        [TableName("GP_11x5_GuiZhou")]
        [Text("5")]
        [Description("贵州11选5")]
        [LotteryCId(73)]
        GuiZhou11x5,
        /// <summary>
        /// 贵州快3
        /// </summary>
        [TableName("GP_K3_GuiZhou")]
        [Text("3")]
        [Description("贵州快3")]
        GuiZhouK3,

        #endregion

        #region 山东

        /// <summary>
        /// 山东11选5
        /// </summary>
        [TableName("GP_11x5_ShanDong")]
        [Text("5")]
        [Description("山东11选5")]
        [LotteryCId(80)]
        ShanDong11X5,
        /// <summary>
        /// 山东群英会
        /// </summary>
        [TableName("GP_QYH_ShanDong")]
        [Text("5")]
        [Description("山东群英会")]
        [LotteryCId(95)]
        ShanDongQYH,
        /// <summary>
        /// 山东快乐扑克3
        /// </summary>
        [TableName("GP_KLPK3_ShanDong")]
        [Text("3")]
        [Description("山东快乐扑克3")]
        [LotteryCId(103)]
        ShanDongKLPK3,

        #endregion

        #region 海南



        #endregion

        #region 甘肃

        /// <summary>
        /// 甘肃11选5
        /// </summary>
        [TableName("GP_11x5_GanSu")]
        [Text("5")]
        [Description("甘肃11选5")]
        [LotteryCId(107)]
        GanSu11x5,
        /// <summary>
        /// 甘肃快3
        /// </summary>
        [TableName("GP_K3_GanSu")]
        [Text("3")]
        [Description("甘肃快3")]
        GanSuK3,

        #endregion

        #region 湖南

        /// <summary>
        /// 湖南幸运赛车
        /// </summary>
        [TableName("GP_XYSC_HuNan")]
        [Text("3")]
        [Description("湖南幸运赛车")]
        [LotteryCId(104)]
        HuNanXYSC,
        /// <summary>
        /// 湖南快乐十分(动物总动员)
        /// </summary>
        [TableName("GP_KL10F_HuNan")]
        [Text("8")]
        [Description("湖南快乐十分(动物总动员)")]
        [LotteryCId(93)]
        HuNanKL10F,

        #endregion

        #region 陕西

        /// <summary>
        /// 陕西快乐十分
        /// </summary>
        [TableName("GP_KL10F_ShanXiXiAn")]
        [Text("8")]
        [Description("陕西快乐十分")]
        ShanXiXiAnKL10F,
        /// <summary>
        /// 陕西11选5
        /// </summary>
        [TableName("GP_11x5_ShanXiXiAn")]
        [Text("5")]
        [Description("陕西11选5")]
        [LotteryCId(111)]
        ShanXiXiAn11x5,

        #endregion

        #region 内蒙古

        /// <summary>
        /// 内蒙古快3
        /// </summary>
        [TableName("GP_K3_NeiMengGu")]
        [Text("3")]
        [Description("内蒙古快3")]
        [LotteryCId(97)]
        NeiMengGuK3,
        /// <summary>
        /// 内蒙古11选5
        /// </summary>
        [TableName("GP_11x5_NeiMengGu")]
        [Text("5")]
        [Description("内蒙古11选5")]
        [LotteryCId(106)]
        NeiMengGu11x5,

        #endregion

        #region 青海

        /// <summary>
        /// 青海快3
        /// </summary>
        [TableName("GP_K3_QingHai")]
        [Text("3")]
        [Description("青海快3")]
        QingHaiK3,

        #endregion

        #region 香港



        #endregion

        #endregion
    }
}

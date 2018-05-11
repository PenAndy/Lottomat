using System;
using System.Collections.Generic;
using System.Text;
using TrendChartSDK.Entity.Base;
using TrendChartSDK.Entity.TrendChart;
using TrendChartSDK.Entity.TrendChartData;
using TrendChartSDK.Interface;
using TrendChartSDK.Common;
using TrendChartSDK.Entity.Lottery.QTC;

namespace TrendChartSDK.TrendChartManager
{
    public abstract class ChartItemRepository<TEntity> : IChartItem<TEntity> where TEntity : LotteryOpenCode
    {
        #region 属性
        /// <summary>
        /// 最大遗漏
        /// </summary>
        protected int[] _maxMiss;
        /// <summary>
        /// 开奖记录
        /// </summary>
        protected int _recordCount;
        /// <summary>
        /// 平均遗漏
        /// </summary>
        protected int[] _avgMiss;
        /// <summary>
        /// 出现次数
        /// </summary>
        protected int[] _times;
        /// <summary>
        /// 上期遗漏
        /// </summary>
        protected int[] _lastMiss;
        /// <summary>
        /// 当前遗漏数据
        /// </summary>
        protected int[] _localMiss;
        /// <summary>
        /// 项配置
        /// </summary>
        protected TrendChartItemInfo _itemConfig;
        /// <summary>
        /// 样式配置
        /// </summary>
        protected ChartCssConfigInfo _cssConfig;
        /// <summary>
        /// 是否画线
        /// </summary>
        protected bool _drawLine { get; set; }
        /// <summary>
        /// 项值
        /// </summary>
        public string _itemValue;
        /// <summary>
        /// 项值索引号
        /// </summary>
        public int _itemIndex;
        /// <summary>
        /// 上期项值索引值
        /// </summary>
        public int[] _lastItemIndex;
        /// <summary>
        /// 上期期号
        /// </summary>
        public long _lastTerm;
        /// <summary>
        /// 本期数据实体
        /// </summary>
        protected TEntity _LocalEntity;
        /// <summary>
        /// 上期开奖号码（根据配置取出）
        /// </summary>
        protected IList<int> _LastOpentCode;
        /// <summary>
        /// 本期索引值
        /// </summary>
        public int[] _ItemIndex;
        #endregion

        #region IChartItem 成员
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="cssConfig"></param>
        /// <param name="itemConfig"></param>
        public abstract void Init(ChartCssConfigInfo cssConfig, TrendChartItemInfo itemConfig);
        /// <summary>
        /// 初始化遗漏
        /// </summary>
        /// <param name="TrendChartCofig"></param>
        /// <param name="i"></param>
        public abstract void MissDataInit(TrendChartData TrendChartCofig, int i);
        /// <summary>
        /// 获取遗漏数据
        /// </summary>
        /// <param name="missDataType"></param>
        /// <returns></returns>
        public abstract string GetMissData(MissDataType missDataType);
        /// <summary>
        /// 设置项值
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract bool SetItemValue(TEntity entity);
        /// <summary>
        /// 设置项值
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="lastentity"></param>
        /// <returns></returns>
        public abstract bool SetItemValue(TEntity entity, TEntity lastentity);
        /// <summary>
        /// 去格式化字符串
        /// </summary>
        /// <param name="fomart"></param>
        /// <param name="cssConfig"></param>
        /// <returns></returns>
        public abstract string GetFomartString(string fomart, ChartCssConfigInfo cssConfig = null);

        #endregion

        /// <summary>
        /// 默认初始化方法
        /// </summary>
        /// <param name="cssConfig"></param>
        /// <param name="itemConfig"></param>
        protected virtual void DefaultInit(ChartCssConfigInfo cssConfig, TrendChartItemInfo itemConfig)
        {
            this._itemConfig = itemConfig;
            this._localMiss = new int[this._itemConfig.ItemCount];
            this._cssConfig = cssConfig;
        }
        /// <summary>
        /// 初使化遗漏
        /// </summary>
        /// <param name="trendChartCofig">遗漏数据</param>
        /// <param name="i">项索引</param>
        protected virtual void DefaultMissDataInit(TrendChartData trendChartCofig, int i)
        {
            if (1 >= this._itemConfig.ItemCount)
                return;

            this._maxMiss = new int[this._itemConfig.ItemCount];
            this._avgMiss = new int[this._itemConfig.ItemCount];
            this._localMiss = new int[this._itemConfig.ItemCount];
            this._lastMiss = new int[this._itemConfig.ItemCount];
            this._times = new int[this._itemConfig.ItemCount];

            if (null == trendChartCofig)
                return;

            this._recordCount = trendChartCofig.RecordCount;

            //初使化上期遗漏
            string[] array = trendChartCofig.LastMiss[i].Split(',');
            for (int j = array.Length - 1; j >= 0; j--)
            {
                this._lastMiss[j] = TypeConverter.StrToInt(array[j]);
            }

            //初使化本期遗漏
            array = trendChartCofig.LocalMiss[i].Split(',');
            for (int j = array.Length - 1; j >= 0; j--)
            {
                this._localMiss[j] = TypeConverter.StrToInt(array[j]);
            }

            //初使化出现次数
            array = trendChartCofig.AllTimes[i].Split(',');
            for (int j = array.Length - 1; j >= 0; j--)
            {
                this._times[j] = TypeConverter.StrToInt(array[j]);
            }

            //初使化最大遗漏
            array = trendChartCofig.AllMaxMiss[i].Split(',');
            for (int j = array.Length - 1; j >= 0; j--)
            {
                this._maxMiss[j] = TypeConverter.StrToInt(array[j]);
            }

            //初使化平均遗漏
            array = trendChartCofig.AllAvgMiss[i].Split(',');
            for (int j = array.Length - 1; j >= 0; j--)
            {
                this._avgMiss[j] = TypeConverter.StrToInt(array[j]);
            }
        }
        /// <summary>
        /// 字符串返回遗漏数据(逗号分隔数组)
        /// </summary>
        /// <param name="missDataType">遗漏数据类型</param>
        /// <returns></returns>
        protected virtual string GetDefaultMissData(MissDataType missDataType)
        {
            if (1 >= this._itemConfig.ItemCount)
                return "-1";

            switch (missDataType)
            {
                case MissDataType.AllAvgMiss:
                    return GetMissDataString(this._avgMiss);
                case MissDataType.AllMaxMiss:
                    return GetMissDataString(this._maxMiss);
                case MissDataType.AllTimes:
                    return GetMissDataString(this._times);
                case MissDataType.LastMiss:
                    return GetMissDataString(this._lastMiss);
                case MissDataType.LocalMiss:
                    return GetMissDataString(this._localMiss);
                default:
                    return "";
            }
        }
        /// <summary>
        /// 字符串返回遗漏数据(已逗号分隔)
        /// </summary>
        /// <param name="missDataArray">遗漏数据</param>
        /// <returns></returns>
        private string GetMissDataString(int[] missDataArray)
        {
            StringBuilder sb = new StringBuilder(missDataArray.Length * 5);
            for (int i = 0; i < missDataArray.Length; i++)
            {
                sb.Append(missDataArray[i].ToString() + ",");
            }
            return sb.ToString().TrimEnd(',');
        }

        /// <summary>
        /// 通用获取HTML部分的结构和样式
        /// </summary>
        /// <param name="fomart"></param>
        /// <param name="cssConfig"></param>
        /// <returns></returns>
        protected virtual Tuple<string, string, string, string> GetFomartHtml(string fomart, ChartCssConfigInfo cssConfig = null)
        {
            string _fomart = fomart;
            string _cssNumber = "", _cssMiss = "", _lineColor = "";
            if (string.IsNullOrEmpty(_fomart))
                _fomart = "<td {0}>{1}</td>";

            _fomart = string.Format(_fomart, "class=\"{0}\"{1}", "{2}");
            var css = this._cssConfig;
            if (null != cssConfig)
                css = cssConfig;

            if (null != css)
            {
                _cssNumber = css.NumberCssName;
                _cssMiss = css.MissCssName;
                _lineColor = css.LineColor;
            }

            return new Tuple<string, string, string, string>(_fomart, _cssNumber, _cssMiss, _lineColor);
        }
        /// <summary>
        /// 单列项返回字符串
        /// </summary>
        /// <param name="fomart"></param>
        /// <param name="cssConfig"></param>
        /// <returns></returns>
        protected string GetSingleCellItemFomartString(string fomart, ChartCssConfigInfo cssConfig = null)
        {
            var html = GetFomartHtml(fomart, cssConfig);
            return string.Format(html.Item1, html.Item2, this._itemValue);
        }

        #region 单值项计算项值及遗漏计算
        /// <summary>
        /// 设置单值项 项的值
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="nextentity"></param>
        /// <returns></returns>
        protected bool SetSingleValue(TEntity entity, TEntity nextentity)
        {
            bool yes = false;
            yes = !entity.OpenCode.Contains(-1);
            if (nextentity != null)
            {
                IList<int> list = new List<int>(nextentity.OpenCode);
                if (-1 != this._itemConfig.IndexEnd)
                {
                    for (int i = list.Count - 1; i >= this._itemConfig.IndexEnd; i--)
                    { list.RemoveAt(this._itemConfig.IndexEnd); }
                }
                for (int i = 0; i < this._itemConfig.IndexStart; i++)
                { list.RemoveAt(0); }
                _lastItemIndex = new int[list.Count];
                for (int i = 0; i < list.Count; i++)
                {
                    _lastItemIndex[i] = list[i];
                }
            }
            switch (this._itemConfig.FuntionType)
            {
                case ChartItemType.Term_TermItem:
                    if (entity.Term > 0)
                    { this._itemValue = SingleValueFunction.GetTermItemValue(entity); yes = true; }
                    break;
                case ChartItemType.SingleCell_HeWeiItem:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetHeWeiItemValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleCell_OpenCodeItem:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetOpenCodeItemValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleCell_ProportionOf012Item:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetProportionOf012ItemValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleCell_ProportionOfJoItem:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetProportionOfJoItemValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleCell_ProportionOfDxItem:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetProportionOfDxItemValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd,
                            this._itemConfig.ItemCount, this._itemConfig.SplitNumberOfDX);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleCell_ProportionOfZhItem:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetProportionOfZhItemValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleCell_SpanItem:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetSpanItemValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleCell_ZSSpanItem:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetZSSpanItemValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleCell_SumItem:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetSumItemValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_HzJoStatusItem:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetHzJoStatusValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_HzDxStatusItem:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetHzDxStatusValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.SplitNumberOfDX, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_DxStatusItem:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetDxStatusValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_JoStatusItem:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetJoStatusItem(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleCell_ShiJiHao:
                    if (entity.ShiJiHao.IndexOf("-1", StringComparison.Ordinal) == -1)
                    {
                        this._itemValue = SingleValueFunction.GetShiJiHaoItemValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_ShiJiHao:
                    if (entity.ShiJiHao.IndexOf("-1", StringComparison.Ordinal) == -1)
                    {
                        this._itemValue = SingleValueFunction.GetShiJiHaoSingleValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_ZsStatusItem:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetZsStatusItemValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_ZsJoStatusItem:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetZsJoStatusValue(entity);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleCell_012StatusItem:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.Get012StatusItemValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_HeWeiItem:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetHeWeiSingleValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_Hw012:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetHeWei012SingleValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_JiOuStatusItem:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetJiOuStatusItemValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_DaXiaoStatusItem:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetDaXiaoStatusItemValue(entity,
                            this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount, this._itemConfig.SplitNumberOfDX);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_Number012StatusItem:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetNumber012StatusItemValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_NumberItem:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetNumberItemValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_SpanItem:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetSpanSingleValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_SpanNumberItem:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetSpanNumberItemValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_SumItem:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetSumSingleValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_ZhiHeStatusItem:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetZhiHeStatusItemValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleCell_ZhiHeStatusItem:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetZhiHeItemValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_ZuHeStatusItem:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetZuHeStatusItemValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleCell_Ac:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetAcValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleCell_SanQu:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetSsqsanqu(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleCell_AcJiOu:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetAcJiOu(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleCell_AcZhiHe:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetAcZhiHe(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleCell_Ac012Lu:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetAc012Lu(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_QuJianFenBu:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetQuJianStatusItemValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_HeWeiJiOu:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetHeWeiJiOuFenBu(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_HeWeiDx:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetHeWeiDxFenBu(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.SplitNumberOfDX, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleCell_ShiJiHaoHzItem:
                    if (entity.ShiJiHao.IndexOf("-1", StringComparison.Ordinal) == -1)
                    {
                        this._itemValue = SingleValueFunction.GetShiJiHaoHzValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleCell_ShiJiHaoSpanItem:
                    if (entity.ShiJiHao.IndexOf("-1", StringComparison.Ordinal) == -1)
                    {
                        this._itemValue = SingleValueFunction.GetShiJiHaoSpanValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleCell_ProportionOfShiJiHaoJoItem:
                    if (entity.ShiJiHao.IndexOf("-1", StringComparison.Ordinal) == -1)
                    {
                        this._itemValue = SingleValueFunction.GetProportionOfShiJiHaoJoItemValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleCell_ProportionOfShiJiHaoDxItem:
                    if (entity.ShiJiHao.IndexOf("-1", StringComparison.Ordinal) == -1)
                    {
                        this._itemValue = SingleValueFunction.GetProportionOfShiJiHaoDxItemValue(entity,
                            this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount, this._itemConfig.SplitNumberOfDX);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_ShiJiHaoTypeItem:
                    if (entity.ShiJiHao.IndexOf("-1", StringComparison.Ordinal) == -1)
                    {
                        this._itemValue = SingleValueFunction.GetShiJiHaoTyepValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleCell_ZsMissItem:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetZsMissItem(entity, ref _lastTerm);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleCell_ZsHaoMaItem:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetZsHaoMaValue(entity);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_ZsDxStatusItem:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetZsDxStatusValue(entity, this._itemConfig.SplitNumberOfDX);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_Zs012StatusItem:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetZs012StatusValue(entity);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleCell_HqItem:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetHqValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleCell_RepeatedNumber:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetRepeatNumItemValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemMinValue, this._itemConfig.ItemMaxValue, ref _lastItemIndex, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleCell_LinkNumber:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetLinkNumItemValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_SumItemGroup:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetSumSingleValueGroup(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_SX:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetSXStatusItemValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_JJ:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetJJStatusItemValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_FW:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetFWStatusItemValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_HB:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.HBSingleValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemMinValue, this._itemConfig.ItemMaxValue, ref _lastItemIndex, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleCell_ZF:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.ZFSingleCell(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemMinValue, this._itemConfig.ItemMaxValue, ref _lastItemIndex, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleCell_FJ31X7SanQu:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetFj31x7sanqu(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleCell_FJ36X7SanQu:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetFj36x7sanqu(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_ShengXiao:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.Getdf6j1sx(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleCell_Hd15x5SanQU:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.Gethd15x5sanqu(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleCell_NY36x7Sanqu:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.Getny36x7sanqu(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_Hd11x5Yq:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.Gethd15x5Yq(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount, 1, 5);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_Hd11x5Eq:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.Gethd15x5Eq(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount, 6, 10);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_Hd11x5Sq:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.Gethd15x5Sq(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount, 11, 15);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleCell_Hz012:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetHz012Value(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_K3sbt:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetK3sbtValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount, this._itemConfig.ItemString);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleCell_K3ebt:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetEbtValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_JoValue:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetJoValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_DxValue:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetDxValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount, this._itemConfig.SplitNumberOfDX); ;
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_ZhValue:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetZhValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_DxjoValue:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetDxjoValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.SplitNumberOfDX, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;

                case ChartItemType.SingleValue_XsValue://小数个数
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetXsValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount, this._itemConfig.SplitNumberOfDX);
                        yes = true;
                    }
                    break;

                case ChartItemType.SingleValue_HsValue://合数个数
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetHsValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;

                case ChartItemType.SingleValue_OsValue://偶数个数
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetOsValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_SJP://升降平
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.SJPSingleValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemMinValue, this._itemConfig.ItemMaxValue, ref _lastItemIndex, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_HwSjp://升降平
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.HwSjpSingleValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemMinValue, this._itemConfig.ItemMaxValue, ref _lastItemIndex, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                //2016-12-28后新增
                //快乐扑克_开奖号
                case ChartItemType.SingleValue_KLPKKJValue:
                    var _klpk_entity1 = entity as GP_KLPK3_ShanDong;
                    if (_klpk_entity1 != null)
                    {
                        this._itemValue = SingleValueFunction.GetTCKLPK_KaiJiangHao(_klpk_entity1, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_KLPKXTValue:
                    var _klpk_entity2 = entity as GP_KLPK3_ShanDong;
                    if (_klpk_entity2 != null)
                    {
                        this._itemValue = SingleValueFunction.GetTCKLPK3_XingTai(_klpk_entity2);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_KLPKXTFBValue:
                    var _klpk_entity3 = entity as GP_KLPK3_ShanDong;
                    if (_klpk_entity3 != null)
                    {
                        this._itemValue = SingleValueFunction.GetTCKLPK3_XingTaiFenBu(_klpk_entity3);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_KLPKHZValue:
                    this._itemValue = SingleValueFunction.GetTCKLPK3_Hz(entity as GP_KLPK3_ShanDong);
                    yes = true;
                    break;
                case ChartItemType.SingleValue_Max:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetMaxSingleValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_Min:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetMinSingleValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_Avg:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetAvgSingleValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_Hkh:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetHkhSingleValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_Hkc:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetHkcSingleValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_Whz:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetWhzSingleValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_KLPKHSValue:
                    this._itemValue = SingleValueFunction.GetTCKLPK3OpenCodeSuit(entity as GP_KLPK3_ShanDong,
                            this._itemConfig.IndexStart, this._itemConfig.IndexEnd);
                    yes = true;
                    break;
                case ChartItemType.SingleValue_XYSCColor:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetTCXYSCOpenCodeColor(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_XYSC012:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetTCXYSCOpenCodeDivide(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
                case ChartItemType.SingleValue_YTDJZuXuan:
                    if (!entity.OpenCode.Contains(-1))
                    {
                        this._itemValue = SingleValueFunction.GetTCYTDJOpenCodeZuXuan(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount);
                        yes = true;
                    }
                    break;
            }
            if (yes)
                this._LocalEntity = entity;
            return yes;
        }

        /// <summary>
        /// 单列项计算项值
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="nextentity"></param>
        /// <returns></returns>
        protected bool SetSingleValueAndMiss(TEntity entity, TEntity nextentity)
        {
            if (!SetSingleValue(entity, nextentity))
                return false;
            //求遗漏
            if (1 < this._itemConfig.ItemCount)
                switch (this._itemConfig.FuntionType)
                {
                    case ChartItemType.SingleValue_SumItemGroup:
                        for (var i = this._itemConfig.ItemCount - 1; i >= 0; i--)
                        {
                            this._localMiss[i]++;
                            if (Convert.ToInt32(this._itemValue) >=
                                Convert.ToInt32(this._itemConfig.ItemString[i].Split('|')[0]) &&
                                Convert.ToInt32(this._itemValue) <=
                                Convert.ToInt32(this._itemConfig.ItemString[i].Split('|')[1]))
                            {
                                if (null != this._lastMiss)
                                    this._lastMiss[i] = this._localMiss[i];//上期遗漏
                                this._localMiss[i] = 0;//重置当前遗漏
                                if (null != this._times)
                                    this._times[i]++;//出现次数加1
                                this._itemIndex = i;//设置项值索引号
                            }
                            //最大遗漏
                            if (null != this._maxMiss)
                                if (this._localMiss[i] > this._maxMiss[i])
                                { this._maxMiss[i] = this._localMiss[i]; }
                            //this._avgMiss[i] = this._maxMiss[i] / (this._times[i] + 1);//计算平均遗漏
                            if (null != this._avgMiss && null != this._times)
                                this._avgMiss[i] = (this._recordCount - this._times[i]) / (this._times[i] + 1);//计算平均遗漏
                        }
                        break;
                    case ChartItemType.SingleValue_QuJianFenBu:
                        for (var i = this._itemConfig.ItemCount - 1; i >= 0; i--)
                        {
                            this._localMiss[i]++;//当前遗漏
                            if (Convert.ToInt32(this._itemValue) >=
                                Convert.ToInt32(this._itemConfig.ItemString[i].Split('|')[0]) &&
                                Convert.ToInt32(this._itemValue) <=
                                Convert.ToInt32(this._itemConfig.ItemString[i].Split('|')[1]))
                            {
                                if (null != this._lastMiss)
                                    this._lastMiss[i] = this._localMiss[i];//上期遗漏
                                this._localMiss[i] = 0;//重置当前遗漏
                                if (null != this._times)
                                    this._times[i]++;//出现次数加1
                                this._itemIndex = i;//设置项值索引号
                            }
                            //最大遗漏
                            if (null != this._maxMiss)
                                if (this._localMiss[i] > this._maxMiss[i])
                                { this._maxMiss[i] = this._localMiss[i]; }
                            //this._avgMiss[i] = this._maxMiss[i] / (this._times[i] + 1);//计算平均遗漏
                            if (null != this._avgMiss && null != this._times)
                                this._avgMiss[i] = (this._recordCount - this._times[i]) / (this._times[i] + 1);//计算平均遗漏
                        }
                        break;
                    default:
                        for (var i = this._itemConfig.ItemCount - 1; i >= 0; i--)
                        {
                            this._localMiss[i]++;
                            if (this._itemValue == this._itemConfig.ItemString[i])
                            {
                                if (null != this._lastMiss)
                                    this._lastMiss[i] = this._localMiss[i];//上期遗漏
                                this._localMiss[i] = 0;//重置当前遗漏
                                if (null != this._times)
                                    this._times[i]++;//出现次数加1
                                this._itemIndex = i;//设置项值索引号
                            }
                            //最大遗漏
                            if (null != this._maxMiss)
                                if (this._localMiss[i] > this._maxMiss[i])
                                { this._maxMiss[i] = this._localMiss[i]; }
                            //this._avgMiss[i] = this._maxMiss[i] / (this._times[i] + 1);//计算平均遗漏
                            if (null != this._avgMiss && null != this._times)
                                this._avgMiss[i] = (this._recordCount - this._times[i]) / (this._times[i] + 1);//计算平均遗漏
                        }
                        break;
                }
            return true;
        }

        #endregion

        #region 多值项计算项值及遗漏计算
        /// <summary>
        /// 设置多值项 项的值
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="index">项值索引</param>
        /// <param name="missNumber">遗漏</param>
        protected bool SetMultiValueAndMiss(TEntity entity, ref int[] index, ref int[] missNumber)
        {
            bool yes = false;
            yes = !entity.OpenCode.Contains(-1);
            switch (this._itemConfig.FuntionType)
            {
                #region 全国性彩种
                case ChartItemType.MultiValue_OpenCodeItem:
                    MultiValueFunction.SetOpenCodeItemValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount, this._itemConfig.ItemMinValue, this._itemConfig.ItemMaxValue, ref index, ref missNumber, ref this._lastMiss, ref this._maxMiss, ref this._times, ref this._avgMiss, this._recordCount);
                    break;
                case ChartItemType.MultiValue_LinkNumber:
                    MultiValueFunction.SetLinkNumberValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount, this._itemConfig.ItemMinValue, this._itemConfig.ItemMaxValue, ref index, ref missNumber, ref this._lastMiss, ref this._maxMiss, ref this._times, ref this._avgMiss, this._recordCount);
                    break;
                case ChartItemType.MultiValue_RepeatNumber:
                    MultiValueFunction.SetRepeatNumberValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount, this._itemConfig.ItemMinValue, this._itemConfig.ItemMaxValue, ref index, ref missNumber, ref this._lastMiss, ref this._maxMiss, ref this._times, ref this._avgMiss, this._recordCount, ref this._LastOpentCode, ref this._lastItemIndex);
                    break;
                case ChartItemType.MultiValue_ZheHaoNumber:
                    MultiValueFunction.SetZheHaoHaoNumberValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount, this._itemConfig.ItemMinValue, this._itemConfig.ItemMaxValue, ref index, ref missNumber, ref this._lastMiss, ref this._maxMiss, ref this._times, ref this._avgMiss, this._recordCount, ref this._LastOpentCode, ref this._lastItemIndex);
                    break;
                case ChartItemType.MultiValue_XieLianHaoNumber:
                    MultiValueFunction.SetXieLianHaoNumberValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount, this._itemConfig.ItemMinValue, this._itemConfig.ItemMaxValue, ref index, ref missNumber, ref this._lastMiss, ref this._maxMiss, ref this._times, ref this._avgMiss, this._recordCount, ref this._LastOpentCode, ref this._lastItemIndex);
                    break;
                case ChartItemType.MultiValue_XieTiaoHaoNumber:
                    MultiValueFunction.SetTiaoHaoNumberValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount, this._itemConfig.ItemMinValue, this._itemConfig.ItemMaxValue, ref index, ref missNumber, ref this._lastMiss, ref this._maxMiss, ref this._times, ref this._avgMiss, this._recordCount, ref this._LastOpentCode, ref this._lastItemIndex);
                    break;
                case ChartItemType.MultiValue_ShuSanLianHaoNumber:
                    MultiValueFunction.SetShuSanLianHaoNumberValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount, this._itemConfig.ItemMinValue, this._itemConfig.ItemMaxValue, ref index, ref missNumber, ref this._lastMiss, ref this._maxMiss, ref this._times, ref this._avgMiss, this._recordCount, ref this._LastOpentCode, ref this._lastItemIndex);
                    break;
                case ChartItemType.MultiValue_ShuTiaoHaoNumber:
                    MultiValueFunction.SetShuTiaoHaoNumberValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount, this._itemConfig.ItemMinValue, this._itemConfig.ItemMaxValue, ref index, ref missNumber, ref this._lastMiss, ref this._maxMiss, ref this._times, ref this._avgMiss, this._recordCount, ref this._LastOpentCode, ref this._lastItemIndex);
                    break;
                case ChartItemType.MultiValue_K3ebt:
                    MultiValueFunction.SetK3ebtItemValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount, this._itemConfig.ItemMinValue, this._itemConfig.ItemMaxValue, ref index, ref missNumber, ref this._lastMiss, ref this._maxMiss, ref this._times, ref this._avgMiss, this._recordCount, this._itemConfig.ItemString);
                    break;
                case ChartItemType.MultiValue_Sbtxt:
                    MultiValueFunction.SetSbtxtItemValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount, this._itemConfig.ItemMinValue, this._itemConfig.ItemMaxValue, ref index, ref missNumber, ref this._lastMiss, ref this._maxMiss, ref this._times, ref this._avgMiss, this._recordCount, this._itemConfig.ItemString);
                    break;
                case ChartItemType.MultiValue_Ebtxt:
                    MultiValueFunction.SetEbtxtItemValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount, this._itemConfig.ItemMinValue, this._itemConfig.ItemMaxValue, ref index, ref missNumber, ref this._lastMiss, ref this._maxMiss, ref this._times, ref this._avgMiss, this._recordCount, this._itemConfig.ItemString);
                    break;
                #endregion

                #region 高频彩种
                case ChartItemType.MultiValue_KL12:
                    MultiValueFunction.SetKL12NumberValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount, this._itemConfig.ItemMinValue, this._itemConfig.ItemMaxValue, ref index, ref missNumber, ref this._lastMiss, ref this._maxMiss, ref this._times, ref this._avgMiss, this._recordCount, ref this._LastOpentCode, ref this._lastItemIndex);
                    break;

                //2016-12-28新增
                //快乐扑克3-号码分布
                case ChartItemType.MultiValue_KLPKHMFBValue:
                    var _klpk_entity1 = entity as GP_KLPK3_ShanDong;
                    if (_klpk_entity1 != null)
                        MultiValueFunction.GetTCKLPK3_HaoMaFengBu(_klpk_entity1, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount, this._itemConfig.ItemMinValue, this._itemConfig.ItemMaxValue, ref index, ref missNumber, ref this._lastMiss, ref this._maxMiss, ref this._times, ref this._avgMiss, this._recordCount);
                    break;
                case ChartItemType.SingleValue_KLPKHZFBValue:
                    SingleValueFunction.GetTCKLPK3_HZFB(entity as GP_KLPK3_ShanDong, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount, this._itemConfig.ItemMinValue, this._itemConfig.ItemMaxValue, ref index, ref missNumber, ref this._lastMiss, ref this._maxMiss, ref this._times, ref this._avgMiss, this._recordCount);
                    yes = true;
                    break;
                    #endregion
            }
            if (yes)
                this._LocalEntity = entity;
            return yes;
        }
        #endregion

        #region 特殊项的计算
        /// <summary>
        /// 特殊项的计算
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="index"></param>
        /// <param name="itemIndex"></param>
        /// <returns></returns>
        protected bool SetSpecialValue(TEntity entity, ref int[] index, ref int[] itemIndex)
        {
            bool yes = false;
            yes = !entity.OpenCode.Contains(-1);
            switch (this._itemConfig.FuntionType)
            {
                case ChartItemType.SpecialValue_FC3D012_4:
                    SpecialValueFunction.SpecialValue_FC3D012_4(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount, this._itemConfig.ItemMinValue, this._itemConfig.ItemMaxValue, ref index);
                    break;
                case ChartItemType.SpecialValue_TCP3012_4:
                    SpecialValueFunction.SpecialValue_FC3D012_4(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount, this._itemConfig.ItemMinValue, this._itemConfig.ItemMaxValue, ref index);
                    break;
                case ChartItemType.SpecialValue_FCSSQ_ChuHaoPL:
                    SpecialValueFunction.SpecialValue_FCSSQ_ChuHaoPL(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount, this._itemConfig.ItemMinValue, this._itemConfig.ItemMaxValue, ref index, ref itemIndex);
                    break;
                case ChartItemType.SpecialValue_TCDLT_ChuHaoPL:
                    SpecialValueFunction.SpecialValue_TCDLT_ChuHaoPL(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.ItemCount, this._itemConfig.ItemMinValue, this._itemConfig.ItemMaxValue, ref index, ref itemIndex);
                    break;
            }
            if (yes)
                this._LocalEntity = entity;
            return yes;
        }
        #endregion

        #region 样式设置
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isValue"></param>
        /// <param name="fomart"></param>
        /// <param name="attr"></param>
        /// <param name="itemValue"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        protected string GetHtml(bool isValue, string fomart, string attr, string itemValue, int index)
        {
            string value = "";

            switch (this._itemConfig.FuntionType)
            {
                #region 多值
                case ChartItemType.MultiValue_ZheHaoNumber:
                    value = CssValueFunction.MultiValue_ZheHaoNumber(this._LocalEntity, this._itemConfig, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.MultiValue_XieLianHaoNumber:
                    value = CssValueFunction.MultiValue_XieLianHaoNumber(this._LocalEntity, this._itemConfig, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.MultiValue_XieTiaoHaoNumber:
                    value = CssValueFunction.MultiValue_XieTiaoHaoNumber(this._LocalEntity, this._itemConfig, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.MultiValue_ShuSanLianHaoNumber:
                    value = CssValueFunction.MultiValue_ShuSanLianHaoNumber(this._LocalEntity, this._itemConfig, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.MultiValue_KLPKHMFBValue:
                case ChartItemType.MultiValue_OpenCodeItem:
                    //多值多值开奖号
                    value = CssValueFunction.MultiValue_OpenCodeItem(this._LocalEntity, this._itemConfig, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.MultiValue_LinkNumber:
                    //多值多列重号
                    value = CssValueFunction.MultiValue_LinkNumber(this._LocalEntity, this._itemConfig, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.MultiValue_ShuTiaoHaoNumber:
                    //多值多列坚跳号
                    value = CssValueFunction.MultiValue_ShuTiaoHaoNumber(this._LocalEntity, this._itemConfig, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //多值多列重号分布
                case ChartItemType.MultiValue_RepeatNumber:
                    value = CssValueFunction.MultiValue_RepeatNumber(this._LocalEntity, this._itemConfig, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.MultiValue_K3ebt:
                    value = CssValueFunction.SetK3ebtItemValue(this._LocalEntity, this._itemConfig, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.MultiValue_Ebtxt:
                    value = CssValueFunction.SetEbtxtItemValue(this._LocalEntity, this._itemConfig, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.MultiValue_Sbtxt:
                    value = CssValueFunction.SetSbtxtItemValue(this._LocalEntity, this._itemConfig, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                #endregion

                #region 单值
                //单值单列期号
                case ChartItemType.Term_TermItem:
                    value = CssValueFunction.SingleCell_TermItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值单列和尾
                case ChartItemType.SingleCell_HeWeiItem:
                    value = CssValueFunction.SingleCell_HeWeiItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值单列开奖号码
                case ChartItemType.SingleCell_OpenCodeItem:
                    value = CssValueFunction.SingleCell_OpenCodeItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值单列012比
                case ChartItemType.SingleCell_ProportionOf012Item:
                    value = CssValueFunction.SingleCell_ProportionOf012Item(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值单列奇偶比例
                case ChartItemType.SingleCell_ProportionOfJoItem:
                    value = CssValueFunction.SingleCell_ProportionOfJoItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值单列大小比例
                case ChartItemType.SingleCell_ProportionOfDxItem:
                    value = CssValueFunction.SingleCell_ProportionOfDxItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值单列质合比例
                case ChartItemType.SingleCell_ProportionOfZhItem:
                    value = CssValueFunction.SingleCell_ProportionOfZhItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值单列跨度
                case ChartItemType.SingleCell_SpanItem:
                    value = CssValueFunction.SingleCell_SpanItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值单列组三跨度
                case ChartItemType.SingleCell_ZSSpanItem:
                    value = CssValueFunction.SingleCell_ZSSpanItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值单列和值
                case ChartItemType.SingleCell_SumItem:
                    value = CssValueFunction.SingleCell_SumItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值多列开奖号码和值奇偶分布
                case ChartItemType.SingleValue_HzJoStatusItem:
                    value = CssValueFunction.SingleValue_HzJoStatusItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值多列开奖号码和值大小分布
                case ChartItemType.SingleValue_HzDxStatusItem:
                    value = CssValueFunction.SingleValue_HzDxStatusItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.SingleValue_DxStatusItem:
                    break;
                case ChartItemType.SingleValue_JoStatusItem:
                    break;
                //单值单列期试机号
                case ChartItemType.SingleCell_ShiJiHao:
                    value = CssValueFunction.SingleCell_ShiJiHaoItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值多列试机号分布
                case ChartItemType.SingleValue_ShiJiHao:
                    value = CssValueFunction.SingleValue_ShiJiHao(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值多列组三形态
                case ChartItemType.SingleValue_ZsStatusItem:
                    value = CssValueFunction.SingleValue_ZsStatusItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值多列组三奇偶形态
                case ChartItemType.SingleValue_ZsJoStatusItem:
                    value = CssValueFunction.SingleValue_ZsJoStatusItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值单列开奖号012路值
                case ChartItemType.SingleCell_012StatusItem:
                    value = CssValueFunction.SingleCell_012StatusItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值多列和尾分布项
                case ChartItemType.SingleValue_HeWeiItem:
                    value = CssValueFunction.SingleValue_HeWeiItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.SingleValue_Hw012:
                    value = CssValueFunction.SingleValue_HeWei012Item(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值多列奇偶状态项
                case ChartItemType.SingleValue_JiOuStatusItem:
                    value = CssValueFunction.SingleValue_JiOuStatusItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值多列大小状态项
                case ChartItemType.SingleValue_DaXiaoStatusItem:
                    value = CssValueFunction.SingleValue_DaXiaoStatusItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值多列012分布项
                case ChartItemType.SingleValue_Number012StatusItem:
                    value = CssValueFunction.SingleValue_Number012StatusItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值多列号码项
                case ChartItemType.SingleValue_NumberItem:
                    value = CssValueFunction.SingleValue_NumberItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值多列多于两个号码跨度
                case ChartItemType.SingleValue_SpanItem:
                    value = CssValueFunction.SingleValue_SpanItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值多列两个号码跨度
                case ChartItemType.SingleValue_SpanNumberItem:
                    value = CssValueFunction.SingleValue_SpanNumberItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值多列开奖号码和值分布
                case ChartItemType.SingleValue_SumItem:
                    value = CssValueFunction.SingleValue_SumItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值单列开奖号码质合项
                case ChartItemType.SingleCell_ZhiHeStatusItem:
                    value = CssValueFunction.SingleCell_ZhiHeStatusItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值多列开奖号码质合分布项
                case ChartItemType.SingleValue_ZhiHeStatusItem:
                    value = CssValueFunction.SingleValue_ZhiHeStatusItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值多列组三类型
                case ChartItemType.SingleValue_ZuHeStatusItem:
                    value = CssValueFunction.SingleValue_ZuHeStatusItem(this._LocalEntity, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值单列AC值
                case ChartItemType.SingleCell_Ac:
                    value = CssValueFunction.SingleCell_Ac(this._LocalEntity, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值单列双色球三区比
                case ChartItemType.SingleCell_SanQu:
                    value = CssValueFunction.SingleCell_SanQu(this._LocalEntity, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值单列福建31选7三区比
                case ChartItemType.SingleCell_FJ31X7SanQu:
                    value = CssValueFunction.SingleCell_FJ31X7SanQu(this._LocalEntity, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值单列福建36选7三区比
                case ChartItemType.SingleCell_FJ36X7SanQu:
                    value = CssValueFunction.SingleCell_FJ36X7SanQu(this._LocalEntity, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值单列华东15选5三区比
                case ChartItemType.SingleCell_Hd15x5SanQU:
                    value = CssValueFunction.SingleCell_HD15X5SanQu(this._LocalEntity, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值单列南粤36选7三区比
                case ChartItemType.SingleCell_NY36x7Sanqu:
                    value = CssValueFunction.SingleCell_NY36x7SanQu(this._LocalEntity, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值单列AC值奇偶值
                case ChartItemType.SingleCell_AcJiOu:
                    value = CssValueFunction.SingleCell_AcJiOu(this._LocalEntity, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值单列AC值质合值
                case ChartItemType.SingleCell_AcZhiHe:
                    value = CssValueFunction.SingleCell_AcZhiHe(this._LocalEntity, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值单列AC值012路
                case ChartItemType.SingleCell_Ac012Lu:
                    value = CssValueFunction.SingleCell_Ac012Lu(this._LocalEntity, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.SingleValue_QuJianFenBu:
                    break;
                //和尾奇偶状态
                case ChartItemType.SingleValue_HeWeiJiOu:
                    value = CssValueFunction.SingleValue_HeWeiJiOu(this._LocalEntity, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.SingleValue_HeWeiDx:
                    value = CssValueFunction.SingleValue_HeWeiDx(this._LocalEntity, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值单列试机号和值项
                case ChartItemType.SingleCell_ShiJiHaoHzItem:
                    value = CssValueFunction.SingleCell_ShiJiHaoHzItem(this._LocalEntity, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值单列试机号跨度项
                case ChartItemType.SingleCell_ShiJiHaoSpanItem:
                    value = CssValueFunction.SingleCell_ShiJiHaoSpanItem(this._LocalEntity, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值单列试机号奇偶比项
                case ChartItemType.SingleCell_ProportionOfShiJiHaoJoItem:
                    value = CssValueFunction.SingleCell_ProportionOfShiJiHaoJoItem(this._LocalEntity, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值单列试机号大小比项
                case ChartItemType.SingleCell_ProportionOfShiJiHaoDxItem:
                    value = CssValueFunction.SingleCell_ProportionOfShiJiHaoDxItem(this._LocalEntity, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值多列试机号大小比项
                case ChartItemType.SingleValue_ShiJiHaoTypeItem:
                    value = CssValueFunction.SingleValue_ShiJiHaoTypeItem(this._LocalEntity, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值单列组三遗漏
                case ChartItemType.SingleCell_ZsMissItem:
                    value = CssValueFunction.SingleCell_ZsMissItem(this._LocalEntity, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值单列组三号码
                case ChartItemType.SingleCell_ZsHaoMaItem:
                    value = CssValueFunction.SingleCell_ZsHaoMaItem(this._LocalEntity, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值多列组三大小形态
                case ChartItemType.SingleValue_ZsDxStatusItem:
                    value = CssValueFunction.SingleValue_ZsDxStatusItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值多列组三012形态
                case ChartItemType.SingleValue_Zs012StatusItem:
                    value = CssValueFunction.SingleValue_Zs012StatusItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                // 单值单列后区号码
                case ChartItemType.SingleCell_HqItem:
                    value = CssValueFunction.SingleCell_HqItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值单列重号
                case ChartItemType.SingleCell_RepeatedNumber:
                    value = CssValueFunction.SingleCell_RepeatedNumber(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //单值单列连号
                case ChartItemType.SingleCell_LinkNumber:
                    value = CssValueFunction.SingleCell_LinkNumber(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //和值(区间)分布
                case ChartItemType.SingleValue_SumItemGroup:
                    value = CssValueFunction.SingleValue_SumItemGroup(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //和值正幅
                case ChartItemType.SingleCell_ZF:
                    value = CssValueFunction.SingleCell_ZF(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //生肖
                case ChartItemType.SingleValue_ShengXiao:
                    value = CssValueFunction.SingleValue_ShengXiao(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.SingleValue_Hd11x5Yq:
                    value = CssValueFunction.SingleValue_Hd11x5Yq(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.SingleValue_Hd11x5Eq:
                    value = CssValueFunction.SingleValue_Hd11x5Eq(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.SingleValue_Hd11x5Sq:
                    value = CssValueFunction.SingleValue_Hd11x5Sq(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.SingleCell_Hz012:
                    value = CssValueFunction.SingleCell_Hz012(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.SingleValue_K3sbt:
                    value = CssValueFunction.SingleCell_K3sbt(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.SingleCell_K3ebt:
                    value = CssValueFunction.SingleCell_K3ebt(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.SingleValue_JoValue:
                case ChartItemType.SingleValue_OsValue:
                    value = CssValueFunction.SingleValue_JoValue(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.SingleValue_DxValue:
                    value = CssValueFunction.SingleValue_DxValue(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.SingleValue_XsValue:
                    value = CssValueFunction.SingleValue_DxValue(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.SingleValue_ZhValue:
                case ChartItemType.SingleValue_HsValue:
                    value = CssValueFunction.SingleValue_ZhValue(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.SingleValue_DxjoValue:
                    value = CssValueFunction.SingleValue_DxJoValue(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                #endregion

                #region 特殊

                case ChartItemType.SpecialValue_FC3D012_4:
                    value = CssValueFunction.SpecialValue_FC3D012_4(this._LocalEntity, this._itemConfig, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.SpecialValue_TCP3012_4:
                    value = CssValueFunction.SpecialValue_TCP3012_4(this._LocalEntity, this._itemConfig, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.SpecialValue_FCSSQ_ChuHaoPL:
                    value = CssValueFunction.SpecialValue_FCSSQ_ChuHaoPL(this._LocalEntity, this._itemConfig, this._cssConfig, this._ItemIndex, fomart);
                    break;

                #endregion

                #region 高频
                case ChartItemType.MultiValue_KL12:
                    value = CssValueFunction.MultiValue_KL12(this._LocalEntity, this._itemConfig, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.SingleValue_HB://单值多列回摆
                    value = CssValueFunction.SingleValue_HB(this._LocalEntity, this._itemConfig, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.SingleValue_SJP:
                    value = CssValueFunction.SingleValue_SJP(this._LocalEntity, this._itemConfig, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.SingleValue_HwSjp:
                    value = CssValueFunction.SingleValue_HwSjp(this._LocalEntity, this._itemConfig, isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.SingleValue_Max:
                    value = CssValueFunction.SingleValue_MaxItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.SingleValue_Min:
                    value = CssValueFunction.SingleValue_MinItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.SingleValue_Avg:
                    value = CssValueFunction.SingleValue_AvgItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.SingleValue_Hkh:
                    value = CssValueFunction.SingleValue_HkhItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.SingleValue_Hkc:
                    value = CssValueFunction.SingleValue_HkcItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                case ChartItemType.SingleValue_Whz:
                    value = CssValueFunction.SingleValue_WhzItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                //2016-12-28新增
                //快乐扑克3 开奖号
                case ChartItemType.SingleValue_KLPKKJValue:
                    if (!isValue) return "";
                    var _kjAttrs = itemValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    value = string.Format(fomart, _kjAttrs[0], "", _kjAttrs[1]);
                    break;
                //单值多列大小状态项
                case ChartItemType.SingleValue_KLPKHSValue:
                    value = CssValueFunction.SingleValue_KLPKHS_StatusItem(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
                #endregion

                default://没有以默认样式显示
                    value = CssValueFunction.GetCssValue(isValue, fomart, attr, this._cssConfig, itemValue, index);
                    break;
            }
            return value;
        }
        #endregion

        /// <summary>
        /// 根据是否画线和是否是项值判断画线（只有项值才画线）
        /// </summary>
        /// <param name="isValue">是否是项值</param>
        /// <param name="color">画线颜色(有默认值)</param>
        /// <returns></returns>
        protected string GetlgroupAndColor(bool isValue, string color)
        {
            if (this._itemConfig.DrawLine && isValue)
            {
                return " lgroup=\"" + this._cssConfig.Id + "\" lcolor=\"" + color + "\"";
            }
            return "";
        }
    }
}

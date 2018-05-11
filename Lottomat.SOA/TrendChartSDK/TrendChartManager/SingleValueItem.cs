using System.Text;
using TrendChartSDK.Entity.Base;
using TrendChartSDK.Entity.TrendChart;
using TrendChartSDK.Entity.TrendChartData;
using TrendChartSDK.Interface;

namespace TrendChartSDK.TrendChartManager
{
    /// <summary>
    /// 单值项(单值多列项)
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class SingleValueItem<TEntity> : ChartItemRepository<TEntity>, IChartItem<TEntity> where TEntity : LotteryOpenCode
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="cssConfig"></param>
        /// <param name="itemConfig"></param>
        public override void Init(ChartCssConfigInfo cssConfig, TrendChartItemInfo itemConfig)
        {
            DefaultInit(cssConfig, itemConfig);
        }

        /// <summary>
        /// 初始化遗漏
        /// </summary>
        /// <param name="trendChartData"></param>
        /// <param name="i"></param>
        public override void MissDataInit(TrendChartData trendChartData, int i)
        {
            DefaultMissDataInit(trendChartData, i);
        }

        /// <summary>
        /// 获取遗漏数据
        /// </summary>
        /// <param name="missDataType"></param>
        /// <returns></returns>
        public override string GetMissData(MissDataType missDataType)
        {
            return GetDefaultMissData(missDataType);
        }

        /// <summary>
        /// 设置项值
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="nextentity"></param>
        /// <returns></returns>
        public override bool SetItemValue(TEntity entity,TEntity nextentity)
        {
            return SetSingleValueAndMiss(entity, nextentity);
        }

        /// <summary>
        /// 设置项值
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool SetItemValue(TEntity entity)
        {
            return SetSingleValueAndMiss(entity,null);
        }

        /// <summary>
        /// 去格式化字符串
        /// </summary>
        /// <param name="fomart"></param>
        /// <param name="cssConfig"></param>
        /// <returns></returns>
        public override string GetFomartString(string fomart, ChartCssConfigInfo cssConfig = null)
        {
            var html = GetFomartHtml(fomart, cssConfig);

            //var attr = "";
            //if (this._itemConfig.DrawLine)
            //{
            //    attr = " lgroup=\"" + this._itemConfig.Id + "\" lcolor=\"" + html.Item4 + "\"";
            //}

            var sp = new StringBuilder((html.Item1.Length + 40) * this._itemConfig.ItemCount);
            for (int i = 0; i < this._itemConfig.ItemCount; i++)
            {
                if (_itemValue == "")
                {
                    sp.Append(GetHtml(false, html.Item1, GetlgroupAndColor(false, html.Item4), _localMiss[i].ToString(), i));
                }
                else
                {
                    sp.Append(this._itemIndex == i ? GetHtml(true, html.Item1, GetlgroupAndColor(true, html.Item4), _itemValue, i) : GetHtml(false, html.Item1, GetlgroupAndColor(false, html.Item4), _localMiss[i].ToString(), i));
                }
            }
            return sp.ToString();
        }
    }
}

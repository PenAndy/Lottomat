using System.Text;
using TrendChartSDK.Entity.Base;
using TrendChartSDK.Entity.TrendChart;
using TrendChartSDK.Entity.TrendChartData;
using TrendChartSDK.Interface;

namespace TrendChartSDK.TrendChartManager
{
    class SpecialValueItem<TEntity> : ChartItemRepository<TEntity>, IChartItem<TEntity> where TEntity : LotteryOpenCode
    {
        private new int[] _itemIndex;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="cssConfig"></param>
        /// <param name="itemConfig"></param>
        public override void Init(ChartCssConfigInfo cssConfig, TrendChartItemInfo itemConfig)
        {
            DefaultInit(cssConfig, itemConfig);
        }

        #region 未用
        public override void MissDataInit(TrendChartData TrendChartCofig, int i)
        {

        }

        public override string GetMissData(MissDataType missDataType)
        {
            return "";
        }
        #endregion

        /// <summary>
        /// 设置项值
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool SetItemValue(TEntity entity)
        {
            if (this._ItemIndex == null)
            {
                this._ItemIndex = new int[this._itemConfig.ItemCount];
            }
            _itemIndex = new int[this._itemConfig.ItemCount];
            return SetSpecialValue(entity, ref _itemIndex, ref this._ItemIndex);
        }

        /// <summary>
        /// 设置项值
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="lastentity"></param>
        /// <returns></returns>
        public override bool SetItemValue(TEntity entity, TEntity lastentity)
        {
            if (this._ItemIndex == null)
            {
                this._ItemIndex = new int[this._itemConfig.ItemCount];
            }
            _itemIndex = new int[this._itemConfig.ItemCount];
            return SetSpecialValue(entity, ref _itemIndex, ref this._ItemIndex);
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
            var sp = new StringBuilder((html.Item1.Length + 40) * this._itemConfig.ItemCount);
            switch (this._itemConfig.FuntionType)
            {
                case ChartItemType.SpecialValue_FCSSQ_ChuHaoPL:
                    sp.Append(CssValueFunction.SpecialValue_FCSSQ_ChuHaoPL(this._LocalEntity, this._itemConfig, this._cssConfig, this._ItemIndex, html.Item1));
                    break;
                case ChartItemType.SpecialValue_TCDLT_ChuHaoPL:
                    sp.Append(CssValueFunction.SpecialValue_TCDLT_ChuHaoPL(this._LocalEntity, this._itemConfig, this._cssConfig, this._ItemIndex, html.Item1));
                    break;
                default:
                    for (int i = 0; i < this._itemConfig.ItemCount; i++)
                    {
                        sp.Append(GetHtml(true, html.Item1, "", this._ItemIndex[i].ToString(), i));
                    }
                    break;
            }
            return sp.ToString();
        }
    }
}

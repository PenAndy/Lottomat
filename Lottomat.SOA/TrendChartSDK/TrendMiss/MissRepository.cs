using System.Collections.Generic;
using TrendChartSDK.Common;
using TrendChartSDK.Entity.Base;
using TrendChartSDK.Entity.TrendMiss;
using TrendChartSDK.Interface;

namespace TrendChartSDK.TrendMiss
{
    /// <summary>
    /// 遗漏走势图计算
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class MissRepository<TEntity> : IMissItem<TEntity> where TEntity : LotteryOpenCode
    {
        /// <summary>
        /// 遗漏项配置
        /// </summary>
        protected TrendMissItemInfo _itemConfig;
        /// <summary>
        /// 遗漏数据
        /// </summary>
        protected IList<TrendMissDataInfo> _missData;
        /// <summary>
        /// 是否已执行遗漏操作
        /// </summary>
        protected bool Completed = false;
        /// <summary>
        /// 是否第一期数据
        /// </summary>
        protected bool IsFirst = false;

        #region IMissItem接口
        public abstract void Init(TrendMissItemInfo itemConfig, IList<TrendMissDataInfo> missData);

        public abstract bool SetItemValue(TEntity entity);

        public abstract bool SaveData();

        public abstract IList<TrendMissDataInfo> GetMissDataList();
        #endregion

        /// <summary>
        /// 默认初始化
        /// </summary>
        /// <param name="itemConfig"></param>
        /// <param name="missData"></param>
        protected virtual void DefaultInit(TrendMissItemInfo itemConfig, IList<TrendMissDataInfo> missData)
        {
            if (null == itemConfig)
                return;
            this._itemConfig = itemConfig;
            this.IsFirst = false;
            if (null == missData || 0 >= missData.Count)
            {
                this._missData = new List<TrendMissDataInfo>();
                for (int i = 0; i <= this._itemConfig.ItemCount - 1; i++)
                {
                    this._missData.Add(new TrendMissDataInfo()
                    {
                        ChartId = this._itemConfig.ChartId,
                        ItemValue = this._itemConfig.ItemString[i],
                        OrderBy = i
                    });
                }
                this.IsFirst = true;
                return;
            }
            this._missData = missData;
        }
        /// <summary>
        /// 计算遗漏数据
        /// </summary>
        /// <param name="term">期数</param>
        protected virtual void ComputMissData(long term)
        {
            for (int i = this._missData.Count - 1; i >= 0; i--)
            {
                this._missData[i].Term = term;//期数
                this._missData[i].Cycle = this._itemConfig.Cycle / TypeConverter.StrToDouble(this._itemConfig.ItemCycle[i], 0);//循环周期
                this._missData[i].TimesTheory = TypeConverter.StrToInt(this._itemConfig.ItemCycle[i]) * this._missData[i].RecordCount / this._itemConfig.Cycle;//理论次数
                this._missData[i].Probability = 0 >= this._missData[i].RecordCount ? 0 : (double)this._missData[i].Times / this._missData[i].RecordCount;//出现概率
                this._missData[i].AppearingProbability = 0 >= this._missData[i].AvgMiss ? 0 : (double)this._missData[i].LocalMiss / this._missData[i].AvgMiss;//欲出几率
                this._missData[i].InvestmentValue =
                    this._missData[i].LocalMiss * (TypeConverter.StrToDouble(this._itemConfig.ItemCycle[i], 0) / this._itemConfig.Cycle);//投资价值
                this._missData[i].CoveringProbability = 0 >= (this._missData[i].LastMiss - this._missData[i].LocalMiss) ? 0 :
                    (this._missData[i].LastMiss - this._missData[i].LocalMiss) * (TypeConverter.StrToDouble(this._itemConfig.ItemCycle[i], 0) / this._itemConfig.Cycle);//回补几率
                this._missData[i].ContinuousProbability = 0 == this._missData[i].Times ? 0 : (double)this._missData[i].ContinuousTimes / this._missData[i].Times; //连出概率
                this._missData[i].ContinuousLocalProbability = 0 == this._missData[i].Times ? 0
                    : (0 < this._missData[i].ContinuousTimes ? (double)this._missData[i].ContinuousTimes / this._missData[i].Times * this._missData[i].ContinuousLocalMiss
                        : (double)this._missData[i].ContinuousTimes / this._missData[i].Times * (this._missData[i].Times - 1));//当前连出几率
            }
        }
        /// <summary>
        /// 默认数据保存方式
        /// </summary>
        /// <returns></returns>
        protected virtual bool DefaultSaveData()
        {
            if (this.Completed)
            {
                this.Completed = TrendMissDataService.BatchSave(this._missData);
            }
            return this.Completed;
        }
    }
}

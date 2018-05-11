using System;
using System.Collections.Generic;
using TrendChartSDK.Entity.Base;
using TrendChartSDK.Entity.TrendMiss;
using TrendChartSDK.Interface;

namespace TrendChartSDK.TrendMiss
{
    /// <summary>
    /// 单值项遗漏
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class SingleValueItem<TEntity> : MissRepository<TEntity>, IMissItem<TEntity> where TEntity : LotteryOpenCode
    {
        public override void Init(TrendMissItemInfo itemConfig, IList<TrendMissDataInfo> missData)
        {
            DefaultInit(itemConfig, missData);
        }

        public override bool SetItemValue(TEntity entity)
        {
            Tuple<bool, string> t;
            switch (this._itemConfig.FuntionType)
            {
                case MissItemType.SingleValue_Sum:
                    t = MissItemFunction.SingleValue_Sum<TEntity>(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd);
                    break;
                case MissItemType.SingleValue_JOItem:
                    t = MissItemFunction.SingleValue_JOItem<TEntity>(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd);
                    break;
                case MissItemType.SingleValue_DXItem:
                    t = MissItemFunction.SingleValue_DXItem<TEntity>(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd, this._itemConfig.SplitNumberOfDX);
                    break;
                case MissItemType.SingleValue_ZHItem:
                    t = MissItemFunction.SingleValue_ZHItem<TEntity>(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd);
                    break;
                case MissItemType.SingleValue_KDItem:
                    t = MissItemFunction.SingleValue_KDItem<TEntity>(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd);
                    break;
                case MissItemType.SingleValue_012Item:
                    t = MissItemFunction.SingleValue_012Item<TEntity>(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd);
                    break;
                case MissItemType.SingleValue_HWItem:
                    t = MissItemFunction.SingleValue_HWItem<TEntity>(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd);
                    break;
                default:
                    t = new Tuple<bool, string>(false, "");
                    break;
            }
            
            this.Completed = t.Item1;

            if (!t.Item1)
                return false;

            int index = -1;
            for (int i = 0; i < this._itemConfig.ItemCount; i++)
            {
                this._missData[i].LocalMiss++;//当前遗漏
                this._missData[i].RecordCount++;
                if (t.Item2 == _missData[i].ItemValue)
                {
                    index = i;
                    this._missData[i].LastMiss = this._missData[i].LocalMiss - 1;//上期遗漏
                    this._missData[i].LocalMiss = 0;//重置当前遗漏
                    this._missData[i].Times++;//出现次数加1

                    //---------连出及连出遗漏 begin---------------
                    if (this._missData[i].ItemSelect && i == index)
                    {
                        this._missData[i].ContinuousTimes++;
                        this._missData[i].ContinuousLocalTimes++;
                        this._missData[i].ContinuousLocalMiss = 0;

                        //最大连出次数
                        if (this._missData[i].ContinuousLocalTimes > this._missData[i].ContinuousMaxTimes)
                        { this._missData[i].ContinuousMaxTimes = this._missData[i].ContinuousLocalTimes; }
                    }
                    else
                    {
                        this._missData[i].ContinuousLocalTimes = 0;
                        this._missData[i].ContinuousLocalMiss++;
                        //最大连出遗漏
                        if (this._missData[i].ContinuousLocalMiss > this._missData[i].ContinuousMaxMiss)
                        { this._missData[i].ContinuousMaxMiss = this._missData[i].ContinuousLocalMiss; }
                    }
                    //---------连出及连出遗漏 end---------------
                }
                if (this.IsFirst)
                    this._missData[i].LastMiss = 0;
                //最大遗漏
                if (this._missData[i].LocalMiss > this._missData[i].MaxMiss)
                { this._missData[i].MaxMiss = this._missData[i].LocalMiss; }
                //历史最大遗漏
                if (this._missData[i].LastMiss > this._missData[i].LastMaxMiss)
                { this._missData[i].LastMaxMiss = this._missData[i].LastMiss; }
                this._missData[i].AvgMiss = ((double)(this._missData[i].RecordCount - this._missData[i].Times)) / (this._missData[i].Times + 1);//计算平均遗漏

                //if (this._missData[i].ContinuousTimes == 0)
                //{ this._missData[i].ContinuousLocalMiss = this._missData[i].Times - 1; }

                this._missData[i].ItemSelect = i == index;
            }

            if (this.Completed)
            {
                ComputMissData(entity.Term);
            }
            return true;
        }

        public override bool SaveData()
        {
            return DefaultSaveData();
        }

        public override IList<TrendMissDataInfo> GetMissDataList()
        {
            return this._missData;
        }
    }
}

using System;
using System.Collections.Generic;
using TrendChartSDK.Entity.Base;
using TrendChartSDK.Entity.TrendMiss;
using TrendChartSDK.Interface;

namespace TrendChartSDK.TrendMiss
{
    /// <summary>
    /// 多值项遗漏
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class MultiValueItem<TEntity> : MissRepository<TEntity>, IMissItem<TEntity> where TEntity : LotteryOpenCode
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
                case MissItemType.MultiValue_OpenCodeItem:
                    t = MissItemFunction.SetOpenCodeItemValue(entity, this._itemConfig.IndexStart, this._itemConfig.IndexEnd,
                        this._itemConfig.ItemMinValue, this._itemConfig.ItemMaxValue, this.IsFirst, this._itemConfig.ItemCount, ref this._missData);
                    break;
                default :
                    t = new Tuple<bool, string>(false, "");
                    break;
            }

            this.Completed = t.Item1;

            if (!t.Item1)
                return false;

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

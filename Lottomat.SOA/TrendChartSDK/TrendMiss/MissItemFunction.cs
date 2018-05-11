using System;
using System.Collections.Generic;
using TrendChartSDK.Common;
using TrendChartSDK.Entity.Base;
using TrendChartSDK.Entity.TrendChart;
using TrendChartSDK.Entity.TrendMiss;

namespace TrendChartSDK.TrendMiss
{
    /// <summary>
    /// 遗漏项值计算
    /// </summary>
    public class MissItemFunction
    {
        /// <summary>
        /// 多开奖号码遗漏
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity">彩种开奖</param>
        /// <param name="indexStart"></param>
        /// <param name="indexEnd"></param>
        /// <param name="minNum"></param>
        /// <param name="maxNum"></param>
        /// <param name="index"></param>
        /// <param name="missData"></param>
        [MissFunction("[多值]开奖号码(数字遗漏)项", MissItemType.MultiValue_OpenCodeItem, ChartItemClassName.MultiValue)]
        public static Tuple<bool, string> SetOpenCodeItemValue<TEntity>(TEntity entity, int indexStart, int indexEnd, int minNum, int maxNum, bool isFirst, int count,
            ref IList<TrendMissDataInfo> missData) where TEntity : LotteryOpenCode
        {
            if(entity.OpenCode.Contains(-1))
                return new Tuple<bool, string>(false, "开奖号码错误!"); 

            IList<int> list = new List<int>(entity.OpenCode);
            if (-1 != indexEnd)
            {
                for (int i = list.Count - 1; i >= indexEnd; i--)
                { list.RemoveAt(i); }
            }
            for (int i = 0; i < indexStart; i++)
            { list.RemoveAt(0); }

            int[] index = new int[count];
            int j = 0;
            for (int i = maxNum; i >= minNum; i--)
            {
                j = i - minNum;
                missData[j].LocalMiss++;//当前遗漏
                missData[j].RecordCount++;
                if (list.Contains(i))
                {
                    index[j]++;
                    missData[j].LastMiss = missData[j].LocalMiss - 1;//上期遗漏
                    missData[j].LocalMiss = 0;//重置当前遗漏
                    missData[j].Times++;//出现次数加1

                    //---------以下为连出及连出遗漏---------------
                    missData[j].ContinuousLocalMiss++;
                    if (missData[j].ItemSelect && 1 <= index[j])
                    {
                        missData[j].ContinuousTimes++;
                        missData[j].ContinuousLocalTimes++;
                        missData[j].ContinuousLocalMiss = 0;

                        //最大连出次数
                        if (missData[j].ContinuousLocalTimes > missData[j].ContinuousMaxTimes)
                        { missData[j].ContinuousMaxTimes = missData[j].ContinuousLocalTimes; }
                    }
                    else
                    {
                        missData[j].ContinuousLocalTimes = 0;
                        missData[j].ContinuousLocalMiss++;

                        //最大连出遗漏
                        if (missData[j].ContinuousLocalMiss > missData[j].ContinuousMaxMiss)
                        { missData[j].ContinuousMaxMiss = missData[j].ContinuousLocalMiss; }
                    }
                }
                if (isFirst)
                    missData[j].LastMiss = 0;
                //最大遗漏
                if (missData[j].LocalMiss > missData[j].MaxMiss)
                { missData[j].MaxMiss = missData[j].LocalMiss; }
                //历史最大遗漏
                if (missData[j].LastMiss > missData[j].LastMaxMiss)
                { missData[j].LastMaxMiss = missData[j].LastMiss; }
                missData[j].AvgMiss = ((double)(missData[j].RecordCount - missData[j].Times)) / (missData[j].Times + 1);//计算平均遗漏

                missData[j].ItemSelect = 1 <= index[j];
            }
            return new Tuple<bool, string>(true, "");;
        }

        ///// <summary>
        ///// 单值开奖号码遗漏
        ///// </summary>
        ///// <typeparam name="TEntity"></typeparam>
        ///// <param name="entity"></param>
        ///// <param name="itemConfig"></param>
        ///// <param name="indexStart"></param>
        ///// <param name="indexEnd"></param>
        ///// <param name="count"></param>
        ///// <param name="isFirst"></param>
        ///// <param name="missData"></param>
        ///// <returns></returns>
        //public static bool SetSingleValueItem<TEntity>(TEntity entity, TrendMissItemInfo itemConfig, int indexStart, int indexEnd, int count,
        //    bool isFirst,
        //    ref IList<TrendMissDataInfo> missData) where TEntity : LotteryOpenCode
        //{
        //    IList<int> list = new List<int>(entity.OpenCode);
        //    if (-1 != indexEnd)
        //    {
        //        for (int i = list.Count - 1; i >= indexEnd; i--)
        //        { list.RemoveAt(i); }
        //    }
        //    for (int i = 0; i < indexStart; i++)
        //    { list.RemoveAt(0); }

        //    switch (itemConfig.FuntionType)
        //    {
        //        case MissItemType.SingleValue_Sum:
        //            return SingleValue_Sum<TEntity>(entity, count, isFirst, ref missData, list);
        //        case MissItemType.SingleValue_JOItem:
        //            return SingleValue_JOItem<TEntity>(entity, count, isFirst, ref missData, list);
        //        case MissItemType.SingleValue_DXItem:
        //            return SingleValue_DXItem<TEntity>(entity, itemConfig, count, isFirst, ref missData, list);
        //        case MissItemType.SingleValue_ZHItem:
        //            return SingleValue_ZHItem<TEntity>(entity, count, isFirst, ref missData, list);
        //        case MissItemType.SingleValue_KDItem:
        //            return SingleValue_KDItem<TEntity>(entity, count, isFirst, ref missData, list);
        //        case MissItemType.SingleValue_012Item:
        //            return SingleValue_012Item<TEntity>(entity, count, isFirst, ref missData, list);
        //        case MissItemType.SingleValue_HWItem:
        //            return SingleValue_HWItem<TEntity>(entity, count, isFirst, ref missData, list);
        //    }

        //    return true;
        //}

        /// <summary>
        /// 单值开奖号码(和尾遗漏)
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="indexStart"></param>
        /// <param name="indexEnd"></param>
        /// <returns></returns>
        [MissFunction("[单值]开奖号码(和尾遗漏)项", MissItemType.SingleValue_HWItem, ChartItemClassName.SingleValue)]
        public static Tuple<bool, string> SingleValue_HWItem<TEntity>(TEntity entity, int indexStart, int indexEnd) where TEntity : LotteryOpenCode
        {
            if (entity.OpenCode.Contains(-1))
                return new Tuple<bool, string>(false, "开奖号码错误!");

            IList<int> list = new List<int>(entity.OpenCode);
            if (-1 != indexEnd)
            {
                for (int i = list.Count - 1; i >= indexEnd; i--)
                { list.RemoveAt(i); }
            }
            for (int i = 0; i < indexStart; i++)
            { list.RemoveAt(0); }

            return new Tuple<bool, string>(true, LotteryUtils.GetHWString(list).ToString());
        }

        /// <summary>
        /// 单值开奖号码(012路遗漏)
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="indexStart"></param>
        /// <param name="indexEnd"></param>
        /// <returns></returns>
        [MissFunction("[单值]开奖号码(012路遗漏)项", MissItemType.SingleValue_012Item, ChartItemClassName.SingleValue)]
        public static Tuple<bool, string> SingleValue_012Item<TEntity>(TEntity entity, int indexStart, int indexEnd) where TEntity : LotteryOpenCode
        {
            if (entity.OpenCode.Contains(-1))
                return new Tuple<bool, string>(false, "开奖号码错误!");

            IList<int> list = new List<int>(entity.OpenCode);
            if (-1 != indexEnd)
            {
                for (int i = list.Count - 1; i >= indexEnd; i--)
                {
                    list.RemoveAt(i);
                }
            }

            for (int i = 0; i < indexStart; i++)
            {
                list.RemoveAt(0);
            }

            return new Tuple<bool, string>(true, LotteryUtils.Get012String(list).ToString());
        }

        /// <summary>
        /// 单值开奖号码(跨度遗漏)
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="indexStart"></param>
        /// <param name="indexEnd"></param>
        /// <returns></returns>
        [MissFunction("[单值]开奖号码(跨度遗漏)项", MissItemType.SingleValue_KDItem, ChartItemClassName.SingleValue)]
        public static Tuple<bool, string> SingleValue_KDItem<TEntity>(TEntity entity, int indexStart, int indexEnd) where TEntity : LotteryOpenCode
        {
            if (entity.OpenCode.Contains(-1))
                return new Tuple<bool, string>(false, "开奖号码错误!");

            IList<int> list = new List<int>(entity.OpenCode);
            if (-1 != indexEnd)
            {
                for (int i = list.Count - 1; i >= indexEnd; i--)
                { list.RemoveAt(i); }
            }
            for (int i = 0; i < indexStart; i++)
            { list.RemoveAt(0); }

            return new Tuple<bool, string>(true, LotteryUtils.GetSpan(list).ToString());
        }

        /// <summary>
        /// 单值开奖号码(质合遗漏)
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="indexStart"></param>
        /// <param name="indexEnd"></param>
        /// <returns></returns>
        [MissFunction("[单值]开奖号码(质合遗漏)项", MissItemType.SingleValue_ZHItem, ChartItemClassName.SingleValue)]
        public static Tuple<bool, string> SingleValue_ZHItem<TEntity>(TEntity entity, int indexStart, int indexEnd) where TEntity : LotteryOpenCode
        {
            if (entity.OpenCode.Contains(-1))
                return new Tuple<bool, string>(false, "开奖号码错误!");

            IList<int> list = new List<int>(entity.OpenCode);
            if (-1 != indexEnd)
            {
                for (int i = list.Count - 1; i >= indexEnd; i--)
                { list.RemoveAt(i); }
            }
            for (int i = 0; i < indexStart; i++)
            { list.RemoveAt(0); }

            return new Tuple<bool, string>(true, LotteryUtils.GetZHString(list).ToString());
        }

        /// <summary>
        /// 单值开奖号码(大小遗漏)
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="indexStart"></param>
        /// <param name="indexEnd"></param>
        /// <param name="splitNumberOfDX"></param>
        /// <returns></returns>
        [MissFunction("[单值]开奖号码(大小遗漏)项", MissItemType.SingleValue_DXItem, ChartItemClassName.SingleValue)]
        public static Tuple<bool, string> SingleValue_DXItem<TEntity>(TEntity entity, int indexStart, int indexEnd, int splitNumberOfDX) where TEntity : LotteryOpenCode
        {
            if (entity.OpenCode.Contains(-1))
                return new Tuple<bool, string>(false, "开奖号码错误!");

            IList<int> list = new List<int>(entity.OpenCode);
            if (-1 != indexEnd)
            {
                for (int i = list.Count - 1; i >= indexEnd; i--)
                { list.RemoveAt(i); }
            }
            for (int i = 0; i < indexStart; i++)
            { list.RemoveAt(0); }

            return new Tuple<bool, string>(true, LotteryUtils.GetDXString(list, splitNumberOfDX).ToString());
        }

        /// <summary>
        /// 单值开奖号码(奇偶遗漏)
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="indexStart"></param>
        /// <param name="indexEnd"></param>
        /// <returns></returns>
        [MissFunction("[单值]开奖号码(奇偶遗漏)项", MissItemType.SingleValue_JOItem, ChartItemClassName.SingleValue)]
        public static Tuple<bool, string> SingleValue_JOItem<TEntity>(TEntity entity, int indexStart, int indexEnd) where TEntity : LotteryOpenCode
        {
            if (entity.OpenCode.Contains(-1))
                return new Tuple<bool, string>(false, "开奖号码错误!");

            IList<int> list = new List<int>(entity.OpenCode);
            if (-1 != indexEnd)
            {
                for (int i = list.Count - 1; i >= indexEnd; i--)
                { list.RemoveAt(i); }
            }
            for (int i = 0; i < indexStart; i++)
            { list.RemoveAt(0); }

            return new Tuple<bool, string>(true, LotteryUtils.GetJOString(list).ToString());
        }

        /// <summary>
        /// 单值开奖号码(和值遗漏)
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="indexStart"></param>
        /// <param name="indexEnd"></param>
        /// <returns></returns>
        [MissFunction("[单值]开奖号码(和值遗漏)项", MissItemType.SingleValue_Sum, ChartItemClassName.SingleValue)]
        public static Tuple<bool, string> SingleValue_Sum<TEntity>(TEntity entity, int indexStart, int indexEnd) where TEntity : LotteryOpenCode
        {
            if (entity.OpenCode.Contains(-1))
                return new Tuple<bool, string>(false, "开奖号码错误!");

            IList<int> list = new List<int>(entity.OpenCode);
            if (-1 != indexEnd)
            {
                for (int i = list.Count - 1; i >= indexEnd; i--)
                { list.RemoveAt(i); }
            }
            for (int i = 0; i < indexStart; i++)
            { list.RemoveAt(0); }

            return new Tuple<bool, string>(true, LotteryUtils.GetSum(list).ToString());
        }
        
        ///// <summary>
        ///// 单值项遗漏计算通用方法
        ///// </summary>
        ///// <typeparam name="TEntity"></typeparam>
        ///// <param name="entity"></param>
        ///// <param name="itemValue"></param>
        ///// <param name="count"></param>
        ///// <param name="isFirst"></param>
        ///// <param name="missData"></param>
        ///// <returns></returns>
        //public static bool SetSingleValue<TEntity>(TEntity entity, string itemValue, int count, bool isFirst,
        //    ref IList<TrendMissDataInfo> missData) where TEntity : LotteryOpenCode
        //{
        //    int index = -1;
        //    for (int i = 0; i < count; i++)
        //    {
        //        missData[i].LocalMiss++;//当前遗漏
        //        missData[i].RecordCount++;
        //        if (itemValue == missData[i].ItemValue)
        //        {
        //            index = i;
        //            missData[i].LastMiss = missData[i].LocalMiss;//上期遗漏
        //            missData[i].LocalMiss = 0;//重置当前遗漏
        //            missData[i].Times++;//出现次数加1
        //        }
        //        if (isFirst)
        //            missData[i].LastMiss = 0;
        //        //最大遗漏
        //        if (missData[i].LocalMiss > missData[i].MaxMiss)
        //        { missData[i].MaxMiss = missData[i].LocalMiss; }
        //        //历史最大遗漏
        //        if (missData[i].LastMiss > missData[i].LastMaxMiss)
        //        { missData[i].LastMaxMiss = missData[i].LastMiss; }
        //        missData[i].AvgMiss = ((double)(missData[i].RecordCount - missData[i].Times)) / (missData[i].Times + 1);//计算平均遗漏

        //        //---------以下为连出及连出遗漏---------------
        //        missData[i].ContinuousLocalMiss++;
        //        if (missData[i].ItemSelect && i == index)
        //        {
        //            missData[i].ContinuousTimes++;
        //            missData[i].ContinuousLocalTimes++;
        //            missData[i].ContinuousLocalMiss = 0;
        //        }
        //        //最大连出遗漏
        //        if (missData[i].ContinuousLocalMiss > missData[i].ContinuousMaxMiss)
        //        { missData[i].ContinuousMaxMiss = missData[i].ContinuousLocalMiss; }
        //        //最大连出次数
        //        if (missData[i].ContinuousLocalTimes > missData[i].ContinuousMaxTimes)
        //        { missData[i].ContinuousMaxTimes = missData[i].ContinuousLocalTimes; }

        //        missData[i].ItemSelect = i == index;
        //    }
        //    return true;
        //}
    }
}

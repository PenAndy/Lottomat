using System;
using System.Collections.Generic;
using System.Text;
using TrendChartSDK.Entity.Lottery.QGC;
using TrendChartSDK.Entity.LotterySearchField;
using TrendChartSDK.Entity.TrendMiss;
using TrendChartSDK.Interface;

namespace TrendChartSDK.TrendMiss.LotteryTrendMiss
{
    /// <summary>
    /// 福彩3D遗漏
    /// </summary>
    public class FC3DTrendMiss
    {
        /// <summary>
        /// 生成遗漏图表，生成当前term数据及以后数据
        /// by JNswins 2015-06-10
        /// </summary>
        /// <param name="chartId">遗漏表ID</param>
        /// <param name="term">需要生成走势图期数</param>
        /// <param name="fields">彩种开奖数据额外查询条件</param>
        /// <returns></returns>
        public static Tuple<bool, string> CreateMissData(int chartId, long term, LotterySearchField fields = null)
        {
            //读取遗漏配置
            var trendMissItem = TrendMissItemService.GetMissEntity(chartId);
            if (null == trendMissItem)
                return new Tuple<bool, string>(false, string.Format("未找到【ChartId={0}】遗漏图的项配置数据", chartId));

            var ListToEnd = FC3DService.GetListToEnd(term, fields);
            if (null == ListToEnd || ListToEnd.Count <= 0)
                return new Tuple<bool, string>(false, "未找到有效开奖数据");

            //初始化项
            IMissItem<QG_FC3D> missItem = TrendMissUtils.GetTrendMissClassName<QG_FC3D>(trendMissItem.ClassName);
            int i = 0;
            bool yes = true;
            foreach (var item in ListToEnd)
            {
                //根据参数term期数获取近两期开奖信息TOP 2 [Term]<=term  ORDER BY [Term] DESC
                var list = FC3DService.ToListForTrend(item.Term, fields);
                QG_FC3D info = null;
                IList<TrendMissDataInfo> missDataList = null;
                if (null == list || 0 >= list.Count)
                    continue;
                info = list[0];
                if (2 == list.Count)
                {
                    //取当前期数的上一期的遗漏数据
                    if (0 == i)
                        missDataList = TrendMissDataService.GetMissDataList(chartId, list[1].Term);
                    else
                        missDataList = missItem.GetMissDataList();
                }
                missItem.Init(trendMissItem, missDataList);
                yes = yes && missItem.SetItemValue(info);
                if (!yes)
                    return new Tuple<bool, string>(false, string.Format("开奖号错误：截止期数【term={0}】", item.Term));
                yes = yes && missItem.SaveData();
                if (!yes)
                    return new Tuple<bool, string>(false, string.Format("数据保存出错：截止期数【term={0}】", item.Term));
            }
            return new Tuple<bool, string>(false, string.Format("生成遗漏成功：截止期数【term={0}】", ListToEnd[ListToEnd.Count - 1].Term));
        }

        /// <summary>
        /// 预览遗漏数据
        /// </summary>
        /// <param name="chartId"></param>
        /// <param name="term"></param>
        /// <returns></returns>
        public static string PreViewMissData(int chartId, LotterySearchField fields = null)
        {
            StringBuilder sb = new StringBuilder(20000);
            var trendMissItem = TrendMissItemService.GetMissEntity(chartId);
            if (null == trendMissItem)
                return "";

            IMissItem<QG_FC3D> missItem = TrendMissUtils.GetTrendMissClassName<QG_FC3D>(trendMissItem.ClassName);
            var list = FC3DService.GetListToEnd(0, fields);
            QG_FC3D info = null;
            IList<TrendMissDataInfo> missDataList = null;
            if (null == list || 0 >= list.Count)
                return "";

            bool yes = true;
            for (int i = 0; i < list.Count; i++)
            {
                info = list[i];

                missItem.Init(trendMissItem, missDataList);
                yes = yes && missItem.SetItemValue(info);
                missDataList = missItem.GetMissDataList();
                if (!yes || i == list.Count - 1) //开奖号码不正确或者最后一条数据的时候显示数据
                {
                    if (null != missDataList)
                        foreach (var item in missDataList)
                        {
                            sb.Append("<tr>");
                            sb.Append(string.Format("<td>{0}</td>", item.ItemValue));
                            sb.Append(string.Format("<td>{0}</td>", string.Format("{0:F2}", item.Cycle)));
                            sb.Append(string.Format("<td>{0}|{1}</td>", item.Times, item.TimesTheory));
                            sb.Append(string.Format("<td>{0}</td>", (string.Format("{0:F2}", item.Probability * 100)) + "%"));
                            sb.Append(string.Format("<td>{0}</td>", string.Format("{0:F2}", item.AvgMiss)));
                            sb.Append(string.Format("<td>{0}</td>", item.MaxMiss));
                            sb.Append(string.Format("<td>{0}</td>", item.LastMaxMiss));
                            sb.Append(string.Format("<td>{0}</td>", item.LastMiss));
                            sb.Append(string.Format("<td>{0}</td>", item.LocalMiss));
                            sb.Append(string.Format("<td>{0}</td>", string.Format("{0:F2}", item.AppearingProbability)));
                            sb.Append(string.Format("<td>{0}</td>", string.Format("{0:F2}", item.InvestmentValue)));
                            sb.Append(string.Format("<td>{0}</td>", string.Format("{0:F2}", item.CoveringProbability)));
                            sb.Append(string.Format("<td>{0}</td>", string.Format("{0:F2}", item.ContinuousProbability)));
                            sb.Append(string.Format("<td>{0}</td>", item.ContinuousMaxTimes));
                            sb.Append(string.Format("<td>{0}</td>", item.ContinuousMaxMiss));
                            sb.Append(string.Format("<td>{0}</td>", 0 == item.ContinuousTimes ? item.Times - 1 : item.ContinuousLocalMiss));
                            sb.Append(string.Format("<td>{0}</td>", string.Format("{0:F2}", item.ContinuousLocalProbability)));
                            sb.Append("</tr>");
                        }
                    break;
                }
            }
            return sb.ToString();
        }
    }
}

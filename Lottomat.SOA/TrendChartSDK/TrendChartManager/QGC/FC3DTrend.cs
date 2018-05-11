using System;
using System.Collections.Generic;
using System.Text;
using TrendChartSDK.Entity.Lottery.QGC;
using TrendChartSDK.Entity.LotterySearchField;
using TrendChartSDK.Entity.TrendChart;
using TrendChartSDK.Entity.TrendChartData;
using TrendChartSDK.Interface;

namespace TrendChartSDK.TrendChartManager.QGC
{
    /// <summary>
    /// 福彩3D走势图生成工具
    /// </summary>
    public sealed class FC3DTrend
    {
        //private static int Cid = 1;//Cid用于设置样式值
        private const LotteryType Cid = LotteryType.FC3D;

        /// <summary>
        /// 生成走势图，生成当前term数据及以后数据
        /// </summary>
        /// <param name="chartId">走势图ID</param>
        /// <param name="chartType">走势图类型</param>
        /// <param name="term">需要生成走势图期数</param>
        /// <param name="fields">彩种开奖数据额外查询条件</param>
        /// <returns></returns>
        public static Tuple<bool, string> CreateTrendChart(int chartId, TrendChartType chartType, long term, LotterySearchField fields = null)
        {
            //获取走势图项及配置数据
            var trendChartItem = TrendChartItemService.ToListByChartId(chartId, chartType);
            if (null == trendChartItem || 0 >= trendChartItem.Count)
                return new Tuple<bool, string>(false, string.Format("未找到【ChartId={0}】走势图的项配置数据", chartId));

            int count = trendChartItem.Count;
            IList<IChartItem<QG_FC3D>> chartItem = new List<IChartItem<QG_FC3D>>(count);
            IList<ChartCssConfigInfo> cssConfig = new List<ChartCssConfigInfo>(count);
            foreach (var item in trendChartItem)
            {
                //实例化项类型
                chartItem.Add(TrendChartUtils.GetTrendChartInterface<QG_FC3D>(item.ClassName));
                //获取项CSS配置信息
                cssConfig.Add(ChartCssConfigService.Get(item.FuntionType, item.ChartCssId));
            }

            //各项初始化
            for (int i = 0; i < count; i++)
            {
                trendChartItem[i].Cid = Cid.GetLotteryCId();
                chartItem[i].Init(cssConfig[i], trendChartItem[i]);
            }

            //大于等于term开奖数据
            IList<QG_FC3D> listToEnd = FC3DService.GetListToEnd(term, fields);

            if (null == listToEnd || 0 >= listToEnd.Count)
                return new Tuple<bool, string>(false, "未找到有效开奖数据");

            int j = 0;
            TrendChartData entity = null;
            TrendChartData resultEntity = new TrendChartData
            {
                ChartId = chartId,
                Term = term,
                ChartType = chartType,
                LocalMiss = new string[count],
                LastMiss = new string[count],
                AllMaxMiss = new string[count],
                AllAvgMiss = new string[count],
                AllTimes = new string[count]
            };

            foreach (QG_FC3D currentData in listToEnd)
            {
                //根据参数term期数获取近两期开奖信息TOP 2 [Term]<=term  ORDER BY [Term] DESC
                IList<QG_FC3D> list = FC3DService.ToListForTrend(currentData.Term, fields);

                QG_FC3D info = null;
                info = list[0];

                if (chartId == 10)// || chartId == 22
                {
                    if (info.OpenCode1 == -1 || info.OpenCode2 == -1 || info.OpenCode3 == -1)
                    {
                        var sjh = info.ShiJiHao;
                        if (!string.IsNullOrEmpty(sjh))
                        {
                            var opArr = sjh.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            //info.OpenCode1 = int.Parse(opArr[0]);
                            //info.OpenCode2 = int.Parse(opArr[1]);
                            //info.OpenCode3 = int.Parse(opArr[2]);
                            info.OpenCode[0] = int.Parse(opArr[0]);
                            info.OpenCode[1] = int.Parse(opArr[1]);
                            info.OpenCode[2] = int.Parse(opArr[2]);
                        }
                    }
                }
                if (2 == list.Count)
                {
                    //取当前期数的上一期的走势图信息及遗漏数据
                    entity = resultEntity;
                    if (0 == j)
                        entity = FC3DTrendChartDataService.GetTrendChartDataByTerm(chartId, chartType, list[1].Term);
                }

                bool yes = true;
                var sp = new StringBuilder(20000);
                sp.Append("<tr>");
                for (int i = 0; i < count; i++)
                {
                    //初始化遗漏数据(根据上期结果集计算_entity)
                    chartItem[i].MissDataInit(entity, i);
                    //计算项值及遗漏计算
                    yes = yes && chartItem[i].SetItemValue(info);
                    
                    if (yes)
                    {
                        //结果集赋值
                        resultEntity.LocalMiss[i] = chartItem[i].GetMissData(MissDataType.LocalMiss);
                        resultEntity.LastMiss[i] = chartItem[i].GetMissData(MissDataType.LastMiss);
                        resultEntity.AllMaxMiss[i] = chartItem[i].GetMissData(MissDataType.AllMaxMiss);
                        resultEntity.AllAvgMiss[i] = chartItem[i].GetMissData(MissDataType.AllAvgMiss);
                        resultEntity.AllTimes[i] = chartItem[i].GetMissData(MissDataType.AllTimes);
                        
                        if (i == 1 && chartId == 10 && info.OpenCode1 == -1)
                        {
                            var singleValueItem = chartItem[i] as SingleValueItem<QG_FC3D>;
                            if (singleValueItem != null)
                                singleValueItem._itemValue = "&nbsp;";
                        }
                        sp.Append(chartItem[i].GetFomartString("<td {0}>{1}</td>"));
                    }
                }
                sp.Append("</tr>");
                if (null != entity)
                    resultEntity.RecordCount = entity.RecordCount + 1;
                else
                    resultEntity.RecordCount = 1;

                resultEntity.Term = currentData.Term;
                resultEntity.HtmlData = sp.ToString();

                if (!yes)
                {
                    return new Tuple<bool, string>(false, string.Format("无效开奖数据：数据生成截止期数为【term={0}】", resultEntity.Term));
                }

                yes = yes && DatabaseProvider.GetDbProvider<IFC3DTrendChartDataService>().Save(resultEntity);
                if (!yes)
                {
                    return new Tuple<bool, string>(false, string.Format("数据保存出错：数据保存截止期数为【term={0}】", resultEntity.Term));
                }

                j++;
            }
            return new Tuple<bool, string>(true, string.Format("正常生成完毕,截止期数为：Term={0}", listToEnd[listToEnd.Count - 1].Term));
        }

        /// <summary>
        /// 预览走势图，默认显示近30期数据 (实时计算生成)
        /// </summary>
        /// <param name="chartId"></param>
        /// <param name="chartType"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static string PreViewTrendChart(int chartId, TrendChartType chartType, LotterySearchField fields = null)
        {
            var list = FC3DService.GetListToEnd(0, fields);
            if (null == list || 0 >= list.Count)
                return "";

            //获取走势图项及配置数据
            var trendChartItem = TrendChartItemService.ToListByChartId(chartId, chartType);
            if (null == trendChartItem || 0 >= trendChartItem.Count)
                return "";
            int count = trendChartItem.Count;
            IList<IChartItem<QG_FC3D>> chartItem = new List<IChartItem<QG_FC3D>>(count);
            IList<ChartCssConfigInfo> cssConfig = new List<ChartCssConfigInfo>(count);
            foreach (var item in trendChartItem)
            {
                //实例化项类型
                chartItem.Add(TrendChartUtils.GetTrendChartInterface<QG_FC3D>(item.ClassName));
                //获取项CSS配置信息
                cssConfig.Add(ChartCssConfigService.Get(item.FuntionType, item.ChartCssId));
            }

            //项初始化
            for (int i = 0; i < count; i++)
            {
                trendChartItem[i].Cid = Cid.GetLotteryCId();
                chartItem[i].Init(cssConfig[i], trendChartItem[i]);
            }

            var sp = new StringBuilder(20000);
            int record = list.Count;
            int topSize = null == fields ? 30 : fields.TopSize;
            if (30 > topSize)
                topSize = 30;

            for (int i = 0; i < record; i++)
            {
                for (int j = count - 1; j >= 0; j--)
                {
                    chartItem[j].SetItemValue(list[i]);
                }
                if (i >= record - topSize)
                {
                    sp.Append("<tr>");
                    for (int j = 0; j < count; j++)
                    {
                        sp.Append(chartItem[j].GetFomartString("<td {0}>{1}</td>"));
                    }
                    sp.Append("</tr>");
                }
            }

            return sp.ToString();
        }

        /// <summary>
        /// 预览(012路走势图4走势图)，默认显示近30期数据 (实时计算生成)
        /// </summary>
        /// <param name="chartId"></param>
        /// <param name="chartType"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static string PreViewFC3D_012_4_TrendChart(int chartId, TrendChartType chartType, LotterySearchField fields = null)
        {
            var list = FC3DService.GetListToEnd(0, fields);
            if (null == list || 0 >= list.Count)
                return "";

            //获取走势图项及配置数据
            var trendChartItem = TrendChartItemService.ToListByChartId(chartId, chartType);
            if (null == trendChartItem || 0 >= trendChartItem.Count)
                return "";
            int count = trendChartItem.Count;
            IList<IChartItem<QG_FC3D>> chartItem = new List<IChartItem<QG_FC3D>>(count);
            IList<ChartCssConfigInfo> cssConfig = new List<ChartCssConfigInfo>(count);
            foreach (var item in trendChartItem)
            {
                //实例化项类型
                chartItem.Add(TrendChartUtils.GetTrendChartInterface<QG_FC3D>(item.ClassName));
                //获取项CSS配置信息
                cssConfig.Add(ChartCssConfigService.Get(item.FuntionType, item.ChartCssId));
            }

            //项初始化
            for (int i = 0; i < count; i++)
            {
                trendChartItem[i].Cid = Cid.GetLotteryCId();
                chartItem[i].Init(cssConfig[i], trendChartItem[i]);
            }

            var sp = new StringBuilder(20000);
            int record = list.Count;
            int topSize = null == fields ? 30 : fields.TopSize;

            int row = 0;// 控制一行4个td
            sp.Append("<div class=\"zstableBox\">");
            for (int i = record - topSize; i < record; i++)
            {
                for (int j = count - 1; j >= 0; j--)
                {
                    chartItem[j].SetItemValue(list[i]);
                }
                if (i >= record - topSize)
                {
                    if ((row % 4) == 0)
                    {
                        sp.Append("<tr>");
                    }
                    for (int j = 0; j < count; j++)
                    {
                        sp.Append(chartItem[j].GetFomartString("<td  {0}>{1}</td>"));
                    }
                    if ((row % 4) == 3)
                    {
                        sp.Append("</tr>");
                    }
                    row++;
                }
            }
            sp.Append("</div>");
            return sp.ToString();
        }

        /// <summary>
        /// 预览(012路走势图4走势图)，默认显示近30期数据 (实时计算生成)
        /// </summary>
        /// <param name="chartId"></param>
        /// <param name="chartType"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static string PreViewFC3D_012_4_TrendChart(int chartId, TrendChartType chartType, TrendChartSearchField fields = null)
        {
            var list = FC3DService.GetListToEnd(0, fields);
            if (null == list || 0 >= list.Count)
                return "";
            if (fields != null && fields.StartTerm == 0 && fields.EndTerm == 0)
            {
                fields.EndTerm = (int)list[0].Term;
                fields.StartTerm = (int)list[list.Count - 1].Term;
            }

            //获取走势图项及配置数据
            var trendChartItem = TrendChartItemService.ToListByChartId(chartId, chartType);
            if (null == trendChartItem || 0 >= trendChartItem.Count)
                return "";
            int count = trendChartItem.Count;
            IList<IChartItem<QG_FC3D>> chartItem = new List<IChartItem<QG_FC3D>>(count);
            IList<ChartCssConfigInfo> cssConfig = new List<ChartCssConfigInfo>(count);
            foreach (var item in trendChartItem)
            {
                //实例化项类型
                chartItem.Add(TrendChartUtils.GetTrendChartInterface<QG_FC3D>(item.ClassName));
                //获取项CSS配置信息
                cssConfig.Add(ChartCssConfigService.Get(item.FuntionType, item.ChartCssId));
            }

            //项初始化
            for (int i = 0; i < count; i++)
            {
                trendChartItem[i].Cid = Cid.GetLotteryCId();
                chartItem[i].Init(cssConfig[i], trendChartItem[i]);
            }

            var sp = new StringBuilder(20000);
            int record = list.Count;

            //int topSize = null == fields ? 30 : fields.TopSize;

            //if (30 > topSize) topSize = 30;
            int row = 0;// 控制一行4个td
            sp.Append("<div class=\"zstableBox\">");
            for (int i = 0; i < record; i++)
            {
                for (int j = count - 1; j >= 0; j--)
                {
                    chartItem[j].SetItemValue(list[i]);
                }

                if ((row % 4) == 0)
                {
                    sp.Append("<tr>");
                }
                for (int j = 0; j < count; j++)
                {
                    sp.Append(chartItem[j].GetFomartString("<td  {0}>{1}</td>"));
                }
                if ((row % 4) == 3)
                {
                    sp.Append("</tr>");
                }
                row++;
            }
            sp.Append("</div>");
            return sp.ToString();
        }
    }
}
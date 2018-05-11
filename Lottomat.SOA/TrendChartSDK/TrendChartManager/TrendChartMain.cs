using System;
using System.Collections.Generic;
using System.Linq;
using TrendChartSDK.Common;
using TrendChartSDK.Entity;

namespace TrendChartSDK.TrendChartManager
{
    /// <summary>
    /// 走势图主方法，暴露调用接口
    /// </summary>
    public sealed class TrendChartMain : BaseManager
    {
        #region 走势图彩种大厅
        /// <summary>
        /// 走势图彩种大厅
        /// </summary>
        /// <returns>Json字符串</returns>
        public override string GetTrendChartList()
        {
            string res = string.Empty;
            try
            {
                List<TrendChartListViewEnyity> trendChartList = new List<TrendChartListViewEnyity>();

                List<DataItem> data = GetDataList();
                string[] lotteryTypeCodeArr = LotteryTypeCodeStr.Split(",".ToCharArray());

                foreach (string code in lotteryTypeCodeArr)
                {
                    string[] arr = code.Split("|".ToCharArray());

                    List<TrendChartItems> items = new List<TrendChartItems>();
                    //获取对应彩种
                    List<DataItem> dataItems = data.Where(d => d.Code.Equals(arr[0])).ToList();
                    foreach (DataItem model in dataItems)
                    {
                        int cid = model.LotteryType.GetLotteryCId();
                        //string lotteryName = model.LotteryType.GetEnumDescription();

                        TrendChartItems temp = new TrendChartItems
                        {
                            ChartId = 1,//TODO 根据cid获取
                            CId = cid,
                            TrendChartName = "",//TODO 根据cid获取
                            EnumCode = model.LotteryType.ToString()
                        };
                        items.Add(temp);
                    }

                    trendChartList.Add(new TrendChartListViewEnyity
                    {
                        TypeCode = arr[0],
                        TypeName = arr[1],
                        LotteryItemses = items
                    });
                }

                res = trendChartList.ToJson();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return res;
        }

        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <returns></returns>
        private List<DataItem> GetDataList()
        {
            List<DataItem> lotteryTypes = new List<DataItem>();
            //遍历出LotteryType所有成员
            foreach (string name in Enum.GetNames(typeof(LotteryType)))
            {
                LotteryType type = (LotteryType)Enum.Parse(typeof(LotteryType), name, true);
                //如果包含LotteryCId，则取出
                int cid = type.GetLotteryCId();
                string tableName = type.GetLotteryTableName();
                if (cid != -1)
                {
                    if (tableName.Contains("QG_"))
                    {
                        lotteryTypes.Add(new DataItem
                        {
                            Code = "QGC",
                            LotteryType = type
                        });
                    }
                    else if (tableName.Contains("DF_"))
                    {
                        lotteryTypes.Add(new DataItem
                        {
                            Code = "DFC",
                            LotteryType = type
                        });
                    }
                    //高频彩特殊处理
                    else if (tableName.Contains("GP_"))
                    {
                        if (tableName.Contains("GP_11x5_"))
                        {
                            lotteryTypes.Add(new DataItem
                            {
                                Code = "GPC11X5",
                                LotteryType = type
                            });
                        }
                        else if (tableName.Contains("GP_K3_"))
                        {
                            lotteryTypes.Add(new DataItem
                            {
                                Code = "GPCK3",
                                LotteryType = type
                            });
                        }
                        else if (tableName.Contains("GP_KL10F_"))
                        {
                            lotteryTypes.Add(new DataItem
                            {
                                Code = "GPCKLSF",
                                LotteryType = type
                            });
                        }
                        else if (tableName.Contains("GP_KL12_"))
                        {
                            lotteryTypes.Add(new DataItem
                            {
                                Code = "GPCKL12",
                                LotteryType = type
                            });
                        }
                        else if (tableName.Contains("GP_SSC_"))
                        {
                            lotteryTypes.Add(new DataItem
                            {
                                Code = "GPCSSC",
                                LotteryType = type
                            });
                        }
                        else
                        {
                            lotteryTypes.Add(new DataItem
                            {
                                Code = "GPCQTC",
                                LotteryType = type
                            });
                        }
                    }
                }
            }

            return lotteryTypes;
        }

        private class DataItem
        {
            public string Code { get; set; }
            public LotteryType LotteryType { get; set; }
        }

        #endregion

        #region 获取走势图Html
        /// <summary>
        /// 获取走势图Html
        /// </summary>
        /// <param name="arg">查询参数</param>
        /// <returns></returns>
        public override string GetTrendChartHtml(QueryTrendChartArg arg)
        {
            string res = string.Empty;
            try
            {
                if (arg == null) return res;
                if (arg.ChatrId <= 0) return res;

                //获取走势图详细信息


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return res;
        }

        #endregion
    }
}
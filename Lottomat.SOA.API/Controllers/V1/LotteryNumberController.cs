using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Lottomat.Application.Busines.CommonManage;
using Lottomat.Application.Cache;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.CommonEntity;
using Lottomat.Application.Entity.LotteryNumberManage.Parameter;
using Lottomat.Application.Entity.LotteryNumberManage.ViewModel;
using Lottomat.Application.Entity.SystemManage.ViewModel;
using Lottomat.SOA.API.Controllers.Base;
using Lottomat.Util;
using Lottomat.Util.Extension;
using Lottomat.Utils;
using Lottomat.Utils.Date;

namespace Lottomat.SOA.API.Controllers.V1
{
    /// <summary>
    /// 开奖号相关接口
    /// </summary>
    public class LotteryNumberController : BaseApiController
    {
        #region 实例
        /// <summary>
        /// 字典缓存BLL
        /// </summary>
        private static readonly DataItemCache dataItemCache = new DataItemCache();
        /// <summary>
        /// 公共BLL
        /// </summary>
        private static readonly CommonBLL commonBll = new CommonBLL();

        /// <summary>
        /// 彩种编码
        /// </summary>
        // QGC|全国彩,DFC|地方彩,GPC11X5|11选5,GPCK3|快3,GPCKL12|快乐十二,GPCKLSF|快乐十分,GPCSSC|时时彩,GPCQTC|其他彩种
        private static readonly string LotteryTypeCodeStr = ConfigHelper.GetValue("LotteryCategoryCodingMapping");

        /// <summary>
        /// 锁
        /// </summary>
        private static readonly object _lock = new Object();

        #endregion

        #region 获取系统热门、推荐彩种列表
        /// <summary>
        /// 获取系统热门、推荐彩种列表
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetRecommendationAndHotLottery(BaseParameterEntity arg)
        {
            BaseJson<List<RecommendationAndHotLotteryViewEntity>> resultMsg = new BaseJson<List<RecommendationAndHotLotteryViewEntity>> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };

            Task task = Task.Factory.StartNew(() =>
            {
                Logger(typeof(LotteryNumberController), arg.TryToJson(), "获取系统热门、推荐彩种列表-GetRecommendationAndHotLottery", () =>
                {
                    if (!string.IsNullOrEmpty(arg.t))
                    {
                        if (arg.t.CheckTimeStamp())
                        {
                            string codeList = GetCodeList();

                            //获取所有彩种
                            List<DataItemModel> cache = dataItemCache.GetDataItemList(codeList).OrderBy(d => d.SortCode ?? 0).ToList();
                            List<RecommendationAndHotLotteryViewEntity> res = new List<RecommendationAndHotLotteryViewEntity>();
                            //筛选推荐
                            List<LotteryItems> lotteryItemsesRecommend = (from model in cache
                                                                          where model.IsRecommend
                                                                          select new LotteryItems
                                                                          {
                                                                              Id = model.ItemDetailId,
                                                                              Code = model.ItemValue,
                                                                              Name = model.ItemName,
                                                                              EnumCode = model.SimpleSpelling
                                                                          }).ToList();
                            RecommendationAndHotLotteryViewEntity Recommend = new RecommendationAndHotLotteryViewEntity
                            {
                                Type = 0,
                                Desc = "推荐",
                                LotteryItemses = lotteryItemsesRecommend
                            };
                            res.Add(Recommend);

                            //筛选热门
                            List<LotteryItems> lotteryItemsesHot = (from model in cache
                                                                    where model.IsHot
                                                                    select new LotteryItems
                                                                    {
                                                                        Id = model.ItemDetailId,
                                                                        Code = model.ItemValue,
                                                                        Name = model.ItemName,
                                                                        EnumCode = model.SimpleSpelling
                                                                    }).ToList();
                            RecommendationAndHotLotteryViewEntity Hot = new RecommendationAndHotLotteryViewEntity
                            {
                                Type = 1,
                                Desc = "热门",
                                LotteryItemses = lotteryItemsesHot
                            };
                            res.Add(Hot);

                            resultMsg = new BaseJson<List<RecommendationAndHotLotteryViewEntity>>
                            {
                                Status = (int)JsonObjectStatus.Success,
                                Data = res,
                                Message = JsonObjectStatus.Success.GetEnumText(),
                                BackUrl = null
                            };
                        }
                        else
                        {
                            resultMsg = new BaseJson<List<RecommendationAndHotLotteryViewEntity>>
                            {
                                Status = (int)JsonObjectStatus.Fail,
                                Data = null,
                                Message = JsonObjectStatus.Fail.GetEnumText() + "，无效参数。",
                                BackUrl = null
                            };
                        }
                    }
                    else
                    {
                        resultMsg = new BaseJson<List<RecommendationAndHotLotteryViewEntity>>
                        {
                            Status = (int)JsonObjectStatus.Fail,
                            Data = null,
                            Message = JsonObjectStatus.Fail.GetEnumText() + "，请求参数为空。",
                            BackUrl = null
                        };
                    }
                }, e =>
                {
                    resultMsg = new BaseJson<List<RecommendationAndHotLotteryViewEntity>>
                    {
                        Status = (int)JsonObjectStatus.Exception,
                        Data = null,
                        Message = JsonObjectStatus.Exception.GetEnumText() + "，异常信息：" + e.Message,
                        BackUrl = null
                    };
                });
            });
            task.Wait();

            return resultMsg.TryToJson().ToHttpResponseMessage();
        }
        #endregion

        #region 彩票大厅
        /// <summary>
        /// 彩票大厅
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetAllLotteryList(BaseParameterEntity arg)
        {
            BaseJson<List<AllLotteryListViewEntity>> resultMsg = new BaseJson<List<AllLotteryListViewEntity>> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };

            Task task = Task.Factory.StartNew(() =>
            {
                Logger(typeof(LotteryNumberController), arg.TryToJson(), "彩票大厅-GetAllLotteryList", () =>
                {
                    if (!string.IsNullOrEmpty(arg.t))
                    {
                        if (arg.t.CheckTimeStamp())
                        {
                            List<AllLotteryListViewEntity> res = new List<AllLotteryListViewEntity>();
                            string[] lotteryTypeCodeArr = LotteryTypeCodeStr.Split(",".ToCharArray());

                            foreach (string code in lotteryTypeCodeArr)
                            {
                                string[] arr = code.Split("|".ToCharArray());

                                List<LotteryItems> items = new List<LotteryItems>();
                                //获取对应彩种
                                List<DataItemModel> cache = dataItemCache.GetDataItemList(arr[0]);
                                foreach (DataItemModel model in cache)
                                {
                                    LotteryItems temp = new LotteryItems
                                    {
                                        Id = model.ItemDetailId,
                                        Code = model.ItemValue,
                                        Name = model.ItemName,
                                        EnumCode = model.SimpleSpelling
                                    };
                                    items.Add(temp);
                                }
                                res.Add(new AllLotteryListViewEntity
                                {
                                    TypeCode = arr[0],
                                    TypeName = arr[1],
                                    LotteryItemses = items
                                });
                            }

                            resultMsg = new BaseJson<List<AllLotteryListViewEntity>>
                            {
                                Status = (int)JsonObjectStatus.Success,
                                Data = res,
                                Message = JsonObjectStatus.Success.GetEnumText(),
                                BackUrl = null
                            };
                        }
                        else
                        {
                            resultMsg = new BaseJson<List<AllLotteryListViewEntity>>
                            {
                                Status = (int)JsonObjectStatus.Fail,
                                Data = null,
                                Message = JsonObjectStatus.Fail.GetEnumText() + "，无效参数。",
                                BackUrl = null
                            };
                        }
                    }
                    else
                    {
                        resultMsg = new BaseJson<List<AllLotteryListViewEntity>>
                        {
                            Status = (int)JsonObjectStatus.Fail,
                            Data = null,
                            Message = JsonObjectStatus.Fail.GetEnumText() + "，请求参数type为空。",
                            BackUrl = null
                        };
                    }
                }, e =>
                {
                    resultMsg = new BaseJson<List<AllLotteryListViewEntity>>
                    {
                        Status = (int)JsonObjectStatus.Exception,
                        Data = null,
                        Message = JsonObjectStatus.Exception.GetEnumText() + "，异常信息：" + e.Message,
                        BackUrl = null
                    };
                });
            });

            task.Wait();
            //Tasks.Add(task);

            return resultMsg.TryToJson().ToHttpResponseMessage();
        }

        #endregion

        #region 获取首页展示详细开奖信息的彩种
        /// <summary>
        /// 获取首页展示详细开奖信息的彩种
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetHomeLotteryList(BaseParameterEntity arg)
        {
            BaseJson<List<HomeLotteryListViewEntity>> resultMsg = new BaseJson<List<HomeLotteryListViewEntity>> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };

            Task task = Task.Factory.StartNew(() =>
            {
                Logger(typeof(LotteryNumberController), arg.TryToJson(), "获取首页展示详细开奖信息的彩种-GetHomeLotteryList", () =>
                {
                    if (!string.IsNullOrEmpty(arg.t))
                    {
                        if (arg.t.CheckTimeStamp())
                        {
                            lock (_lock)
                            {
                                string codeList = GetCodeList();

                                //获取所有彩种
                                List<DataItemModel> cache = dataItemCache.GetDataItemList(codeList).OrderBy(d => d.SortCode ?? 0).ToList();
                                //获取类型码-枚举码字典
                                Dictionary<string, SCCLottery> dictionary = GetTypeCodeAndEnumCodeDict(cache, true);
                                //结果集合
                                List<HomeLotteryListViewEntity> res = GetRealData(dictionary);

                                resultMsg = new BaseJson<List<HomeLotteryListViewEntity>>
                                {
                                    Status = (int)JsonObjectStatus.Success,
                                    Data = res,
                                    Message = JsonObjectStatus.Success.GetEnumText(),
                                    BackUrl = null
                                };
                            }
                        }
                        else
                        {
                            resultMsg = new BaseJson<List<HomeLotteryListViewEntity>>
                            {
                                Status = (int)JsonObjectStatus.Fail,
                                Data = null,
                                Message = JsonObjectStatus.Fail.GetEnumText() + "，无效参数。",
                                BackUrl = null
                            };
                        }
                    }
                    else
                    {
                        resultMsg = new BaseJson<List<HomeLotteryListViewEntity>>
                        {
                            Status = (int)JsonObjectStatus.Fail,
                            Data = null,
                            Message = JsonObjectStatus.Fail.GetEnumText() + "，请求参数为空。",
                            BackUrl = null
                        };
                    }
                }, e =>
                {
                    resultMsg = new BaseJson<List<HomeLotteryListViewEntity>>
                    {
                        Status = (int)JsonObjectStatus.Exception,
                        Data = null,
                        Message = JsonObjectStatus.Exception.GetEnumText() + "，异常信息：" + e.Message,
                        BackUrl = null
                    };
                });
            });
            task.Wait();

            return resultMsg.TryToJson().ToHttpResponseMessage();
        }

        /// <summary>
        /// 根据彩种编码获取对应推荐的彩种开奖详情
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetHomeLotteryListByTypeCode(HomeLotteryListByTypeCodeArgEntity arg)
        {
            BaseJson<List<HomeLotteryListViewEntity>> resultMsg = new BaseJson<List<HomeLotteryListViewEntity>> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };

            Task task = Task.Factory.StartNew(() =>
            {
                Logger(typeof(LotteryNumberController), arg.TryToJson(), "获取首页展示详细开奖信息的彩种-GetHomeLotteryList", () =>
                {
                    if (!string.IsNullOrEmpty(arg.t) && !string.IsNullOrEmpty(arg.TypeCode))
                    {
                        if (arg.t.CheckTimeStamp())
                        {
                            lock (_lock)
                            {
                                //获取所有彩种
                                List<DataItemModel> cache = dataItemCache.GetDataItemList(arg.TypeCode).OrderBy(d => d.SortCode ?? 0).ToList();
                                //获取类型码-枚举码字典
                                Dictionary<string, SCCLottery> dictionary = GetTypeCodeAndEnumCodeDict(cache, false, true, false, arg.TypeCode);
                                //结果集合
                                List<HomeLotteryListViewEntity> res = GetRealData(dictionary);

                                resultMsg = new BaseJson<List<HomeLotteryListViewEntity>>
                                {
                                    Status = (int)JsonObjectStatus.Success,
                                    Data = res,
                                    Message = JsonObjectStatus.Success.GetEnumText(),
                                    BackUrl = null
                                };
                            }
                        }
                        else
                        {
                            resultMsg = new BaseJson<List<HomeLotteryListViewEntity>>
                            {
                                Status = (int)JsonObjectStatus.Fail,
                                Data = null,
                                Message = JsonObjectStatus.Fail.GetEnumText() + "，无效参数。",
                                BackUrl = null
                            };
                        }
                    }
                    else
                    {
                        resultMsg = new BaseJson<List<HomeLotteryListViewEntity>>
                        {
                            Status = (int)JsonObjectStatus.Fail,
                            Data = null,
                            Message = JsonObjectStatus.Fail.GetEnumText() + "，请求参数为空。",
                            BackUrl = null
                        };
                    }
                }, e =>
                {
                    resultMsg = new BaseJson<List<HomeLotteryListViewEntity>>
                    {
                        Status = (int)JsonObjectStatus.Exception,
                        Data = null,
                        Message = JsonObjectStatus.Exception.GetEnumText() + "，异常信息：" + e.Message,
                        BackUrl = null
                    };
                });
            });
            task.Wait();

            return resultMsg.TryToJson().ToHttpResponseMessage();
        }

        /// <summary>
        /// 获取彩种编码
        /// </summary>
        /// <returns></returns>
        private static string GetCodeList()
        {
            string codeList = String.Empty;
            string[] lotteryTypeCodeArr = LotteryTypeCodeStr.Split(",".ToCharArray());
            codeList = lotteryTypeCodeArr.Select(code => code.Split("|".ToCharArray())).Aggregate(codeList, (current, arr) => current + (arr[0] + "|"));

            codeList = StringHelper.DelLastChar(codeList, "|");
            return codeList;
        }

        /// <summary>
        /// 获取类型码-枚举码字典
        /// </summary>
        /// <param name="model">系统字典集合</param>
        /// <param name="TypeCode"></param>
        /// <returns></returns>
        private Dictionary<string, SCCLottery> GetTypeCodeAndEnumCodeDict(List<DataItemModel> model, bool IsRecommend = false, bool IsShowHomePage = false, bool IsHot = false, string TypeCode = "")
        {
            string key = IsRecommend ? "__IsRecommend___" : IsShowHomePage ? "__IsShowHomePage__" : IsHot ? "__IsHot__" : "__Public__";
            if (!string.IsNullOrEmpty(TypeCode))
                key += TypeCode + "__";

            //类型码-枚举
            Dictionary<string, SCCLottery> dictionary = Cache.Factory.CacheFactory.Cache().GetCache<Dictionary<string, SCCLottery>>(CacheKeyEnum.TypeCodeAndEnumCodeDict.ToString() + key);

            if (dictionary == null || dictionary.Count == 0)
            {
                dictionary = new Dictionary<string, SCCLottery>();

                //枚举码
                List<string> enumCode = new List<string>();
                //类型码
                List<string> typeCode = new List<string>();

                //筛选推荐
                if (IsRecommend)
                {
                    enumCode = model.Where(d => d.IsRecommend).Select(d => d.SimpleSpelling).ToList();
                    typeCode = model.Where(d => d.IsRecommend).Select(d => d.ItemValue).ToList();
                }
                //筛选二级首页显示彩种
                if (IsShowHomePage)
                {
                    enumCode = model.Where(d => d.IsShowHomePage).Select(d => d.SimpleSpelling).ToList();
                    typeCode = model.Where(d => d.IsShowHomePage).Select(d => d.ItemValue).ToList();
                }
                //筛选热门
                if (IsHot)
                {
                    enumCode = model.Where(d => d.IsHot).Select(d => d.SimpleSpelling).ToList();
                    typeCode = model.Where(d => d.IsHot).Select(d => d.ItemValue).ToList();
                }

                //枚举列表
                List<SCCLottery> lotteryType = enumCode.Select(s => (SCCLottery)Enum.Parse(typeof(SCCLottery), s, true)).ToList();

                for (int i = 0; i < typeCode.Count; i++)
                {
                    if (!dictionary.ContainsKey(typeCode[i]))
                    {
                        dictionary.Add(typeCode[i], lotteryType[i]);
                    }
                }
                Cache.Factory.CacheFactory.Cache().WriteCache(dictionary, CacheKeyEnum.TypeCodeAndEnumCodeDict.ToString() + key, DateTimeHelper.Now.AddDays(30));
            }
            return dictionary;
        }

        /// <summary>
        /// 组装最终数据
        /// </summary>
        /// <param name="dictionary">彩票类型码-枚举码</param>
        /// <returns></returns>
        private List<HomeLotteryListViewEntity> GetRealData(Dictionary<string, SCCLottery> dictionary)
        {
            List<HomeLotteryListViewEntity> res = new List<HomeLotteryListViewEntity>();
            foreach (KeyValuePair<string, SCCLottery> pair in dictionary)
            {
                string typeCode = pair.Key;
                SCCLottery lottery = pair.Value;
                //组装查询语句
                string sql = GetSeleteSQL(lottery);
                //查询结果
                DataTable o = commonBll.ExcuteSqlDataTable(sql, DatabaseLinksEnum.LotteryNumber, null);
                if (o.Rows.Count > 0)
                {
                    //Trace.WriteLine(o.Rows[0].ItemArray);

                    res.Add(GetOpenCodeRealDataItem(o, lottery, typeCode));
                }
            }
            return res;
        }

        /// <summary>
        /// 组装开奖详情
        /// </summary>
        /// <param name="data">最新一期开奖详情</param>
        /// <param name="type">彩票类型</param>
        /// <param name="typeCode">类型码</param>
        /// <returns></returns>
        private HomeLotteryListViewEntity GetOpenCodeRealDataItem(DataTable data, SCCLottery type, string typeCode)
        {
            //枚举码
            string enumCode = type.ToString();
            List<LotteryBallItem> ballItems = new List<LotteryBallItem>();
            //有特殊球的情况下
            if (LotteryBallTypeDict.ContainsKey(enumCode))
            {
                GetOpenCodeValue(data.Rows[0], enumCode, out ballItems);
            }
            else
            {
                //总共球个数
                int total = type.GetEnumText().TryToInt32();

                #region 如果当前彩种最大开奖号大于10，则小于10的开奖号需要在前面加0

                List<int> ballList = new List<int>();
                for (int i = 1; i <= total; i++)
                {
                    ballList.Add(data.Rows[0]["OpenCode" + i].TryToInt32());
                }
                //取出当前最大开奖号
                //TODO 此做法欠妥，如果当前彩种开奖号取值在0-20之间，有可能某一期开奖号全部都小于10
                int max = ballList.Max();
                //是否需要添加0
                bool hasAppendZero = (type != SCCLottery.ShanDongKLPK3 && max >= 10) || type == SCCLottery.GuangDongHC1;
                #endregion

                for (int i = 1; i <= total; i++)
                {
                    int openCode = ballList[i - 1];//data.Rows[0]["OpenCode" + i].TryToInt32();

                    ballItems.Add(new LotteryBallItem
                    {
                        //山东快乐扑克3为特殊球
                        BallType = type == SCCLottery.ShanDongKLPK3 ? LotteryBallType.Poker : LotteryBallType.Normal,
                        OpenCode = hasAppendZero ? openCode.RepairZero() : openCode.ToString()
                    });
                }
            }

            //获取当前彩种配置信息
            SCCConfig config = LotteryConfig.FirstOrDefault(s => s.EnumCode.Equals(enumCode));
            //当前最新一期已经开奖时间
            DateTime currentOpenTime = data.Rows[0]["OpenTime"].TryToDateTime();
            //期数
            string term = data.Rows[0]["Term"].ToStringEx();
            //开奖详情
            string spare = data.Rows[0]["Spare"].ToString().Trim();
            //当前时间
            DateTime nowTime = DateTimeHelper.Now;

            if (config != null)
            {
                //当前彩种每周开奖时间
                string[] openThePrizeOnTheDayOfTheWeek = config.KJTime.Split(",".ToCharArray());
                //下一次开奖时间
                DateTime nextOpenTime = GetNextOpenTime(config, currentOpenTime, openThePrizeOnTheDayOfTheWeek, nowTime);

                //彩种名称
                string lotteryName = string.IsNullOrEmpty(config.LotteryName) ? type.GetEnumDescription() : config.LotteryName;

                //组装三要素,描述
                string keywords, desc;

                #region 从配置文件里面获取三要素（暂时弃用）
                ////组装三要素
                //string keywords = string.Format(ConfigHelper.GetValue("LotteryDetailElementsKeywords"), lotteryName, term);
                ////组装描述
                //string desc = string.Format(ConfigHelper.GetValue("LotteryDetailElementsDescription"), lotteryName, config.KJRate, lotteryName); 
                #endregion

                #region 从数据库获取三要素
                List<SystemTdkViewEntity> tdkData = GetTDKData();
                //Key
                string key = "LotteryTDK_" + enumCode;
                SystemTdkViewEntity tdk = tdkData.FirstOrDefault(t => t.LotteryCode.Equals(key));
                if (tdk != null)
                {
                    desc = tdk.Desc;
                    keywords = tdk.Title;
                }
                else
                {
                    keywords = string.Format(ConfigHelper.GetValue("LotteryDetailElementsKeywords"), lotteryName, term);
                    desc = string.Format(ConfigHelper.GetValue("LotteryDetailElementsDescription"), lotteryName, config.KJRate, lotteryName);
                }
                #endregion

                //组装最终结果
                HomeLotteryListViewEntity res = new HomeLotteryListViewEntity
                {
                    TypeCode = typeCode,
                    CurrentTerm = term,
                    EnumCode = enumCode,
                    CurrentOpenTime = currentOpenTime.ToString("yyyy-MM-dd HH:mm"),
                    ServerTime = nowTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    NextOpenTime = nextOpenTime.ToString("yyyy-MM-dd HH:mm"),
                    //CurrentTotalTerm = config.TimesPerDay,
                    LotteryName = lotteryName,
                    LotteryBallItems = ballItems,
                    //Interval = config.Interval,
                    KJRate = config.KJRate,
                    //OpenThePrizeOnTheDayOfTheWeek = openThePrizeOnTheDayOfTheWeek,
                    //StartThePrizeTimeEveryDay = config.StartHour.RepairZero() + ":" + config.StartMinute.RepairZero(),
                    Keywords = keywords,
                    Desc = desc,
                    Spare = spare
                };
                return res;
            }
            else
            {
                //TODO 容错处理，仅返回开奖号、开奖时间

                //彩种名称
                string lotteryName = type.GetEnumDescription();
                //组装三要素
                string keywords = string.Format(ConfigHelper.GetValue("LotteryDetailElementsKeywords"), lotteryName, term);
                //组装描述
                string desc = string.Format(ConfigHelper.GetValue("LotteryDetailElementsDescription"), lotteryName, "", lotteryName);

                //组装最终结果
                HomeLotteryListViewEntity res = new HomeLotteryListViewEntity
                {
                    TypeCode = typeCode,
                    CurrentTerm = term,
                    EnumCode = enumCode,
                    CurrentOpenTime = currentOpenTime.ToString("yyyy-MM-dd HH:mm"),
                    ServerTime = nowTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    NextOpenTime = nowTime.ToString("yyyy-MM-dd HH:mm"),
                    //CurrentTotalTerm = 1,
                    LotteryName = lotteryName,
                    LotteryBallItems = ballItems,
                    //Interval = 1,
                    KJRate = "",
                    //OpenThePrizeOnTheDayOfTheWeek = new[] { "" },
                    //StartThePrizeTimeEveryDay = "",
                    Keywords = keywords,
                    Desc = desc,
                    Spare = spare
                };
                return res;
            }
        }

        /// <summary>
        /// 计算下一次开奖时间
        /// </summary>
        /// <param name="config">彩种配置文件</param>
        /// <param name="currentOpenTime">最新一期开奖时间</param>
        /// <param name="openThePrizeOnTheDayOfTheWeek">每周那几天开奖数组</param>
        /// <param name="now">当前时间</param>
        /// <returns></returns>
        private DateTime GetNextOpenTime(SCCConfig config, DateTime currentOpenTime, string[] openThePrizeOnTheDayOfTheWeek, DateTime now)
        {
            //今天是星期几
            string week = DateTimeHelper.Now.DayOfWeek.ToString("d");
            //今天星期在数组中的索引
            int pointer = Array.IndexOf(openThePrizeOnTheDayOfTheWeek, week);
            //间隔时间
            int interval = config.Interval;
            //每天期数
            int timesPerDay = config.TimesPerDay;
            //当前彩种今天真实开始开奖时间
            DateTime todayRealStartOpentime = (DateTimeHelper.Now.ToString("yyyy-MM-dd") + " " + config.StartHour + ":" + config.StartMinute).TryToDateTime();
            //下次开奖时间
            DateTime nextOpenTime = default(DateTime);

            //今天将要开奖，计算出将要开奖时间
            if (pointer != -1)
            {
                //如果当前时间小于将要开奖时间，说明还未开奖，返回将要开奖时间
                if (now < todayRealStartOpentime)
                {
                    //Modify By 大师兄 解决高频彩问题
                    if (interval > 0 && timesPerDay > 1)
                    {
                        //计算真实开始开奖时间到现在时间已经开了多少期
                        TimeSpan span = now - todayRealStartOpentime;
                        int total = (int)Math.Floor(span.TotalMinutes * 1.0 / interval) + 1;

                        nextOpenTime = todayRealStartOpentime.AddMinutes(total * interval);
                    }
                    else
                    {
                        nextOpenTime = todayRealStartOpentime;
                    }
                }
                else
                {
                    if (interval > 0 && timesPerDay > 1)
                    {
                        //Modify By 大师兄 解决偶尔开奖号未抓取到，导致中间少了

                        //判断当前时间是否大于数据库最新一条记录开奖时间，如果大于，说明数据正常；如果小于，则说明数据库数据存在异常
                        if (now > currentOpenTime)
                        {
                            //计算当前时间到当前最新一期开奖时间已经开了多少期
                            TimeSpan span = now - currentOpenTime;
                            int total = (int)Math.Floor(Math.Abs(span.TotalMinutes) * 1.0 / interval) + 1;

                            nextOpenTime = currentOpenTime.AddMinutes(total * interval);
                        }
                        else
                        {
                            //Modify By 大师兄 解决当前最新一期开奖时间大于当前时间问题

                            //矫正开奖时间

                            #region 此算法较粗糙，可能会出现当天开奖时间有误差的情况
                            //计算真实开始开奖时间到现在时间已经开了多少期

                            //TimeSpan span = now - todayRealStartOpentime;
                            //int total = (int)Math.Floor(span.TotalMinutes * 1.0 / interval) + 1;

                            //nextOpenTime = todayRealStartOpentime.AddMinutes(total * interval);
                            #endregion

                            //计算当前最新一期开奖时间到现在已经开了多少期
                            TimeSpan span = currentOpenTime - now;
                            int total = (int)Math.Floor(Math.Abs(span.TotalMinutes) * 1.0 / interval) + 1;

                            nextOpenTime = currentOpenTime.AddMinutes(total * interval);
                        }
                    }
                    else
                    {
                        if (openThePrizeOnTheDayOfTheWeek.Length > pointer + 1)
                        {
                            pointer++;
                        }
                        else
                        {
                            pointer = 0;
                        }
                        //解决星期天的问题
                        int week_n = openThePrizeOnTheDayOfTheWeek[pointer].TryToInt32() == 0 ? 7 : openThePrizeOnTheDayOfTheWeek[pointer].TryToInt32();
                        int days = Math.Abs(week_n - week.TryToInt32());

                        nextOpenTime = todayRealStartOpentime.AddDays(days);
                    }
                }
            }
            else
            {
                //今天不开奖，计算出下一次开奖时间
                int week_t = week.TryToInt32();
                for (int i = 0; i < 7; i++)
                {
                    week_t++;
                    week_t = week_t.TryToInt32() <= 6 ? week_t.TryToInt32() : 0;
                    int index = Array.IndexOf(openThePrizeOnTheDayOfTheWeek, week_t.ToString());
                    if (index >= 0)
                    {
                        //解决星期天的问题
                        int week_n = openThePrizeOnTheDayOfTheWeek[index].TryToInt32() == 0 ? 7 : openThePrizeOnTheDayOfTheWeek[index].TryToInt32();
                        int days = Math.Abs(week_n - week.TryToInt32());

                        nextOpenTime = todayRealStartOpentime.AddDays(days);
                        break;
                    }
                }
            }

            return nextOpenTime.AddMinutes(1);
        }

        /// <summary>
        /// 组装开奖号
        /// </summary>
        /// <param name="data">最新一期开奖详情</param>
        /// <param name="enumCode">枚举码</param>
        /// <param name="ballItems">开奖号集合</param>
        private static void GetOpenCodeValue(DataRow data, string enumCode, out List<LotteryBallItem> ballItems)
        {
            List<LotteryBallItem> temp = new List<LotteryBallItem>();

            int[] codeArr = LotteryBallTypeDict[enumCode];
            //正常球个数
            int normal = codeArr[0];
            //蓝球个数
            int blue = codeArr[1];
            //生肖球个数
            int zodiac = codeArr[2];
            //季节球个数
            int season = codeArr[3];
            //方位球个数
            int position = codeArr[4];

            #region 如果当前彩种最大开奖号大于10，则小于10的开奖号需要在前面加0
            //总球数
            int total = normal + blue + zodiac + season + position;
            List<int> ballList = new List<int>();
            for (int i = 1; i <= total; i++)
            {
                ballList.Add(data["OpenCode" + i].TryToInt32());
            }
            //取出当前最大开奖号
            //TODO 此做法欠妥，如果当前彩种开奖号取值在0-20之间，有可能某一期开奖号全部都小于10
            int max = ballList.Max();
            //是否需要添加0
            bool hasAppendZero = (!enumCode.Equals(SCCLottery.ShanDongKLPK3.ToString()) && max >= 10) || enumCode.Equals(SCCLottery.GuangDongHC1.ToString());
            #endregion

            #region 正常球
            if (normal > 0)
            {
                for (int i = 1; i <= normal; i++)
                {
                    int openCode = ballList[i - 1];//data["OpenCode" + i].TryToInt32();

                    temp.Add(new LotteryBallItem
                    {
                        BallType = LotteryBallType.Normal,
                        OpenCode = hasAppendZero ? openCode.RepairZero() : openCode.ToString()
                    });
                }
            }
            #endregion

            #region 蓝球
            if (blue > 0)
            {
                int index = normal;
                //for (int i = blue; i >= 1; i--)
                for (int i = 1; i <= blue; i++)
                {
                    int openCode = ballList[index + i - 1];//data["OpenCode" + (normal + i)].TryToInt32();

                    temp.Add(new LotteryBallItem
                    {
                        BallType = LotteryBallType.Blue,
                        OpenCode = hasAppendZero ? openCode.RepairZero() : openCode.ToString()
                    });
                }
            }
            #endregion

            #region 生肖球
            if (zodiac > 0)
            {
                int index = normal + blue;
                for (int i = zodiac; i >= 1; i--)
                {
                    int openCode = ballList[index + i - 1];//data["OpenCode" + (normal + blue + i)].TryToInt32();

                    temp.Add(new LotteryBallItem
                    {
                        BallType = LotteryBallType.Zodiac,
                        OpenCode = hasAppendZero ? openCode.RepairZero() : openCode.ToString()
                    });
                }
            }
            #endregion

            #region 季节球
            if (season > 0)
            {
                int index = normal + blue + zodiac;
                for (int i = season; i >= 1; i--)
                {
                    int openCode = ballList[index + i - 1];//data["OpenCode" + (normal + blue + zodiac + i)].TryToInt32();

                    temp.Add(new LotteryBallItem
                    {
                        BallType = LotteryBallType.Season,
                        OpenCode = hasAppendZero ? openCode.RepairZero() : openCode.ToString()
                    });
                }
            }
            #endregion

            #region 方位球
            if (position > 0)
            {
                int index = normal + blue + zodiac + season;
                for (int i = position; i >= 1; i--)
                {
                    int openCode = ballList[index + i - 1];//data["OpenCode" + (normal + blue + zodiac + season + i)].TryToInt32();

                    temp.Add(new LotteryBallItem
                    {
                        BallType = LotteryBallType.Position,
                        OpenCode = hasAppendZero ? openCode.RepairZero() : openCode.ToString()
                    });
                }
            }
            #endregion

            ballItems = temp;
        }

        #endregion

        #region 根据彩票枚举码获取最新一期开奖详情
        /// <summary>
        /// 根据彩票枚举码获取最新一期开奖详情
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetTheLatestAwardByEnumCode(TheLatestAwardArgEntity arg)
        {
            BaseJson<HomeLotteryListViewEntity> resultMsg = new BaseJson<HomeLotteryListViewEntity> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };

            Logger(typeof(LotteryNumberController), arg.TryToJson(), "根据彩票枚举码获取最新一期开奖详情-GetTheLatestAwardByEnumCode", () =>
            {
                if (!string.IsNullOrEmpty(arg.t))
                {
                    if (arg.t.CheckTimeStamp())
                    {
                        //通过枚举码查询
                        if (!string.IsNullOrEmpty(arg.EnumCode))
                        {
                            bool isSucc = Enum.TryParse<SCCLottery>(arg.EnumCode, true, out SCCLottery type);
                            //SCCLottery type = (SCCLottery)Enum.Parse(typeof(SCCLottery), arg.EnumCode, true);
                            if (!isSucc)
                            {
                                resultMsg = new BaseJson<HomeLotteryListViewEntity>
                                {
                                    Status = (int)JsonObjectStatus.Fail,
                                    Data = null,
                                    Message = $"参数值{arg.EnumCode}无效。",
                                    BackUrl = null
                                };
                            }
                            else
                            {
                                //查询SQL
                                string sql = !string.IsNullOrEmpty(arg.Term) ? GetSeleteSQL(type, arg.Term) : GetSeleteSQL(type);

                                //查询结果
                                DataTable o = commonBll.ExcuteSqlDataTable(sql, DatabaseLinksEnum.LotteryNumber, null);
                                if (o.Rows.Count > 0)
                                {
                                    HomeLotteryListViewEntity entity = GetOpenCodeRealDataItem(o, type, "");

                                    resultMsg = new BaseJson<HomeLotteryListViewEntity>
                                    {
                                        Status = (int)JsonObjectStatus.Success,
                                        Data = entity,
                                        Message = JsonObjectStatus.Success.GetEnumText(),
                                        BackUrl = null
                                    };
                                }
                                else
                                {
                                    resultMsg = new BaseJson<HomeLotteryListViewEntity>
                                    {
                                        Status = (int)JsonObjectStatus.Success,
                                        Data = null,
                                        Message = JsonObjectStatus.Success.GetEnumText(),
                                        BackUrl = null
                                    };
                                }
                            }
                        }
                        else
                        {
                            resultMsg = new BaseJson<HomeLotteryListViewEntity>
                            {
                                Status = (int)JsonObjectStatus.Fail,
                                Data = null,
                                Message = JsonObjectStatus.Fail.GetEnumText() + "，请求参数EnumCode为空。",
                                BackUrl = null
                            };
                        }
                    }
                    else
                    {
                        resultMsg = new BaseJson<HomeLotteryListViewEntity>
                        {
                            Status = (int)JsonObjectStatus.Fail,
                            Data = null,
                            Message = JsonObjectStatus.Fail.GetEnumText() + "，无效参数。",
                            BackUrl = null
                        };
                    }
                }
                else
                {
                    resultMsg = new BaseJson<HomeLotteryListViewEntity>
                    {
                        Status = (int)JsonObjectStatus.Fail,
                        Data = null,
                        Message = JsonObjectStatus.Fail.GetEnumText() + "，请求参数为空。",
                        BackUrl = null
                    };
                }
            }, e =>
            {
                resultMsg = new BaseJson<HomeLotteryListViewEntity>
                {
                    Status = (int)JsonObjectStatus.Exception,
                    Data = null,
                    Message = JsonObjectStatus.Exception.GetEnumText() + "，异常信息：" + e.Message,
                    BackUrl = null
                };
            });

            return resultMsg.TryToJson().ToHttpResponseMessage();
        }

        #endregion

        #region 获取开机号、试机号
        /// <summary>
        /// 获取开机号、试机号
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetLotteryKJHAndSJH(LotteryKJHAndSJHArgEntity arg)
        {
            BaseJson<List<LotteryKJHAndSJHViewEntity>> resultMsg = new BaseJson<List<LotteryKJHAndSJHViewEntity>> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };

            Logger(typeof(LotteryNumberController), arg.TryToJson(), "获取开机号、试机号-GetLotteryKJHAndSJH", () =>
            {
                if (!string.IsNullOrEmpty(arg.t))
                {
                    if (arg.t.CheckTimeStamp())
                    {
                        //TODO 加上缓存
                        List<LotteryKJHAndSJHViewEntity> list = new List<LotteryKJHAndSJHViewEntity>();

                        string[] enumArr = arg.EnumCode.Split("|".ToCharArray());
                        foreach (string s in enumArr)
                        {
                            bool isSucc = Enum.TryParse<SCCLottery>(s, true, out SCCLottery type);
                            //SCCLottery type = (SCCLottery)Enum.Parse(typeof(SCCLottery), arg.EnumCode, true);
                            if (!isSucc) continue;

                            //组装查询语句
                            string sql = GetSeleteSQL(type, arg);
                            //查询结果
                            DataTable data = commonBll.ExcuteSqlDataTable(sql, DatabaseLinksEnum.LotteryNumber, null);
                            if (data.Rows.Count > 0)
                            {
                                for (int i = 0; i < data.Rows.Count; i++)
                                {
                                    LotteryKJHAndSJHViewEntity res = new LotteryKJHAndSJHViewEntity
                                    {
                                        EnumCode = s,
                                        Term = data.Rows[i]["Term"].ToStringEx(),
                                        Addtime = data.Rows[i]["OpenTime"].TryToDateTimeToString("yyyy-MM-dd"),
                                        KjhAndSjhItems = GetKjhAndSjhItems(data.Rows[i], type)
                                    };

                                    list.Add(res);
                                }
                            }
                        }

                        list = list.OrderByDescending(l => l.Term).ToList();

                        resultMsg = new BaseJson<List<LotteryKJHAndSJHViewEntity>>
                        {
                            Status = (int)JsonObjectStatus.Success,
                            Data = list,
                            Message = JsonObjectStatus.Success.GetEnumText(),
                            BackUrl = null
                        };
                    }
                    else
                    {
                        resultMsg = new BaseJson<List<LotteryKJHAndSJHViewEntity>>
                        {
                            Status = (int)JsonObjectStatus.Fail,
                            Data = null,
                            Message = JsonObjectStatus.Fail.GetEnumText() + "，无效参数。",
                            BackUrl = null
                        };
                    }
                }
                else
                {
                    resultMsg = new BaseJson<List<LotteryKJHAndSJHViewEntity>>
                    {
                        Status = (int)JsonObjectStatus.Fail,
                        Data = null,
                        Message = JsonObjectStatus.Fail.GetEnumText() + "，请求参数为空。",
                        BackUrl = null
                    };
                }
            }, e =>
            {
                resultMsg = new BaseJson<List<LotteryKJHAndSJHViewEntity>>
                {
                    Status = (int)JsonObjectStatus.Exception,
                    Data = null,
                    Message = JsonObjectStatus.Exception.GetEnumText() + "，异常信息：" + e.Message,
                    BackUrl = null
                };
            });

            return resultMsg.TryToJson().ToHttpResponseMessage();
        }

        private List<KJHAndSJHItem> GetKjhAndSjhItems(DataRow data, SCCLottery type)
        {
            List<KJHAndSJHItem> res = new List<KJHAndSJHItem>();
            string sjh = data["ShiJiHao"].ToString();
            string kjh = data["KaiJiHao"].ToString();

            //开机号
            if (!string.IsNullOrEmpty(kjh) && !kjh.Contains("-1"))
            {
                //15 17 20 24 27 33 + 11
                if (kjh.Contains("+"))
                {
                    kjh = kjh.Trim().Replace(" + ", ",").Replace(" ", ",");
                }

                string[] sjhArr = kjh.Split(",".ToCharArray());
                List<int> code = sjhArr.Select(s => s.TryToInt32()).ToList();

                KJHAndSJHItem item = new KJHAndSJHItem
                {
                    Which = 1,
                    TypeName = "开机号",
                    LotteryBallItems = AppendKJHAndSJHBall(kjh, type),
                    SizeRatio = LotteryUtils.GetProportionOfDX(code, GetSizeRatioSplitNumber(type)),
                    ParityRatio = LotteryUtils.GetProportionOfJO(code),
                    SumValue = LotteryUtils.GetSum(code, GetSumNumberCount(type)).ToString()
                };
                res.Add(item);
            }

            //试机号
            if (!string.IsNullOrEmpty(sjh) && !sjh.Contains("-1"))
            {
                if (sjh.Contains("+"))
                {
                    sjh = sjh.Trim().Replace(" + ", ",").Replace(" ", ",");
                }

                string[] sjhArr = sjh.Split(",".ToCharArray());
                List<int> code = sjhArr.Select(s => s.TryToInt32()).ToList();

                KJHAndSJHItem item = new KJHAndSJHItem
                {
                    Which = 2,
                    TypeName = "试机号",
                    LotteryBallItems = AppendKJHAndSJHBall(sjh, type),
                    SizeRatio = LotteryUtils.GetProportionOfDX(code, GetSizeRatioSplitNumber(type)),
                    ParityRatio = LotteryUtils.GetProportionOfJO(code),
                    SumValue = LotteryUtils.GetSum(code, GetSumNumberCount(type)).ToString()
                };
                res.Add(item);
            }

            //开奖号
            res.Add(GetOpenCode(data, type));

            return res;
        }

        private List<LotteryBallItem> AppendKJHAndSJHBall(string sjh, SCCLottery enumCode)
        {
            List<LotteryBallItem> res = new List<LotteryBallItem>();
            string[] sjhArr = sjh.Split(",".ToCharArray());

            #region 是否需要添加0的彩种
            //是否需要添加0
            bool hasAppendZero = enumCode == SCCLottery.SSQ || enumCode == SCCLottery.DLT;
            #endregion

            //有特殊球的情况下
            if (LotteryBallTypeDict.ContainsKey(enumCode.ToString()))
            {
                int[] codeArr = LotteryBallTypeDict[enumCode.ToString()];
                //正常球个数
                int normal = codeArr[0];
                //蓝球个数
                int blue = codeArr[1];

                #region 正常球
                if (normal > 0)
                {
                    for (int i = 1; i <= normal; i++)
                    {
                        int openCode = sjhArr[i - 1].TryToInt32();

                        res.Add(new LotteryBallItem
                        {
                            BallType = LotteryBallType.Normal,
                            OpenCode = hasAppendZero ? openCode.RepairZero() : openCode.ToString()
                        });
                    }
                }
                #endregion

                #region 蓝球
                if (blue > 0)
                {
                    for (int i = blue; i >= 1; i--)
                    {
                        int openCode = sjhArr[normal + i - 1].TryToInt32();

                        res.Add(new LotteryBallItem
                        {
                            BallType = LotteryBallType.Blue,
                            OpenCode = hasAppendZero ? openCode.RepairZero() : openCode.ToString()
                        });
                    }
                }
                #endregion
            }
            else
            {
                //总共球个数
                int total = enumCode.GetEnumText().TryToInt32();
                for (int i = 1; i <= total; i++)
                {
                    int openCode = sjhArr[i - 1].TryToInt32();

                    res.Add(new LotteryBallItem
                    {
                        BallType = LotteryBallType.Normal,
                        OpenCode = hasAppendZero ? openCode.RepairZero() : openCode.ToString()
                    });
                }
            }
            return res;
        }

        private KJHAndSJHItem GetOpenCode(DataRow data, SCCLottery type)
        {
            //枚举码
            string enumCode = type.ToString();
            List<LotteryBallItem> ballItems = new List<LotteryBallItem>();
            //总共球个数
            int total = type.GetEnumText().TryToInt32();
            //有特殊球的情况下
            if (LotteryBallTypeDict.ContainsKey(enumCode))
            {
                GetOpenCodeValue(data, enumCode, out ballItems);
            }
            else
            {
                #region 是否需要添加0的彩种
                //是否需要添加0
                bool hasAppendZero = type == SCCLottery.SSQ || type == SCCLottery.DLT;
                #endregion

                for (int i = 1; i <= total; i++)
                {
                    int openCode = data["OpenCode" + i].TryToInt32();

                    ballItems.Add(new LotteryBallItem
                    {
                        //山东快乐扑克3为特殊球
                        BallType = type == SCCLottery.ShanDongKLPK3 ? LotteryBallType.Poker : LotteryBallType.Normal,
                        OpenCode = hasAppendZero ? openCode.RepairZero() : openCode.ToString()
                    });
                }
            }

            List<int> openCodeList = new List<int>();
            for (int i = 1; i <= total; i++)
            {
                int openCode = data["OpenCode" + i].TryToInt32();
                openCodeList.Add(openCode);
            }

            KJHAndSJHItem itemOpencode = new KJHAndSJHItem
            {
                Which = 3,
                TypeName = "开奖号",
                LotteryBallItems = ballItems,
                SizeRatio = LotteryUtils.GetProportionOfDX(openCodeList, GetSizeRatioSplitNumber(type)),
                ParityRatio = LotteryUtils.GetProportionOfJO(openCodeList),
                SumValue = LotteryUtils.GetSum(openCodeList, GetSumNumberCount(type)).ToString()
            };

            return itemOpencode;
        }

        /// <summary>
        /// 查询开机号、试机号
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage QueryLotteryKJHAndSJH(QueryLotteryKJHAndSJHArgEntity arg)
        {
            BaseJson<List<LotteryKJHAndSJHViewEntity>> resultMsg = new BaseJson<List<LotteryKJHAndSJHViewEntity>> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };

            Logger(typeof(LotteryNumberController), arg.TryToJson(), "查询开机号、试机号-QueryLotteryKJHAndSJH", () =>
            {
                if (!string.IsNullOrEmpty(arg.t))
                {
                    if (arg.t.CheckTimeStamp())
                    {
                        //TODO 加上缓存
                        List<LotteryKJHAndSJHViewEntity> list = new List<LotteryKJHAndSJHViewEntity>();

                        bool isSucc = Enum.TryParse<SCCLottery>(arg.EnumCode, true, out SCCLottery type);
                        //SCCLottery type = (SCCLottery)Enum.Parse(typeof(SCCLottery), arg.EnumCode, true);
                        if (!isSucc)
                        {
                            resultMsg = new BaseJson<List<LotteryKJHAndSJHViewEntity>>
                            {
                                Status = (int)JsonObjectStatus.Fail,
                                Data = null,
                                Message = $"参数值{arg.EnumCode}无效。",
                                BackUrl = null
                            };
                        }
                        else
                        {
                            //组装查询语句
                            string sql = GetSeleteSQL(type, arg);
                            //查询结果
                            DataTable data = commonBll.ExcuteSqlDataTable(sql, DatabaseLinksEnum.LotteryNumber, null);
                            if (data.Rows.Count > 0)
                            {
                                for (int i = 0; i < data.Rows.Count; i++)
                                {
                                    LotteryKJHAndSJHViewEntity res = new LotteryKJHAndSJHViewEntity
                                    {
                                        Term = data.Rows[i]["Term"].ToStringEx(),
                                        Addtime = data.Rows[i]["Addtime"].ToStringEx(),
                                        KjhAndSjhItems = GetKjhAndSjhItems(data.Rows[i], type)
                                    };

                                    list.Add(res);
                                }
                            }

                            list = list.OrderByDescending(l => l.Term).ToList();

                            resultMsg = new BaseJson<List<LotteryKJHAndSJHViewEntity>>
                            {
                                Status = (int)JsonObjectStatus.Success,
                                Data = list,
                                Message = JsonObjectStatus.Success.GetEnumText(),
                                BackUrl = null
                            };
                        }
                    }
                    else
                    {
                        resultMsg = new BaseJson<List<LotteryKJHAndSJHViewEntity>>
                        {
                            Status = (int)JsonObjectStatus.Fail,
                            Data = null,
                            Message = JsonObjectStatus.Fail.GetEnumText() + "，无效参数。",
                            BackUrl = null
                        };
                    }
                }
                else
                {
                    resultMsg = new BaseJson<List<LotteryKJHAndSJHViewEntity>>
                    {
                        Status = (int)JsonObjectStatus.Fail,
                        Data = null,
                        Message = JsonObjectStatus.Fail.GetEnumText() + "，请求参数为空。",
                        BackUrl = null
                    };
                }
            }, e =>
            {
                resultMsg = new BaseJson<List<LotteryKJHAndSJHViewEntity>>
                {
                    Status = (int)JsonObjectStatus.Exception,
                    Data = null,
                    Message = JsonObjectStatus.Exception.GetEnumText() + "，异常信息：" + e.Message,
                    BackUrl = null
                };
            });

            return resultMsg.TryToJson().ToHttpResponseMessage();
        }

        #endregion

        #region 公共私有方法

        /// <summary>
        /// 组装查询语句
        /// </summary>
        /// <param name="type">枚举码</param>
        /// <returns></returns>
        private string GetSeleteSQL(SCCLottery type)
        {
            string res = Cache.Factory.CacheFactory.Cache().GetCache<string>(CacheKeyEnum.QueryNewestLotteryDataSQLByTableName.ToString() + "_" + type.ToString());
            if (string.IsNullOrEmpty(res))
            {
                StringBuilder builder = new StringBuilder();

                int total = type.GetEnumText().TryToInt32();
                string tableName = type.GetSCCLotteryTableName();
                for (int i = 1; i <= total; i++)
                {
                    builder.Append("[OpenCode" + i + "],");
                }
                res = string.Format(GetLotterySqlByTableName, StringHelper.DelLastChar(builder.ToString(), ","), tableName);

                //存入缓存
                Cache.Factory.CacheFactory.Cache().WriteCache(res, CacheKeyEnum.QueryNewestLotteryDataSQLByTableName.ToString() + "_" + type.ToString(), DateTimeHelper.Now.AddDays(30));
            }

            return res;
        }

        /// <summary>
        /// 组装查询语句
        /// </summary>
        /// <param name="type">枚举码</param>
        /// <param name="term">期数</param>
        /// <returns></returns>
        private string GetSeleteSQL(SCCLottery type, string term)
        {
            StringBuilder builder = new StringBuilder();

            int total = type.GetEnumText().TryToInt32();
            string tableName = type.GetSCCLotteryTableName();
            for (int i = 1; i <= total; i++)
            {
                builder.Append("[OpenCode" + i + "],");
            }
            string res = string.Format(GetLotterySqlByTerm, StringHelper.DelLastChar(builder.ToString(), ","), tableName, term);

            return res;
        }

        /// <summary>
        /// 组装查询语句
        /// </summary>
        /// <param name="type">枚举码</param>
        /// <param name="arg"></param>
        /// <returns></returns>
        private string GetSeleteSQL(SCCLottery type, LotteryKJHAndSJHArgEntity arg)
        {
            string res = String.Empty;

            string tableName = type.GetSCCLotteryTableName();

            StringBuilder builder = new StringBuilder();
            int total = type.GetEnumText().TryToInt32();
            for (int i = 1; i <= total; i++)
            {
                builder.Append("[OpenCode" + i + "],");
            }

            if (arg.TotalRecord > 0)
            {
                if (!string.IsNullOrEmpty(arg.StartTime))
                {
                    string time = arg.StartTime.CheckDateTime()
                        ? arg.StartTime
                        : DateTimeHelper.Now.AddDays(-7).ToString("yyyy-MM-dd");

                    res = string.Format(GetLotterySqlByTableNameWithStartTimeAndTop, tableName, time, StringHelper.DelLastChar(builder.ToString(), ","), arg.TotalRecord);
                }
                else
                {
                    res = string.Format(GetLotterySqlByTableNameWithTop, arg.TotalRecord, tableName, StringHelper.DelLastChar(builder.ToString(), ","));
                }
            }
            else if (!string.IsNullOrEmpty(arg.StartTime))
            {
                string time = arg.StartTime.CheckDateTime()
                    ? arg.StartTime
                    : DateTimeHelper.Now.AddDays(-7).ToString("yyyy-MM-dd");

                res = string.Format(GetLotterySqlByTableNameWithStartTime, tableName, time, StringHelper.DelLastChar(builder.ToString(), ","));
            }
            else
            {
                res = string.Format(GetLotterySqlByTableNameWithTop, "20", tableName, StringHelper.DelLastChar(builder.ToString(), ","));
            }

            return res;
        }

        /// <summary>
        /// 组装查询语句
        /// </summary>
        /// <param name="type">枚举码</param>
        /// <param name="arg"></param>
        /// <returns></returns>
        private string GetSeleteSQL(SCCLottery type, QueryLotteryKJHAndSJHArgEntity arg)
        {
            string res = String.Empty;
            string sql = "SELECT {3} [ID],[Term],[ShiJiHao],[KaiJiHao],[Addtime],{0} FROM [dbo].[{1}] WHERE [Term] = {2} ORDER BY Term DESC";

            string tableName = type.GetSCCLotteryTableName();

            StringBuilder builder = new StringBuilder();
            int total = type.GetEnumText().TryToInt32();
            for (int i = 1; i <= total; i++)
            {
                builder.Append("[OpenCode" + i + "],");
            }

            if (!string.IsNullOrEmpty(arg.Term))
            {
                res = string.Format(sql, StringHelper.DelLastChar(builder.ToString(), ","), tableName, arg.Term, "");
            }
            else if (!string.IsNullOrEmpty(arg.Year))
            {
                int.TryParse(arg.Year, out int year);
                year = year == 0 ? DateTime.Now.Year : year <= DateTime.Now.Year ? year : DateTime.Now.Year;

                sql = "SELECT TOP {3} [ID],[Term],[ShiJiHao],[KaiJiHao],[Addtime],{0} FROM [dbo].[{1}] WHERE [OpenTime] LIKE '%{2}%' ORDER BY Term DESC";
                res = string.Format(sql, StringHelper.DelLastChar(builder.ToString(), ","), tableName, year.ToString(), arg.TotalRecord == 0 ? 30 : arg.TotalRecord);
            }
            else if (!string.IsNullOrEmpty(arg.StartTerm) && !string.IsNullOrEmpty(arg.EndTerm))
            {
                long start = arg.StartTerm.TryToInt64();
                long end = arg.EndTerm.TryToInt64();
                sql = "SELECT [ID],[Term],[ShiJiHao],[KaiJiHao],[Addtime],{0} FROM [dbo].[{1}] WHERE Term BETWEEN {2} AND {3} ORDER BY Term DESC";

                if (start <= end)
                {
                    //WHERE Term BETWEEN 2018064 AND 2018067
                    res = string.Format(sql, StringHelper.DelLastChar(builder.ToString(), ","), tableName, start, end);
                }
                else
                {
                    res = string.Format(sql, StringHelper.DelLastChar(builder.ToString(), ","), tableName, end, start);
                }
            }

            return res;
        }
        #endregion

        #region SQL语句

        /// <summary>
        /// 通过表名查询数据为校验后的最新一条数据
        /// </summary>
        private static string GetLotterySqlByTableName = @"SELECT TOP 1 [ID],[Term],[OpenTime],[Spare],{0} FROM [dbo].[{1}] ORDER BY Term DESC ";//WHERE [IsChecked] = 1 AND [IsPassed] = 1 

        /// <summary>
        /// 通过期数查询数据为校验后的一条数据
        /// </summary>
        private static string GetLotterySqlByTerm = @"SELECT [ID],[Term],[OpenTime],[Spare],{0} FROM [dbo].[{1}] WHERE [Term]='{2}'";//[IsChecked] = 1 AND [IsPassed] = 1 AND 

        /// <summary>
        /// 通过表名查询数据为校验后的前n行数据
        /// </summary>
        private static string GetLotterySqlByTableNameWithTop = @"SELECT TOP {0} [ID],[Term],[ShiJiHao],[KaiJiHao],[OpenTime],{2} FROM [dbo].[{1}] ORDER BY Term DESC ";
        /// <summary>
        /// 通过开奖时间查询数据为校验后的所有数据
        /// </summary>
        private static string GetLotterySqlByTableNameWithStartTime = @"SELECT [ID],[Term],[ShiJiHao],[KaiJiHao],[OpenTime],{2} FROM [dbo].[{0}] WHERE DATEDIFF(DAY,'{1}',OpenTime) >= 0 ORDER BY Term DESC";
        /// <summary>
        /// 通过开奖时间查询数据为校验后的所有数据
        /// </summary>
        private static string GetLotterySqlByTableNameWithStartTimeAndTop = @"SELECT TOP {3} [ID],[Term],[ShiJiHao],[KaiJiHao],[OpenTime],{2} FROM [dbo].[{0}] WHERE DATEDIFF(DAY,'{1}',OpenTime) >= 0 ORDER BY Term DESC";
        #endregion
    }
}

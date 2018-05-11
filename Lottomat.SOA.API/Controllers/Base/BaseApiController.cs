using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using Lottomat.Application.Busines.CommonManage;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.LotteryNumberManage.ViewModel;
using Lottomat.Cache.Factory;
using Lottomat.SOA.API.Attribute;
using Lottomat.SOA.API.Filters;
using Lottomat.Util;
using Lottomat.Util.Extension;
using Lottomat.Util.Log;
using Lottomat.Util.WebControl;
using Lottomat.Utils;
using Lottomat.Utils.Date;

namespace Lottomat.SOA.API.Controllers.Base
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2015.11.9 10:45
    /// 描 述：基控Api制器
    /// </summary>
    //[RequireHttps]
    //[RequestAuthorize]
    [TimingActionFilter]
    public abstract class BaseApiController : ApiController, ILogger
    {
        #region 公共实例

        /// <summary>
        /// 配置信息
        /// </summary>
        protected static List<SCCConfig> LotteryConfig => InitLotteryConfig.Init();

        /// <summary>
        /// 公共
        /// </summary>
        private static readonly CommonBLL commonBll = new CommonBLL();
        #endregion

        #region 系统日志
        /// <summary>
        /// 系统日志 主动调用
        /// </summary>
        protected readonly LogHelper _logHelper = new LogHelper(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 系统日志 对try块进行了封装，无返回值
        /// </summary>
        /// <param name="type"></param>
        /// <param name="function">方法名称</param>
        /// <param name="errorHandel">异常处理方式</param>
        /// <param name="tryHandel">调试代码</param>
        /// <param name="catchHandel">异常处理方式</param>
        /// <param name="finallHandel">最终处理方式</param>
        public void Logger(Type type, string argJsonString, string function, Action tryHandel, Action<Exception> catchHandel = null,
            Action finallHandel = null, ErrorHandel errorHandel = ErrorHandel.Continue)
        {
            LogHelper.Logger(type, argJsonString, function, errorHandel, tryHandel, catchHandel, finallHandel);
        }

        /// <summary>
        /// 系统日志 对try块进行了封装，有返回值
        /// </summary>
        /// <param name="type"></param>
        /// <param name="function">方法名称</param>
        /// <param name="errorHandel">异常处理方式</param>
        /// <param name="tryHandel">调试代码</param>
        /// <param name="catchHandel">异常处理方式</param>
        /// <param name="finallHandel">最终处理方式</param>
        public T Logger<T>(Type type, string function, Func<T> tryHandel, Func<Exception, T> catchHandel = null,
            Action finallHandel = null, ErrorHandel errorHandel = ErrorHandel.Continue) where T : new()
        {
            T res = LogHelper.Logger(type, function, errorHandel, tryHandel, catchHandel, finallHandel);

            return res;
        }
        #endregion

        #region 加载特殊球配置文件

        //TODO 待完善 ShanDongKLPK3
        /// <summary>
        /// 默认特殊球字典
        /// <para>依次含义为正常球个数,蓝球个数,生肖球个数,季节球个数,方位球个数。不在这的是全部正常或者其他格式</para>
        /// </summary>
        private static readonly Dictionary<string, int[]> DefaultLotteryBallTypeDict = new Dictionary<string, int[]>() {
            //双色球
            { "SSQ",new []{6,1,0,0,0 }},
            //大乐透
            { "DLT",new []{5,2,0,0,0 }},
            //七乐彩
            { "QLC",new []{7,1,0,0,0 }},
            //东方6+1
            { "DF6J1",new []{6,0,1,0,0}},
            //浙江6+1
            { "ZheJiang6J1",new []{6,1,0,0,0 }},
            //新疆福彩35选7
            { "XinJiangFC35X7",new []{7,1,0,0,0 } },
            //福建体彩36选7
            { "FuJianTC36x7",new []{7,1,0,0,0 } },
            //广东36选7
            { "GuangDong36x7",new []{6,1,0,0,0 } },
            //福建31选7
            { "FuJian31x7",new []{7,1,0,0,0 } },
            //北京快乐8
            { "BeiJingKL8",new []{20,1,0,0,0 } },
            //广东好彩1
            { "GuangDongHC1",new []{1,0,1,1,1 } },
            //广西快乐双彩
            { "GuangXiKLSC",new []{6,1,0,0,0 } },
            //深圳风采
            { "GuangDongSZFC",new []{7,1,0,0,0 } },
            //黑龙江体彩6+1
            { "HeiLongJiangTC6J1",new []{6,1,0,0,0 } },
            //黑龙江36选7
            { "HeiLongJiang36x7",new []{7,1,0,0,0 } },
            //黑龙江P62
            { "HeiLongJiangP62",new []{6,1,0,0,0 } },
            //辽宁风采35选7
            { "LiaoNingFC35X7",new []{7,1,0,0,0 } },
            //新疆新疆风采25选7
            { "XinJiangFC25X7",new []{7,1,0,0,0 } },
        };
        /// <summary>
        /// 特殊球字典
        /// </summary>
        protected static Dictionary<string, int[]> LotteryBallTypeDict => LoadLotteryBallTypeDict();

        /// <summary>
        /// 加载特殊球配置文件
        /// </summary>
        /// <returns></returns>
        private static Dictionary<string, int[]> LoadLotteryBallTypeDict()
        {
            Dictionary<string, int[]> dictionary = Cache.Factory.CacheFactory.Cache().GetCache<Dictionary<string, int[]>>(CacheKeyEnum.SpecialLotteryBallDict.ToString());
            if (dictionary == null || dictionary.Count == 0)
            {
                dictionary = new Dictionary<string, int[]>();

                string configStr = ConfigHelper.GetValue("SpecialLotteryBallDict");
                if (!string.IsNullOrEmpty(configStr))
                {
                    //第一次分割
                    string[] arr1 = configStr.Split("|".ToCharArray());
                    //第二次分割
                    foreach (string s in arr1)
                    {
                        string[] arr2 = s.Split(",".ToCharArray());
                        if (arr2.Length == 6 && !dictionary.ContainsKey(arr2[0]))
                        {
                            dictionary.Add(arr2[0], new[]
                            {
                                arr2[1].TryToInt32(),
                                arr2[2].TryToInt32(),
                                arr2[3].TryToInt32(),
                                arr2[4].TryToInt32(),
                                arr2[5].TryToInt32()
                            });
                        }
                    }
                }
                else
                {
                    dictionary = DefaultLotteryBallTypeDict;
                }

                Cache.Factory.CacheFactory.Cache().WriteCache(dictionary, CacheKeyEnum.SpecialLotteryBallDict.ToString(), DateTimeHelper.Now.AddDays(30));
            }
            return dictionary;
        }

        #endregion

        #region 获取开奖号模板
        /// <summary>
        /// 获取开奖号模板
        /// </summary>
        /// <param name="type"></param>
        /// <param name="openCodeList">开奖号集合</param>
        /// <returns></returns>
        protected string GetOpenCodeTemplate(SCCLottery type, List<int> openCodeList)
        {
            StringBuilder res = new StringBuilder();
            string blueTemplate = "<span class='ball-list blue'>{0}</span>";
            string redTemplate = "<span class='ball-list red'>{0}</span>";

            //快乐扑克3为特殊彩种
            if (type == SCCLottery.ShanDongKLPK3)
                return GetKLPK3OpenCodeTemplate(openCodeList);

            #region 如果当前彩种最大开奖号大于10，则小于10的开奖号需要在前面加0
            //取出当前最大开奖号
            //TODO 此做法欠妥，如果当前彩种开奖号取值在0-20之间，有可能某一期开奖号全部都小于10
            int max = openCodeList.Max();
            //是否需要添加0
            bool hasAppendZero = (type != SCCLottery.ShanDongKLPK3 && max >= 10) || type == SCCLottery.GuangDongHC1;
            #endregion

            //其他彩
            string typeCode = type.ToString();
            if (LotteryBallTypeDict.ContainsKey(typeCode))
            {
                int[] codeArr = LotteryBallTypeDict[typeCode];
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

                #region 正常球
                if (normal > 0)
                {
                    for (int i = 1; i <= normal; i++)
                    {
                        int openCode = openCodeList[i - 1].TryToInt32();

                        res.Append(string.Format(redTemplate, hasAppendZero ? openCode.RepairZero() : openCode.ToString()));
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
                        int openCode = openCodeList[index + i - 1].TryToInt32();

                        res.Append(string.Format(blueTemplate, hasAppendZero ? openCode.RepairZero() : openCode.ToString()));
                    }
                }
                #endregion

                #region 生肖球
                if (zodiac > 0)
                {
                    int index = normal + blue;
                    for (int i = zodiac; i >= 1; i--)
                    {
                        int openCode = openCodeList[index + i - 1].TryToInt32();
                        res.Append(string.Format(blueTemplate, LotteryUtils.GetZodiacByOpenCode(openCode)));
                    }
                }
                #endregion

                #region 季节球
                if (season > 0)
                {
                    int index = normal + blue + zodiac;
                    for (int i = season; i >= 1; i--)
                    {
                        int openCode = openCodeList[index + i - 1].TryToInt32();
                        res.Append(string.Format(blueTemplate, LotteryUtils.GetSeasonByOpenCode(openCode)));

                    }
                }
                #endregion

                #region 方位球
                if (position > 0)
                {
                    int index = normal + blue + zodiac + season;
                    for (int i = position; i >= 1; i--)
                    {
                        int openCode = openCodeList[index + i - 1].TryToInt32();
                        res.Append(string.Format(blueTemplate, LotteryUtils.GetPositionByOpenCode(openCode)));
                    }
                }
                #endregion

            }
            else
            {
                //总共球个数
                int total = type.GetEnumText().TryToInt32();
                for (int i = 1; i <= total; i++)
                {
                    int openCode = openCodeList[i - 1].TryToInt32();
                    res.Append(string.Format(redTemplate, hasAppendZero ? openCode.RepairZero() : openCode.ToString()));
                }
            }

            return res.ToString();
        }

        /// <summary>
        /// 组装快乐扑克3开奖号码
        /// </summary>
        /// <param name="openCodeList"></param>
        /// <returns></returns>
        private string GetKLPK3OpenCodeTemplate(List<int> openCodeList)
        {
            string res = string.Join(",", openCodeList.ToArray());

            return res;
        }

        #endregion

        #region 开奖号相关
        /// <summary>
        /// 获取彩种大小比分割值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected int GetSizeRatioSplitNumber(SCCLottery type)
        {
            int res = -1;
            switch (type)
            {
                case SCCLottery.FC3D:
                    res = 4;
                    break;
                case SCCLottery.SSQ:
                    res = 17;
                    break;
                case SCCLottery.PL3:
                    res = 5;
                    break;
                case SCCLottery.DLT:
                    res = 18;
                    break;
                case SCCLottery.HD15X5:
                    res = 30;
                    break;
            }
            return res;
        }

        /// <summary>
        /// 获取计算和值球个数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected int GetSumNumberCount(SCCLottery type)
        {
            int res = type.GetEnumText().TryToInt32();

            //有特殊球的情况下
            if (LotteryBallTypeDict.ContainsKey(type.ToString()))
            {
                res = LotteryBallTypeDict[type.ToString()][0];
            }
            return res;
        }

        #endregion

        #region 默认返回方法
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected virtual string ToJsonResult(object data)
        {
            return data.ToJson();
        }
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        protected virtual HttpResponseMessage Success(string message)
        {
            return new HttpResponseMessage
            {
                Content =
                    new StringContent(new AjaxResult<string> { type = ResultType.Success, message = message }.ToJson(),
                        Encoding.GetEncoding("UTF-8"), "application/json")
            };
        }
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected virtual HttpResponseMessage Success<T>(string message, T data)
        {
            return new HttpResponseMessage
            {
                Content =
                    new StringContent(
                        new AjaxResult<T> { type = ResultType.Success, message = message, resultdata = data }.ToJson(),
                        Encoding.GetEncoding("UTF-8"), "application/json")
            };
        }
        /// <summary>
        /// 返回失败消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        protected virtual HttpResponseMessage Error(string message)
        {
            return new HttpResponseMessage
            {
                Content =
                    new StringContent(new AjaxResult<string> { type = ResultType.Error, message = message }.ToJson(),
                        Encoding.GetEncoding("UTF-8"), "application/json")
            };
        }
        #endregion

        #region 系统TDK数据源

        /// <summary>
        /// 获取所有TDK数据
        /// </summary>
        /// <returns></returns>
        protected List<SystemTdkViewEntity> GetTDKData()
        {
            List<SystemTdkViewEntity> res = CacheFactory.Cache().GetCache<List<SystemTdkViewEntity>>("SystemTdkCacheKey");
            if (res == null || res.Count == 0)
            {
                string sql = @"select [Name],[Title],[Desc],[Keyword],[LotteryCode] from dbo.Base_SiteTDK";

                DataTable data = commonBll.ExcuteSqlDataTable(sql, DatabaseLinksEnum.Base);

                res = data.DataTableToList<SystemTdkViewEntity>();

                CacheFactory.Cache().WriteCache(res, "SystemTdkCacheKey", DateTime.Now.AddDays(15));
            }

            return res;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Lottomat.Application.Busines.CommonManage;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.CommonEntity;
using Lottomat.Application.Entity.LotteryNumberManage.Parameter;
using Lottomat.Application.Entity.LotteryNumberManage.ViewModel;
using Lottomat.SOA.API.Controllers.Base;
using Lottomat.Util.Extension;
using Lottomat.Utils;
using Lottomat.Utils.Date;

namespace Lottomat.SOA.API.Controllers.V1
{
    /// <summary>
    /// 高频彩其他彩
    /// </summary>
    public class GPCQTCController : BaseApiController
    {
        /// <summary>
        /// 公共BLL
        /// </summary>
        private static readonly CommonBLL commonBll = new CommonBLL();

        #region 获取高频彩其他彩彩种历史记录
        /// <summary>
        /// 获取高频彩其他彩彩种历史记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetGPCQTCHistoryLotteryList(HistoryLotteryArgEnyity arg)
        {
            BaseJson<string> resultMsg = new BaseJson<string> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };

            Logger(typeof(GPCSSCController), arg.TryToJson(), "获取高频彩其他彩彩种历史记录-GetGPCQTCHistoryLotteryList", () =>
            {
                if (!string.IsNullOrEmpty(arg.t))
                {
                    if (arg.t.CheckTimeStamp())
                    {
                        if (!string.IsNullOrEmpty(arg.EnumCode))
                        {
                            bool isSucc = Enum.TryParse<SCCLottery>(arg.EnumCode, true, out SCCLottery type);
                            //SCCLottery type = (SCCLottery)Enum.Parse(typeof(SCCLottery), arg.EnumCode, true);
                            if (!isSucc)
                            {
                                resultMsg = new BaseJson<string>
                                {
                                    Status = (int)JsonObjectStatus.Fail,
                                    Data = null,
                                    Message = $"参数值{arg.EnumCode}无效。",
                                    BackUrl = null
                                };
                            }
                            else
                            {
                                //获取组装完成后的Json字符串
                                string res = GetResultByEnumCode(type, arg);

                                resultMsg = new BaseJson<string>
                                {
                                    Status = (int)JsonObjectStatus.Success,
                                    Data = res.ToString(),
                                    Message = JsonObjectStatus.Success.GetEnumText(),
                                    BackUrl = null
                                };
                            }
                        }
                        else
                        {
                            resultMsg = new BaseJson<string>
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
                        resultMsg = new BaseJson<string>
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
                    resultMsg = new BaseJson<string>
                    {
                        Status = (int)JsonObjectStatus.Fail,
                        Data = null,
                        Message = JsonObjectStatus.Fail.GetEnumText() + "，请求参数为空。",
                        BackUrl = null
                    };
                }
            }, e =>
            {
                resultMsg = new BaseJson<string>
                {
                    Status = (int)JsonObjectStatus.Exception,
                    Data = null,
                    Message = JsonObjectStatus.Exception.GetEnumText() + "，异常信息：" + e.Message,
                    BackUrl = null
                };
            });

            return resultMsg.TryToJson().ToHttpResponseMessage();
        }

        /// <summary>
        /// 获取开奖历史
        /// </summary>
        /// <param name="type"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        private string GetResultByEnumCode(SCCLottery type, HistoryLotteryArgEnyity arg)
        {
            string res = String.Empty;
            DataTable data = GetData(type, arg);

            switch (type)
            {
                case SCCLottery.ShanDongKLPK3:
                    res = AppendShanDongKLPK3Result(data, SCCLottery.ShanDongKLPK3);
                    break;
                default:
                    res = AppendCommonResult(data, type);
                    break;
            }
            return res;
        }

        /// <summary>
        /// 组装公共记录
        /// </summary>
        /// <param name="data"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private string AppendCommonResult(DataTable data, SCCLottery type)
        {
            List<GPCQTCHistoryLotteryViewEntity> res = new List<GPCQTCHistoryLotteryViewEntity>();
            if (data.Rows.Count > 0)
            {
                //总共球个数
                int total = type.GetEnumText().TryToInt32();

                for (int j = 0; j < data.Rows.Count; j++)
                {
                    //开奖号集合
                    List<int> openCodeList = new List<int>();
                    StringBuilder builder = new StringBuilder();

                    for (int i = 1; i <= total; i++)
                    {
                        int openCode = data.Rows[j]["OpenCode" + i].TryToInt32();
                        openCodeList.Add(openCode);
                    }
                    builder.Append(GetOpenCodeTemplate(type, openCodeList));

                    res.Add(new GPCQTCHistoryLotteryViewEntity
                    {
                        Term = data.Rows[j]["Term"].ToStringEx(),
                        OpenTime = data.Rows[j]["OpenTime"].TryToDateTimeToString("yyyy-MM-dd HH:mm:ss"),
                        NormalOpenCode = builder.ToString(),
                        SumValue = LotteryUtils.GetSum(openCodeList, GetSumNumberCount(type)).ToString(),
                        ParityRatio = LotteryUtils.GetProportionOfJO(openCodeList),
                        Span = LotteryUtils.GetSpan(openCodeList).ToString(),
                        PrimeAndNumberRatio = LotteryUtils.GetProportionOfZh(openCodeList)
                    });
                }
            }

            return res.ToJson();
        }

        /// <summary>
        /// 组装快乐扑克3记录
        /// </summary>
        /// <param name="data"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private string AppendShanDongKLPK3Result(DataTable data, SCCLottery type)
        {
            //山东快乐扑克3号码规则：号码形如211（红心J），第一位是花色（1为方片，2为红心，3为梅花，4为黑桃），后两位是号码（A为1，J为11，Q为12，K为13）

            List<GPCKLPK3HistoryLotteryViewEntity> res = new List<GPCKLPK3HistoryLotteryViewEntity>();
            if (data.Rows.Count > 0)
            {
                //总共球个数
                int total = type.GetEnumText().TryToInt32();

                for (int j = 0; j < data.Rows.Count; j++)
                {
                    //开奖号集合
                    List<int> openCodeList1 = new List<int>();
                    List<int> openCodeList2 = new List<int>();
                    StringBuilder builder = new StringBuilder();

                    for (int i = 1; i <= total; i++)
                    {
                        int openCode1 = data.Rows[j]["OpenCode" + i].ToString().Substring(1, 2).TryToInt32();
                        openCodeList1.Add(openCode1);

                        int openCode2 = data.Rows[j]["OpenCode" + i].ToString().TryToInt32();
                        openCodeList2.Add(openCode2);
                    }
                    builder.Append(GetOpenCodeTemplate(type, openCodeList2));

                    res.Add(new GPCKLPK3HistoryLotteryViewEntity
                    {
                        Term = data.Rows[j]["Term"].ToStringEx(),
                        OpenTime = data.Rows[j]["OpenTime"].TryToDateTimeToString("yyyy-MM-dd HH:mm:ss"),
                        NormalOpenCode = builder.ToString(),
                        SumValue = LotteryUtils.GetSum(openCodeList1, GetSumNumberCount(type)).ToString(),
                    });
                }
            }

            return res.ToJson();
        }

        /// <summary>
        /// 查询数据集
        /// </summary>
        /// <param name="type"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        private DataTable GetData(SCCLottery type, HistoryLotteryArgEnyity arg)
        {
            //组装查询语句
            string sql = GetSeleteSQL(type, arg);
            //查询结果
            DataTable o = commonBll.ExcuteSqlDataTable(sql, DatabaseLinksEnum.LotteryNumber, null);

            return o;
        }

        #endregion

        #region 公共私有方法
        /// <summary>
        /// 组装查询语句
        /// </summary>
        /// <param name="type">枚举码</param>
        /// <param name="arg"></param>
        /// <returns></returns>
        private string GetSeleteSQL(SCCLottery type, HistoryLotteryArgEnyity arg)
        {
            StringBuilder builder = new StringBuilder();
            string res = String.Empty;

            int total = type.GetEnumText().TryToInt32();
            string tableName = type.GetSCCLotteryTableName();
            for (int i = 1; i <= total; i++)
            {
                builder.Append("[OpenCode" + i + "],");
            }

            if (arg.TotalRecord > 0)
            {
                res = string.Format(GetLotterySqlByTableNameWithTop, arg.TotalRecord, StringHelper.DelLastChar(builder.ToString(), ","), tableName);
            }
            else if (!string.IsNullOrEmpty(arg.StartTime))
            {
                string time = arg.StartTime.CheckDateTime()
                    ? arg.StartTime
                    : DateTimeHelper.Now.AddDays(-7).ToString("yyyy-MM-dd");

                res = string.Format(GetLotterySqlByTableNameWithStartTime, StringHelper.DelLastChar(builder.ToString(), ","), tableName, time);
            }
            else
            {
                res = string.Format(GetLotterySqlByTableNameWithTop, "20", StringHelper.DelLastChar(builder.ToString(), ","), tableName);
            }

            return res;
        }

        /// <summary>
        /// 将花色和号码转化为开奖号码
        /// 1为方片，2为红心，3为梅花，4为黑桃
        /// A为1，J为11，Q为12，K为13
        /// </summary>
        /// <param name="kjHaoMa">号码，形如404，第一位是花色（1为方片，2为红心，3为梅花，4为黑桃），后两位是号码（A为1，J为11，Q为12，K为13）</param>
        /// <param name="className">花色类名称</param>
        /// <returns></returns>
        private string GetMainPuKeString(string kjHaoMa, string className)
        {
            string result = string.Empty;
            switch (kjHaoMa)
            {
                case "A":
                    result = "01";
                    break;
                case "J":
                    result = "11";
                    break;
                case "Q":
                    result = "12";
                    break;
                case "K":
                    result = "13";
                    break;
                default:
                    result = kjHaoMa.PadLeft(2, '0');
                    break;
            }
            if (className.Contains("fangpian"))
                return "1" + result;
            if (className.Contains("hongtao"))
                return "2" + result;
            if (className.Contains("meihua"))
                return "3" + result;
            return "4" + result;//heitao
        }

        #endregion

        #region SQL语句

        /// <summary>
        /// 通过表名查询数据为校验后的前n行数据
        /// </summary>
        private static string GetLotterySqlByTableNameWithTop = @"SELECT TOP {0} [ID],[Term],[OpenTime],[Spare],{1} FROM [dbo].[{2}] ORDER BY Term DESC ";//WHERE [IsChecked] = 1 AND [IsPassed] = 1 
        /// <summary>
        /// 通过开奖时间查询数据为校验后的所有数据
        /// </summary>
        private static string GetLotterySqlByTableNameWithStartTime = @"SELECT [ID],[Term],[OpenTime],[Spare],{0} FROM [dbo].[{1}] WHERE DATEDIFF(DAY,'{2}',OpenTime) = 0 ORDER BY Term DESC";//AND [IsChecked] = 1 AND [IsPassed] = 1 

        #endregion
    }
}

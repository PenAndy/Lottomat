using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lottomat.Application.Code;
using Lottomat.Application.SystemAutoJob.Interface;
using Lottomat.Util;
using Lottomat.Util.Extension;
using Lottomat.Utils;
using Lottomat.Utils.Date;

namespace Lottomat.Application.SystemAutoJob
{
    /// <summary>
    /// 自动提醒即将开奖彩种
    /// </summary>
    public class AutoRemindingTheForthcomingLotteryManager : ISchedulerJob
    {
        /// <summary>
        /// 对象锁
        /// </summary>
        private static readonly object _lock = new object();
        /// <summary>
        /// 配置信息
        /// </summary>
        private static List<SCCConfig> LotteryConfig => InitLotteryConfig.Init();
        /// <summary>
        /// 待开奖彩种字典
        /// </summary>
        private static Dictionary<string, SCCConfig> dictionary = new Dictionary<string, SCCConfig>();
        /// <summary>
        /// 已经发送过邮件的彩种
        /// </summary>
        private static List<string> sendEmailSuccList = new List<string>();

        /// <summary>
        /// 入口程序
        /// </summary>
        public void Execute()
        {
            lock (_lock)
            {
                DoSomething();
            }
        }

        /// <summary>
        /// 真正操作逻辑
        /// </summary>
        private void DoSomething()
        {
            GetTodayLotteryDict();

            //批量发送邮件
            SendEmail();
        }

        /// <summary>
        /// 获取今日开奖彩种
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, SCCConfig> GetTodayLotteryDict()
        {
            //当前时间
            DateTime now = DateTimeHelper.Now;
            //今天是星期几
            string week = now.DayOfWeek.ToString("d");
            //获取当前彩种配置信息（全国彩和地方彩）
            List<SCCConfig> configList = LotteryConfig.Where(s => s.Name.Contains("DFC_") || s.Name.Contains("QGC_")).OrderBy(s => s.LotteryName.Length).ToList();
            foreach (SCCConfig config in configList)
            {
                //当前彩种每周开奖时间
                string[] openThePrizeOnTheDayOfTheWeek = config.KJTime.Split(",".ToCharArray());
                //今天星期在数组中的索引
                int pointer = Array.IndexOf(openThePrizeOnTheDayOfTheWeek, week.ToString());
                if (pointer != -1)//今天要开奖
                {
                    //当前彩种今天真实开始开奖时间
                    DateTime todayRealStartOpentime = (now.ToString("yyyy-MM-dd") + " " + config.StartHour + ":" + config.StartMinute).TryToDateTime();
                    //如果时间差小于半个小时，则提醒系统管理员，有彩种即将开奖
                    TimeSpan timeSpan = todayRealStartOpentime - now;

                    Trace.WriteLine(string.Format("【{1}】时间差为：{0}分钟，开奖时间为：{2}.", timeSpan.TotalMinutes, config.LotteryName, todayRealStartOpentime));

                    if (timeSpan.TotalMinutes > 0 && timeSpan.TotalMinutes <= 30)
                    {
                        if (!dictionary.ContainsKey(config.EnumCode))
                        {
                            dictionary.Add(config.EnumCode, config);
                        }
                    }
                    else
                    {
                        if (dictionary.Count > 0)
                        {
                            if (dictionary.ContainsKey(config.EnumCode))
                            {
                                dictionary.Remove(config.EnumCode);
                                //移除已经发送邮件的彩种
                                sendEmailSuccList.Remove(config.EnumCode);
                            }
                        }
                    }
                }
            }

            return dictionary;
        }

        /// <summary>
        /// 批量发送邮件
        /// </summary>
        private void SendEmail()
        {
            if (dictionary.Count > 0)
            {
                //当前时间
                DateTime now = DateTimeHelper.Now;

                Task task = Task.Factory.StartNew(() =>
                {
                    StringBuilder builder = new StringBuilder();
                    foreach (KeyValuePair<string, SCCConfig> pair in dictionary)
                    {
                        if (!sendEmailSuccList.Contains(pair.Key))
                        {
                            SCCConfig config = pair.Value;
                            //当前彩种今天真实开始开奖时间
                            string todayRealStartOpentime = now.ToString("yyyy-MM-dd") + " " + config.StartHour.RepairZero() + ":" + config.StartMinute.RepairZero();

                            builder.Append(config.LotteryName + "，开奖时间：" + todayRealStartOpentime + "；<br />");//<a href='" + config.MainUrl + "  target='_blank''>[参考网址]</a>

                            //添加到已经开奖集合，下一次就需要发送邮件了
                            sendEmailSuccList.Add(pair.Key);

                            Trace.WriteLine(config.LotteryName + "  添加成功！");
                        }
                    }
                    if (!string.IsNullOrEmpty(builder.ToString()))
                    {
                        string body = "管理员请注意，以下彩种将在30分钟后开奖：<br /><br />" + StringHelper.DelLastChar(builder.ToString(), "；") + "。";
                        //发送邮件
                        string address = ConfigHelper.GetValue("ErrorReportTo");
                        string subject = "开奖提醒";

                        MailHelper.SendByThread(address, subject, body);
                    }
                });
                task.Wait();
            }
        }
    }
}
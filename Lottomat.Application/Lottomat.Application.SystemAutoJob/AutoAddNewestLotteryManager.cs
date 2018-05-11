using System;
using System.ComponentModel;
using System.Text;
using Lottomat.Application.Busines.LotteryNumberManage;
using Lottomat.Application.Busines.OpenCodeManage;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.CommonEntity;
using Lottomat.Application.SystemAutoJob.Interface;
using Lottomat.Util;
using Lottomat.Util.Extension;
using Lottomat.Utils;

namespace Lottomat.Application.SystemAutoJob
{
    /// <summary>
    /// 自动添加一条最新开奖信息，无开奖号
    /// </summary>
    public class AutoAddNewestLotteryManager : ISchedulerJob
    {
        /// <summary>
        /// 对象锁
        /// </summary>
        private static readonly object _lock = new object();

        private static QGFC3DBLL qgfc3Dbll = new QGFC3DBLL();

        private static Open3CodeBLL open3CodeBll = new Open3CodeBLL();
        private static Open4CodeBLL open4CodeBll = new Open4CodeBLL();
        private static Open5CodeBLL open5CodeBll = new Open5CodeBLL();
        private static Open7CodeBLL open7CodeBll = new Open7CodeBLL();
        private static Open8CodeBLL open8CodeBll = new Open8CodeBLL();
        private static Open10CodeBLL open10CodeBll = new Open10CodeBLL();
        private static Open21CodeBLL open21CodeBll = new Open21CodeBLL();

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
            string config = ConfigHelper.GetValue("AutoAddNewestLottery");
            if (!string.IsNullOrEmpty(config))
            {
                string[] arr = config.Split(",".ToCharArray());
                foreach (string s in arr)
                {
                    bool isSucc = Enum.TryParse<SCCLottery>(s, true, out SCCLottery type);
                    //SCCLottery type = (SCCLottery)Enum.Parse(typeof(SCCLottery), arg.EnumCode, true);
                    if (!isSucc) continue;

                    //官网最新期数
                    string latestaward = GrabTheLatestAwardManager.GetTheLatestAward(type);
                    if (!string.IsNullOrEmpty(latestaward))
                    {
                        //处理期数
                        latestaward = latestaward.IndexOf("20", 0, 2, StringComparison.Ordinal) >= 0 ? latestaward : "20" + latestaward;

                        //本地最新期数
                        string old = (qgfc3Dbll.GetNewTermByTableName(type.GetSCCLotteryTableName()).TryToInt32() - 1).ToString();
                        StringBuilder builder = new StringBuilder();

                        //TODO 检测二者之间差了多少期，并将差了的期插入本地
                        int o = old.TryToInt32();
                        int l = latestaward.TryToInt32();
                        if (o < l)
                        {
                            while (o + 1 <= l)
                            {
                                o++;

                                //向本地插入一条不包括开奖号的数据
                                int totalBall = string.IsNullOrEmpty(type.GetEnumText()) ? 0 : type.GetEnumText().TryToInt32();
                                //插入差额期数
                                Insert(totalBall, o.ToString(), type);

                                builder.Append(o.ToString() + "、");
                            }
                            //TODO 插入下一期开奖信息并且发送邮件对未复查的进行提醒
                            SendEmail(StringHelper.DelLastChar(builder.ToString(), "、"), type);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 插入下一期开奖信息并且发送邮件对未复查的进行提醒
        /// </summary>
        /// <param name="latestaward"></param>
        /// <param name="scc"></param>
        private void SendEmail(string latestaward, SCCLottery scc)
        {
            //发送邮件
            string address = ConfigHelper.GetValue("ErrorReportTo");
            string subject = scc.GetEnumDescription() + "[第" + latestaward + "期]开奖号未及时复查提醒";
            string body = "系统管理员请注意：" + scc.GetEnumDescription() + "[第" + latestaward +
                          "期]开奖号未及时复查，请尽快登陆系统后台进行复查操作！<br /><br />" + scc.GetEnumDescription() + "官方参考网址为：<a href='" + GrabTheLatestAwardManager.GetRequestUrlAndXPath(scc)[0] + "'>【点我前往】</a>";
            MailHelper.SendByThread(address, subject, body);

            //同时插入下一期
            //int totalBall = string.IsNullOrEmpty(scc.GetEnumText()) ? 0 : scc.GetEnumText().TryToInt32();
            //string[] arr = latestaward.Split("、".ToCharArray());
            //Insert(totalBall, (arr[arr.Length - 1].TryToInt32() + 1).ToString(), scc);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="totalBall"></param>
        /// <param name="latestaward"></param>
        /// <param name="scc"></param>
        private void Insert(int totalBall, string latestaward, SCCLottery scc)
        {
            switch (totalBall)
            {
                case 3:
                    open3CodeBll.AddOpen3Code(scc, new OpenCode3Model
                    {
                        Term = latestaward.TryToInt32(),
                        OpenCode1 = -1,
                        OpenCode2 = -1,
                        OpenCode3 = -1,
                        OpenTime = DateTime.Now
                    });
                    break;
                case 4:
                    open4CodeBll.AddOpen4Code(scc, new OpenCode4Model
                    {
                        Term = latestaward.TryToInt32(),
                        OpenCode1 = -1,
                        OpenCode2 = -1,
                        OpenCode3 = -1,
                        OpenCode4 = -1,
                        OpenTime = DateTime.Now
                    });
                    break;
                case 5:
                    open5CodeBll.AddOpen5Code(scc, new OpenCode5Model
                    {
                        Term = latestaward.TryToInt32(),
                        OpenCode1 = -1,
                        OpenCode2 = -1,
                        OpenCode3 = -1,
                        OpenCode4 = -1,
                        OpenCode5 = -1,
                        OpenTime = DateTime.Now
                    });
                    break;
                case 7:
                    open7CodeBll.AddOpen7Code(scc, new OpenCode7Model
                    {
                        Term = latestaward.TryToInt32(),
                        OpenCode1 = -1,
                        OpenCode2 = -1,
                        OpenCode3 = -1,
                        OpenCode4 = -1,
                        OpenCode5 = -1,
                        OpenCode6 = -1,
                        OpenCode7 = -1,
                        OpenTime = DateTime.Now
                    });
                    break;
                case 8:
                    open8CodeBll.AddOpen8Code(scc, new OpenCode8Model
                    {
                        Term = latestaward.TryToInt32(),
                        OpenCode1 = -1,
                        OpenCode2 = -1,
                        OpenCode3 = -1,
                        OpenCode4 = -1,
                        OpenCode5 = -1,
                        OpenCode6 = -1,
                        OpenCode7 = -1,
                        OpenCode8 = -1,
                        OpenTime = DateTime.Now
                    });
                    break;
                case 10:
                    open10CodeBll.AddOpen10Code(scc, new OpenCode10Model
                    {
                        Term = latestaward.TryToInt32(),
                        OpenCode1 = -1,
                        OpenCode2 = -1,
                        OpenCode3 = -1,
                        OpenCode4 = -1,
                        OpenCode5 = -1,
                        OpenCode6 = -1,
                        OpenCode7 = -1,
                        OpenCode8 = -1,
                        OpenCode9 = -1,
                        OpenCode10 = -1,
                        OpenTime = DateTime.Now
                    });
                    break;
                case 21:
                    open21CodeBll.AddOpen21Code(scc, new OpenCode21Model
                    {
                        Term = latestaward.TryToInt32(),
                        OpenCode1 = -1,
                        OpenCode2 = -1,
                        OpenCode3 = -1,
                        OpenCode4 = -1,
                        OpenCode5 = -1,
                        OpenCode6 = -1,
                        OpenCode7 = -1,
                        OpenCode8 = -1,
                        OpenCode9 = -1,
                        OpenCode10 = -1,
                        OpenCode11 = -1,
                        OpenCode12 = -1,
                        OpenCode13 = -1,
                        OpenCode14 = -1,
                        OpenCode15 = -1,
                        OpenCode16 = -1,
                        OpenCode17 = -1,
                        OpenCode18 = -1,
                        OpenCode19 = -1,
                        OpenCode20 = -1,
                        OpenCode21 = -1,
                        OpenTime = DateTime.Now
                    });
                    break;
                default:
                    break;
            }
        }
    }
}
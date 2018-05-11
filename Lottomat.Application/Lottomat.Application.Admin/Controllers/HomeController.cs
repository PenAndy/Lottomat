using Lottomat.Application.Busines;
using Lottomat.Application.Busines.BaseManage;
using Lottomat.Application.Busines.SystemManage;
using Lottomat.Application.Code;
using Lottomat.Application.Entity;
using Lottomat.Application.Entity.SystemManage;
using Lottomat.Util;
using Lottomat.Util.Attributes;
using Lottomat.Util.Log;
using Lottomat.Util.Offices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lottomat.Application.Busines.CommonManage;
using Lottomat.Application.Busines.LotteryNumberManage;
using Lottomat.Application.Cache;
using Lottomat.Application.Entity.CommonEntity;
using Lottomat.Application.Entity.LotteryNumberManage.ViewModel;
using Lottomat.Application.Entity.SystemManage.ViewModel;
using Lottomat.Application.SystemAutoJob;
using Lottomat.Util.Extension;
using Lottomat.Utils.Date;

namespace Lottomat.Application.Admin.Controllers
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2015.09.01 13:32
    /// 描 述：后台首页
    /// </summary>
    [HandlerLogin(LoginMode.Enforce)]
    public class HomeController : MvcControllerBase
    {
        //UserBLL user = new UserBLL();
        //DepartmentBLL department = new DepartmentBLL();

        private static QGFC3DBLL qgfc3Dbll = new QGFC3DBLL();
        private static CommonBLL commonBll = new CommonBLL();
        private static DataItemCache dataItemCache = new DataItemCache();
        /// <summary>
        /// 配置信息
        /// </summary>
        private static List<SCCConfig> LotteryConfig => InitLotteryConfig.Init();

        #region 视图功能
        /// <summary>
        /// 后台框架页
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminDefault()
        {
            return View();
        }
        public ActionResult AdminLTE()
        {
            return View();
        }
        public ActionResult AdminWindos()
        {
            return View();
        }
        /// <summary>
        /// 后台框架页
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminBeyond()
        {
            return View();
        }
        /// <summary>
        /// 我的桌面
        /// </summary>
        /// <returns></returns>
        public ActionResult Desktop()
        {
            return View();
        }
        public ActionResult AdminDefaultDesktop()
        {
            return View();
        }
        public ActionResult AdminLTEDesktop()
        {
            return View();
        }
        public ActionResult AdminWindosDesktop()
        {
            return View();
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 访问功能
        /// </summary>
        /// <param name="moduleId">功能Id</param>
        /// <param name="moduleName">功能模块</param>
        /// <param name="moduleUrl">访问路径</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult VisitModule(string moduleId, string moduleName, string moduleUrl)
        {
            LogEntity logEntity = new LogEntity
            {
                CategoryId = (int)CategoryType.Visit,
                OperateTypeId = ((int)OperationType.Visit).ToString(),
                OperateType = OperationType.Visit.GetEnumDescription(),
                OperateAccount = OperatorProvider.Provider.Current().Account,
                OperateUserId = OperatorProvider.Provider.Current().UserId,
                ModuleId = moduleId,
                Module = moduleName,
                ExecuteResult = 1,
                ExecuteResultJson = "访问地址：" + moduleUrl
            };
            logEntity.WriteLog();
            return Content(moduleId);
        }

        /// <summary>
        /// 离开功能
        /// </summary>
        /// <param name="moduleId">功能模块Id</param>
        /// <returns></returns>
        public ActionResult LeaveModule(string moduleId)
        {
            return null;
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取首页未复查的数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly(true)]
        public ActionResult GetNotInvestigationList()
        {
            List<NotInvestigationEntity> res = commonBll.GetNotInvestigationList();

            return Success("操作成功", res);
        }

        /// <summary>
        /// 获取今日开奖彩种
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly(true)]
        public ActionResult GetTodaysLottery()
        {
            Dictionary<string, SCCConfig> dict = GetTodayLotteryDict();
            List<TodaysLotteryViewEntity> obj = new List<TodaysLotteryViewEntity>();
            foreach (KeyValuePair<string, SCCConfig> pair in dict)
            {
                SCCConfig config = pair.Value;
                SCCLottery type = (SCCLottery)Enum.Parse(typeof(SCCLottery), config.EnumCode, true);
                //当前彩种今天真实开始开奖时间
                DateTime todayRealStartOpentime = (DateTimeHelper.Now.ToString("yyyy-MM-dd") + " " + config.StartHour + ":" + config.StartMinute).TryToDateTime();
                //如果当前时间大于当前彩种今天真实开始开奖时间，则忽略
                if (DateTimeHelper.Now < todayRealStartOpentime)
                {
                    string lastTerm = qgfc3Dbll.GetNewTermByTableName(type.GetSCCLotteryTableName());
                    obj.Add(new TodaysLotteryViewEntity
                    {
                        LotteryName = config.LotteryName,
                        Term = lastTerm,
                        OpenTime = todayRealStartOpentime,
                        MainUrl = config.MainUrl,
                        EnumCode = config.EnumCode
                    });
                }
            }
            obj = obj.OrderBy(o => o.OpenTime).ToList();
            return Success("操作成功", obj.ToJson());
        }

        /// <summary>
        /// 获取今日开奖彩种
        /// </summary>
        /// <returns></returns>
        private static Dictionary<string, SCCConfig> GetTodayLotteryDict()
        {
            Dictionary<string, SCCConfig> dictionary = new Dictionary<string, SCCConfig>();

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
                    if (!dictionary.ContainsKey(config.EnumCode))
                    {
                        dictionary.Add(config.EnumCode, config);
                    }
                }
            }

            return dictionary;
        }

        #endregion
    }
}

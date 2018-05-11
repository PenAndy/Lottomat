using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Lottomat.Application.Busines.CommonManage;
using Lottomat.Application.Busines.LotteryNumberManage;
using Lottomat.Application.Busines.SystemManage;
using Lottomat.Application.Cache;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.CommonEntity;
using Lottomat.Application.Entity.LotteryNumberManage;
using Lottomat.Application.Entity.LotteryNumberManage.Parameter;
using Lottomat.Application.Entity.LotteryNumberManage.ViewModel;
using Lottomat.Application.Entity.SystemManage;
using Lottomat.Application.Entity.SystemManage.ViewModel;
using Lottomat.Cache.Factory;
using Lottomat.SOA.API.Controllers.Base;
using Lottomat.Util;
using Lottomat.Util.Extension;
using Lottomat.Utils;
using Lottomat.Utils.Date;
using Lottomat.Utils.Web;

namespace Lottomat.SOA.API.Controllers.V1
{
    /// <summary>
    /// 公共接口
    /// </summary>
    public class CommonController : BaseApiController
    {
        #region 实例
        /// <summary>
        /// 字典缓存BLL
        /// </summary>
        private static readonly DataItemCache dataItemCache = new DataItemCache();
        /// <summary>
        /// 友情链接
        /// </summary>
        private static BaseFriendLinksBLL baseFriendLinksBll = new BaseFriendLinksBLL();
        /// <summary>
        /// 意见反馈
        /// </summary>
        private static FeedbackBLL feedbackBll = new FeedbackBLL();
        /// <summary>
        /// 中奖规则
        /// </summary>
        private static AwardsBLL awardsBll = new AwardsBLL();
        /// <summary>
        /// 彩种编码
        /// </summary>
        //{ "QGC|全国彩", "DFC|地方彩", "GPC11X5|11选5", "GPCK3|快3", "GPCKL12|快乐十二", "GPCKLSF|快乐十分", "GPCQTC|其他彩种" }
        private static readonly string LotteryTypeCodeStr = ConfigHelper.GetValue("LotteryCategoryCodingMapping");
        /// <summary>
        /// TDK范围值域
        /// </summary>
        private static readonly string[] TdkAreaCodeArr = new[] { "MainPageTDK", "LotteryTDK", "SpecialTDK", "OpeningNumberTDK", "TestNumberTDK", "PlayTDK", "CommonTDK" };
        #endregion

        #region 根据key清除缓存（废弃，新接口见APICacheManageController）
        /// <summary>
        /// 根据key清除缓存
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage RemoveCacheByKey(string type)
        {
            BaseJson<string> resultMsg = new BaseJson<string> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };

            Logger(typeof(CommonController), type, "根据key清除缓存-RemoveCacheByKey", () =>
            {
                if (!string.IsNullOrEmpty(type))
                {
                    //Cache.Factory.CacheFactory.Cache().RemoveCache(type);

                    //转换成系统枚举
                    bool isSucc = Enum.TryParse(type, false, out SCCLottery which);
                    Cache.Factory.CacheFactory.Cache().RemoveCache(isSucc ? which.ToString() : type);

                    resultMsg = new BaseJson<string>
                    {
                        Status = (int)JsonObjectStatus.Success,
                        Data = null,
                        Message = JsonObjectStatus.Success.GetEnumText(),
                        BackUrl = null
                    };
                }
                else
                {
                    resultMsg = new BaseJson<string>
                    {
                        Status = (int)JsonObjectStatus.Fail,
                        Data = null,
                        Message = JsonObjectStatus.Fail.GetEnumText() + "，请求参数type为空。",
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

                //如果发生异常，则清除全部缓存
                //Cache.Factory.CacheFactory.Cache().RemoveCache();
            });

            return resultMsg.TryToJson().ToHttpResponseMessage();
        }
        #endregion

        #region 验证码
        /// <summary>
        /// 验证码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetVerificationCode(VerificationCodeArgEntity arg)
        {
            BaseJson<VerificationCodeViewEntity> resultMsg = new BaseJson<VerificationCodeViewEntity> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };

            Logger(typeof(CommonController), arg.TryToJson(), "验证码-GetVerificationCode", () =>
            {
                if (!string.IsNullOrEmpty(arg.t))
                {
                    if (arg.t.CheckTimeStamp())
                    {
                        string code = String.Empty;
                        // Feedback-意见反馈 Login-登陆 Register-注册
                        switch (arg.Modular)
                        {
                            case "Feedback":
                                code = GetSystemVerificationCode(arg.Type);
                                break;
                            case "Login":
                                break;
                            case "Register":
                                break;
                        }

                        VerificationCodeViewEntity res = new VerificationCodeViewEntity
                        {
                            Code = code.ToUpper(),
                            ExpiryTime = DateTimeHelper.Now.AddMinutes(5).ToString("yyyy-MM-dd HH:mm:ss")
                        };

                        //存入缓存
                        Cache.Factory.CacheFactory.Cache().WriteCache(code, GlobalStaticConstant.Session_Key_Verification_Code + "_" + code, DateTimeHelper.Now.AddMinutes(5));

                        //写入日志
                        string ip = NetHelper.Ip;
                        string addr = NetHelper.GetAddressByIP(ip);
                        _logHelper.Info("\r\nIP地址：" + ip + "，位置：" + addr + "，请求类型：" + arg.Modular + "，验证码信息：" + res.ToJson() + "。\r\n");

                        resultMsg = new BaseJson<VerificationCodeViewEntity>
                        {
                            Status = (int)JsonObjectStatus.Success,
                            Data = res,
                            Message = JsonObjectStatus.Success.GetEnumText(),
                            BackUrl = null
                        };
                    }
                    else
                    {
                        resultMsg = new BaseJson<VerificationCodeViewEntity>
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
                    resultMsg = new BaseJson<VerificationCodeViewEntity>
                    {
                        Status = (int)JsonObjectStatus.Fail,
                        Data = null,
                        Message = JsonObjectStatus.Fail.GetEnumText() + "，请求参数为空。",
                        BackUrl = null
                    };
                }
            }, e =>
            {
                resultMsg = new BaseJson<VerificationCodeViewEntity>
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
        /// 生成验证码
        /// </summary>
        /// <param name="type">1-系统级验证码 2-手机短信验证码 3-邮箱验证码</param>
        /// <returns></returns>
        private string GetSystemVerificationCode(string type)
        {
            string code = String.Empty;
            switch (type)
            {
                case "1":
                    code = ValidateCodeHelper.GetRandLetterAndNumber(4, true);
                    break;
                case "2":
                    break;
                case "3":
                    break;
            }
            return code;
        }

        #endregion

        #region 获取友情链接
        /// <summary>
        /// 获取友情链接
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetFriendLinkList(BaseParameterEntity arg)
        {
            BaseJson<List<FriendLinkViewEntity>> resultMsg = new BaseJson<List<FriendLinkViewEntity>> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };

            Logger(typeof(CommonController), arg.TryToJson(), "获取友情链接-GetFriendLinkList", () =>
            {
                if (!string.IsNullOrEmpty(arg.t))
                {
                    if (arg.t.CheckTimeStamp())
                    {
                        List<FriendLinkViewEntity> cache = Cache.Factory.CacheFactory.Cache().GetCache<List<FriendLinkViewEntity>>(CacheKeyEnum.FriendLink.ToString());
                        if (cache == null)
                        {
                            List<BaseFriendLinksEntity> temp = baseFriendLinksBll.GetList(f => f.IsEnable == true && f.IsDelete == false).ToList();
                            if (temp.Count > 0)
                            {
                                List<FriendLinkViewEntity> list = new List<FriendLinkViewEntity>();
                                foreach (BaseFriendLinksEntity entity in temp)
                                {
                                    if (entity.TermOfValidity != 0)
                                    {
                                        if (entity.CreateTime != null && entity.CreateTime.Value.AddYears(entity.TermOfValidity) > DateTimeHelper.Now)
                                        {
                                            list.Add(new FriendLinkViewEntity
                                            {
                                                Name = entity.Name,
                                                Url = entity.Url,
                                                Type = entity.TypeName
                                            });
                                        }
                                    }
                                    else
                                    {
                                        list.Add(new FriendLinkViewEntity
                                        {
                                            Name = entity.Name,
                                            Url = entity.Url,
                                            Type = entity.TypeName
                                        });
                                    }
                                }

                                cache = list;

                                Cache.Factory.CacheFactory.Cache().WriteCache(list, CacheKeyEnum.FriendLink.ToString(), DateTimeHelper.Now.AddDays(30));
                            }
                        }

                        resultMsg = new BaseJson<List<FriendLinkViewEntity>>
                        {
                            Status = (int)JsonObjectStatus.Success,
                            Data = cache,
                            Message = JsonObjectStatus.Success.GetEnumText(),
                            BackUrl = null
                        };
                    }
                    else
                    {
                        resultMsg = new BaseJson<List<FriendLinkViewEntity>>
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
                    resultMsg = new BaseJson<List<FriendLinkViewEntity>>
                    {
                        Status = (int)JsonObjectStatus.Fail,
                        Data = null,
                        Message = JsonObjectStatus.Fail.GetEnumText() + "，请求参数为空。",
                        BackUrl = null
                    };
                }
            }, e =>
            {
                resultMsg = new BaseJson<List<FriendLinkViewEntity>>
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

        #region 获取彩票的玩法、介绍、中奖规则
        /// <summary>
        /// 获取彩票的玩法、介绍、中奖规则
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetIntroductionOfLottery(IntroductionOfLotteryArgEntity arg)
        {
            BaseJson<IntroductionOfLotteryViewEntity> resultMsg = new BaseJson<IntroductionOfLotteryViewEntity> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };

            Logger(typeof(CommonController), arg.ToJson(), "获取彩票的玩法、介绍、中奖规则-GetIntroductionOfLottery", () =>
            {
                if (!string.IsNullOrEmpty(arg.t) && !string.IsNullOrEmpty(arg.EnumCode))
                {
                    if (arg.t.CheckTimeStamp())
                    {
                        IntroductionOfLotteryViewEntity cache = Cache.Factory.CacheFactory.Cache().GetCache<IntroductionOfLotteryViewEntity>(CacheKeyEnum.IntroductionOfLottery.ToString() + "__" + arg.EnumCode);
                        if (cache == null)
                        {
                            string codeList = GetCodeList();

                            //获取所有彩种
                            List<DataItemModel> dataItem = dataItemCache.GetDataItemList(codeList).OrderBy(d => d.SortCode ?? 0).ToList();
                            //得到当前彩种ID
                            string id = dataItem.Where(d => d.SimpleSpelling.Equals(arg.EnumCode)).Select(d => d.ItemDetailId).FirstOrDefault();

                            AwardsEntity temp = awardsBll.GetEntity(f => f.IsDelete == false && f.PrizeID.Equals(id));
                            if (temp != null)
                            {
                                cache = new IntroductionOfLotteryViewEntity
                                {
                                    Gameplay = temp.Gameplay,
                                    LotteryRule = temp.LotteryRule,
                                    LotteryTime = temp.LotteryTime,
                                    Winning = temp.Winning
                                };
                                Cache.Factory.CacheFactory.Cache().WriteCache(cache, CacheKeyEnum.IntroductionOfLottery.ToString() + "__" + arg.EnumCode, DateTimeHelper.Now.AddDays(30));
                            }
                        }

                        resultMsg = new BaseJson<IntroductionOfLotteryViewEntity>
                        {
                            Status = (int)JsonObjectStatus.Success,
                            Data = cache,
                            Message = JsonObjectStatus.Success.GetEnumText(),
                            BackUrl = null
                        };
                    }
                    else
                    {
                        resultMsg = new BaseJson<IntroductionOfLotteryViewEntity>
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
                    resultMsg = new BaseJson<IntroductionOfLotteryViewEntity>
                    {
                        Status = (int)JsonObjectStatus.Fail,
                        Data = null,
                        Message = JsonObjectStatus.Fail.GetEnumText() + "，请求参数为空。",
                        BackUrl = null
                    };
                }
            }, e =>
            {
                resultMsg = new BaseJson<IntroductionOfLotteryViewEntity>
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

        #region 获取系统公告
        /// <summary>
        /// 获取系统公告
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetSystemNotice(BaseParameterEntity arg)
        {
            BaseJson<string> resultMsg = new BaseJson<string> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };

            Logger(typeof(CommonController), arg.TryToJson(), "获取系统公告-GetSystemNotice", () =>
            {
                if (!string.IsNullOrEmpty(arg.t))
                {
                    if (arg.t.CheckTimeStamp())
                    {



                        resultMsg = new BaseJson<string>
                        {
                            Status = (int)JsonObjectStatus.Success,
                            Data = null,
                            Message = JsonObjectStatus.Success.GetEnumText(),
                            BackUrl = null
                        };
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

        #endregion

        #region 获取系统三要素 （暂时弃用）
        /// <summary>
        /// 获取系统三要素
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetSystemThreeElements(SystemThreeElementsArgEntity arg)
        {
            BaseJson<SystemThreeElementsViewEntity> resultMsg = new BaseJson<SystemThreeElementsViewEntity> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };

            Logger(typeof(CommonController), arg.TryToJson(), "获取系统三要素-GetSystemThreeElements", () =>
            {
                if (!string.IsNullOrEmpty(arg.t) && !string.IsNullOrEmpty(arg.EnumCode))
                {
                    if (arg.t.CheckTimeStamp())
                    {
                        SystemThreeElementsViewEntity res = new SystemThreeElementsViewEntity();
                        switch (arg.Type)
                        {
                            case "1":
                                res = AppendSecondElements(arg.EnumCode);
                                break;
                            case "2":
                                res = AppendMainElements(arg.EnumCode);
                                break;
                        }

                        resultMsg = new BaseJson<SystemThreeElementsViewEntity>
                        {
                            Status = (int)JsonObjectStatus.Success,
                            Data = res,
                            Message = JsonObjectStatus.Success.GetEnumText(),
                            BackUrl = null
                        };
                    }
                    else
                    {
                        resultMsg = new BaseJson<SystemThreeElementsViewEntity>
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
                    resultMsg = new BaseJson<SystemThreeElementsViewEntity>
                    {
                        Status = (int)JsonObjectStatus.Fail,
                        Data = null,
                        Message = JsonObjectStatus.Fail.GetEnumText() + "，请求参数为空。",
                        BackUrl = null
                    };
                }
            }, e =>
            {
                resultMsg = new BaseJson<SystemThreeElementsViewEntity>
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
        /// 组装二级页面三要素
        /// </summary>
        /// <returns></returns>
        private SystemThreeElementsViewEntity AppendSecondElements(string type)
        {
            SystemThreeElementsViewEntity res = Cache.Factory.CacheFactory.Cache().GetCache<SystemThreeElementsViewEntity>(CacheKeyEnum.SystemThreeElements.ToString() + "__" + type + "__");
            if (res == null)
            {
                res = new SystemThreeElementsViewEntity();

                string keywordTemplate = ConfigHelper.GetValue("SystemElementsKeywords");
                string descTemplate = ConfigHelper.GetValue("SystemElementsDescription");

                //获取当前彩种配置信息
                SCCConfig config = LotteryConfig.FirstOrDefault(s => s.EnumCode.Equals(type));
                if (config != null)
                {
                    res.Keywords = keywordTemplate.Replace("$Name$", config.LotteryName);
                    res.Description = descTemplate.Replace("$Name$", config.LotteryName);
                }
                else
                {
                    bool isSucc = Enum.TryParse<SCCLottery>(type, true, out SCCLottery lottery);
                    //SCCLottery type = (SCCLottery)Enum.Parse(typeof(SCCLottery), arg.EnumCode, true);
                    if (isSucc)
                    {
                        res.Keywords = keywordTemplate.Replace("$Name$", lottery.GetEnumDescription());
                        res.Description = descTemplate.Replace("$Name$", lottery.GetEnumDescription());
                    }
                }
                Cache.Factory.CacheFactory.Cache().WriteCache(res, CacheKeyEnum.SystemThreeElements.ToString() + "__" + type + "__", DateTimeHelper.Now.AddDays(30));
            }
            return res;
        }

        /// <summary>
        /// 组装主页面三要素
        /// </summary>
        /// <returns></returns>
        private SystemThreeElementsViewEntity AppendMainElements(string type)
        {
            SystemThreeElementsViewEntity res = Cache.Factory.CacheFactory.Cache().GetCache<SystemThreeElementsViewEntity>(CacheKeyEnum.SystemThreeElements.ToString() + "__" + type + "__");
            if (res == null)
            {
                res = new SystemThreeElementsViewEntity();

                string keywordTemplate = ConfigHelper.GetValue("SystemElementsKeywords");
                string descTemplate = ConfigHelper.GetValue("SystemElementsDescription");

                StringBuilder builder = new StringBuilder();
                List<DataItemModel> data = dataItemCache.GetDataItemList(type);
                foreach (DataItemModel model in data)
                {
                    builder.Append(model.ItemName + ",");
                }

                res.Keywords = keywordTemplate.Replace("$Name$", builder.ToString());
                res.Description = descTemplate.Replace("$Name$", builder.ToString());

                Cache.Factory.CacheFactory.Cache().WriteCache(res, CacheKeyEnum.SystemThreeElements.ToString() + "__" + type + "__", DateTimeHelper.Now.AddDays(30));
            }
            return res;
        }

        #endregion

        #region 获取系统TDK
        /// <summary>
        /// 获取系统TDK
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetSystemTdks(SystemTdkArgEntity arg)
        {
            BaseJson<List<SystemTdkViewEntity>> resultMsg = new BaseJson<List<SystemTdkViewEntity>> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };

            Logger(typeof(CommonController), arg.TryToJson(), "获取系统TDK-GetSystemTdks", () =>
            {
                if (!string.IsNullOrEmpty(arg.t))
                {
                    if (arg.t.CheckTimeStamp())
                    {
                        //{ "MainPageTDK", "LotteryTDK", "SpecialTDK", "OpeningNumberTDK", "TestNumberTDK", "PlayTDK", "CommonTDK" };

                        List<SystemTdkViewEntity> res = new List<SystemTdkViewEntity>();
                        switch (arg.AreaCode)
                        {
                            case "MainPageTDK":
                                res = GetNotHasLotterySystemTdk(arg);
                                break;
                            case "SpecialTDK":
                                res = GetNotHasLotterySystemTdk(arg);
                                break;
                            case "CommonTDK":
                                res = GetNotHasLotterySystemTdk(arg);
                                break;
                            case "LotteryTDK":
                                res = GetHasLotterySystemTdk(arg);
                                break;
                            case "OpeningNumberTDK":
                                res = GetHasLotterySystemTdk(arg);
                                break;
                            case "TestNumberTDK":
                                res = GetHasLotterySystemTdk(arg);
                                break;
                            case "PlayTDK":
                                res = GetHasLotterySystemTdk(arg);
                                break;
                            default:
                                arg.AreaCode = "CommonTDK";
                                arg.EnumCode = "";

                                res = GetNotHasLotterySystemTdk(arg);
                                break;
                        }

                        resultMsg = new BaseJson<List<SystemTdkViewEntity>>
                        {
                            Status = (int)JsonObjectStatus.Success,
                            Data = res,
                            Message = JsonObjectStatus.Success.GetEnumText(),
                            BackUrl = null
                        };
                    }
                    else
                    {
                        resultMsg = new BaseJson<List<SystemTdkViewEntity>>
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
                    resultMsg = new BaseJson<List<SystemTdkViewEntity>>
                    {
                        Status = (int)JsonObjectStatus.Fail,
                        Data = null,
                        Message = JsonObjectStatus.Fail.GetEnumText() + "，请求参数为空。",
                        BackUrl = null
                    };
                }
            }, e =>
            {
                resultMsg = new BaseJson<List<SystemTdkViewEntity>>
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
        /// 获取与彩种相关的TDK
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private List<SystemTdkViewEntity> GetHasLotterySystemTdk(SystemTdkArgEntity arg)
        {
            if (!string.IsNullOrEmpty(arg.EnumCode))
            {
                string[] codeArr = arg.EnumCode.Split("|".ToCharArray());
                //StringBuilder builder = new StringBuilder();
                string[] lotterycodeArr = new string[codeArr.Length];
                for (int i = 0; i < codeArr.Length; i++)
                {
                    lotterycodeArr[i] = arg.AreaCode + "_" + codeArr[i];
                }

                //string str = builder.ToString().Substring(0, builder.ToString().Length - 1);
                //string sql = $"select [Name],[Title],[Desc],[Keyword],[LotteryCode] from dbo.Base_SiteTDK where LotteryCode in({str})";

                //DataTable data = commonBll.ExcuteSqlDataTable(sql, DatabaseLinksEnum.Base);

                //List<SystemTdkViewEntity> res = data.DataTableToList<SystemTdkViewEntity>();

                //return res;

                List<SystemTdkViewEntity> data = GetTDKData();

                data = data.Where(d => lotterycodeArr.Contains(d.LotteryCode)).ToList();

                return data;
            }
            return null;
        }

        /// <summary>
        /// 获取与彩种无相关的TDK
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private List<SystemTdkViewEntity> GetNotHasLotterySystemTdk(SystemTdkArgEntity arg)
        {
            if (!string.IsNullOrEmpty(arg.AreaCode))
            {
                string[] codeArr = arg.AreaCode.Split("|".ToCharArray());
                //StringBuilder builder = new StringBuilder();
                string[] lotterycodeArr = new string[codeArr.Length];
                for (int i = 0; i < codeArr.Length; i++)
                {
                    lotterycodeArr[i] = codeArr[i];
                }

                //string str = builder.ToString().Substring(0, builder.ToString().Length - 1);
                //string sql = $"select [Name],[Title],[Desc],[Keyword],[LotteryCode] from dbo.Base_SiteTDK where LotteryCode in({str})";

                //DataTable data = commonBll.ExcuteSqlDataTable(sql, DatabaseLinksEnum.Base);

                //List<SystemTdkViewEntity> res = data.DataTableToList<SystemTdkViewEntity>();

                //return res;

                List<SystemTdkViewEntity> data = GetTDKData();

                data = data.Where(d => lotterycodeArr.Contains(d.LotteryCode)).ToList();

                return data;
            }
            return null;
        }
        
        #endregion

        #region 意见反馈
        /// <summary>
        /// 意见反馈
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage SubmitFeedback(FeedbackArgEntity arg)
        {
            BaseJson<string> resultMsg = new BaseJson<string> { Status = (int)JsonObjectStatus.Error, Message = "服务器未知错误。", Data = null };

            Logger(typeof(CommonController), arg.TryToJson(), "意见反馈-SubmitFeedback", () =>
            {
                if (!string.IsNullOrEmpty(arg.t) && !string.IsNullOrEmpty(arg.VerificationCode))
                {
                    if (arg.t.CheckTimeStamp())
                    {
                        //string cacheCode = CookieHelper.GetCookie(GlobalStaticConstant.Session_Key_Verification_Code);
                        string cacheCode = Cache.Factory.CacheFactory.Cache().GetCache<string>(GlobalStaticConstant.Session_Key_Verification_Code + "_" + arg.VerificationCode.ToUpper());
                        //写入日志
                        _logHelper.Info("\r\n传入验证码为：" + arg.VerificationCode + "，缓存验证码为：" + cacheCode + "。\r\n");

                        if (!string.IsNullOrEmpty(cacheCode))
                        {
                            if (cacheCode.Equals(arg.VerificationCode.ToUpper()))
                            {
                                string ip = NetHelper.Ip;

                                FeedbackEntity entity = new FeedbackEntity
                                {
                                    NickName = arg.NickName.ReplaceHtmlTag().SqlFilters(),
                                    Contact = arg.Contact.ReplaceHtmlTag().SqlFilters(),
                                    Content = arg.Content.ReplaceHtmlTag().SqlFilters(),
                                    IP = ip,
                                    From = NetHelper.GetAddressByIP(ip)
                                };
                                int i = feedbackBll.SaveForm("", entity);
                                if (i > 0)
                                {
                                    //验证码使用后清除缓存
                                    Cache.Factory.CacheFactory.Cache().RemoveCache(GlobalStaticConstant.Session_Key_Verification_Code + "_" + arg.VerificationCode.ToUpper());

                                    //发送邮件提醒管理员
                                    string template = @"<table style='width: 100%;height: 100%;border-collapse:collapse'>
			                            <tr>
				                            <td>昵称：{0}</td>
			                            </tr>
			                            <tr>
				                            <td>联系方式：{1}</td>
			                            </tr>
			                            <tr>
				                            <td>反馈内容：{2}</td>
			                            </tr>
		                            </table>";

                                    string res = String.Format(template, arg.NickName, arg.Contact, arg.Content);
                                    string address = ConfigHelper.GetValue("ErrorReportTo");
                                    MailHelper.SendByThread(address, "来自【" + arg.NickName + "】意见反馈，请及时处理！", res.ToString());

                                    resultMsg = new BaseJson<string>
                                    {
                                        Status = (int)JsonObjectStatus.Success,
                                        Data = "意见保存成功！",
                                        Message = JsonObjectStatus.Success.GetEnumText(),
                                        BackUrl = null
                                    };
                                }
                                else
                                {
                                    resultMsg = new BaseJson<string>
                                    {
                                        Status = (int)JsonObjectStatus.Fail,
                                        Data = "",
                                        Message = JsonObjectStatus.Fail.GetEnumText(),
                                        BackUrl = null
                                    };
                                }
                            }
                            else
                            {
                                resultMsg = new BaseJson<string>
                                {
                                    Status = (int)JsonObjectStatus.ValidateCodeErr,
                                    Data = null,
                                    Message = JsonObjectStatus.ValidateCodeErr.GetEnumText(),
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
                                Message = JsonObjectStatus.Fail.GetEnumText() + "，验证码失效，请重新获取！",
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
                        Message = JsonObjectStatus.Fail.GetEnumText() + "，请求参数VerificationCode为空。",
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

        #endregion

        #region 私有方法
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

        #endregion
    }
}

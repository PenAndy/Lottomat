using Lottomat.Application.Busines;
using Lottomat.Application.Busines.AuthorizeManage;
using Lottomat.Application.Busines.BaseManage;
using Lottomat.Application.Busines.SystemManage;
using Lottomat.Application.Code;
using Lottomat.Application.Entity;
using Lottomat.Application.Entity.BaseManage;
using Lottomat.Application.Entity.SystemManage;
using Lottomat.Util;
using Lottomat.Util.Attributes;
using Lottomat.Util.Extension;
using Lottomat.Util.WebControl;
using System;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Threading;
using System.Web.Mvc;
using Lottomat.Application.Code.Authorize;
using Lottomat.Utils;
using Lottomat.Utils.Date;
using Lottomat.Utils.Security;
using Lottomat.Utils.Web;

namespace Lottomat.Application.Admin.Controllers
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2015.09.01 13:32
    /// 描 述：系统登录
    /// </summary>
    [HandlerLogin(LoginMode.Ignore)]
    public class LoginController : MvcControllerBase
    {
        #region 视图功能
        /// <summary>
        /// 默认页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Default()
        {
            return View();
        }
        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult VerifyCode()
        {
            return File(new VerifyCode().GetVerifyCode(), @"image/Gif");
        }
        /// <summary>
        /// 安全退出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult OutLogin()
        {
            LogEntity logEntity = new LogEntity
            {
                CategoryId = (int)CategoryType.Login,
                OperateTypeId = ((int)OperationType.Exit).ToString(),
                OperateType = OperationType.Exit.GetEnumDescription(),
                OperateAccount = OperatorProvider.Provider.Current().Account,
                OperateUserId = OperatorProvider.Provider.Current().UserId,
                ExecuteResult = 1,
                ExecuteResultJson = "退出系统",
                Module = ConfigHelper.GetValue("SoftName")
            };
            logEntity.WriteLog();
            Session.Abandon();                                          //清除当前会话
            Session.Clear();                                            //清除当前浏览器所有Session
            OperatorProvider.Provider.EmptyCurrent(); ;                  //清除登录者信息
            WebHelper.RemoveCookie("__autologin");                  //清除自动登录
            return Content(new AjaxResult<string> { type = ResultType.Success, message = "退出系统" }.ToJson());
        }
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="verifycode">验证码</param>
        /// <param name="autologin">下次自动登录</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult CheckLogin(string username, string password, string verifycode, int autologin)
        {
            LogEntity logEntity = new LogEntity
            {
                CategoryId = (int)CategoryType.Login,
                OperateTypeId = ((int)OperationType.Login).ToString(),
                OperateType = OperationType.Login.GetEnumDescription(),
                OperateAccount = username,
                OperateUserId = username,
                Module = ConfigHelper.GetValue("SoftName")
            };

            try
            {
                #region 验证码验证
                if (autologin == 0)
                {
                    verifycode = Md5Helper.MD5(verifycode.ToLower(), 16);
                    if (Session["session_verifycode"].IsEmpty() || verifycode != Session["session_verifycode"].ToString())
                    {
                        throw new Exception("验证码错误，请重新输入");
                    }
                }
                #endregion

                #region 第三方账户验证 modify by chengzg 20160812 关闭该验证
                //AccountEntity accountEntity = accountBLL.CheckLogin(username, password);
                //if (accountEntity != null)
                //{
                //    Operator operators = new Operator();
                //    operators.UserId = accountEntity.AccountId;
                //    operators.Code = accountEntity.MobileCode;
                //    operators.Account = accountEntity.MobileCode;
                //    operators.UserName = accountEntity.FullName;
                //    operators.Password = accountEntity.Password;
                //    operators.IPAddress = Net.Ip;
                //    operators.IPAddressName = IPLocation.GetLocation(Net.Ip);
                //    operators.LogTime = DateTimeHelper.Now;
                //    operators.Token = DESEncrypt.Encrypt(CommonHelper.GetGuid().ToString());
                //    operators.IsSystem = true;
                //    OperatorProvider.Provider.AddCurrent(operators);
                //    //登录限制
                //    LoginLimit(username, operators.IPAddress, operators.IPAddressName);
                //    return Success("登录成功。");
                //}
                #endregion

                #region 内部账户验证
                UserEntity userEntity = new UserBLL().CheckLogin(username, password);
                if (userEntity != null)
                {
                    AuthorizeBLL authorizeBLL = new AuthorizeBLL();
                    Operator operators = new Operator
                    {
                        UserId = userEntity.UserId,
                        Code = userEntity.EnCode,
                        Account = userEntity.Account,
                        UserName = userEntity.RealName,
                        Password = userEntity.Password,
                        Secretkey = userEntity.Secretkey,
                        CompanyId = userEntity.OrganizeId,
                        DepartmentId = userEntity.DepartmentId,
                        IPAddress = NetHelper.Ip,
                        IPAddressName = IPLocation.GetLocation(NetHelper.Ip),
                        ObjectId = new PermissionBLL().GetObjectStr(userEntity.UserId),
                        LogTime = DateTimeHelper.Now,
                        Token = DESEncrypt.Encrypt(CommonHelper.GetGuid().ToString())
                    };
                    //写入当前用户数据权限
                    AuthorizeDataModel dataAuthorize = new AuthorizeDataModel
                    {
                        ReadAutorize = authorizeBLL.GetDataAuthor(operators),
                        ReadAutorizeUserId = authorizeBLL.GetDataAuthorUserId(operators),
                        WriteAutorize = authorizeBLL.GetDataAuthor(operators, true),
                        WriteAutorizeUserId = authorizeBLL.GetDataAuthorUserId(operators, true)
                    };
                    operators.DataAuthorize = dataAuthorize;
                    //判断是否系统管理员
                    operators.IsSystem = userEntity.Account == "System";
                    OperatorProvider.Provider.AddCurrent(operators);
                    //登录限制  modify by chengzg 20160812 关闭该验证
                    //LoginLimit(username, operators.IPAddress, operators.IPAddressName);
                    //写入日志
                    logEntity.ExecuteResult = 1;
                    logEntity.ExecuteResultJson = "登录成功";
                    logEntity.WriteLog();
                }
                return Success("登录成功。");
                #endregion
            }
            catch (Exception ex)
            {
                WebHelper.RemoveCookie("__autologin");                  //清除自动登录
                logEntity.ExecuteResult = -1;
                logEntity.ExecuteResultJson = ex.Message;
                logEntity.WriteLog();
                return Error(ex.Message);
            }
        }
        #endregion

        #region 注册账户、登录限制
        private AccountBLL accountBLL = new AccountBLL();
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="mobileCode">手机号码</param>
        /// <returns>返回6位数验证码</returns>
        [HttpGet]
        public ActionResult GetSecurityCode(string mobileCode)
        {
            if (!ValidateUtil.IsValidMobile(mobileCode))
            {
                throw new Exception("手机格式不正确,请输入正确格式的手机号码。");
            }
            var data = accountBLL.GetSecurityCode(mobileCode);
            if (!string.IsNullOrEmpty(data))
            {
                SmsModel smsModel = new SmsModel
                {
                    account = ConfigHelper.GetValue("SMSAccount"),
                    pswd = ConfigHelper.GetValue("SMSPswd"),
                    url = ConfigHelper.GetValue("SMSUrl"),
                    mobile = mobileCode,
                    msg = "验证码 " + data + "，(请确保是本人操作且为本人手机，否则请忽略此短信)"
                };
                SmsHelper.SendSmsByJM(smsModel);
            }
            return Success("获取成功。");
        }
        /// <summary>
        /// 注册账户
        /// </summary>
        /// <param name="mobileCode">手机号</param>
        /// <param name="securityCode">短信验证码</param>
        /// <param name="fullName">姓名</param>
        /// <param name="password">密码（md5）</param>
        /// <param name="verifycode">图片验证码</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(string mobileCode, string securityCode, string fullName, string password, string verifycode)
        {
            AccountEntity accountEntity = new AccountEntity
            {
                MobileCode = mobileCode,
                SecurityCode = securityCode,
                FullName = fullName,
                Password = password,
                IPAddress = NetHelper.Ip
            };
            accountEntity.IPAddressName = IPLocation.GetLocation(accountEntity.IPAddress);
            accountEntity.AmountCount = 30;
            accountBLL.Register(accountEntity);
            return Success("注册成功。");
        }
        /// <summary>
        /// 登录限制
        /// </summary>
        /// <param name="account">账户</param>
        /// <param name="iPAddress">IP</param>
        /// <param name="iPAddressName">IP所在城市</param>
        public void LoginLimit(string account, string iPAddress, string iPAddressName)
        {
            //if (account == "System")
            //{
            //    return;
            //}
            string platform = NetHelper.Browser;
            accountBLL.LoginLimit(platform, account, iPAddress, iPAddressName);
        }
        #endregion
    }
}

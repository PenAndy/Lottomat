using Lottomat.Application.Busines.BaseManage;
using Lottomat.Application.Busines.SystemManage;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.BaseManage;
using Lottomat.Application.Entity.SystemManage;
using Lottomat.Cache.Factory;
using Lottomat.Cache;
using System;
using System.Net.Http;
using System.Web.Http;
using Lottomat.SOA.API.Controllers.Base;
using Lottomat.Util;
using Lottomat.Util.Extension;
using Lottomat.Utils.Date;
using Lottomat.Utils.Security;

namespace Lottomat.SOA.API.Controllers
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2015.09.01 13:32
    /// 描 述：单点登录
    /// </summary>
    public class LoginController : BaseApiController
    {
        /// <summary>
        /// 测试是否连接成功
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage HelloWorld()
        {
            LogEntity logEntity = new LogEntity
            {
                CategoryId = (int)CategoryType.Login,
                OperateTypeId = ((int)OperationType.Login).ToString(),
                OperateType = OperationType.Login.GetEnumDescription(),
                OperateAccount = "18284594619",
                OperateUserId = "18284594619",
                Module = ""
            };

            return logEntity.ToHttpResponseMessage();
        }
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="system">系统</param>
        /// <param name="account">账户</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage CheckLogin(string system, string account, string password)
        {
            LogEntity logEntity = new LogEntity
            {
                CategoryId = (int)CategoryType.Login,
                OperateTypeId = ((int)OperationType.Login).ToString(),
                OperateType = OperationType.Login.GetEnumDescription(),
                OperateAccount = account,
                OperateUserId = account,
                Module = system
            };

            try
            {
                //验证账户
                UserEntity userEntity = new UserBLL().CheckLogin(account, password);

                //生成票据
                string ticket = Md5Helper.MD5(CommonHelper.GetGuid(), 32);
                //写入票据
                CacheFactory.Cache().WriteCache(userEntity, ticket, DateTimeHelper.Now.AddHours(8));

                //写入日志
                logEntity.ExecuteResult = 1;
                logEntity.ExecuteResultJson = "登录成功";
                logEntity.WriteLog();

                return Success("登录成功", ticket);
            }
            catch (Exception ex)
            {
                logEntity.ExecuteResult = -1;
                logEntity.ExecuteResultJson = ex.Message;
                logEntity.WriteLog();
                return Error(ex.Message);
            }
        }
        /// <summary>
        /// 票据验证
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage CheckTicket(string ticket)
        {
            UserEntity userEntity = CacheFactory.Cache().GetCache<UserEntity>(ticket);
            if (userEntity != null)
            {
                return Success("通过", userEntity);
            }
            else
            {
                return Error("错误");
            }
        }
    }
}
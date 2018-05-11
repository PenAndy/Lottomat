using Lottomat.Application.Entity;
using Lottomat.Application.IService;
using Lottomat.Data;
using Lottomat.Data.Repository;
using Lottomat.Util;
using Lottomat.Util.Extension;
using System;
using System.Data.Common;
using System.Linq;
using Lottomat.Application.Code;
using Lottomat.Utils.Date;
using Lottomat.Utils.Web;

namespace Lottomat.Application.Service
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2016.05.11 16:23
    /// 描 述：注册账户
    /// </summary>
    public class AccountService : RepositoryFactory<AccountEntity>, IAccountService
    {
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="mobileCode">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public AccountEntity CheckLogin(string mobileCode, string password)
        {
            var expression = LinqExtensions.True<AccountEntity>();
            expression = expression.And(t => t.MobileCode == mobileCode);
            expression = expression.And(t => t.DeleteMark == (int)DeleteMarkEnum.NotDelete);
            return this.BaseRepository("AccountDb").FindEntity(expression);
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="mobileCode">手机号码</param>
        /// <returns>返回6位数验证码</returns>
        public string GetSecurityCode(string mobileCode)
        {
            if (!this.BaseRepository("AccountDb").IQueryable(t => t.MobileCode == mobileCode).Any())
            {
                //验证每个IP 不能获取超过5次
                if (this.BaseRepository("AccountDb").IQueryable(t => t.IPAddress == NetHelper.Ip).Count() >= 5)
                {
                    throw new Exception("获取验证码失败。");
                }
                AccountEntity accountEntity = new AccountEntity
                {
                    AccountId = CommonHelper.GetGuid().ToString(),
                    MobileCode = mobileCode,
                    SecurityCode = CommonHelper.RndNum(6),
                    IPAddress = NetHelper.Ip,
                    CreateDate = DateTimeHelper.Now,
                    EnabledMark = -1
                };
                this.BaseRepository("AccountDb").Insert(accountEntity);
                return accountEntity.SecurityCode;
            }
            else
            {
                throw new Exception("手机号已被注册过了。");
            }
        }
        /// <summary>
        /// 注册账户
        /// </summary>
        /// <param name="accountEntity">实体对象</param>
        public void Register(AccountEntity accountEntity)
        {
            var data = this.BaseRepository("AccountDb").FindEntity(t => t.MobileCode == accountEntity.MobileCode);
            if (data == null && data.SecurityCode == accountEntity.SecurityCode)
            {
                throw new Exception("短信验证码不正确。");
            }
            if (data.RegisterTime != null)
            {
                throw new Exception("手机号已被注册过了。");
            }
            accountEntity.AccountId = data.AccountId;
            accountEntity.RegisterTime = DateTimeHelper.Now;
            accountEntity.ExpireTime = DateTimeHelper.Now.AddMonths(1);
            accountEntity.DeleteMark = (int)DeleteMarkEnum.NotDelete;
            accountEntity.EnabledMark = 1;
            this.BaseRepository("AccountDb").Update(accountEntity);
        }
        /// <summary>
        /// 登录限制
        /// </summary>
        /// <param name="platform">平台（苹果、安卓、PC浏览器）</param>
        /// <param name="account">账户</param>
        /// <param name="iPAddress">IP地址</param>
        /// <param name="iPAddressName">IP所在城市</param>
        public void LoginLimit(string platform, string account, string iPAddress, string iPAddressName)
        {
            //调用存储过程
            DbParameter[] parameter = 
            {
                DbParameters.CreateDbParameter("@IPAddress",iPAddress),
                DbParameters.CreateDbParameter("@IPAddressName",iPAddressName),
                DbParameters.CreateDbParameter("@Account",account),
                DbParameters.CreateDbParameter("@Platform",platform),
            };
            int IsOk = this.BaseRepository("AccountDb").ExecuteByProc("PROC_verify_IPAddress", parameter);
        }
    }
}

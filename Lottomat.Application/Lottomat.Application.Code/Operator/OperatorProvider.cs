using Lottomat.Cache.Factory;
using Lottomat.Util;
using System;
using Lottomat.Application.Code.Authorize;
using Lottomat.Util.Extension;
using Lottomat.Utils;
using Lottomat.Utils.Security;
using Lottomat.Utils.Web;

namespace Lottomat.Application.Code
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2015.10.10
    /// 描 述：当前操作者回话
    /// </summary>
    public class OperatorProvider : IOperatorIProvider
    {
        #region 静态实例
        /// <summary>
        /// 当前提供者
        /// </summary>
        public static IOperatorIProvider Provider
        {
            get { return new OperatorProvider(); }
        }
        /// <summary>
        /// 给app调用
        /// </summary>
        public static string AppUserId
        {
            set;
            get;
        }
        #endregion

        /// <summary>
        /// 秘钥
        /// </summary>
        private string LoginUserKey = "System_Login_User_Key";
        /// <summary>
        /// 登陆提供者模式:Session、Cookie 
        /// </summary>
        private string LoginProvider = ConfigHelper.GetValue("LoginProvider");

        private IOperatorIProvider _operatorIProviderImplementation;

        /// <summary>
        /// 写入登录信息
        /// </summary>
        /// <param name="user">成员信息</param>
        public virtual void AddCurrent(Operator user)
        {
            try
            {
                if (LoginProvider == "Cookie")
                {
                    #region 解决cookie时，设置数据权限较多时无法登陆的bug 
                    CacheFactory.Cache().WriteCache(user.DataAuthorize, LoginUserKey, user.LogTime.AddHours(12));
                    user.DataAuthorize = null;
                    #endregion

                    WebHelper.WriteCookie(LoginUserKey, DESEncrypt.Encrypt(user.ToJson()));
                }
                else
                {
                    WebHelper.WriteSession(LoginUserKey, DESEncrypt.Encrypt(user.ToJson()));
                }
                CacheFactory.Cache().WriteCache(user.Token, user.UserId, user.LogTime.AddHours(12));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 当前用户
        /// </summary>
        /// <returns></returns>
        public virtual Operator Current()
        {
            try
            {
                Operator user = new Operator();
                if (LoginProvider == "Cookie")
                {
                    user = DESEncrypt.Decrypt(WebHelper.GetCookie(LoginUserKey).ToString()).ToObject<Operator>();

                    #region 解决cookie时，设置数据权限较多时无法登陆的bug modify by chengzg 2016.8.19 13:15
                    AuthorizeDataModel dataAuthorize = CacheFactory.Cache().GetCache<AuthorizeDataModel>(LoginUserKey);
                    user.DataAuthorize = dataAuthorize;
                    #endregion
                }
                else if (LoginProvider == "AppClient")
                {
                    user = CacheFactory.Cache().GetCache<Operator>(AppUserId);
                }
                else
                {
                    user = DESEncrypt.Decrypt(WebHelper.GetSession(LoginUserKey).ToString()).ToObject<Operator>();
                }
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 删除登录信息
        /// </summary>
        public virtual void EmptyCurrent()
        {
            if (LoginProvider == "Cookie")
            {
                WebHelper.RemoveCookie(LoginUserKey.Trim());

                #region 解决cookie时，设置数据权限较多时无法登陆的bug modify by chengzg 2016.8.19 13:15
                CacheFactory.Cache().RemoveCache(LoginUserKey);
                #endregion
            }
            else
            {
                WebHelper.RemoveSession(LoginUserKey.Trim());
            }
        }
        /// <summary>
        /// 是否过期
        /// </summary>
        /// <returns></returns>
        public virtual bool IsOverdue()
        {
            try
            {
                object str = "";
                AuthorizeDataModel dataAuthorize = null;
                if (LoginProvider == "Cookie")
                {
                    str = WebHelper.GetCookie(LoginUserKey);

                    #region 解决cookie时，设置数据权限较多时无法登陆的bug modify by chengzg 2016.8.19 13:15
                    dataAuthorize = CacheFactory.Cache().GetCache<AuthorizeDataModel>(LoginUserKey);

                    if (dataAuthorize == null)
                    {
                        return true;
                    }
                    #endregion
                }
                else
                {
                    str = WebHelper.GetSession(LoginUserKey);
                }
                if (str != null && str.ToString() != "")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return true;
            }
        }
        /// <summary>
        /// 是否已登录
        /// </summary>
        /// <returns></returns>
        public virtual int IsOnLine()
        {
            Operator user = new Operator();
            if (LoginProvider == "Cookie")
            {
                user = DESEncrypt.Decrypt(WebHelper.GetCookie(LoginUserKey).ToString()).ToObject<Operator>();

                #region 解决cookie时，设置数据权限较多时无法登陆的bug modify by chengzg 2016.8.19 13:15
                AuthorizeDataModel dataAuthorize = CacheFactory.Cache().GetCache<AuthorizeDataModel>(LoginUserKey);
                user.DataAuthorize = dataAuthorize;
                #endregion
            }
            else
            {
                user = DESEncrypt.Decrypt(WebHelper.GetSession(LoginUserKey).ToString()).ToObject<Operator>();
            }
            object token = CacheFactory.Cache().GetCache<string>(user.UserId);
            if (token == null)
            {
                return -1;//过期
            }
            if (user.Token == token.ToString())
            {
                return 1;//正常
            }
            else
            {
                return 0;//已登录
            }
        }
    }
}

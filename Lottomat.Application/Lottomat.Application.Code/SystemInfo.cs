using Lottomat.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lottomat.Utils;
using Lottomat.Utils.Web;

namespace Lottomat.Application.Code
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2015.11.15 9:10:45
    /// 描 述：系统信息
    /// </summary>
    public class SystemInfo
    {
        /// <summary>
        /// 当前Tab页面模块Id
        /// </summary>
        public static string CurrentModuleId => WebHelper.GetCookie("currentmoduleId");

        /// <summary>
        /// 当前登录用户Id
        /// </summary>
        public static string CurrentUserId => OperatorProvider.Provider.Current().UserId;
    }
}

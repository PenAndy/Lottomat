using System;

namespace Lottomat.Application.Code.Authorize
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2016.03.28
    /// 描 述：用户数据权限
    /// </summary>
    [Serializable]
    public class AuthorizeDataModel
    {
        /// <summary>
        /// 功能模块主键
        /// </summary>
        public string ModuleId { get; set; }
        /// <summary>
        /// 获得有权限的数据列表SQL语句
        /// </summary>
        public string ReadAutorize { get; set; }
        /// <summary>
        /// 可读用户ID
        /// </summary>
        public string ReadAutorizeUserId { get; set; }
        /// <summary>
        /// 可写数据权限SQL语句
        /// </summary>
        public string WriteAutorize { get; set; }
        /// <summary>
        /// 可写数据权限
        /// </summary>
        public string WriteAutorizeUserId { get; set; }
    }
}

using Lottomat.Util.Extension;

namespace Lottomat.Application.Code
{
    #region ===========全局通用状态枚举===========
    /// <summary>
    /// 全局通用状态枚举
    /// </summary>
    public enum JsonObjectStatus
    {
        #region 系统
        /// <summary>
        /// 默认，无实际意义。
        /// </summary>
        Default = -1,

        /// <summary>
        /// 请求(或处理)成功
        /// </summary>
        [Text("请求(或处理)成功")]
        Success = 200,

        /// <summary>
        /// 内部请求出错
        /// </summary>
        [Text("内部请求出错")]
        Error = 500,

        /// <summary>
        /// 操作失败
        /// </summary>
        [Text("操作失败")]
        Fail = 408,

        /// <summary>
        /// 服务器异常
        /// </summary>
        [Text("服务器异常")]
        Exception = 409,
        #endregion

        #region Http请求
        /// <summary>
        /// 请求授权信息参数不完整或不正确
        /// </summary>
        [Text("请求授权信息参数不完整或不正确")]
        ParameterError = 400,

        /// <summary>
        /// 未授权
        /// </summary>
        [Text("未授权")]
        Unauthorized = 401,

        /// <summary>
        /// 请求Token授权信息参数【用户编号】不存在
        /// </summary>
        [Text("请求Token授权信息参数【用户编号】不存在")]
        ParameterStaffIdNotExist = 402,

        /// <summary>
        /// 请求TOKEN失效
        /// </summary>
        [Text("请求TOKEN失效")]
        TokenInvalid = 403,

        /// <summary>
        /// HTTP请求类型不合法
        /// </summary>
        [Text("HTTP请求类型不合法")]
        HttpMehtodError = 405,

        /// <summary>
        /// HTTP请求不合法,请求参数可能被篡改
        /// </summary>
        [Text("HTTP请求不合法，请求参数可能被篡改")]
        HttpRequestError = 406,

        /// <summary>
        /// 该URL已经失效或请求时间戳失效
        /// </summary>
        [Text("该URL已经失效或请求时间戳失效")]
        UrlExpireError = 407,
        #endregion

        #region 登录验证
        /// <summary>
        /// 账号错误
        /// </summary>
        [Text("账号错误")]
        AccountErr = 410,

        /// <summary>
        /// 密码错误
        /// </summary>
        [Text("密码错误")]
        PasswordErr = 411,

        /// <summary>
        /// 验证码错误
        /// </summary>
        [Text("验证码错误")]
        ValidateCodeErr = 412,

        /// <summary>
        /// 用户不存在
        /// </summary>
        [Text("用户不存在")]
        UserNotExist = 413,

        /// <summary>
        /// 记录已经存在
        /// </summary>
        [Text("记录已经存在")]
        RecordAlreadyExists = 414,

        /// <summary>
        /// 记录不存在
        /// </summary>
        [Text("记录不存在")]
        RecordNotExists = 419,
        #endregion

        #region 账号状态
        /// <summary>
        /// 正常
        /// </summary>
        [Text("正常")]
        AccountNormal = 415,
        /// <summary>
        /// 账号被锁定
        /// </summary>
        [Text("账号被锁定")]
        AccountLock = 416,
        /// <summary>
        /// 账号被禁用
        /// </summary>
        [Text("账号被禁用")]
        AccountDisable = 417,
        /// <summary>
        /// 未启用
        /// </summary>
        [Text("未启用")]
        AccountNotEnabled = 418,
        #endregion
        
    }
    #endregion
}
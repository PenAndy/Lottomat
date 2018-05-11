using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lottomat.Util.WebControl
{
    /// <summary>
    /// 表示Ajax操作结果 
    /// </summary>
    public class AjaxResult<T>
    {
        /// <summary>
        /// 获取 Ajax操作结果类型
        /// </summary>
        public ResultType type { get; set; }

        /// <summary>
        /// 获取 Ajax操作结果编码
        /// </summary>
        public int errorcode { get; set; }

        /// <summary>
        /// 获取 消息内容
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 获取 返回数据
        /// </summary>
        public T resultdata { get; set; }
    }
    public class AjaxJsonResult<T> {
        /// <summary>
        /// 状态码
        /// </summary>
        public ResultType code { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 内容载体
        /// </summary>
        public T obj { get; set; }
    }

    /// <summary>
    /// 表示 ajax 操作结果类型的枚举
    /// </summary>
    public enum ResultType
    {
        /// <summary>
        /// 消息结果类型
        /// </summary>
        Info = 0,

        /// <summary>
        /// 成功结果类型
        /// </summary>
        Success = 1,

        /// <summary>
        /// 警告结果类型
        /// </summary>
        Warning = 2,

        /// <summary>
        /// 异常结果类型
        /// </summary>
        Error = 3
    }
}

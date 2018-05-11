using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lottomat.Utils.Json
{
    #region 标准Json基类 不带分页

    /// <summary>
    /// 基类Json 非分页
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class BaseJson<T>
    {
        /// <summary>
        /// 状态码
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        [JsonProperty("data")]
        public T Data { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        [JsonProperty("msg")]
        public string Message { get; set; }

        /// <summary>
        /// 跳转地址
        /// </summary>
        [JsonProperty("backurl")]
        public string BackUrl { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        [JsonProperty("executiontime")]
        public string ExecutionTime { get; set; }
    }

    /// <summary>
    /// 返回到前台的数据模型基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class BaseResultModel<T>
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int records { get; set; }

        /// <summary>
        /// 查询花费的时间
        /// </summary>
        public string costtime { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public T rows { get; set; }
    }
    #endregion

    #region 标准Json基类 带分页
    /// <summary>
    /// 基类Json 带分页
    /// </summary>
    /// <typeparam name="T">实体</typeparam>
    public class BaseJson4Page<T>
    {
        /// <summary>
        /// 状态码
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        [JsonProperty("data")]
        public PageData<T> Data { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        [JsonProperty("msg")]
        public string Message { get; set; }

        /// <summary>
        /// 跳转地址
        /// </summary>
        [JsonProperty("backurl")]
        public string BackUrl { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        [JsonProperty("executiontime")]
        public string ExecutionTime { get; set; }
    }

    /// <summary>
    /// 分页数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageData<T>
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage { get; set; }

        /// <summary>
        /// 总行数
        /// </summary>
        public int TotalRow { get; set; }

        /// <summary>
        /// 数据行
        /// </summary>
        public List<T> Rows { get; set; }
    }

    #endregion
}
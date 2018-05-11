namespace Lottomat.Application.Entity.LotteryNumberManage.Parameter
{
    /// <summary>
    /// 基础参数实体
    /// </summary>
    public class BaseParameterEntity
    {
        /// <summary>
        /// 校验值，一般为时间戳
        /// </summary>
        public string t { get; set; }
        /// <summary>
        /// APPKey
        /// </summary>
        public string Appkey { get; set; }
        /// <summary>
        /// 8位随机数
        /// </summary>
        public string Nonce { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        public string AccessToken { get; set; }
    }
}
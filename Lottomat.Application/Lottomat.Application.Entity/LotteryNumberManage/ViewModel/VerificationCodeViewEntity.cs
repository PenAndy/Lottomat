namespace Lottomat.Application.Entity.LotteryNumberManage.ViewModel
{
    /// <summary>
    /// 验证码
    /// </summary>
    public class VerificationCodeViewEntity
    {
        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public string ExpiryTime { get; set; }
    }
}
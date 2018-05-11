namespace Lottomat.Application.Entity.LotteryNumberManage.Parameter
{
    /// <summary>
    /// 验证码
    /// </summary>
    public class VerificationCodeArgEntity : BaseParameterEntity
    {
        /// <summary>
        /// 1-系统级验证码 2-手机短信验证码 3-邮箱验证码
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Feedback-意见反馈 Login-登陆 Register-注册
        /// </summary>
        public string Modular { get; set; }
    }
}
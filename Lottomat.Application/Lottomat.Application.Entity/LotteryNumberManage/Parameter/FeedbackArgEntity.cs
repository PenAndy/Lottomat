namespace Lottomat.Application.Entity.LotteryNumberManage.Parameter
{
    /// <summary>
    /// 意见反馈
    /// </summary>
    public class FeedbackArgEntity : BaseParameterEntity
    {
        /// <summary>
        /// 验证码
        /// </summary>
        public string VerificationCode { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string Contact{ get; set; }
        /// <summary>
        /// 反馈内容
        /// </summary>
        public string Content { get; set; }
    }
}
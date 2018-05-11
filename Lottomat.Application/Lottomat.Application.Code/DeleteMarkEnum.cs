namespace Lottomat.Application.Code
{
    /// <summary>
    /// 删除标记
    /// </summary>
    public enum DeleteMarkEnum
    {
        /// <summary>
        /// 正常
        /// </summary>
        NotDelete = 0,
        /// <summary>
        /// 逻辑删除
        /// </summary>
        Delete = 1,
        /// <summary>
        /// 彻底删除
        /// </summary>
        DeleteCompletely = 2,

    }
}
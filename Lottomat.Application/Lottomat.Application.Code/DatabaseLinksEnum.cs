using Lottomat.Util;
using Lottomat.Util.Extension;

namespace Lottomat.Application.Code
{
    /// <summary>
    /// 数据库链接枚举
    /// </summary>
    public enum DatabaseLinksEnum
    {
        /// <summary>
        /// 基础库
        /// </summary>
        Base,
        
        /// <summary>
        /// 资讯数据库
        /// </summary>
        InformationBase,

        /// <summary>
        /// 图库
        /// </summary>
        GalleryBase,

        /// <summary>
        /// CP55128
        /// </summary>
        CP55128,

        /// <summary>
        /// CB55128
        /// </summary>
        CB55128,

        /// <summary>
        /// 开奖号
        /// </summary>
        LotteryNumber,

        /// <summary>
        /// 专题数据
        /// </summary>
        ThematicArticle
    }
}
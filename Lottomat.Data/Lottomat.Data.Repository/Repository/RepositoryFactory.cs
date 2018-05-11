using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
using Lottomat.Application.Code;
using Lottomat.Util;

namespace Lottomat.Data.Repository
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2015.10.10
    /// 描 述：定义仓储模型工厂
    /// </summary>
    public class RepositoryFactory
    {
        /// <summary>
        /// 定义仓储
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <returns></returns>
        public IRepository BaseRepository(string connString)
        {
            return new Repository(DbFactory.Base(connString, DatabaseType.SqlServer));
        }
        /// <summary>
        /// 定义仓储（基础库）
        /// </summary>
        /// <returns></returns>
        public IRepository BaseRepository(DatabaseLinksEnum links = DatabaseLinksEnum.Base)
        {
            switch (links)
            {
                case DatabaseLinksEnum.Base:
                    return new Repository(DbFactory.Base());
                case DatabaseLinksEnum.InformationBase:
                    return new Repository(DbFactory.Base(GlobalStaticConstant.DB_LINK_INFORMATION_BASE, DatabaseType.SqlServer));
                case DatabaseLinksEnum.GalleryBase:
                    return new Repository(DbFactory.Base(GlobalStaticConstant.DB_LINK_GALLERY_BASE, DatabaseType.SqlServer));
                case DatabaseLinksEnum.CP55128:
                    return new Repository(DbFactory.Base(GlobalStaticConstant.DB_LINK_CP55128_BASE, DatabaseType.SqlServer));
                case DatabaseLinksEnum.CB55128:
                    return new Repository(DbFactory.Base(GlobalStaticConstant.DB_LINK_CB55128_BASE, DatabaseType.SqlServer));
                case DatabaseLinksEnum.LotteryNumber:
                    return new Repository(DbFactory.Base(GlobalStaticConstant.DB_LINK_LOTTERY_NUMBER_BASE, DatabaseType.SqlServer));
                case DatabaseLinksEnum.ThematicArticle:
                    return new Repository(DbFactory.Base(GlobalStaticConstant.DB_LINK_THEMATIC_ARTICLE_BASE, DatabaseType.SqlServer));
                default:
                    return new Repository(DbFactory.Base());
            }
        }
    }
}

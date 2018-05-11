using System;
using System.Data.Entity.Infrastructure;

namespace Lottomat.Data.EF
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2016.04.07
    /// 描 述：数据库连接接口 
    /// </summary>
    public interface IDbContext: IDisposable, IObjectContextAdapter
    {
    }
}

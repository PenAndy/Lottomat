using Lottomat.Application.Busines.BaseManage;
using Lottomat.Application.Entity.BaseManage;
using Lottomat.Cache.Factory;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Lottomat.Application.Cache
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2016.3.4 9:56
    /// 描 述：职位信息缓存
    /// </summary>
    public class JobCache
    {
        private JobBLL busines = new JobBLL();
        
        /// <summary>
        /// 职位列表
        /// </summary>
        /// <param name="organizeId">机构Id</param>
        /// <returns></returns>
        public IEnumerable<RoleEntity> GetList(string organizeId)
        {
            IEnumerable<RoleEntity> data = this.GetList();
            if (!string.IsNullOrEmpty(organizeId))
            {
                data = data.Where(t => t.OrganizeId == organizeId);
            }
            return data;
        }

        /// <summary>
        /// 职位列表
        /// </summary>
        /// <returns></returns>
        private IEnumerable<RoleEntity> GetList()
        {
            IEnumerable<RoleEntity> cacheList = CacheFactory.Cache().GetCache<IEnumerable<RoleEntity>>(busines.cacheKey);
            if (cacheList == null)
            {
                IEnumerable<RoleEntity> data = busines.GetList();

                CacheFactory.Cache().WriteCache(data, busines.cacheKey);
                return data;
            }
            else
            {
                return cacheList;
            }
        }
    }
}

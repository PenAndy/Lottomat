using Lottomat.Application.Entity.AuthorizeManage;
using Lottomat.Application.IService.AuthorizeManage;
using Lottomat.Data.Repository;
using Lottomat.Util.Extension;
using System.Collections.Generic;
using System.Linq;

namespace Lottomat.Application.Service.BaseManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2015.08.01 14:00
    /// 描 述：系统按钮
    /// </summary>
    public class ModuleButtonService : RepositoryFactory<ModuleButtonEntity>, IModuleButtonService
    {
        #region 获取数据
        /// <summary>
        /// 按钮列表
        /// </summary>
        /// <returns></returns>
        public List<ModuleButtonEntity> GetList()
        {
            return this.BaseRepository().IQueryable().OrderBy(t => t.SortCode).ToList();
        }
        /// <summary>
        /// 按钮列表
        /// </summary>
        /// <param name="moduleId">功能Id</param>
        /// <returns></returns>
        public List<ModuleButtonEntity> GetList(string moduleId)
        {
            var expression = LinqExtensions.True<ModuleButtonEntity>();
            expression = expression.And(t => t.ModuleId.Equals(moduleId));
            return this.BaseRepository().IQueryable(expression).OrderBy(t => t.SortCode).ToList();
        }
        /// <summary>
        /// 按钮实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ModuleButtonEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="moduleButtonEntity">按钮实体</param>
        public void AddEntity(ModuleButtonEntity moduleButtonEntity)
        {
            moduleButtonEntity.Create();
            this.BaseRepository().Insert(moduleButtonEntity);
        }
        #endregion
    }
}

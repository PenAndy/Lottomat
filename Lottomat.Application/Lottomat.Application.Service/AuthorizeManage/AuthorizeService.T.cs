using Lottomat.Application.Code;
using Lottomat.Application.Entity.AuthorizeManage;
using Lottomat.Application.Entity.BaseManage;
using Lottomat.Application.IService.AuthorizeManage;
using Lottomat.Data;
using Lottomat.Data.Repository;
using Lottomat.Util.Extension;
using Lottomat.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lottomat.Application.Service.AuthorizeManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2016.03.29 22:35
    /// 描 述：授权认证
    /// </summary>
    public class AuthorizeService<T> : RepositoryFactory<T>, IAuthorizeService<T> where T : class,new()
    {
        private IRepository db = new RepositoryFactory().BaseRepository();
        private AuthorizeService authorizeService = new AuthorizeService();

        #region 带权限的数据源查询
        /// <summary>
        /// 带权限的数据源查询
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> IQueryable()
        {
            if (GetReadUserId() == "")
            {
                return this.BaseRepository().IQueryable();
            }
            else
            {
                var parameter = Expression.Parameter(typeof(T), "t");
                var authorConditon = Expression.Constant(GetReadUserId()).Call("Contains", parameter.Property("CreateUserId"));
                var lambda = authorConditon.ToLambda<Func<T, bool>>(parameter);
                return this.BaseRepository().IQueryable(lambda);
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public IQueryable<T> IQueryable(Expression<Func<T, bool>> condition)
        {
            if (GetReadUserId() != "")
            {
                var parameter = Expression.Parameter(typeof(T), "t");
                var authorConditon = Expression.Constant(GetReadUserId()).Call("Contains", parameter.Property("CreateUserId"));
                var lambda = authorConditon.ToLambda<Func<T, bool>>(parameter);
                condition = condition.And(lambda);
            }
            return db.IQueryable<T>(condition);
        }

        /// <summary>
        /// 得到分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        public IEnumerable<T> FindList(Pagination pagination)
        {
            if (GetReadUserId() == "")
            {
                return this.BaseRepository().FindList(pagination);
            }
            else
            {
                var parameter = Expression.Parameter(typeof(T), "t");
                var authorConditon = Expression.Constant(GetReadUserId()).Call("Contains", parameter.Property("CreateUserId"));
                var lambda = authorConditon.ToLambda<Func<T, bool>>(parameter);
                return this.BaseRepository().FindList(lambda, pagination);
            }
        }
        /// <summary>
        /// 得到分页数据
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        public IEnumerable<T> FindList(Expression<Func<T, bool>> condition, Pagination pagination)
        {
            if (GetReadUserId() != "")
            {
                var parameter = Expression.Parameter(typeof(T), "t");
                var authorConditon = Expression.Constant(GetReadUserId()).Call("Contains", parameter.Property("CreateUserId"));
                var lambda = authorConditon.ToLambda<Func<T, bool>>(parameter);
                condition = condition.And(lambda);
            }
            return this.BaseRepository().FindList(condition, pagination);
        }
        /// <summary>
        /// 得到分页数据
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns></returns>
        public IEnumerable<T> FindList(string strSql)
        {
            strSql = strSql + (GetReadSql() == "" ? "" : string.Format("and CreateUserId in({0})", GetReadSql()));
            return this.BaseRepository().FindList(strSql);
        }
        /// <summary>
        /// 得到分页数据
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="dbParameter">SQL参数</param>
        /// <returns></returns>
        public IEnumerable<T> FindList(string strSql, DbParameter[] dbParameter)
        {
            strSql = strSql + (GetReadSql() == "" ? "" : string.Format("and CreateUserId in({0})", GetReadSql()));
            return this.BaseRepository().FindList(strSql, dbParameter);
        }
        /// <summary>
        /// 得到分页数据
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        public IEnumerable<T> FindList(string strSql, Pagination pagination)
        {
            strSql = strSql + (GetReadSql() == "" ? "" : string.Format("and CreateUserId in({0})", GetReadSql()));
            return this.BaseRepository().FindList(strSql, pagination);
        }
        /// <summary>
        /// 得到分页数据
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="dbParameter">SQL参数</param>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        public IEnumerable<T> FindList(string strSql, DbParameter[] dbParameter, Pagination pagination)
        {
            strSql = strSql + (GetReadSql() == "" ? "" : string.Format("and CreateUserId in({0})", GetReadSql()));
            return this.BaseRepository().FindList(strSql, dbParameter, pagination);
        }
        #endregion

        #region 取数据权限用户
        /// <summary>
        /// 获取权限可读用户ID
        /// </summary>
        /// <returns></returns>
        private string GetReadUserId()
        {
            return OperatorProvider.Provider.Current().DataAuthorize.ReadAutorizeUserId;
        }
        /// <summary>
        /// 获得有权限的数据列表SQL语句
        /// </summary>
        /// <returns></returns>
        private string GetReadSql()
        {
            return OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize;
        }
        #endregion
    }
}

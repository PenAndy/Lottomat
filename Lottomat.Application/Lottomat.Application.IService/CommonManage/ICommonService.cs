using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.CommonEntity;

namespace Lottomat.Application.IService.CommonManage
{
    /// <summary>
    /// 公共
    /// </summary>
    public interface ICommonService
    {
        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="procName"></param>
        /// <returns></returns>
        object ExcuteSql(string procName, CommandType commandType, params DbParameter[] dbParameter);
        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbParameter"></param>
        /// <returns></returns>
        DataTable ExcuteSqlDataTable(string sql, DatabaseLinksEnum links = DatabaseLinksEnum.LotteryNumber, params DbParameter[] dbParameter);
        /// <summary>
        /// 获取分页DataTable
        /// </summary>
        /// <param name="strSql">T-SQL语句</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">索引</param>
        /// <param name="total">总记录</param>
        /// <returns></returns>
        DataTable FindTable(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex, out int total,
            DatabaseLinksEnum links = DatabaseLinksEnum.LotteryNumber);
        /// <summary>
        /// 获取首页未复查的数据
        /// </summary>
        /// <returns></returns>
        List<NotInvestigationEntity> GetNotInvestigationList();
    }
}
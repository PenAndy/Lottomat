using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.CommonEntity;
using Lottomat.Application.IService.CommonManage;
using Lottomat.Application.Service.CommonManage;
using Lottomat.Util;
using Lottomat.Util.Extension;

namespace Lottomat.Application.Busines.CommonManage
{
    /// <summary>
    /// 公共操作
    /// </summary>
    public class CommonBLL
    {
        private ICommonService commonService = new CommonService();

        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="procName"></param>
        /// <returns></returns>
        public object ExcuteSql(string procName, CommandType commandType = CommandType.Text, params DbParameter[] dbParameter)
        {
            return commonService.ExcuteSql(procName, commandType, dbParameter);
        }
        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable ExcuteSqlDataTable(string sql, DatabaseLinksEnum links = DatabaseLinksEnum.LotteryNumber, params DbParameter[] dbParameter)
        {
            return commonService.ExcuteSqlDataTable(sql, links, dbParameter);
        }

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
        public DataTable FindPageDataTable(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex, out int total, DatabaseLinksEnum links = DatabaseLinksEnum.LotteryNumber)
        {
            DataTable data = commonService.FindTable(strSql, orderField, isAsc, pageSize, pageIndex, out int totalRows, links);

            total = totalRows;

            return data;
        }

        /// <summary>
        /// 获取首页未复查的数据
        /// </summary>
        /// <returns></returns>
        public List<NotInvestigationEntity> GetNotInvestigationList()
        {
            return commonService.GetNotInvestigationList();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.CommonEntity;
using Lottomat.Application.IService.CommonManage;
using Lottomat.Data.Repository;
using Lottomat.Util;
using Lottomat.Util.Extension;

namespace Lottomat.Application.Service.CommonManage
{
    public class CommonService : RepositoryFactory, ICommonService
    {
        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="procName"></param>
        /// <returns></returns>
        public object ExcuteSql(string procName, CommandType commandType = CommandType.Text, params DbParameter[] dbParameter)
        {
            return this.BaseRepository(DatabaseLinksEnum.LotteryNumber).FindObject(procName, dbParameter) ?? "";
        }
        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="links">使用哪一个数据库</param>
        /// <returns></returns>
        public DataTable ExcuteSqlDataTable(string sql, DatabaseLinksEnum links = DatabaseLinksEnum.LotteryNumber, params DbParameter[] dbParameter)
        {
            return this.BaseRepository(links).FindTable(sql, dbParameter);
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
        public DataTable FindTable(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex, out int total, DatabaseLinksEnum links = DatabaseLinksEnum.LotteryNumber)
        {
            DataTable data = this.BaseRepository(links).FindTable(strSql, orderField, isAsc, pageSize, pageIndex, out int totalRows);

            total = totalRows;

            return data;
        }

        /// <summary>
        /// 获取首页未复查的数据
        /// </summary>
        /// <returns></returns>
        public List<NotInvestigationEntity> GetNotInvestigationList()
        {
            string config = ConfigHelper.GetValue("AutoAddNewestLottery");
            List<NotInvestigationEntity> list = new List<NotInvestigationEntity>();
            if (!string.IsNullOrEmpty(config))
            {
                string[] arr = config.Split(",".ToCharArray());
                foreach (string s in arr)
                {
                    bool isSucc = Enum.TryParse<SCCLottery>(s, true, out SCCLottery type);
                    //SCCLottery type = (SCCLottery)Enum.Parse(typeof(SCCLottery), arg.EnumCode, true);
                    if (isSucc)
                    {
                        string res = this.ExcuteSql(string.Format(GetNotInvestigationSQL, type.GetSCCLotteryTableName())).ToString();
                        if (!string.IsNullOrEmpty(res))
                        {
                            NotInvestigationEntity entity = new NotInvestigationEntity
                            {
                                Id = "",
                                Name = type.GetEnumDescription(),
                                Desc = res
                            };
                            list.Add(entity);
                        }
                    }
                }
            }
            return list;
        }

        #region SQL语句

        private static string GetNotInvestigationSQL = "SELECT TOP 1 Term FROM [dbo].[{0}] WHERE IsChecked = 0 ORDER BY Term DESC";

        #endregion
    }
}
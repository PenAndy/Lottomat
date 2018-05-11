using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.CommonEntity;
using Lottomat.Application.IService.OpenCodeManage;
using Lottomat.Data.Repository;
using Lottomat.Util;
using Lottomat.Util.Extension;

namespace Lottomat.Application.Service.OpenCodeManage
{
    /// <summary>
    /// 开奖8个球号的彩种数据服务
    /// </summary>
    public class Open8CodeServices : RepositoryFactory, IOpen8Code
    {
        /// <summary>
        /// 获取最新一条记录
        /// </summary>
        /// <param name="lottery">彩种名称</param>
        /// <returns></returns>
        public OpenCode8Model GetLastItem(SCCLottery lottery)
        {
            var TableName = lottery.GetSCCLotteryTableName();
            var sqlString = string.Format(LastItemSql, TableName);

            var ds = this.BaseRepository(DatabaseLinksEnum.LotteryNumber).FindTable(sqlString);
            if (ds != null && ds.DataSet.Tables.Count > 0 && ds.DataSet.Tables[0].Rows.Count > 0)
            {
                var result = BaseServices.LoadData<OpenCode8Model>(ds.DataSet.Tables[0].Rows[0]);
                return result;
            }
            return null;
        }
        
        /// <summary>
        /// 新增彩种开奖数据
        /// </summary>
        /// <param name="lottery">彩种名称</param>
        /// <param name="model">开奖数据模型</param>
        /// <returns></returns>
        public bool AddOpen8Code(SCCLottery lottery, OpenCode8Model model)
        {
            var TableName = lottery.GetSCCLotteryTableName();
            var sqlString = string.Format(AddItemSql, TableName);
            DbParameter[] param = new DbParameter[]{
                new SqlParameter("@Term",model.Term),
                new SqlParameter("@OpenCode1",model.OpenCode1),
                new SqlParameter("@OpenCode2",model.OpenCode2),
                new SqlParameter("@OpenCode3",model.OpenCode3),
                new SqlParameter("@OpenCode4",model.OpenCode4),
                new SqlParameter("@OpenCode5",model.OpenCode5),
                new SqlParameter("@OpenCode6",model.OpenCode6),
                new SqlParameter("@OpenCode7",model.OpenCode7),
                new SqlParameter("@OpenCode8",model.OpenCode8),
                new SqlParameter("@OpenTime",model.OpenTime),
                 new SqlParameter("@ID",Guid.NewGuid().ToString().Replace("-", ""))
            };
            var result = this.BaseRepository(DatabaseLinksEnum.LotteryNumber).ExecuteBySql(sqlString, param);
            return result > 0;
        }


        #region Sql语句
        /// <summary>
        /// 获取最新一条记录的Sql语句
        /// </summary>
        private static string LastItemSql = @"SELECT TOP 1 * FROM {0} ORDER BY Term DESC";

        /// <summary>
        /// 新增开奖数据的Sql语句
        /// </summary>
        private static string AddItemSql = @"IF NOT EXISTS(SELECT TOP 1 1 FROM {0} WHERE Term = @Term)
                                            BEGIN
	                                            INSERT INTO {0}(Term,OpenCode1,OpenCode2,OpenCode3,OpenCode4,OpenCode5,OpenCode6,OpenCode7,OpenCode8,OpenTime,Addtime,ID)
                                                SELECT @Term,@OpenCode1,@OpenCode2,@OpenCode3,@OpenCode4,@OpenCode5,@OpenCode6,@OpenCode7,@OpenCode8,@OpenTime,GETDATE(),@ID
                                            END";
        #endregion
    }
}

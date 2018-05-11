using Lottomat.Application.Entity.LotteryNumberManage;
using Lottomat.Application.IService.LotteryNumberManage;
using Lottomat.Data.Repository;
using Lottomat.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.PublicInfoManage;
using Lottomat.Util.Extension;
using Lottomat.Utils.Date;
using Newtonsoft.Json.Linq;

namespace Lottomat.Application.Service.LotteryNumberManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-11-17 12:51
    /// 描 述：全国彩-福彩3D
    /// </summary>
    public class QGFC3DService : RepositoryFactory<QGFC3DEntity>, IQGFC3DService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<QGFC3DEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<QGFC3DEntity>();
            JObject queryParam = queryJson.ToJObject();
            if (queryParam != null)
            {
                if (!queryParam["Term"].IsEmpty())
                {
                    int Term = queryParam["Term"].TryToInt32();
                    expression = expression.And(t => t.Term == Term);
                }
            }

            return this.BaseRepository(DatabaseLinksEnum.LotteryNumber).FindList(expression, pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<QGFC3DEntity> GetList(string queryJson)
        {
            return this.BaseRepository(DatabaseLinksEnum.LotteryNumber).IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public QGFC3DEntity GetEntity(string keyValue)
        {
            return this.BaseRepository(DatabaseLinksEnum.LotteryNumber).FindEntity(keyValue);
        }
        /// <summary>
        /// 获取最新一期期数
        /// </summary>
        /// <returns></returns>
        public string GetNewTerm(string type)
        {
            string sql = GetSql(type);

            var data = this.BaseRepository(DatabaseLinksEnum.LotteryNumber).FindList(sql).ToList();

            if (data.Count > 0)
                return ((data[0].Term ?? 0) + 1).ToString();
            return DateTimeHelper.Now.Year + "";
        }

        /// <summary>
        /// 根据表名获取最新一期期数
        /// </summary>
        /// <returns></returns>
        public string GetNewTermByTableName(string tablename)
        {
            string sql = @"SELECT TOP 1 [ID],[Term] FROM [dbo].[{0}] ORDER BY Term DESC";

            sql = string.Format(sql, tablename);

            var data = this.BaseRepository(DatabaseLinksEnum.LotteryNumber).FindList(sql).ToList();

            if (data.Count > 0)
                return ((data[0].Term ?? 0) + 1).ToString();
            return DateTimeHelper.Now.Year + "";
        }

        /// <summary>
        /// 组装sql语句
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetSql(string type)
        {
            string sql = @"SELECT TOP 1 [ID],[Term] FROM [dbo].[{0}] ORDER BY Term DESC";
            switch (type)
            {
                #region 全国彩
                case "fc3d":
                    sql = string.Format(sql, SCCLottery.FC3D.GetSCCLotteryTableName());
                    break;
                case "fcssq":
                    sql = string.Format(sql, SCCLottery.SSQ.GetSCCLotteryTableName());
                    break;
                case "fcqlc":
                    sql = string.Format(sql, SCCLottery.QLC.GetSCCLotteryTableName());
                    break;
                case "pl3":
                    sql = string.Format(sql, SCCLottery.PL3.GetSCCLotteryTableName());
                    break;
                case "pl5":
                    sql = string.Format(sql, SCCLottery.PL5.GetSCCLotteryTableName());
                    break;
                case "dlt":
                    sql = string.Format(sql, SCCLottery.DLT.GetSCCLotteryTableName());
                    break;
                case "qxc":
                    sql = string.Format(sql, SCCLottery.QXC.GetSCCLotteryTableName());
                    break;
                #endregion

                #region 地方彩
                case "DFFC25x5AnHui"://安徽25选5
                    sql = string.Format(sql, SCCLottery.AnHui25x5.GetSCCLotteryTableName());
                    break;
                case "DFDF6J1"://东方6+1
                    sql = string.Format(sql, SCCLottery.DF6J1.GetSCCLotteryTableName());
                    break;
                case "DFTC22x5FuJian"://福建体彩22选5
                    sql = string.Format(sql, SCCLottery.FuJian22x5.GetSCCLotteryTableName());
                    break;
                case "DFTC36x7FuJian"://福建体彩36选7
                    sql = string.Format(sql, SCCLottery.FuJianTC36x7.GetSCCLotteryTableName());
                    break;
                case "DF26x5GuangDong"://广东26选5
                    sql = string.Format(sql, SCCLottery.GuangDong26X5.GetSCCLotteryTableName());
                    break;
                case "DF36x7GuangDong"://广东36选7
                    sql = string.Format(sql,SCCLottery.GuangDong36x7.GetSCCLotteryTableName());
                    break;
                case "DFHC1GuangDong"://广东好彩1
                    sql = string.Format(sql, SCCLottery.GuangDongHC1.GetSCCLotteryTableName());
                    break;
                case "DFSZFCGuangDong"://广东深圳风采35选7
                    sql = string.Format(sql,SCCLottery.GuangDongSZFC.GetSCCLotteryTableName());
                    break;
                case "DFKLSCGuangXi"://广西快乐双彩
                    sql = string.Format(sql, SCCLottery.GuangXiKLSC.GetSCCLotteryTableName());
                    break;
                case "DFHD15x5"://华东15选5
                    sql = string.Format(sql, SCCLottery.HD15X5.GetSCCLotteryTableName());
                    break;
                case "DFLJFC22x5HeiLongJiang"://黑龙江25选5
                    sql = string.Format(sql, SCCLottery.HeiLongJiangLJFC22x5.GetSCCLotteryTableName());
                    break;
                case "DFTC6J1HeiLongJiang"://黑龙江体彩6+1
                    sql = string.Format(sql, SCCLottery.HeiLongJiangTC6J1.GetSCCLotteryTableName());
                    break;
                case "DFTC7WSJiangSu"://江苏7位数
                    //sql = string.Format(sql, "DF_TC7WS_JiangSu");
                    sql = string.Format(sql, SCCLottery.JiangSuTC7WS.GetSCCLotteryTableName());
                    break;
                case "DF35x7LiaoNing"://辽宁35选7
                    sql = string.Format(sql, SCCLottery.LiaoNingFC35X7.GetSCCLotteryTableName());
                    break;
                case "DFTTCx4ShangHai"://上海天天彩选4
                    sql = string.Format(sql, SCCLottery.ShangHaiTTCX4.GetSCCLotteryTableName());
                    break;
                case "DF18x7XinJiang"://新疆18选7
                    sql = string.Format(sql, SCCLottery.XinJiangFC18X7.GetSCCLotteryTableName());
                    break;
                case "DF25x7XinJiang"://新疆25选7
                    sql = string.Format(sql, SCCLottery.XinJiangFC25X7.GetSCCLotteryTableName());
                    break;
                case "DF35x7XinJiang"://新疆35选7
                    sql = string.Format(sql, SCCLottery.XinJiangFC35X7.GetSCCLotteryTableName());
                    break;
                case "DF20x5HeBei"://河北20选5
                    sql = string.Format(sql,SCCLottery.HeBei20X5.GetSCCLotteryTableName());
                    break;
                case "DFHYC2HeBei"://河北好彩2
                    sql = string.Format(sql, SCCLottery.HeBeiHYC2.GetSCCLotteryTableName());
                    break;
                case "DFHYC3HeBei"://河北好彩3
                    sql = string.Format(sql, SCCLottery.HeBeiHYC3.GetSCCLotteryTableName());
                    break;
                case "DFPL5HeBei"://河北排列5
                    sql = string.Format(sql,SCCLottery.HeBeiPL5.GetSCCLotteryTableName());
                    break;
                case "DFPL7HeBei"://河北排列7
                    sql = string.Format(sql, SCCLottery.HeBeiPL7.GetSCCLotteryTableName());
                    break;
                case "DF22x5HeNan"://河南22选5
                    sql = string.Format(sql, SCCLottery.HeNan22X5.GetSCCLotteryTableName());
                    break;
                case "DFSMHLHCHongKong"://香港六合彩
                    sql = string.Format(sql, SCCLottery.HongKongSMHLHC.GetSCCLotteryTableName());
                    break;
                case "DF30x5HuBei"://湖北30选5
                    sql = string.Format(sql, SCCLottery.HuBei30X5.GetSCCLotteryTableName());
                    break;
                case "DF6J1ZheJiang"://浙江6+1
                    sql = string.Format(sql, SCCLottery.ZheJiang6J1.GetSCCLotteryTableName());
                    break;
                case "DF31x7FuJian"://福建31选7
                    sql = string.Format(sql, SCCLottery.FuJian31x7.GetSCCLotteryTableName());
                    break;
                case "DF20x5ZheJiang"://浙江20选5
                    sql = string.Format(sql, SCCLottery.ZheJiang20x5.GetSCCLotteryTableName());
                    break;
                case "DF4J1HaiNan"://海南4+1
                    sql = string.Format(sql, SCCLottery.HaiNan4J1.GetSCCLotteryTableName());
                    break;
                case "DFP62HeiLongJiang"://黑龙江P62
                    sql = string.Format(sql, SCCLottery.HeiLongJiangP62.GetSCCLotteryTableName());
                    break;
                case "DF36x7HeiLongJiang"://黑龙江36选7
                    sql = string.Format(sql, SCCLottery.HeiLongJiang36x7.GetSCCLotteryTableName());
                    break;
                #endregion
            }
            return sql;
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository(DatabaseLinksEnum.LotteryNumber).Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, QGFC3DEntity entity, string isCheck)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (isCheck == "1")
                {
                    entity.IsChecked = true;
                    entity.IsPassed = true;
                }
                entity.Modify(keyValue);
                this.BaseRepository(DatabaseLinksEnum.LotteryNumber).Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository(DatabaseLinksEnum.LotteryNumber).Insert(entity);
            }
        }
        #endregion
    }
}

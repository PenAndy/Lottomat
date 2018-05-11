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
    /// �� �� 1.0
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-17 12:51
    /// �� ����ȫ����-����3D
    /// </summary>
    public class QGFC3DService : RepositoryFactory<QGFC3DEntity>, IQGFC3DService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
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
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<QGFC3DEntity> GetList(string queryJson)
        {
            return this.BaseRepository(DatabaseLinksEnum.LotteryNumber).IQueryable().ToList();
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public QGFC3DEntity GetEntity(string keyValue)
        {
            return this.BaseRepository(DatabaseLinksEnum.LotteryNumber).FindEntity(keyValue);
        }
        /// <summary>
        /// ��ȡ����һ������
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
        /// ���ݱ�����ȡ����һ������
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
        /// ��װsql���
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetSql(string type)
        {
            string sql = @"SELECT TOP 1 [ID],[Term] FROM [dbo].[{0}] ORDER BY Term DESC";
            switch (type)
            {
                #region ȫ����
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

                #region �ط���
                case "DFFC25x5AnHui"://����25ѡ5
                    sql = string.Format(sql, SCCLottery.AnHui25x5.GetSCCLotteryTableName());
                    break;
                case "DFDF6J1"://����6+1
                    sql = string.Format(sql, SCCLottery.DF6J1.GetSCCLotteryTableName());
                    break;
                case "DFTC22x5FuJian"://�������22ѡ5
                    sql = string.Format(sql, SCCLottery.FuJian22x5.GetSCCLotteryTableName());
                    break;
                case "DFTC36x7FuJian"://�������36ѡ7
                    sql = string.Format(sql, SCCLottery.FuJianTC36x7.GetSCCLotteryTableName());
                    break;
                case "DF26x5GuangDong"://�㶫26ѡ5
                    sql = string.Format(sql, SCCLottery.GuangDong26X5.GetSCCLotteryTableName());
                    break;
                case "DF36x7GuangDong"://�㶫36ѡ7
                    sql = string.Format(sql,SCCLottery.GuangDong36x7.GetSCCLotteryTableName());
                    break;
                case "DFHC1GuangDong"://�㶫�ò�1
                    sql = string.Format(sql, SCCLottery.GuangDongHC1.GetSCCLotteryTableName());
                    break;
                case "DFSZFCGuangDong"://�㶫���ڷ��35ѡ7
                    sql = string.Format(sql,SCCLottery.GuangDongSZFC.GetSCCLotteryTableName());
                    break;
                case "DFKLSCGuangXi"://��������˫��
                    sql = string.Format(sql, SCCLottery.GuangXiKLSC.GetSCCLotteryTableName());
                    break;
                case "DFHD15x5"://����15ѡ5
                    sql = string.Format(sql, SCCLottery.HD15X5.GetSCCLotteryTableName());
                    break;
                case "DFLJFC22x5HeiLongJiang"://������25ѡ5
                    sql = string.Format(sql, SCCLottery.HeiLongJiangLJFC22x5.GetSCCLotteryTableName());
                    break;
                case "DFTC6J1HeiLongJiang"://���������6+1
                    sql = string.Format(sql, SCCLottery.HeiLongJiangTC6J1.GetSCCLotteryTableName());
                    break;
                case "DFTC7WSJiangSu"://����7λ��
                    //sql = string.Format(sql, "DF_TC7WS_JiangSu");
                    sql = string.Format(sql, SCCLottery.JiangSuTC7WS.GetSCCLotteryTableName());
                    break;
                case "DF35x7LiaoNing"://����35ѡ7
                    sql = string.Format(sql, SCCLottery.LiaoNingFC35X7.GetSCCLotteryTableName());
                    break;
                case "DFTTCx4ShangHai"://�Ϻ������ѡ4
                    sql = string.Format(sql, SCCLottery.ShangHaiTTCX4.GetSCCLotteryTableName());
                    break;
                case "DF18x7XinJiang"://�½�18ѡ7
                    sql = string.Format(sql, SCCLottery.XinJiangFC18X7.GetSCCLotteryTableName());
                    break;
                case "DF25x7XinJiang"://�½�25ѡ7
                    sql = string.Format(sql, SCCLottery.XinJiangFC25X7.GetSCCLotteryTableName());
                    break;
                case "DF35x7XinJiang"://�½�35ѡ7
                    sql = string.Format(sql, SCCLottery.XinJiangFC35X7.GetSCCLotteryTableName());
                    break;
                case "DF20x5HeBei"://�ӱ�20ѡ5
                    sql = string.Format(sql,SCCLottery.HeBei20X5.GetSCCLotteryTableName());
                    break;
                case "DFHYC2HeBei"://�ӱ��ò�2
                    sql = string.Format(sql, SCCLottery.HeBeiHYC2.GetSCCLotteryTableName());
                    break;
                case "DFHYC3HeBei"://�ӱ��ò�3
                    sql = string.Format(sql, SCCLottery.HeBeiHYC3.GetSCCLotteryTableName());
                    break;
                case "DFPL5HeBei"://�ӱ�����5
                    sql = string.Format(sql,SCCLottery.HeBeiPL5.GetSCCLotteryTableName());
                    break;
                case "DFPL7HeBei"://�ӱ�����7
                    sql = string.Format(sql, SCCLottery.HeBeiPL7.GetSCCLotteryTableName());
                    break;
                case "DF22x5HeNan"://����22ѡ5
                    sql = string.Format(sql, SCCLottery.HeNan22X5.GetSCCLotteryTableName());
                    break;
                case "DFSMHLHCHongKong"://������ϲ�
                    sql = string.Format(sql, SCCLottery.HongKongSMHLHC.GetSCCLotteryTableName());
                    break;
                case "DF30x5HuBei"://����30ѡ5
                    sql = string.Format(sql, SCCLottery.HuBei30X5.GetSCCLotteryTableName());
                    break;
                case "DF6J1ZheJiang"://�㽭6+1
                    sql = string.Format(sql, SCCLottery.ZheJiang6J1.GetSCCLotteryTableName());
                    break;
                case "DF31x7FuJian"://����31ѡ7
                    sql = string.Format(sql, SCCLottery.FuJian31x7.GetSCCLotteryTableName());
                    break;
                case "DF20x5ZheJiang"://�㽭20ѡ5
                    sql = string.Format(sql, SCCLottery.ZheJiang20x5.GetSCCLotteryTableName());
                    break;
                case "DF4J1HaiNan"://����4+1
                    sql = string.Format(sql, SCCLottery.HaiNan4J1.GetSCCLotteryTableName());
                    break;
                case "DFP62HeiLongJiang"://������P62
                    sql = string.Format(sql, SCCLottery.HeiLongJiangP62.GetSCCLotteryTableName());
                    break;
                case "DF36x7HeiLongJiang"://������36ѡ7
                    sql = string.Format(sql, SCCLottery.HeiLongJiang36x7.GetSCCLotteryTableName());
                    break;
                #endregion
            }
            return sql;
        }

        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository(DatabaseLinksEnum.LotteryNumber).Delete(keyValue);
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
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

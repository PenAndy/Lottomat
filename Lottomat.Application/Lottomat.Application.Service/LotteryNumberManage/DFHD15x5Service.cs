using Lottomat.Application.Entity.LotteryNumberManage;
using Lottomat.Application.IService.LotteryNumberManage;
using Lottomat.Data.Repository;
using Lottomat.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using Lottomat.Application.Code;
using Lottomat.Util.Extension;
using Newtonsoft.Json.Linq;

namespace Lottomat.Application.Service.LotteryNumberManage
{
    /// <summary>
    /// �� �� 1.0
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-22 15:24
    /// �� �����ط���-����15ѡ5
    /// </summary>
    public class DFHD15x5Service : RepositoryFactory<DFHD15x5Entity>, IDFHD15x5Service
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<DFHD15x5Entity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<DFHD15x5Entity>();
            JObject queryParam = queryJson.ToJObject();
            if (queryParam != null)
            {
                if (!queryParam["Term"].IsEmpty())
                {
                    long Term = queryParam["Term"].TryToInt32();
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
        public IEnumerable<DFHD15x5Entity> GetList(string queryJson)
        {
            return this.BaseRepository(DatabaseLinksEnum.LotteryNumber).IQueryable().ToList();
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public DFHD15x5Entity GetEntity(string keyValue)
        {
            return this.BaseRepository(DatabaseLinksEnum.LotteryNumber).FindEntity(keyValue);
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
        public void SaveForm(string keyValue, DFHD15x5Entity entity,string isCheck)
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

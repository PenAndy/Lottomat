using Lottomat.Application.Entity.LotteryNumberManage;
using Lottomat.Application.IService.LotteryNumberManage;
using Lottomat.Application.Service.LotteryNumberManage;
using Lottomat.Util.WebControl;
using System.Collections.Generic;
using System;

namespace Lottomat.Application.Busines.LotteryNumberManage
{
    /// <summary>
    /// �� �� 1.0
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-17 12:51
    /// �� ����ȫ����-����3D
    /// </summary>
    public class QGFC3DBLL
    {
        private IQGFC3DService service = new QGFC3DService();

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<QGFC3DEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<QGFC3DEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public QGFC3DEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// ����һ������
        /// </summary>
        /// <returns></returns>
        public string GetNewTerm(string type)
        {
            return service.GetNewTerm(type);
        }
        /// <summary>
        /// ���ݱ����ƻ�ȡ����һ������
        /// </summary>
        /// <returns></returns>
        public string GetNewTermByTableName(string tablename)
        {
            return service.GetNewTermByTableName(tablename);
        }

        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, QGFC3DEntity entity,string isCheck)
        {
            try
            {
                service.SaveForm(keyValue, entity, isCheck);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}

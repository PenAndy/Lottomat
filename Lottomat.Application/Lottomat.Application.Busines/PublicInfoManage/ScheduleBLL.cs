using Lottomat.Application.Entity.PublicInfoManage;
using Lottomat.Application.IService.PublicInfoManage;
using Lottomat.Application.Service.PublicInfoManage;
using Lottomat.Util.WebControl;
using System.Collections.Generic;
using System;

namespace Lottomat.Application.Busines.PublicInfoManage
{
    /// <summary>
    /// �� �� 1.0
    /// Copyright (c) 2016-2017
    /// �� ����XXX
    /// �� �ڣ�2016-04-25 10:45
    /// �� �����ճ̹���
    /// </summary>
    public class ScheduleBLL
    {
        private IScheduleService service = new ScheduleService();

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        public IEnumerable<ScheduleEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public ScheduleEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
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
        /// ����������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, ScheduleEntity entity)
        {
            try
            {
                service.SaveForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
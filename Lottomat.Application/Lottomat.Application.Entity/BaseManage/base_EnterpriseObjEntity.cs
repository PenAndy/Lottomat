using System;
using Lottomat.Application.Code;
using Lottomat.Util;

namespace Lottomat.Application.Entity.BaseManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-05-25 14:15
    /// �� ������ҵ����
    /// </summary>
    public class base_EnterpriseObjEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        public string ID { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }
        /// <summary>
        /// ͳһ������ô���
        /// </summary>
        /// <returns></returns>
        public string RegNum { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string Type { get; set; }
        /// <summary>
        /// ס��
        /// </summary>
        /// <returns></returns>
        public string Address { get; set; }
        /// <summary>
        /// ����������
        /// </summary>
        /// <returns></returns>
        public string Person { get; set; }
        /// <summary>
        /// ע���ʱ�
        /// </summary>
        /// <returns></returns>
        public string RegCapital { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public DateTime? RegDate { get; set; }
        /// <summary>
        /// Ӫҵ����
        /// </summary>
        /// <returns></returns>
        public string BusnissTerm { get; set; }
        /// <summary>
        /// ��Ӫ��Χ
        /// </summary>
        /// <returns></returns>
        public string BusinessScope { get; set; }
        /// <summary>
        /// �Ǽǻ���
        /// </summary>
        /// <returns></returns>
        public string RegOrg { get; set; }
        /// <summary>
        /// �Ǽ�����
        /// </summary>
        /// <returns></returns>
        public DateTime? RecordDate { get; set; }
        /// <summary>
        /// ������Դ
        /// </summary>
        /// <returns></returns>
        public int? Source { get; set; }
        /// <summary>
        /// ¼�����ID
        /// </summary>
        /// <returns></returns>
        public string RecordOrgID { get; set; }
        /// <summary>
        /// ɾ��
        /// </summary>
        /// <returns></returns>
        public bool? IsDelete { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.ID = CommonHelper.GetGuid().ToString();
                                            }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ID = keyValue;
                                            }
        #endregion
    }
}
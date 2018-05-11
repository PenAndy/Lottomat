using System;
using Lottomat.Application.Code;

using Lottomat.Util;
using Lottomat.Utils.Date;

namespace Lottomat.Application.Entity.SystemManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-12-06 13:49
    /// �� ������������
    /// </summary>
    public class BaseFriendLinksEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// PK
        /// </summary>
        /// <returns></returns>
        //public int? PK { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        public string ID { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string Type { get; set; }
        /// <summary>
        /// ������������
        /// </summary>
        /// <returns></returns>
        public string TypeName { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }
        /// <summary>
        /// ��ַ
        /// </summary>
        /// <returns></returns>
        public string Url { get; set; }
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        /// <returns></returns>
        public bool? IsEnable { get; set; }
        /// <summary>
        /// �Ƿ�ɾ��
        /// </summary>
        /// <returns></returns>
        public bool? IsDelete { get; set; }
        /// <summary>
        /// ������ID
        /// </summary>
        /// <returns></returns>
        public string CreateUserId { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public string CreateUserName { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// �޸���ID
        /// </summary>
        /// <returns></returns>
        public string ModifyUserId { get; set; }
        /// <summary>
        /// �޸���
        /// </summary>
        /// <returns></returns>
        public string ModifyUserName { get; set; }
        /// <summary>
        /// �޸�ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        public string Remark { get; set; }
        /// <summary>
        /// ��Ч�ڣ���λ���ꡣ0��ʾ����ʧЧ
        /// </summary>
        /// <returns></returns>
        public int TermOfValidity { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.ID = CommonHelper.GetGuid().ToString();
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.CreateTime = DateTimeHelper.Now;
            this.IsDelete = false;
            this.IsEnable = true;
            this.TermOfValidity = 0;
        }

        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ID = keyValue;
            this.ModifyDate = DateTimeHelper.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}
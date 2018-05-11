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
    /// �� �ڣ�2017-12-18 10:55
    /// �� �����������
    /// </summary>
    public class FeedbackEntity : BaseEntity
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
        /// �ǳ�
        /// </summary>
        /// <returns></returns>
        public string NickName { get; set; }
        /// <summary>
        /// ��ϵ��ʽ
        /// </summary>
        /// <returns></returns>
        public string Contact { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string Content { get; set; }
        /// <summary>
        /// ���ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? AddTime { get; set; }
        /// <summary>
        /// IP��ַ
        /// </summary>
        /// <returns></returns>
        public string IP { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string From { get; set; }
        /// <summary>
        /// �Ƿ�ظ�
        /// </summary>
        /// <returns></returns>
        public bool? IsReply { get; set; }
        /// <summary>
        /// �ظ�ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? ReplyTime { get; set; }
        /// <summary>
        /// �ظ�����
        /// </summary>
        /// <returns></returns>
        public string ReplyContent { get; set; }
        /// <summary>
        /// �ظ���
        /// </summary>
        /// <returns></returns>
        public string ReplyUserName { get; set; }
        /// <summary>
        /// �Ƿ�ɾ��
        /// </summary>
        /// <returns></returns>
        public bool? IsDelete { get; set; }
        /// <summary>
        /// �Ƿ񹫿�
        /// </summary>
        /// <returns></returns>
        public bool? IsPublic { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.ID = CommonHelper.GetGuid().ToString();
            this.IsDelete = false;
            this.AddTime = DateTimeHelper.Now;
            this.IsReply = false;
            this.IsPublic = true;
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
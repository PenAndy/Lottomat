using System;
using Lottomat.Application.Code;
using Lottomat.Util;

namespace Lottomat.Application.Entity.AttachmentManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-06-05 15:31
    /// �� ��������
    /// </summary>
    public class AttachmentEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        public string ID { get; set; }
        /// <summary>
        /// ��ĿĿ¼ID
        /// </summary>
        /// <returns></returns>
        public string ProjectID { get; set; }
        /// <summary>
        /// ����ĿID
        /// </summary>
        /// <returns></returns>
        public string MasterProjectID { get; set; }
        /// <summary>
        /// �ļ�����
        /// </summary>
        /// <returns></returns>
        public string FileType { get; set; }
        /// <summary>
        /// �ļ���ַ
        /// </summary>
        /// <returns></returns>
        public string FilePath { get; set; }
        /// <summary>
        /// �ϴ�ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? UploadTime { get; set; }
        /// <summary>
        /// �ϴ���ID
        /// </summary>
        /// <returns></returns>
        public string UploadPersonID { get; set; }
        /// <summary>
        /// �ϴ�������
        /// </summary>
        /// <returns></returns>
        public string UploadPersonName { get; set; }
        /// <summary>
        /// ��Ч
        /// </summary>
        /// <returns></returns>
        public int? EnabledMark { get; set; }


        /// <summary>
        /// ��Դ
        /// </summary>
        public int? Source { get; set; }
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
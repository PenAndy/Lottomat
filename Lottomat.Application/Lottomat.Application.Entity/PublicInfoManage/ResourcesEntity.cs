using System;
using Lottomat.Application.Code;
using Lottomat.Util;
using Lottomat.Utils.Date;

namespace Lottomat.Application.Entity.PublicInfoManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-08-02 12:20
    /// �� ������Դ����
    /// </summary>
    public class ResourcesEntity : BaseEntity 
    {
        #region ʵ���Ա
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        /// <summary>
        /// ��Դ����ID
        /// </summary>
        /// <returns></returns>
        public string TypeId { get; set; }
        /// <summary>
        /// ��Դ��������
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// �ϴ���ID
        /// </summary>
        /// <returns></returns>
        public string UploadUserId { get; set; }
        /// <summary>
        /// �ϴ�������
        /// </summary>
        public string UploadUserName { get; set; }
        /// <summary>
        /// ��Դ����
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }
        /// <summary>
        /// ��Դ��ͼ��ַ
        /// </summary>
        /// <returns></returns>
        public string Pic { get; set; }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// ���ش���
        /// </summary>
        /// <returns></returns>
        public int? DownloadCount { get; set; }
        /// <summary>
        /// ��Դ��ַ
        /// </summary>
        /// <returns></returns>
        public string Address { get; set; }
        /// <summary>
        /// ��ԴԤ����ַ
        /// </summary>
        /// <returns></returns>
        public string PreviewUrl { get; set; }
        /// <summary>
        /// �ϴ�ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? UploadTime { get; set; }
        /// <summary>
        /// ��Դ��С
        /// </summary>
        /// <returns></returns>
        public float? Size { get; set; }
        /// <summary>
        /// ɾ����־
        /// </summary>
        /// <returns></returns>
        public int? DeleteMark { get; set; }
        /// <summary>
        /// ���ñ�־
        /// </summary>
        /// <returns></returns>
        public int? EnabledMark { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public int? SortCode { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.Id = CommonHelper.GetGuid().ToString();
            this.UploadUserId = OperatorProvider.Provider.Current().UserId;
            this.UploadUserName = OperatorProvider.Provider.Current().UserName;
            this.DeleteMark = (int)DeleteMarkEnum.NotDelete;
            this.EnabledMark = (int)EnabledMarkEnum.Enabled;
            this.UploadTime = DateTimeHelper.Now;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
        }
        #endregion
    }
}
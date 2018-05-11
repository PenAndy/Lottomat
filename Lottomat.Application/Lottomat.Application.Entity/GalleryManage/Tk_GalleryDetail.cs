using System;
using Lottomat.Application.Code;

using Lottomat.Util;
using Lottomat.Utils.Date;

namespace Lottomat.Application.Entity.GalleryManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� �����������˺�
    /// �� �ڣ�2017-10-19 14:29
    /// �� ����ͼ�������
    /// </summary>
    public class Tk_GalleryDetail : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// PK
        /// </summary>
        /// <returns></returns>
       // public int? PK { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string ID { get; set; }
        /// <summary>
        /// ͼ��ID
        /// </summary>
        /// <returns></returns>
        public string GalleryId { get; set; }
        /// <summary>
        /// ͼƬ��ַ
        /// </summary>
        /// <returns></returns>
        public string ImageUrl { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string PeriodsNumber { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public int? SortCode { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateTime { get; set; }
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
        /// �Ƿ�ɾ��
        /// </summary>
        /// <returns></returns>
        public bool? IsDelete { get; set; }
        /// <summary>
        /// ���ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? AddTime { get; set; }
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
            this.AddTime = DateTimeHelper.Now;
            this.CreateTime = DateTimeHelper.Now;
            this.IsDelete = false;
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
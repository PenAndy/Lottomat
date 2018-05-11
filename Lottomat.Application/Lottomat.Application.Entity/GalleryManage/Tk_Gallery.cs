using System;
using Lottomat.Application.Code;

using Lottomat.Util;

namespace Lottomat.Application.Entity.GalleryManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� �����������˺�
    /// �� �ڣ�2017-10-19 14:24
    /// �� ����ͼ������
    /// </summary>
    public class Tk_Gallery : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// PK
        /// </summary>
        /// <returns></returns>
        //public int? PK { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string ID { get; set; }
        /// <summary>
        /// ͼ����
        /// </summary>
        /// <returns></returns>
        public int? GalleryNumber { get; set; }
        /// <summary>
        /// ͼ������
        /// </summary>
        /// <returns></returns>
        public string GalleryName { get; set; }
        /// <summary>
        /// ����ID
        /// </summary>
        /// <returns></returns>
        public string CategoryId { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public int? SortCode { get; set; }
        /// <summary>
        /// �Ƿ�ѹ��
        /// </summary>
        /// <returns></returns>
        public bool? IsPicZip { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        public string Reamrk { get; set; }
        /// <summary>
        /// SEO�ؼ���
        /// </summary>
        /// <returns></returns>
        public string SeoKey { get; set; }
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
        /// ������룬ֵΪA-B-C
        /// </summary>
        /// <returns></returns>
        public string AreaCode { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public int? HotNumber { get; set; }
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
            this.CreateTime = DateTime.Now;
            this.HotNumber = 0;
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
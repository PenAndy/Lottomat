using System;
using Lottomat.Application.Code;

using Lottomat.Util;
using Lottomat.Util.Extension;
using Lottomat.Utils.Date;

namespace Lottomat.Application.Entity.InformationManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2018-01-02 15:17
    /// �� ����ר������
    /// </summary>
    public class ZTArticleEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        public int? Id { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        /// <returns></returns>
        public string Title { get; set; }
        /// <summary>
        /// Cid
        /// </summary>
        /// <returns></returns>
        public int? Cid { get; set; }
        /// <summary>
        /// TagId
        /// </summary>
        /// <returns></returns>
        public int? TagId { get; set; }
        /// <summary>
        /// ShortDetail
        /// </summary>
        /// <returns></returns>
        public string ShortDetail { get; set; }
        /// <summary>
        /// Addtime
        /// </summary>
        /// <returns></returns>
        public DateTime? Addtime { get; set; }
        /// <summary>
        /// IsDelete
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
            //this.Id = CommonHelper.GetGuid().ToString();
            this.Addtime = DateTimeHelper.Now;
            this.IsDelete = false;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue.TryToInt32();
        }
        #endregion
    }
}
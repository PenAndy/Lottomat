using System;
using Lottomat.Application.Code;

using Lottomat.Util;
using Lottomat.Utils.Date;

namespace Lottomat.Application.Entity.LotteryNumberManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-11-17 15:47
    /// �� ������Ʊ����
    /// </summary>
    public class AwardsEntity : BaseEntity
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
        /// ����ID
        /// </summary>
        /// <returns></returns>
        public string PrizeID { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string ItemName { get; set; }
        /// <summary>
        /// �淨����
        /// </summary>
        /// <returns></returns>
        public string Gameplay { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string LotteryRule { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        public string LotteryTime { get; set; }
        /// <summary>
        /// �н�����
        /// </summary>
        /// <returns></returns>
        public string Winning { get; set; }
        /// <summary>
        /// ���ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? AddTime { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string SourceFrom { get; set; }
        /// <summary>
        /// �Ƿ�ɾ��
        /// </summary>
        /// <returns></returns>
        public bool? IsDelete { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        public string Remark { get; set; }
        /// <summary>
        /// Logo��ַ
        /// </summary>
        /// <returns></returns>
        public string Logo { get; set; }
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
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
    /// �� �ڣ�2017-11-21 15:48
    /// �� �����ط���-�������36ѡ7
    /// </summary>
    public class DFTC36x7FuJianEntity : BaseEntity
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
        /// ����
        /// </summary>
        /// <returns></returns>
        public long? Term { get; set; }
        /// <summary>
        /// ������1
        /// </summary>
        /// <returns></returns>
        public int? OpenCode1 { get; set; }
        /// <summary>
        /// ������2
        /// </summary>
        /// <returns></returns>
        public int? OpenCode2 { get; set; }
        /// <summary>
        /// ������3
        /// </summary>
        /// <returns></returns>
        public int? OpenCode3 { get; set; }
        /// <summary>
        /// ������4
        /// </summary>
        /// <returns></returns>
        public int? OpenCode4 { get; set; }
        /// <summary>
        /// ������5
        /// </summary>
        /// <returns></returns>
        public int? OpenCode5 { get; set; }
        /// <summary>
        /// ������6
        /// </summary>
        /// <returns></returns>
        public int? OpenCode6 { get; set; }
        /// <summary>
        /// ������7
        /// </summary>
        /// <returns></returns>
        public int? OpenCode7 { get; set; }
        /// <summary>
        /// ������8
        /// </summary>
        /// <returns></returns>
        public int? OpenCode8 { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? OpenTime { get; set; }
        /// <summary>
        /// ���ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? Addtime { get; set; }
        /// <summary>
        /// �����ֶ�
        /// </summary>
        /// <returns></returns>
        public string Spare { get; set; }
        /// <summary>
        /// �Ƿ�У���
        /// </summary>
        /// <returns></returns>
        public bool? IsChecked { get; set; }
        /// <summary>
        /// У���Ƿ�ͨ��
        /// </summary>
        /// <returns></returns>
        public bool? IsPassed { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.ID = CommonHelper.GetGuid().ToString();
            this.IsChecked = false;
            this.IsPassed = false;
            this.Addtime = DateTimeHelper.Now;
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
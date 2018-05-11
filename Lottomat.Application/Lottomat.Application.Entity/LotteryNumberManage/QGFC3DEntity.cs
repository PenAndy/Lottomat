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
    /// �� �ڣ�2017-11-17 12:51
    /// �� ����ȫ����-����3D
    /// </summary>
    public class QGFC3DEntity : BaseEntity
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
        public int? Term { get; set; }
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
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? OpenTime { get; set; }
        /// <summary>
        /// �Ի���
        /// </summary>
        /// <returns></returns>
        public string ShiJiHao { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public string KaiJiHao { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string Detail { get; set; }
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
        /// �Ƿ����
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
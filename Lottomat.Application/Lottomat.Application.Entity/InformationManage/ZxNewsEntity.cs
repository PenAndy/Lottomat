using System;
using Lottomat.Application.Code;

using Lottomat.Util;
using Lottomat.Util.Extension;

namespace Lottomat.Application.Entity.InformationManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2016-2017
    /// �� ������������Ա
    /// �� �ڣ�2017-10-17 09:58
    /// �� ����55128��Ѷ����
    /// </summary>
    public class ZxNewsEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        public int ID { get; set; }
        /// <summary>
        /// guid
        /// </summary>
        /// <returns></returns>
        public string guid { get; set; }
        /// <summary>
        /// title
        /// </summary>
        /// <returns></returns>
        public string title { get; set; }
        /// <summary>
        /// description
        /// </summary>
        /// <returns></returns>
        public string description { get; set; }
        /// <summary>
        /// menuId
        /// </summary>
        /// <returns></returns>
        public int? menuId { get; set; }
        /// <summary>
        /// labelId
        /// </summary>
        /// <returns></returns>
        public int? labelId { get; set; }
        /// <summary>
        /// periodsNumber
        /// </summary>
        /// <returns></returns>
        public int? periodsNumber { get; set; }
        /// <summary>
        /// sortNo
        /// </summary>
        /// <returns></returns>
        public int? sortNo { get; set; }
        /// <summary>
        /// createTime
        /// </summary>
        /// <returns></returns>
        public DateTime? createTime { get; set; }
        /// <summary>
        /// createUserId
        /// </summary>
        /// <returns></returns>
        public int? createUserId { get; set; }
        /// <summary>
        /// createUserName
        /// </summary>
        /// <returns></returns>
        public string createUserName { get; set; }
        /// <summary>
        /// isDelete
        /// </summary>
        /// <returns></returns>
        public bool? isDelete { get; set; }
        /// <summary>
        /// isHot
        /// </summary>
        /// <returns></returns>
        public bool? isHot { get; set; }
        /// <summary>
        /// Clickamount
        /// </summary>
        /// <returns></returns>
        public int? Clickamount { get; set; }
        /// <summary>
        /// titleElement
        /// </summary>
        /// <returns></returns>
        public string titleElement { get; set; }
        /// <summary>
        /// descriptionElement
        /// </summary>
        /// <returns></returns>
        public string descriptionElement { get; set; }
        /// <summary>
        /// keywordElement
        /// </summary>
        /// <returns></returns>
        public string keywordElement { get; set; }
        /// <summary>
        /// SourceUrl
        /// </summary>
        /// <returns></returns>
        public string SourceUrl { get; set; }
        /// <summary>
        /// IsGenerated
        /// </summary>
        /// <returns></returns>
        public bool? IsGenerated { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            //this.ID = CommonHelper.GetGuid().ToString();
            //this.createUserId = OperatorProvider.Provider.Current().UserId;

            this.createUserName = OperatorProvider.Provider.Current().UserName;
            this.guid = CommonHelper.GetGuid().ToString();
            this.IsGenerated = false;
            this.Clickamount = 0;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            //this.ID = keyValue.TryToInt32();
        }
        #endregion
    }
}
using System;
using Lottomat.Application.Code;

using Lottomat.Util;
using Lottomat.Utils.Date;

namespace Lottomat.Application.Entity.LotteryNumberManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-11-17 15:47
    /// 描 述：彩票规则
    /// </summary>
    public class AwardsEntity : BaseEntity
    {
        #region 实体成员
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
        /// 奖项ID
        /// </summary>
        /// <returns></returns>
        public string PrizeID { get; set; }
        /// <summary>
        /// 奖项名称
        /// </summary>
        /// <returns></returns>
        public string ItemName { get; set; }
        /// <summary>
        /// 玩法介绍
        /// </summary>
        /// <returns></returns>
        public string Gameplay { get; set; }
        /// <summary>
        /// 开奖规则
        /// </summary>
        /// <returns></returns>
        public string LotteryRule { get; set; }
        /// <summary>
        /// 开奖时间
        /// </summary>
        /// <returns></returns>
        public string LotteryTime { get; set; }
        /// <summary>
        /// 中奖规则
        /// </summary>
        /// <returns></returns>
        public string Winning { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        /// <returns></returns>
        public DateTime? AddTime { get; set; }
        /// <summary>
        /// 来自
        /// </summary>
        /// <returns></returns>
        public string SourceFrom { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        /// <returns></returns>
        public bool? IsDelete { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string Remark { get; set; }
        /// <summary>
        /// Logo地址
        /// </summary>
        /// <returns></returns>
        public string Logo { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.ID = CommonHelper.GetGuid().ToString();
            this.IsDelete = false;
            this.AddTime = DateTimeHelper.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ID = keyValue;
        }
        #endregion
    }
}
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
    /// 日 期：2017-11-21 15:48
    /// 描 述：地方彩-福建体彩36选7
    /// </summary>
    public class DFTC36x7FuJianEntity : BaseEntity
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
        /// 期数
        /// </summary>
        /// <returns></returns>
        public long? Term { get; set; }
        /// <summary>
        /// 开奖号1
        /// </summary>
        /// <returns></returns>
        public int? OpenCode1 { get; set; }
        /// <summary>
        /// 开奖号2
        /// </summary>
        /// <returns></returns>
        public int? OpenCode2 { get; set; }
        /// <summary>
        /// 开奖号3
        /// </summary>
        /// <returns></returns>
        public int? OpenCode3 { get; set; }
        /// <summary>
        /// 开奖号4
        /// </summary>
        /// <returns></returns>
        public int? OpenCode4 { get; set; }
        /// <summary>
        /// 开奖号5
        /// </summary>
        /// <returns></returns>
        public int? OpenCode5 { get; set; }
        /// <summary>
        /// 开奖号6
        /// </summary>
        /// <returns></returns>
        public int? OpenCode6 { get; set; }
        /// <summary>
        /// 开奖号7
        /// </summary>
        /// <returns></returns>
        public int? OpenCode7 { get; set; }
        /// <summary>
        /// 开奖号8
        /// </summary>
        /// <returns></returns>
        public int? OpenCode8 { get; set; }
        /// <summary>
        /// 开奖时间
        /// </summary>
        /// <returns></returns>
        public DateTime? OpenTime { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        /// <returns></returns>
        public DateTime? Addtime { get; set; }
        /// <summary>
        /// 备用字段
        /// </summary>
        /// <returns></returns>
        public string Spare { get; set; }
        /// <summary>
        /// 是否校验过
        /// </summary>
        /// <returns></returns>
        public bool? IsChecked { get; set; }
        /// <summary>
        /// 校验是否通过
        /// </summary>
        /// <returns></returns>
        public bool? IsPassed { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.ID = CommonHelper.GetGuid().ToString();
            this.IsChecked = false;
            this.IsPassed = false;
            this.Addtime = DateTimeHelper.Now;
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
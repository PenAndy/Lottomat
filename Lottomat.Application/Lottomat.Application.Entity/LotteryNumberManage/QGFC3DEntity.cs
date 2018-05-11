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
    /// 日 期：2017-11-17 12:51
    /// 描 述：全国彩-福彩3D
    /// </summary>
    public class QGFC3DEntity : BaseEntity
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
        public int? Term { get; set; }
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
        /// 开奖时间
        /// </summary>
        /// <returns></returns>
        public DateTime? OpenTime { get; set; }
        /// <summary>
        /// 试机号
        /// </summary>
        /// <returns></returns>
        public string ShiJiHao { get; set; }
        /// <summary>
        /// 开机号
        /// </summary>
        /// <returns></returns>
        public string KaiJiHao { get; set; }
        /// <summary>
        /// 开奖详情
        /// </summary>
        /// <returns></returns>
        public string Detail { get; set; }
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
        /// 是否检查过
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
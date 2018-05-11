using System;
using System.Collections.Generic;

namespace Lottomat.Application.Entity.CommonEntity
{
    /// <summary>
    /// 开奖模型
    /// </summary>
    class OpenCodeModel
    {
    }

    /// <summary>
    /// 基本开奖对象
    /// </summary>
    public class BaseOpenCodeModel
    {
        /// <summary>
        /// 自增主键
        /// </summary>
        public int PK { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 期号
        /// </summary>
        public long Term { get; set; }
        /// <summary>
        /// 开奖日期
        /// </summary>
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// 数据添加时间
        /// </summary>
        public DateTime Addtime { get; set; }
        /// <summary>
        /// 描述备用
        /// </summary>
        public string Spare { get; set; }
        /// <summary>
        /// 是否校验
        /// </summary>

        public bool IsChecked { get; set; }
        /// <summary>
        /// 校验是否通过
        /// </summary>

        public string IsPassed { get; set; }

    }
    /// <summary>
    /// 1个球号的开奖对象
    /// </summary>
    public class OpenCode1Model : BaseOpenCodeModel
    {
        /// <summary>
        /// 开奖球号1
        /// </summary>
        public int OpenCode1 { get; set; }
    }
    /// <summary>
    /// 3个球号的开奖对象
    /// </summary>
    public class OpenCode3Model : OpenCode1Model
    {
        /// <summary>
        /// 开奖球号2
        /// </summary>
        public int OpenCode2 { get; set; }
        /// <summary>
        /// 开奖球号3
        /// </summary>
        public int OpenCode3 { get; set; }
    }
    /// <summary>
    /// 4个球号的开奖对象
    /// </summary>
    public class OpenCode4Model : OpenCode3Model
    {
        /// <summary>
        /// 开奖球号4
        /// </summary>
        public int OpenCode4 { get; set; }
    }
    /// <summary>
    /// 5个球号的开奖对象
    /// </summary>
    public class OpenCode5Model : OpenCode4Model
    {
        /// <summary>
        /// 开奖球号5
        /// </summary>
        public int OpenCode5 { get; set; }
    }
    /// <summary>
    /// 7个球号的开奖对象
    /// </summary>
    public class OpenCode7Model : OpenCode5Model
    {
        /// <summary>
        /// 开奖球号6
        /// </summary>
        public int OpenCode6 { get; set; }
        /// <summary>
        /// 开奖球号7
        /// </summary>
        public int OpenCode7 { get; set; }
    }
    /// <summary>
    /// 8个球号的开奖对象
    /// </summary>
    public class OpenCode8Model : OpenCode7Model
    {
        /// <summary>
        /// 开奖球号8
        /// </summary>
        public int OpenCode8 { get; set; }
    }
    /// <summary>
    /// 10个球号的开奖对象
    /// </summary>
    public class OpenCode10Model : OpenCode8Model
    {
        /// <summary>
        /// 开奖球号9
        /// </summary>
        public int OpenCode9 { get; set; }
        /// <summary>
        /// 开奖球号10
        /// </summary>
        public int OpenCode10 { get; set; }
    }
    /// <summary>
    /// 21个球号的开奖对象
    /// </summary>
    public class OpenCode21Model : OpenCode10Model
    {
        /// <summary>
        /// 开奖球号11
        /// </summary>
        public int OpenCode11 { get; set; }
        /// <summary>
        /// 开奖球号12
        /// </summary>
        public int OpenCode12 { get; set; }
        /// <summary>
        /// 开奖球号13
        /// </summary>
        public int OpenCode13 { get; set; }
        /// <summary>
        /// 开奖球号14
        /// </summary>
        public int OpenCode14 { get; set; }
        /// <summary>
        /// 开奖球号15
        /// </summary>
        public int OpenCode15 { get; set; }
        /// <summary>
        /// 开奖球号16
        /// </summary>
        public int OpenCode16 { get; set; }
        /// <summary>
        /// 开奖球号17
        /// </summary>
        public int OpenCode17 { get; set; }
        /// <summary>
        /// 开奖球号18
        /// </summary>
        public int OpenCode18 { get; set; }
        /// <summary>
        /// 开奖球号19
        /// </summary>
        public int OpenCode19 { get; set; }
        /// <summary>
        /// 开奖球号20
        /// </summary>
        public int OpenCode20 { get; set; }
        /// <summary>
        /// 开奖球号21
        /// </summary>
        public int OpenCode21 { get; set; }
    }

    /// <summary>
    /// 开奖1个球号的地方彩
    /// </summary>
    public class OpenCode1DTModel : OpenCode1Model
    {
        /// <summary>
        /// 开奖详情
        /// </summary>
        public string Detail { get; set; }
        /// <summary>
        /// 详情地址
        /// </summary>
        public string DetailUrl { get; set; }
    }
    /// <summary>
    /// 开奖5个球号的地方彩
    /// </summary>
    public class OpenCode5DTModel : OpenCode5Model
    {
        /// <summary>
        /// 开奖详情
        /// </summary>
        public string Detail { get; set; }
        /// <summary>
        /// 详情地址
        /// </summary>
        public string DetailUrl { get; set; }
    }
    /// <summary>
    /// 开奖7个球号的地方彩
    /// </summary>
    public class OpenCode7DTModel : OpenCode7Model
    {
        /// <summary>
        /// 开奖详情
        /// </summary>
        public string Detail { get; set; }
        /// <summary>
        /// 详情地址
        /// </summary>
        public string DetailUrl { get; set; }
    }
    /// <summary>
    /// 开奖8个球号的地方彩
    /// </summary>
    public class OpenCode8DTModel : OpenCode8Model
    {
        /// <summary>
        /// 开奖详情
        /// </summary>
        public string Detail { get; set; }
        /// <summary>
        /// 详情地址
        /// </summary>
        public string DetailUrl { get; set; }
    }

    /// <summary>
    /// 开奖详情模型
    /// </summary>
    public class OpenCodeDetailModel
    {
        /// <summary>
        /// 本期销售额
        /// </summary>
        public decimal Sales { get; set; }
        /// <summary>
        /// 奖池累计金额
        /// </summary>
        public decimal Jackpots { get; set; }
        /// <summary>
        /// 奖级信息列表
        /// </summary>
        public List<OpenCodeDetailLevelModel> Levels { get; set; }
    }
    /// <summary>
    /// 奖级模型
    /// </summary>
    public class OpenCodeDetailLevelModel
    {
        /// <summary>
        /// 奖级名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 奖级中奖人数
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 奖级金额
        /// </summary>
        public decimal Money { get; set; }
    }

}

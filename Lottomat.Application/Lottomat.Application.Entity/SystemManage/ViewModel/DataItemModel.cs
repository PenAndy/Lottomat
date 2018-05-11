
namespace Lottomat.Application.Entity.SystemManage.ViewModel
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2015.11.17 9:56
    /// 描 述：数据字典明细
    /// </summary>
    public class DataItemModel
    {
        #region 实体成员
        /// <summary>
        /// 分类主键
        /// </summary>
        public string ItemId { get; set; }
        /// <summary>
        /// 分类编码
        /// </summary>		
        public string EnCode { get; set; }
        /// <summary>
        /// 明细主键
        /// </summary>		
        public string ItemDetailId { get; set; }
        /// <summary>
        /// 父级主键
        /// </summary>		
        public string ParentId { get; set; }
        /// <summary>
        /// 项目名
        /// </summary>		
        public string ItemName { get; set; }
        /// <summary>
        /// 项目值
        /// </summary>		
        public string ItemValue { get; set; }
        /// <summary>
        /// 简拼
        /// </summary>		
        public string SimpleSpelling { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>		
        public int? SortCode { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>		
        public int? EnabledMark { get; set; }
        /// <summary>
        /// 是否热门
        /// </summary>
        public bool IsHot { get; set; }
        /// <summary>
        /// 是否推荐
        /// </summary>
        public bool IsRecommend { get; set; }
        /// <summary>
        /// 是否二级首页显示
        /// </summary>
        public bool IsShowHomePage { get; set; }
        #endregion
    }
}

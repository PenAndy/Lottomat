using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottomat.Application.Entity.ViewModel.ConsultationMangerModel
{
  public  class GetZX_NewsDetails
    {
        /// <summary>
        /// 文章  PK
        /// </summary>
        public int? PK { get; set;}

        /// <summary>
        /// 新闻主键
        /// </summary>
        public string NewsId { get; set; }
        /// <summary>
        /// 完整标题
        /// </summary>
        public string FullHead { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public string CreateDate { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        public string CreateUserName{ get; set; }
        /// <summary>
        /// 新闻内容
        /// </summary>
        public string NewsContent { get; set; }
        /// <summary>
        /// 期数
        /// </summary>
        public string PeriodsNumber { get; set; }
        /// <summary>
        /// 是否推荐
        /// </summary>
        public bool? IsRecommend { get; set; }
        /// <summary>
        /// 是否热门
        /// </summary>
        public bool? IsHot { get; set; }
        /// <summary>
        /// 浏览量
        /// </summary>
        public int? PV { get; set; }
        /// <summary>
        /// 三要素-Title
        /// </summary>
        /// <returns></returns>
        public string TitleElement { get; set; }
        /// <summary>
        /// 三要素-Description
        /// </summary>
        /// <returns></returns>
        public string DescriptionElement { get; set; }
        /// <summary>
        /// 三要素-Keyword
        /// </summary>
        /// <returns></returns>
        public string KeywordElement { get; set; }
        /// <summary>
        /// 上一期、下一期
        /// </summary>
        public List<PreAndNextNews> PreAndNextNewsList { get; set; }
    }
    public class PreAndNextNews
    {
        /// <summary>
        /// 标识当前文章是上一期还是下一期 1-上一期 2-下一期
        /// </summary>
        public int Which { get; set; }

        /// <summary>
        /// 新闻主键
        /// </summary>
        public string NewsId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
    }
}

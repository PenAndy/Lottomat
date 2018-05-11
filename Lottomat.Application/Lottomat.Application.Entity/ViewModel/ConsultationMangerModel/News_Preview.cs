using Lottomat.Application.Entity.PublicInfoManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottomat.Application.Entity.ViewModel.ConsultationMangerModel
{
    public class News_Preview
    {
        /// <summary>
        /// 彩种ID
        /// </summary>
        public string NewsType { get; set; }
        /// <summary>
        /// 彩种名称
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 彩种下面的分类名称
        /// </summary>
        public string ClassiFication { get; set; }
        /// <summary>
        /// 文章集合表
        /// </summary>
        public List<NewsPreviewItem> NewsPreviewItem { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class NewsPreviewItem
    {
        /// <summary>
        /// 文章id
        /// 
        /// </summary>
        public string NewsId { get; set; }
        /// <summary>
        /// 文章创建时间
        /// </summary>
        public string AddTime { get; set; }
        /// <summary>
        /// 文章标题
        /// </summary>
        public string Title { get; set; }

    }
}

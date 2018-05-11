using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottomat.Application.Entity.ViewModel.ConsultationMangerModel
{
 public   class ClassiFication
    {
        /// <summary>
        /// 菜单集合
        /// </summary>
        public List<ItemList> ItemList { get; set; }

        /// <summary>
        /// 分类下默认返回的文章
        /// </summary>
        public List<NewsPreviewItem> NewsList { get; set; }
    }

    /// <summary>
    /// 菜单
    /// </summary>
    public class ItemList
    {
        /// <summary>
        /// 分类主键
        /// </summary>		
        public string ItemId { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
        public string SimpleSpelling { get; set; }
        /// <summary>
        /// 项目名
        /// </summary>		
        public string ItemName { get; set; }
    }
}

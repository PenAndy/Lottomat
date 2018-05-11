using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottomat.Application.Entity.ViewModel.GallerMangerModel
{
    /// <summary>
    /// 热门图库菜单
    /// </summary>
   public class HotMenuModel
    {
        /// <summary>
        /// 明细主键
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 项目名
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        ///  项目值
        /// </summary>
        public string ItemValue { get; set; }
        public int SortCode { get; set; }
        /// <summary>
        ///  简拼
        /// </summary>
        public string SimpleSpelling { get; set; }
        /// <summary>
        /// 分类编码
        /// </summary>

        public string EnCode { get; set; }

      


    }
}

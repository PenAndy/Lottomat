using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottomat.Application.Entity.GalleryManage
{
   /// <summary>
   /// 查询子栏目文章列表参数
   /// </summary>
public  class ArtListParams
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页条数，默认10条
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 分类code，值固定为：A，B，C
        /// </summary>
        public string AreaCode { get; set; }
        /// <summary>
        /// 期数
        /// </summary>

        public int PeriodsNumber { get; set; }



    }
}

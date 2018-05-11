using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottomat.Application.Entity.GalleryManage
{
    /// <summary>
    /// 热门图谜查询条件
    /// </summary>
public    class ArtHotListParams
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
        /// 图库热门分类栏目名字 （seokey包含）
        /// </summary>
        public string menuName { get; set; }
        /// <summary>
        /// 期数
        /// </summary>

        public int PeriodsNumber { get; set; }


    }
}

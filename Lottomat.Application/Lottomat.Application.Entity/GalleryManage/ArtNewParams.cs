using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottomat.Application.Entity.GalleryManage
{
    /// <summary>
    /// 图库最新的栏目分页查询参数
    /// </summary>
    public class ArtNewParams
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
        /// 期数
        /// </summary>

        public int PeriodsNumber { get; set; }
    }
}

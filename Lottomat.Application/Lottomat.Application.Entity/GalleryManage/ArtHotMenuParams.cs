using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottomat.Application.Entity.GalleryManage
{
    /// <summary>
    /// 图库热门菜单查询参数
    /// </summary>
  public  class ArtHotMenuParams
    {
        /// <summary>
        /// A，B，C之一 不传视为查全部
        /// </summary>
        public string ItemName { get; set; }

    }
}

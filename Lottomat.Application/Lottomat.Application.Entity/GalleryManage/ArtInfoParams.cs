using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottomat.Application.Entity.GalleryManage
{
    /// <summary>
    /// 文章详细内容查询
    /// 可根据id查询
    /// 可根据GalleryId和PeriodsNumber 查询
    /// 参数只为GalleryId时返回最新一期数据
    /// </summary>
    public class ArtInfoParams
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 期数
        /// </summary>
        public string PeriodsNumber { get; set; }
        /// <summary>
        /// 栏目id
        /// </summary>
        public string GalleryId { get; set; }
    }
}

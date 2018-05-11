using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottomat.Application.Entity.ViewModel.GallerMangerModel
{
    /// <summary>
    /// 图库栏目实体
    /// </summary>
   public class ImageMenuInfoModel
    {
       
        public string ID { get; set; }
        /// <summary>
        /// abc分类下编号
        /// </summary>
        public int GalleryNumber { get; set; }
        /// <summary>
        /// area+GalleryNumber
        /// </summary>
        public string GalleryNumStr { get; set; }

        /// <summary>
        /// 图谜名字
        /// </summary>
        public string GalleryName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Reamrk { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 区域代码
        /// </summary>
        public string AreaCode { get; set; }
        /// <summary>
        /// 热度
        /// </summary>
        public int HotNumber { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>

        public string ImageUrl { get; set; }

        /// <summary>
        /// 是否有图片
        /// </summary>
        public int HasImage { get; set; }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottomat.Application.Entity.ViewModel.GallerMangerModel
{
   /// <summary>
   /// 图库文章详细实体
   /// </summary>
   public class ImageInfoModel
    {
        /// <summary>
        /// 信息主键
        /// </summary>
        public string ID { get; set; }
        public string ImageUrl { get; set; }

        /// <summary>
        /// 图库id
        /// </summary>
        public string galleryId { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string PeriodsNumber { get; set; }
        /// <summary>
        /// 期数
        /// </summary>
        public int SortCode { get; set; }


    }
}

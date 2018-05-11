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
   public class MenuInfoModel
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        /// <summary>
        /// 图库编号
        /// </summary>
        public string galleryNumber { get; set; }
        /// <summary>
        /// 图库名称
        /// </summary>
        public string galleryName { get; set; }
        /// <summary>
        /// 分类id
        /// </summary>
        public string menuId { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string description { get; set; }



    }
}

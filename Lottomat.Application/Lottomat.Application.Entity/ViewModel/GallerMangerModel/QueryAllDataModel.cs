using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottomat.Application.Entity.ViewModel.GallerMangerModel
{
    /// <summary>
    /// 图库全部查询viewmodel
    /// </summary>
  public  class QueryAllDataModel
    {
        /// <summary>
        /// 区域名字
        /// </summary>
        public string AreaName { get; set; }
        public int PeriodsNumber { get; set; }
        /// <summary>
        /// 区域查出的数据
        /// </summary>
        public List<ImageMenuInfoModel> list { get; set; }

    }
}

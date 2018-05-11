using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottomat.Application.Entity.InformationManage
{
   public class TrendChart
    {
        /// <summary>
        /// 走势图名称，值为"QGC", "DFC", "GPC11X5", "GPCK3", "GPCKL12", "GPCKLSF" , "GPCQTC"，注意：高频彩请传（GPC），高频彩下面的分类请传对应代码
        /// </summary>
        public string Category { get; set; }
    }
}

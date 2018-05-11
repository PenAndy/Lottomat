using Lottomat.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottomat.Application.Entity.InformationManage
{
   public  class ColorNews
    {
        /// <summary>
        /// 彩种名称
        /// </summary>
        public string Category{ get; set; }

        private int _PageSize { get; set; }
        public int PageSize
        {
            get { return _PageSize <= 0 ? GlobalStaticConstant.SYSTEM_DEFAULT_PAGE_SIZE : _PageSize; }
            set
            {
                _PageSize = value;
            }
        }

        private int _PageIndex { get; set; }
        public int PageIndex
        {
            get { return _PageIndex <= 0 ? 1 : _PageIndex; }
            set
            {
                _PageIndex = value;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottomat.Application.Entity.ViewModel.ToolsMangerModer
{
    public class Tools_Preview
    {
        /// <summary>
        /// 工具名称
        /// </summary>
        public string ToolsName { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
        public string SimpleSpelling { get; set; }

        public List<ToolsItem> ToolsPreviewItem { get; set; }

    }

    public class ToolsItem
    { 
        /// <summary>
        /// 工具名称
        /// </summary>
        /// <returns></returns>
        public string Title { get; set; }
        /// <summary>
        /// 工具连接的url
        /// </summary>
        /// <returns></returns>
        public string ToolsUrl { get; set; }

    }
}
using System.Data;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.CommonEntity;
using Lottomat.Application.IService.OpenCodeManage;
using Lottomat.Application.Service.OpenCodeManage;
using Lottomat.Util.Extension;

namespace Lottomat.Application.Busines.OpenCodeManage
{
    /// <summary>
    /// 开奖7个球号的彩种数据服务
    /// </summary>
    public class Open7CodeBLL
    {
        private IOpen7Code iOpen7Code = new Open7CodeServices();

        /// <summary>
        /// 获取最新一条记录
        /// </summary>
        /// <param name="lottery">彩种名称</param>
        /// <returns></returns>
        public OpenCode7Model GetLastItem(SCCLottery lottery)
        {
            return iOpen7Code.GetLastItem(lottery);
        }
        
        /// <summary>
        /// 新增彩种开奖数据
        /// </summary>
        /// <param name="lottery">彩种名称</param>
        /// <param name="model">开奖数据模型</param>
        /// <returns></returns>
        public bool AddOpen7Code(SCCLottery lottery, OpenCode7Model model)
        {
            return iOpen7Code.AddOpen7Code(lottery, model);
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public OpenCode7Model GetOpenCode7Model(DataTable table)
        {
            if (table.Rows.Count > 0)
            {
                return table.DataTableToObject<OpenCode7Model>();
            }
            return default(OpenCode7Model);
        }
    }
}

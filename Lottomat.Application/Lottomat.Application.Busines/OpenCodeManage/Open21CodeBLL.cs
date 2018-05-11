using System.Data;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.CommonEntity;
using Lottomat.Application.IService.OpenCodeManage;
using Lottomat.Application.Service.OpenCodeManage;
using Lottomat.Util.Extension;

namespace Lottomat.Application.Busines.OpenCodeManage
{
    /// <summary>
    /// 开奖21个球号的彩种数据服务
    /// </summary>
    public class Open21CodeBLL
    {
        private IOpen21Code iOpen21Code = new Open21CodeServices();

        /// <summary>
        /// 获取最新一条记录
        /// </summary>
        /// <param name="lottery">彩种名称</param>
        /// <returns></returns>
        public OpenCode21Model GetLastItem(SCCLottery lottery)
        {
            return iOpen21Code.GetLastItem(lottery);
        }
        
        /// <summary>
        /// 新增彩种开奖数据
        /// </summary>
        /// <param name="lottery">彩种名称</param>
        /// <param name="model">开奖数据模型</param>
        /// <returns></returns>
        public bool AddOpen21Code(SCCLottery lottery, OpenCode21Model model)
        {
            return iOpen21Code.AddOpen21Code(lottery, model);
        }
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>F
        public OpenCode21Model GetOpenCode21Model(DataTable table)
        {
            if (table.Rows.Count > 0)
            {
                return table.DataTableToObject<OpenCode21Model>();
            }
            return default(OpenCode21Model);
        }
    }
}

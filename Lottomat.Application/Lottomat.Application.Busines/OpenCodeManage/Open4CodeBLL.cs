using System.Data;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.CommonEntity;
using Lottomat.Application.IService.OpenCodeManage;
using Lottomat.Application.Service.OpenCodeManage;
using Lottomat.Util.Extension;

namespace Lottomat.Application.Busines.OpenCodeManage
{
    /// <summary>
    /// 开奖4个球号的彩种数据服务
    /// </summary>
    public class Open4CodeBLL
    {
        private IOpen4Code iOpen4Code = new Open4CodeServices();

        /// <summary>
        /// 获取最新一条记录
        /// </summary>
        /// <param name="lottery">彩种名称</param>
        /// <returns></returns>
        public OpenCode4Model GetLastItem(SCCLottery lottery)
        {
            return iOpen4Code.GetLastItem(lottery);
        }
        
        /// <summary>
        /// 新增彩种开奖数据
        /// </summary>
        /// <param name="lottery">彩种名称</param>
        /// <param name="model">开奖数据模型</param>
        /// <returns></returns>
        public bool AddOpen4Code(SCCLottery lottery, OpenCode4Model model)
        {
            return iOpen4Code.AddOpen4Code(lottery, model);
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public OpenCode4Model GetOpenCode4Model(DataTable table)
        {
            if (table.Rows.Count > 0)
            {
                return table.DataTableToObject<OpenCode4Model>();
            }
            return default(OpenCode4Model);
        }
    }
}

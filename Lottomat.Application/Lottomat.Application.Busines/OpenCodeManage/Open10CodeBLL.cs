using System.Data;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.CommonEntity;
using Lottomat.Application.IService.OpenCodeManage;
using Lottomat.Application.Service.OpenCodeManage;
using Lottomat.Util.Extension;

namespace Lottomat.Application.Busines.OpenCodeManage
{
    /// <summary>
    /// 开奖10个球号的彩种数据服务
    /// </summary>
    public class Open10CodeBLL
    {
        private IOpen10Code iOpen10Code = new Open10CodeServices();

        /// <summary>
        /// 获取最新一条记录
        /// </summary>
        /// <param name="lottery">彩种名称</param>
        /// <returns></returns>
        public OpenCode10Model GetLastItem(SCCLottery lottery)
        {
            return iOpen10Code.GetLastItem(lottery);
        }

        /// <summary>
        /// 新增彩种开奖数据
        /// </summary>
        /// <param name="lottery">彩种名称</param>
        /// <param name="model">开奖数据模型</param>
        /// <returns></returns>
        public bool AddOpen10Code(SCCLottery lottery, OpenCode10Model model)
        {
            return iOpen10Code.AddOpen10Code(lottery, model);
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public OpenCode10Model GetOpenCode10Model(DataTable table)
        {
            if (table.Rows.Count > 0)
            {
                return table.DataTableToObject<OpenCode10Model>();
            }
            return default(OpenCode10Model);
        }
    }
}

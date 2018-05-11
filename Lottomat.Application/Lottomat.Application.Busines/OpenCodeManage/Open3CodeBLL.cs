using System.Data;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.CommonEntity;
using Lottomat.Application.IService.OpenCodeManage;
using Lottomat.Application.Service.OpenCodeManage;
using Lottomat.Util.Extension;

namespace Lottomat.Application.Busines.OpenCodeManage
{
    /// <summary>
    /// 开奖3个球号的彩种数据服务
    /// </summary>
    public class Open3CodeBLL
    {
        private IOpen3Code iOpen3Code = new Open3CodeServices();

        /// <summary>
        /// 获取最新一条记录
        /// </summary>
        /// <param name="lottery">彩种名称</param>
        /// <returns></returns>
        public OpenCode3Model GetLastItem(SCCLottery lottery)
        {
            return iOpen3Code.GetLastItem(lottery);
        }
        
        /// <summary>
        /// 新增彩种开奖数据
        /// </summary>
        /// <param name="lottery">彩种名称</param>
        /// <param name="model">开奖数据模型</param>
        /// <returns></returns>
        public bool AddOpen3Code(SCCLottery lottery, OpenCode3Model model)
        {
            return iOpen3Code.AddOpen3Code(lottery, model);
        }
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public OpenCode3Model GetOpenCode3Model(DataTable table)
        {
            if (table.Rows.Count > 0)
            {
                return table.DataTableToObject<OpenCode3Model>();
            }
            return default(OpenCode3Model);
        }

    }
}

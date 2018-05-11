using System.Data;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.CommonEntity;
using Lottomat.Application.IService.OpenCodeManage;
using Lottomat.Application.Service.OpenCodeManage;
using Lottomat.Util.Extension;

namespace Lottomat.Application.Busines.OpenCodeManage
{
    /// <summary>
    /// 开奖5个球号的彩种数据服务
    /// </summary>
    public class Open5CodeBLL
    {
        private IOpen5Code iOpen5Code = new Open5CodeServices();

        /// <summary>
        /// 获取最新一条记录
        /// </summary>
        /// <param name="lottery">彩种名称</param>
        /// <returns></returns>
        public OpenCode5Model GetLastItem(SCCLottery lottery)
        {
            return iOpen5Code.GetLastItem(lottery);
        }
        
        /// <summary>
        /// 新增彩种开奖数据
        /// </summary>
        /// <param name="lottery">彩种名称</param>
        /// <param name="model">开奖数据模型</param>
        /// <returns></returns>
        public bool AddOpen5Code(SCCLottery lottery, OpenCode5Model model)
        {
            return iOpen5Code.AddOpen5Code(lottery, model);
        }
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public OpenCode5Model GetOpenCode5Model(DataTable table)
        {
            if (table.Rows.Count > 0)
            {
                return table.DataTableToObject<OpenCode5Model>();
            }
            return default(OpenCode5Model);
        }
    }
}

using System.Data;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.CommonEntity;
using Lottomat.Application.IService.OpenCodeManage;
using Lottomat.Application.Service.OpenCodeManage;
using Lottomat.Util.Extension;

namespace Lottomat.Application.Busines.OpenCodeManage
{
    /// <summary>
    /// 开奖8个球号的彩种数据服务
    /// </summary>
    public class Open8CodeBLL
    {
        private IOpen8Code iOpen8Code = new Open8CodeServices();

        /// <summary>
        /// 获取最新一条记录
        /// </summary>
        /// <param name="lottery">彩种名称</param>
        /// <returns></returns>
        public OpenCode8Model GetLastItem(SCCLottery lottery)
        {
            return iOpen8Code.GetLastItem(lottery);
        }
        
        /// <summary>
        /// 新增彩种开奖数据
        /// </summary>
        /// <param name="lottery">彩种名称</param>
        /// <param name="model">开奖数据模型</param>
        /// <returns></returns>
        public bool AddOpen8Code(SCCLottery lottery, OpenCode8Model model)
        {
            return iOpen8Code.AddOpen8Code(lottery, model);
        }
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public OpenCode8Model GetOpenCode8Model(DataTable table)
        {
            if (table.Rows.Count > 0)
            {
                return table.DataTableToObject<OpenCode8Model>();
            }
            return default(OpenCode8Model);
        }
    }
}

using System.Collections.Generic;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.CommonEntity;

namespace Lottomat.Application.IService.OpenCodeManage
{
    /// <summary>
    /// 开奖7个球号的彩种数据接口
    /// </summary>
    public interface IOpen7Code
    {
        /// <summary>
        /// 获取最新一条记录
        /// </summary>
        /// <param name="lottery">彩种名称</param>
        /// <returns></returns>
        OpenCode7Model GetLastItem(SCCLottery lottery);
        
        /// <summary>
        /// 新增彩种开奖数据
        /// </summary>
        /// <param name="lottery">彩种名称</param>
        /// <param name="model">开奖数据模型</param>
        /// <returns></returns>
        bool AddOpen7Code(SCCLottery lottery, OpenCode7Model model);
        
    }
}

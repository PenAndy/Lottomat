using System.Collections.Generic;
using Lottomat.Application.Code;
using Lottomat.Application.Entity.CommonEntity;

namespace Lottomat.Application.IService.OpenCodeManage
{
    /// <summary>
    /// 开奖8个球号的彩种相关数据接口
    /// </summary>
    public interface IOpen8Code
    {
        /// <summary>
        /// 获取最新一条记录
        /// </summary>
        /// <param name="lottery">彩种名称</param>
        /// <returns></returns>
        OpenCode8Model GetLastItem(SCCLottery lottery);
        
        /// <summary>
        /// 新增彩种开奖数据
        /// </summary>
        /// <param name="lottery">彩种名称</param>
        /// <param name="model">开奖数据模型</param>
        /// <returns></returns>
        bool AddOpen8Code(SCCLottery lottery, OpenCode8Model model);
    }
}

using Lottomat.Application.Entity.LotteryNumberManage;
using Lottomat.Application.IService.LotteryNumberManage;
using Lottomat.Application.Service.LotteryNumberManage;
using Lottomat.Util.WebControl;
using System.Collections.Generic;
using System;

namespace Lottomat.Application.Busines.LotteryNumberManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创 建：超级管理员
    /// 日 期：2017-11-22 16:17
    /// 描 述：地方彩-黑龙江25选5
    /// </summary>
    public class DFLJFC22x5HeiLongJiangBLL
    {
        private IDFLJFC22x5HeiLongJiangService service = new DFLJFC22x5HeiLongJiangService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<DFLJFC22x5HeiLongJiangEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<DFLJFC22x5HeiLongJiangEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DFLJFC22x5HeiLongJiangEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, DFLJFC22x5HeiLongJiangEntity entity,string isCheck)
        {
            try
            {
                service.SaveForm(keyValue, entity, isCheck);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}

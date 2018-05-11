using Lottomat.Application.Entity.BaseManage;
using Lottomat.Application.IService.BaseManage;
using Lottomat.Application.Service.BaseManage;
using Lottomat.Util.WebControl;
using System.Collections.Generic;
using System;
using Lottomat.Application.IService.GalleryManage;
using Lottomat.Application.Service.GalleryManage;
using Lottomat.Application.Entity.GalleryManage;
using System.Linq.Expressions;
using Lottomat.Application.Entity.PublicInfoManage;
using System.Linq;
using Newtonsoft.Json;
namespace Lottomat.Application.Busines.GalleryManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2016-2017
    /// 创 建：开发者账号
    /// 日 期：2017-10-19 14:29
    /// 描 述：图库详情表
    /// </summary>
    public class Tk_GalleryDetailBLL
    {
        private ITk_GalleryDetailIService service = new Tk_GalleryDetailService();
        private Tk_GalleryBLL  tk_GalleryBLL = new Tk_GalleryBLL();
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Tk_GalleryDetail> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        public IEnumerable<Tk_GalleryDetail> GetList(Expression<Func<Tk_GalleryDetail, bool>> condition)
        {
            return service.GetList(condition);
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Tk_GalleryDetail GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, Tk_GalleryDetail entity)
        {
            try
            {
                
                service.SaveForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Tk_GalleryDetail> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                if (!String.IsNullOrEmpty(queryJson))
                {
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(queryJson);
                    List<Tk_Gallery> glist = new List<Tk_Gallery>();
                    if (dict.Keys.Contains("GalleryName"))
                    {
                        string gname = dict["GalleryName"];
                        List<Tk_Gallery> tlist = tk_GalleryBLL.GetList(w => w.GalleryName == gname).ToList();
                        if (tlist.Count > 0)
                        {
                            dict["GalleryId"] = tlist[0].ID;
                            queryJson = JsonConvert.SerializeObject(dict);
                        }
                    }
                }
                
                List<Tk_GalleryDetail> list= service.GetPageList(pagination, queryJson).ToList();
                List<string> GalleryIdlist = list.Select(s => s.GalleryId).ToList();
                List<Tk_Gallery> gallerylist = tk_GalleryBLL.GetList(w => GalleryIdlist.Contains(w.ID)).ToList() ;
                for (int i = 0; i < list.Count; i++)
                {
                    Tk_Gallery tmp = gallerylist.SingleOrDefault(w=>w.ID==list[i].GalleryId);
                    if (tmp!=null)
                    {
                        list[i].GalleryId = tmp.GalleryName;
                    }
                    else
                    {
                        list[i].GalleryId = "";
                    }
                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int MenuNewPeriodsNumber(string menuname)
        {
            try
            {
                return service.MenuNewPeriodsNumber(menuname);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int NewPeriodsNumber()
        {
            try
            {
                return service.NewPeriodsNumber();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<Tk_GalleryDetail> QueryDetailByGalleryId(List<string> galleryIds, int periodsNumber)
        {
            try
            {
                return service.QueryDetailByGalleryId(galleryIds, periodsNumber);
            }
            catch (Exception)
            {

                throw;
            }
        }




        #endregion
    }
}

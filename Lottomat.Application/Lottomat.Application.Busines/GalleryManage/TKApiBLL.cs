using Lottomat.Application.Busines.SystemManage;
using Lottomat.Application.Entity.GalleryManage;
using Lottomat.Application.Entity.SystemManage;
using Lottomat.Application.Entity.ViewModel.GallerMangerModel;
using Lottomat.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Lottomat.Util.Extension;
using Lottomat.Application.Entity.SystemManage.ViewModel;
using System.Globalization;

namespace Lottomat.Application.Busines.GalleryManage
{
    public static class TKApiBLL
    {
        /// <summary>
        /// 根据菜单id拉取到栏目列表
        /// </summary>
        public static List<DataItemDetailEntity> QueryCloumn(string menuid)
        {
            DataItemDetailBLL dataItemDetailBLL = new DataItemDetailBLL();
            List<DataItemDetailEntity> list = dataItemDetailBLL.GetList(menuid).ToList();
            return list;

        }

        /// <summary>
        ///查询图库全部列表
        /// </summary>
        /// <returns></returns>
        public static List<QueryAllDataModel> QueryAll(int count)
        {
            List<QueryAllDataModel> rtdata = new List<QueryAllDataModel>();
            Tk_GalleryBLL tk_GalleryBLL = new Tk_GalleryBLL();
            List<Tk_Gallery> list = tk_GalleryBLL.QueryAll(count);
            string[] aryname = new string[] { "A", "B", "C" };
            for (int i = 0; i < aryname.Length; i++)
            {
                List<ImageMenuInfoModel> tlist = new List<ImageMenuInfoModel>();
                IEnumerator<Tk_Gallery> tmp = list.Where(t => t.AreaCode == aryname[i]).GetEnumerator();
                while (tmp.MoveNext())
                {
                    ImageMenuInfoModel tmpImg = new ImageMenuInfoModel()
                    {
                        AreaCode = tmp.Current.AreaCode,
                        CreateTime = tmp.Current.CreateTime.Value.ToString(),
                        GalleryName = tmp.Current.GalleryName,
                        GalleryNumber = tmp.Current.GalleryNumber.Value,
                        HotNumber = tmp.Current.HotNumber.Value,
                        ID = tmp.Current.ID,
                        Reamrk = tmp.Current.Reamrk
                    };
                    tlist.Add(tmpImg);
                }
                rtdata.Add(new QueryAllDataModel() { AreaName = aryname[i], list = tlist });
            }
            return rtdata;
        }
        public static List<QueryAllDataModel> QueryAll(int page, int rows, string queryjson, out int totalrows, out int totalpage, int periodsNumber)
        {
            List<QueryAllDataModel> rtdata = new List<QueryAllDataModel>();
            Tk_GalleryBLL tk_GalleryBLL = new Tk_GalleryBLL();
            Tk_GalleryDetailBLL tk_GalleryDetailBLL = new Tk_GalleryDetailBLL();

            Pagination pageparam = new Pagination()
            {
                page = page,
                rows = rows,
                sidx = "GalleryNumber",
                sord = "asc"
            };
            List<Tk_Gallery> list = tk_GalleryBLL.GetPageList(pageparam, queryjson).ToList();
            List<string> gallid = list.Select(s => s.ID).ToList();
            List<Tk_GalleryDetail> qDetailList = new List<Tk_GalleryDetail>();
            if (periodsNumber == 0)
            {
                periodsNumber = getNewperoidNumber();
            }
            if (gallid.Count > 0)
            {
                qDetailList = tk_GalleryDetailBLL.QueryDetailByGalleryId(gallid, periodsNumber);
            }
            totalrows = pageparam.records;
            totalpage = pageparam.total;
            string[] aryname = new string[] { "A", "B", "C" };
            for (int i = 0; i < aryname.Length; i++)
            {

                List<ImageMenuInfoModel> tlist = new List<ImageMenuInfoModel>();
                IEnumerator<Tk_Gallery> tmp = list.Where(t => t.AreaCode == aryname[i]).GetEnumerator();
                while (tmp.MoveNext())
                {
                    ImageMenuInfoModel tmpImg = new ImageMenuInfoModel()
                    {
                        GalleryNumStr = tmp.Current.AreaCode + GetGallerNum(tmp.Current.GalleryNumber.Value),
                        AreaCode = tmp.Current.AreaCode,
                        CreateTime = tmp.Current.CreateTime.Value.ToString(),
                        GalleryName = tmp.Current.GalleryName,
                        GalleryNumber = tmp.Current.GalleryNumber.Value,
                        HotNumber = tmp.Current.HotNumber.Value,
                        ID = tmp.Current.ID,
                        Reamrk = tmp.Current.Reamrk,
                        HasImage = 0

                    };
                    List<Tk_GalleryDetail> qtmp = qDetailList.Where(w => w.GalleryId == tmpImg.ID && w.PeriodsNumber == periodsNumber.ToString()).ToList();
                    if (qtmp.Count > 0)
                    {
                        tmpImg.ImageUrl = qtmp[0].ImageUrl;
                        tmpImg.HasImage = 1;
                    }
                    tlist.Add(tmpImg);
                }
                tlist = tlist.OrderBy(s => s.GalleryNumber).ToList();//升序排序
                rtdata.Add(new QueryAllDataModel() { AreaName = aryname[i], list = tlist, PeriodsNumber = periodsNumber });
            }
            return rtdata;
        }
        /// <summary>
        /// 获取最新期数(计算出非数据库)
        /// </summary>
        /// <returns></returns>
        public static int getNewperoidNumber()
        {
            string timeStr = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            //obj.Year = DateTime.Now.Year.ToString();

            //判断当前时间是否在工作时间段内
            string _strWorkingDayPM = "21:30";
            //string _strWorkingDayPM = "17:30";
            TimeSpan dspWorkingDayPM = DateTime.Parse(_strWorkingDayPM).TimeOfDay;
            //TimeSpan dspWorkingDayPM = DateTime.Parse(_strWorkingDayPM).TimeOfDay;

            //string time1 = "2017-2-17 8:10:00";
            DateTime t1 = Convert.ToDateTime(timeStr);
            int peroidNumber = 0;
            TimeSpan dspNow = t1.TimeOfDay;
            if (dspNow < dspWorkingDayPM)
            {
                peroidNumber = (int.Parse(DateTime.Now.DayOfYear.ToString()) - 8 + 1);

            }
            else
            {
                peroidNumber = (int.Parse(DateTime.Now.DayOfYear.ToString()) - 8 + 1 + 1);

            }
            return peroidNumber;
        }
        public static string GetGallerNum(int num)
        {
            if (num.ToString().Length < 2)
            {
                return "0" + num.ToString();
            }
            else
            {
                return num.ToString();
            }
        }
        /// <summary>
        /// 查询热门图库分类
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<HotMenuModel> QueryHotMenu(string[] itemname)
        {

            DataItemDetailBLL dataItemCache = new DataItemDetailBLL();
            IEnumerator<DataItemModel> enumerator = dataItemCache.GetDataItemList().Where(t => itemname.Contains(t.EnCode)).GetEnumerator();
            List<HotMenuModel> rtdata = new List<HotMenuModel>();
            while (enumerator.MoveNext())
            {
                HotMenuModel tmp = new HotMenuModel()
                {
                    EnCode = enumerator.Current.EnCode,
                    SimpleSpelling = enumerator.Current.SimpleSpelling,
                    SortCode = enumerator.Current.SortCode.Value,
                    ID = enumerator.Current.ItemDetailId,
                    ItemName = enumerator.Current.ItemName,
                    ItemValue = enumerator.Current.ItemValue,

                };
                rtdata.Add(tmp);
            }
            //rtdata = rtdata.OrderBy(s => s.GalleryNumber).ToList();//升序排序
            return rtdata;
        }
        /// <summary>
        /// 查询热门图库
        /// </summary>
        /// <returns></returns>
        public static List<ImageMenuInfoModel> QueryHotArtList(int page, int rows, string menuname, out int totalrows, out int totalpage, int periodsNumber)
        {
            Tk_GalleryDetailBLL tk_GalleryDetailBLL = new Tk_GalleryDetailBLL();
            Tk_GalleryBLL tk_GalleryBLL = new Tk_GalleryBLL();
            Pagination pageparam = new Pagination()
            {
                page = page,
                rows = rows,
                sidx = "GalleryNumber",
                sord = "asc"
            };
            List<Tk_Gallery> list = tk_GalleryBLL.GetPageList(pageparam, new { SeoKey = menuname }.TryToJson()).ToList();
            totalrows = pageparam.records;
            totalpage = pageparam.total;
            List<string> gallid = list.Select(s => s.ID).ToList();
            List<Tk_GalleryDetail> qDetailList = new List<Tk_GalleryDetail>();
            //int periodsNumber = tk_GalleryDetailBLL.NewPeriodsNumber();
            if (periodsNumber == 0)
            {
                periodsNumber = tk_GalleryDetailBLL.NewPeriodsNumber();
            }
            if (gallid.Count > 0)
            {
                qDetailList = tk_GalleryDetailBLL.QueryDetailByGalleryId(gallid, periodsNumber);
            }
            List<ImageMenuInfoModel> rtdata = new List<ImageMenuInfoModel>();
            List<ImageMenuInfoModel> tplist = new List<ImageMenuInfoModel>();
            IEnumerator<Tk_Gallery> enumerator = list.GetEnumerator();
            while (enumerator.MoveNext())
            {
                ImageMenuInfoModel tmp = new ImageMenuInfoModel()
                {
                    GalleryNumStr = enumerator.Current.AreaCode + GetGallerNum(enumerator.Current.GalleryNumber.Value),
                    GalleryName = enumerator.Current.GalleryName,
                    AreaCode = enumerator.Current.AreaCode,
                    CreateTime = enumerator.Current.CreateTime.ToString(),
                    GalleryNumber = enumerator.Current.GalleryNumber.Value,
                    HotNumber = enumerator.Current.HotNumber.Value,
                    ID = enumerator.Current.ID,
                    Reamrk = enumerator.Current.Reamrk

                };
                List<Tk_GalleryDetail> qtmp = qDetailList.Where(w => w.GalleryId == tmp.ID && w.PeriodsNumber == periodsNumber.ToString()).ToList();
                if (qtmp.Count > 0)
                {
                    tmp.ImageUrl = qtmp[0].ImageUrl;
                    tmp.HasImage = 1;
                }

                tplist.Add(tmp);
            }
            //  rtdata = rtdata.OrderBy(s => s.GalleryNumber).ToList();//升序排序
            List<ImageMenuInfoModel> alist = tplist.Where(w => w.AreaCode == "A").OrderBy(o => o.GalleryNumber).ToList();
            List<ImageMenuInfoModel> blist = tplist.Where(w => w.AreaCode == "B").OrderBy(o => o.GalleryNumber).ToList();
            List<ImageMenuInfoModel> clist = tplist.Where(w => w.AreaCode == "C").OrderBy(o => o.GalleryNumber).ToList();
            rtdata.AddRange(alist);
            rtdata.AddRange(blist);
            rtdata.AddRange(clist);
            return rtdata;


        }

        /// <summary>
        /// 查询图谜包
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<Tk_Gallery> QueryImgMystery(int count)
        {
            //string sql = string.Format("");
            //return this.BaseRepository().FindList(sql).ToList();
            return null;

        }

        /// <summary>
        /// 查询最新的
        /// </summary>
        /// <returns></returns>
        public static List<ImageMenuInfoModel> QueryNew(int page, int rows, out int totalrows, out int totalpage, int periodsNumber)
        {
            Tk_GalleryDetailBLL tk_GalleryDetailBLL = new Tk_GalleryDetailBLL();
            Tk_GalleryBLL tk_GalleryBLL = new Tk_GalleryBLL();
            Pagination pageparam = new Pagination()
            {
                page = page,
                sidx = "GalleryNumber",
                rows = rows,
                sord = "asc"
            };

            List<Tk_Gallery> list = tk_GalleryBLL.GetPageList(pageparam, "").ToList();
            IEnumerator<Tk_Gallery> enumerator = list.GetEnumerator();
            totalrows = pageparam.records;
            totalpage = pageparam.total;
            List<string> gallid = list.Select(s => s.ID).ToList();
            List<Tk_GalleryDetail> qDetailList = new List<Tk_GalleryDetail>();
            if (periodsNumber == 0)
            {
                periodsNumber = tk_GalleryDetailBLL.NewPeriodsNumber();
            }
            if (gallid.Count > 0)
            {
                qDetailList = tk_GalleryDetailBLL.QueryDetailByGalleryId(gallid, periodsNumber);
            }
            List<ImageMenuInfoModel> rtdata = new List<ImageMenuInfoModel>();
            List<ImageMenuInfoModel> tplist = new List<ImageMenuInfoModel>();
            while (enumerator.MoveNext())
            {
                ImageMenuInfoModel tmp = new ImageMenuInfoModel()
                {
                    GalleryNumStr = enumerator.Current.AreaCode + GetGallerNum(enumerator.Current.GalleryNumber.Value),
                    GalleryName = enumerator.Current.GalleryName,
                    AreaCode = enumerator.Current.AreaCode,
                    CreateTime = enumerator.Current.CreateTime.ToString(),
                    GalleryNumber = enumerator.Current.GalleryNumber.Value,
                    HotNumber = enumerator.Current.HotNumber.Value,
                    ID = enumerator.Current.ID,
                    Reamrk = enumerator.Current.Reamrk
                };
                List<Tk_GalleryDetail> qtmp = qDetailList.Where(w => w.GalleryId == tmp.ID && w.PeriodsNumber == periodsNumber.ToString()).ToList();
                if (qtmp.Count > 0)
                {
                    tmp.ImageUrl = qtmp[0].ImageUrl;
                    tmp.HasImage = 1;
                }
                tplist.Add(tmp);
            }
            List<ImageMenuInfoModel> alist = tplist.Where(w => w.AreaCode == "A").OrderBy(o => o.GalleryNumber).ToList();
            List<ImageMenuInfoModel> blist = tplist.Where(w => w.AreaCode == "B").OrderBy(o => o.GalleryNumber).ToList();
            List<ImageMenuInfoModel> clist = tplist.Where(w => w.AreaCode == "C").OrderBy(o => o.GalleryNumber).ToList();
            rtdata.AddRange(alist);
            rtdata.AddRange(blist);
            rtdata.AddRange(clist);
            return rtdata;
            // rtdata = rtdata.OrderBy(s => s.GalleryNumber).ToList();//升序排序

        }

        /// <summary>
        /// 查询图谜实体
        /// </summary>
        /// <param name="artInfoParams"></param>
        /// <returns></returns>
        public static ImageInfoModel QueryArtInfo(ArtInfoParams artInfoParams)
        {
            Tk_GalleryDetailBLL tk_GalleryDetailBLL = new Tk_GalleryDetailBLL();
            string json = artInfoParams.TryToJson();
            List<Tk_GalleryDetail> list = tk_GalleryDetailBLL.GetPageList(new Pagination() { page = 1, rows = 1, sidx = "PeriodsNumber", sord = "desc" }, json).ToList();
            ImageInfoModel rtdata = new ImageInfoModel();
            if (list.Count > 0)
            {
                rtdata = new ImageInfoModel()
                {
                    galleryId = list[0].GalleryId,
                    SortCode = list[0].SortCode.Value,
                    ID = list[0].ID,
                    ImageUrl = list[0].ImageUrl,
                    PeriodsNumber = list[0].PeriodsNumber
                };
            }
            return rtdata;


        }
        
        public static ImageInfoModel NewArtList(ArtInfoParams artInfoParams)
        {
            Tk_GalleryDetailBLL tk_GalleryDetailBLL = new Tk_GalleryDetailBLL();
            string json = artInfoParams.TryToJson();
            List<Tk_GalleryDetail> list = tk_GalleryDetailBLL.GetPageList(new Pagination() { page = 1, rows = 1, sidx = "PeriodsNumber", sord = "desc" }, json).ToList();
            ImageInfoModel rtdata = new ImageInfoModel();
            if (list.Count > 0)
            {
                rtdata = new ImageInfoModel()
                {
                    PeriodsNumber = list[0].PeriodsNumber
                };
            }
            // rtdata = rtdata.OrderBy(s => s.GalleryNumber).ToList();//升序排序
            return rtdata;


        }
        public static int MenuNewPeriodsNumber(string menuname)
        {
            Tk_GalleryDetailBLL tk_GalleryDetailBLL = new Tk_GalleryDetailBLL();
            return tk_GalleryDetailBLL.MenuNewPeriodsNumber(menuname);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lottomat.Util;
using Lottomat.Util.Extension;
using Lottomat.Util.WebControl;
using Lottomat.Util.Offices;
using Lottomat.Utils.Date;

namespace Lottomat.Application.Admin.Controllers
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2016-2017
    /// 创建人：赵轶
    /// 日 期：2016.2.03 10:58
    /// 描 述：公共控制器
    /// </summary>
    public class UtilityController : Controller
    {
        #region 验证对象值不能重复
        #endregion

        #region 导出Excel
        /// <summary>
        /// 请选择要导出的字段页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ExcelExportForm()
        {
            return View();
        }
        /// <summary>
        /// 执行导出Excel
        /// </summary>
        /// <param name="columnJson">表头</param>
        /// <param name="rowJson">数据</param>
        /// <param name="exportField">导出字段</param>
        /// <param name="filename">文件名</param>
        /// <returns></returns>
        [ValidateInput(false)]
        public void ExecuteExportExcel(string columnJson, string rowJson, string exportField, string filename)
        {
            //设置导出格式
            ExcelConfig excelconfig = new ExcelConfig
            {
                Title = string.IsNullOrEmpty(filename) ? DateTimeHelper.GetToday("yyyyMMdd") : Server.UrlDecode(filename),
                TitleFont = "微软雅黑",
                TitlePoint = 15,
                FileName = string.IsNullOrEmpty(filename) ? DateTimeHelper.GetToday("yyyyMMddHHmmss") + ".xls" : Server.UrlDecode(filename) + ".xls",
                IsAllSizeColumn = true,
                ColumnEntity = new List<ColumnEntity>()
            };
            //表头
            List<GridColumnModel> columnData = columnJson.ToList<GridColumnModel>();
            //行数据
            DataTable rowData = rowJson.ToTable();
            //写入Excel表头
            string[] fieldInfo = exportField.Split(',');
            foreach (string item in fieldInfo)
            {
                var list = columnData.FindAll(t => t.name == item);
                foreach (GridColumnModel gridcolumnmodel in list)
                {
                    if (gridcolumnmodel.hidden.ToLower() == "false" && gridcolumnmodel.label != null)
                    {
                        string align = gridcolumnmodel.align;
                        excelconfig.ColumnEntity.Add(new ColumnEntity()
                        {
                            Column = gridcolumnmodel.name,
                            ExcelColumn = gridcolumnmodel.label,
                            //Width = gridcolumnmodel.width,
                            Alignment = gridcolumnmodel.align,
                        });
                    }
                }
            }
            ExcelHelper.ExcelDownload(rowData, excelconfig);
        }
        #endregion
    }
}

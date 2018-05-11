using System;
using System.Collections.Generic;
using System.Data;
using Lottomat.Util;

namespace Lottomat.Application.Service.OpenCodeManage
{
    /// <summary>
    /// 基础服务
    /// </summary>
    public class BaseServices
    {
        /// <summary>
        /// 装箱单个数据对象
        /// </summary>
        /// <typeparam name="T">装箱对象</typeparam>
        /// <param name="dr">装箱数据行</param>
        /// <returns></returns>
        public static T LoadData<T>(DataRow dr)
        {
            if (dr == null) return default(T);
            var t = typeof(T);
            var obj = Activator.CreateInstance(t);
            var properts = t.GetProperties();
            foreach (var pi in properts)
            {
                if (!dr.Table.Columns.Contains(pi.Name)) continue;
                pi.SetValue(obj, CommonHelper.ChangeType(dr[pi.Name], pi.PropertyType), null);
            }
            return (T)obj;
        }

        /// <summary>
        /// 装箱列表数据对象
        /// </summary>
        /// <typeparam name="T">装箱对象</typeparam>
        /// <param name="dt">装箱数据来源表格</param>
        /// <returns></returns>
        public static List<T> LoadDataList<T>(DataTable dt)
        {
            List<T> result = new List<T>();
            var t = typeof(T);
            var properts = t.GetProperties();
            object obj;
            foreach (DataRow dr in dt.Rows)
            {
                obj = Activator.CreateInstance(t);
                foreach (var pi in properts)
                {
                    if (!dt.Columns.Contains(pi.Name)) continue;
                    pi.SetValue(obj, CommonHelper.ChangeType(dr[pi.Name], pi.PropertyType), null);
                }
                result.Add((T)obj);
            }
            return result;
        }
    }
}

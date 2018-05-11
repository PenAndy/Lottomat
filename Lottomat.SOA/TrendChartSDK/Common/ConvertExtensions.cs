using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace TrendChartSDK.Common
{
    /// <summary>
    /// 类型转换扩展
    /// </summary>
    public static partial class Extensions
    {
        #region 数值转换
        /// <summary>
        /// 转换为整型
        /// </summary>
        /// <param name="data">数据</param>
        public static int ToInt(this object data)
        {
            if (data == null)
                return 0;
            int result;
            var success = int.TryParse(data.ToString(), out result);
            if (success)
                return result;
            try
            {
                return Convert.ToInt32(ToDouble(data, 0));
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// 转换为可空整型
        /// </summary>
        /// <param name="data">数据</param>
        public static int? ToIntOrNull(this object data)
        {
            if (data == null)
                return null;
            int result;
            bool isValid = int.TryParse(data.ToString(), out result);
            if (isValid)
                return result;
            return null;
        }

        /// <summary>
        /// 转换为双精度浮点数
        /// </summary>
        /// <param name="data">数据</param>
        public static double ToDouble(this object data)
        {
            if (data == null)
                return 0;
            double result;
            return double.TryParse(data.ToString(), out result) ? result : 0;
        }

        /// <summary>
        /// 转换为双精度浮点数,并按指定的小数位4舍5入
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="digits">小数位数</param>
        public static double ToDouble(this object data, int digits)
        {
            return Math.Round(ToDouble(data), digits);
        }

        /// <summary>
        /// 转换为可空双精度浮点数
        /// </summary>
        /// <param name="data">数据</param>
        public static double? ToDoubleOrNull(this object data)
        {
            if (data == null)
                return null;
            double result;
            bool isValid = double.TryParse(data.ToString(), out result);
            if (isValid)
                return result;
            return null;
        }

        /// <summary>
        /// 转换为高精度浮点数
        /// </summary>
        /// <param name="data">数据</param>
        public static decimal ToDecimal(this object data)
        {
            if (data == null)
                return 0;
            decimal result;
            return decimal.TryParse(data.ToString(), out result) ? result : 0;
        }

        /// <summary>
        /// 转换为高精度浮点数,并按指定的小数位4舍5入
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="digits">小数位数</param>
        public static decimal ToDecimal(this object data, int digits)
        {
            return Math.Round(ToDecimal(data), digits);
        }

        /// <summary>
        /// 转换为可空高精度浮点数
        /// </summary>
        /// <param name="data">数据</param>
        public static decimal? ToDecimalOrNull(this object data)
        {
            if (data == null)
                return null;
            decimal result;
            bool isValid = decimal.TryParse(data.ToString(), out result);
            if (isValid)
                return result;
            return null;
        }

        /// <summary>
        /// 转换为可空高精度浮点数,并按指定的小数位4舍5入
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="digits">小数位数</param>
        public static decimal? ToDecimalOrNull(this object data, int digits)
        {
            var result = ToDecimalOrNull(data);
            if (result == null)
                return null;
            return Math.Round(result.Value, digits);
        }

        #endregion

        #region 日期转换
        /// <summary>
        /// 转换为日期
        /// </summary>
        /// <param name="data">数据</param>
        public static DateTime ToDate(this object data)
        {
            if (data == null)
                return DateTime.MinValue;
            DateTime result;
            return DateTime.TryParse(data.ToString(), out result) ? result : DateTime.MinValue;
        }

        /// <summary>
        /// 转换为可空日期
        /// </summary>
        /// <param name="data">数据</param>
        public static DateTime? ToDateOrNull(this object data)
        {
            if (data == null)
                return null;
            DateTime result;
            bool isValid = DateTime.TryParse(data.ToString(), out result);
            if (isValid)
                return result;
            return null;
        }

        /// <summary>
        /// Json 的日期格式与.Net DateTime类型的转换
        /// </summary>
        /// <param name="jsonDate">Date(1242357713797+0800)</param>
        /// <returns></returns>
        public static DateTime JsonToDateTime(this string jsonDate)
        {
            string value = jsonDate.Substring(5, jsonDate.Length - 6) + "+0800";
            DateTimeKind kind = DateTimeKind.Utc;
            int index = value.IndexOf('+', 1);
            if (index == -1)
                index = value.IndexOf('-', 1);
            if (index != -1)
            {
                kind = DateTimeKind.Local;
                value = value.Substring(0, index);
            }
            long javaScriptTicks = long.Parse(value, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture);
            long initialJavaScriptDateTicks = (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks;
            DateTime utcDateTime = new DateTime((javaScriptTicks * 10000) + initialJavaScriptDateTicks, DateTimeKind.Utc);
            DateTime dateTime;
            switch (kind)
            {
                case DateTimeKind.Unspecified:
                    dateTime = DateTime.SpecifyKind(utcDateTime.ToLocalTime(), DateTimeKind.Unspecified);
                    break;
                case DateTimeKind.Local:
                    dateTime = utcDateTime.ToLocalTime();
                    break;
                default:
                    dateTime = utcDateTime;
                    break;
            }
            return dateTime;
        }
        #endregion

        #region 布尔转换
        /// <summary>
        /// 转换为布尔值
        /// </summary>
        /// <param name="data">数据</param>
        public static bool ToBool(this object data)
        {
            if (data == null)
                return false;
            bool? value = GetBool(data);
            if (value != null)
                return value.Value;
            bool result;
            return bool.TryParse(data.ToString(), out result) && result;
        }

        /// <summary>
        /// 获取布尔值
        /// </summary>
        private static bool? GetBool(this object data)
        {
            switch (data.ToString().Trim().ToLower())
            {
                case "0":
                    return false;
                case "1":
                    return true;
                case "是":
                    return true;
                case "否":
                    return false;
                case "yes":
                    return true;
                case "no":
                    return false;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 转换为可空布尔值
        /// </summary>
        /// <param name="data">数据</param>
        public static bool? ToBoolOrNull(this object data)
        {
            if (data == null)
                return null;
            bool? value = GetBool(data);
            if (value != null)
                return value.Value;
            bool result;
            bool isValid = bool.TryParse(data.ToString(), out result);
            if (isValid)
                return result;
            return null;
        }

        #endregion

        #region 数据类型转换扩展方法
        /// <summary>
        /// object 转换成string 包括为空的情况
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>返回值不含空格</returns>
        public static string ToStringEx(this object obj)
        {
            return obj == null ? string.Empty : obj.ToString().Trim();
        }

        /// <summary>
        /// 时间object 转换成格式化的string 包括为空的情况
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="format"></param>
        /// <returns>返回值不含空格</returns>
        public static string TryToDateTimeToString(this object obj, string format)
        {
            if (obj == null)
                return string.Empty;
            if (DateTime.TryParse(obj.ToString(), out DateTime dt))
                return dt.ToString(format);
            else
                return obj.ToString();
        }

        /// <summary>
        /// 字符转Int
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>成功:返回对应Int值;失败:返回0</returns>
        public static int TryToInt32(this object obj)
        {
            int rel = 0;

            if (!string.IsNullOrEmpty(obj.ToStringEx()))
            {
                int.TryParse(obj.ToStringEx(), out rel);
            }
            return rel;
        }

        /// <summary>
        /// 字符转Int64
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Int64 TryToInt64(this object obj)
        {
            Int64 rel = 0;
            if (!string.IsNullOrEmpty(obj.ToStringEx()))
            {
                Int64.TryParse(obj.ToStringEx(), out rel);
            }
            return rel;
        }

        /// <summary>
        /// 字符转DateTime
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>成功:返回对应Int值;失败:时间初始值</returns>
        public static DateTime TryToDateTime(this object obj)
        {
            DateTime rel = new DateTime();
            if (!string.IsNullOrEmpty(obj.ToStringEx()))
            {
                DateTime.TryParse(obj.ToStringEx(), out rel);
            }
            return rel;
        }

        /// <summary>
        /// 转换成bool类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Boolean TryToBoolean(this object obj)
        {
            Boolean rel = false;
            if (!string.IsNullOrEmpty(obj.ToStringEx()))
            {
                string s = obj.ToStringEx();

                if (s.Equals("true") || s.Equals("1") || s.Equals("是"))
                {
                    rel = true;
                }
                else
                {
                    Boolean.TryParse(obj.ToStringEx(), out rel);
                }
            }
            return rel;
        }
        #endregion

        #region 对象序列化成字节流
        /// <summary>
        /// 将对象序列化到字节流中
        /// </summary>
        /// <param name="instance">对象</param>        
        public static byte[] ToBytes(this object instance)
        {
            if (instance == null)
                return null;
            BinaryFormatter serializer = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.Serialize(stream, instance);
                stream.Position = 0;
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        /// <summary>
        /// 将字节流反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类名</typeparam>
        /// <param name="buffer">字节流</param>        
        public static T FromBytes<T>(this byte[] buffer) where T : class
        {
            if (buffer == null)
                return default(T);
            BinaryFormatter serializer = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(buffer, 0, buffer.Length);
                stream.Position = 0;
                object result = serializer.Deserialize(stream);
                if (result == null)
                    return default(T);
                return (T)result;
            }
        }

        #endregion

        #region Json操作相关
        /// <summary>
        /// 转换成对象
        /// </summary>
        /// <param name="json">json字符串</param>
        /// <returns></returns>
        public static object ToJson(this string json)
        {
            return string.IsNullOrEmpty(json) ? null : JsonConvert.DeserializeObject(json);
        }
        /// <summary>
        /// 对象序列化成Json字符串，时间默认格式化成yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter
            {
                DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
            };
            return JsonConvert.SerializeObject(obj, timeConverter);
        }
        /// <summary>
        /// 对象序列化成Json字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="datetimeformats">时间格式化字符串</param>
        /// <returns></returns>
        public static string ToJson(this object obj, string datetimeformats)
        {
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter
            {
                DateTimeFormat = datetimeformats
            };
            return JsonConvert.SerializeObject(obj, timeConverter);
        }

        /// <summary>
        /// Json字符串转换成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">json字符串</param>
        /// <returns></returns>
        public static T ToObject<T>(this string json)
        {
            return string.IsNullOrEmpty(json) ? default(T) : JsonConvert.DeserializeObject<T>(json);
        }
        /// <summary>
        /// Json字符串转换成List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">Json字符串</param>
        /// <returns></returns>
        public static List<T> ToList<T>(this string json)
        {
            return string.IsNullOrEmpty(json) ? null : JsonConvert.DeserializeObject<List<T>>(json);
        }
        /// <summary>
        /// Json字符串转换成DataTable
        /// </summary>
        /// <param name="json">Json字符串</param>
        /// <returns></returns>
        public static DataTable ToTable(this string json)
        {
            return string.IsNullOrEmpty(json) ? null : JsonConvert.DeserializeObject<DataTable>(json);
        }
        /// <summary>
        /// Json字符串转换成DataTable
        /// </summary>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static DataTable JsonToDataTable(this string strJson)
        {
            DataTable tb = null;
            //获取数据  
            Regex rg = new Regex(@"(?<={)[^}]+(?=})");
            MatchCollection mc = rg.Matches(strJson);
            for (int i = 0; i < mc.Count; i++)
            {
                string strRow = mc[i].Value;
                string[] strRows = strRow.Split(',');
                //创建表  
                if (tb == null)
                {
                    tb = new DataTable
                    {
                        TableName = "Table"
                    };
                    foreach (string str in strRows)
                    {
                        DataColumn dc = new DataColumn();
                        string[] strCell = str.Split(':');
                        dc.DataType = typeof(String);
                        dc.ColumnName = strCell[0].ToString().Replace("\"", "").Trim();
                        tb.Columns.Add(dc);
                    }
                    tb.AcceptChanges();
                }
                //增加内容  
                DataRow dr = tb.NewRow();
                for (int r = 0; r < strRows.Length; r++)
                {
                    object strText = strRows[r].Split(':')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("/", "").Replace("\"", "").Trim();
                    if (strText.ToString().Length >= 5)
                    {
                        if (strText.ToString().Substring(0, 5) == "Date(")//判断是否JSON日期格式
                        {
                            strText = strText.ToString().JsonToDateTime().ToString("yyyy-MM-dd HH:mm:ss");
                        }
                    }
                    dr[r] = strText;
                }
                tb.Rows.Add(dr);
                tb.AcceptChanges();
            }
            return tb;
        }
        /// <summary>
        /// Json字符串转换成JObject
        /// </summary>
        /// <param name="json">Json字符串</param>
        /// <returns></returns>
        public static JObject ToJObject(this string json)
        {
            return string.IsNullOrEmpty(json) ? null : JObject.Parse(json.Replace("&nbsp;", ""));
        }
        #endregion

        #region 将对象属性转换为Key-Value键值对
        /// <summary>
        /// 将对象属性转换为Key-Value键值对
        /// </summary>  
        /// <param name="o"></param>  
        /// <returns></returns>  
        public static Dictionary<string, object> Object2Dictionary(this object o)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();

            Type t = o.GetType();

            PropertyInfo[] pi = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo p in pi)
            {
                MethodInfo mi = p.GetGetMethod();

                if (mi != null && mi.IsPublic)
                {
                    map.Add(p.Name, mi.Invoke(o, new object[] { }));
                }
            }
            return map;
        }
        #endregion

        #region 合并序列、数组去重
        /// <summary>
        /// 合并两个序列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a">原数组</param>
        /// <param name="b">需要合并的数组</param>
        /// <returns></returns>
        public static T[] ConcatArray<T>(this T[] a, T[] b)
        {
            return a.Concat(b).ToArray();
        }

        /// <summary>
        /// 数组去掉重复的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">要处理的数组</param>
        /// <returns></returns>
        public static T[] DeleteRepeatData<T>(this T[] array)
        {
            return array.GroupBy(p => p).Select(p => p.Key).ToArray();
        }
        #endregion

        #region 补足位数
        /// <summary>
        /// 指定字符串的固定长度，如果字符串小于固定长度，
        /// 则在字符串的前面补足零，可设置的固定长度最大为9位
        /// </summary>
        /// <param name="text">原始字符串</param>
        /// <param name="limitedLength">字符串的固定长度</param>
        public static string RepairZero(this string text, int limitedLength)
        {
            //补足0的字符串
            string temp = "";

            //补足0
            for (int i = 0; i < limitedLength - text.Length; i++)
            {
                temp += "0";
            }

            //连接text
            temp += text;

            //返回补足0的字符串
            return temp;
        }

        /// <summary>
        /// 小时、分钟、秒小于10补足0
        /// </summary>
        /// <param name="text">原始字符串</param>
        /// <returns></returns>
        public static string RepairZero(this int text)
        {
            string res = string.Empty;
            if (text >= 0 && text < 10)
            {
                res += "0" + text;
            }
            else
            {
                res = text.ToString();
            }
            return res;
        }

        #endregion

        #region 判断指定类型是否为数字类型
        /// <summary>
        /// 判断指定类型是否为数字类型
        /// </summary>
        /// <param name="t">要检查的类型</param>
        /// <returns>是否是数字类型</returns>
        public static bool IsNumeric(this Type t)
        {
            return t == typeof(Byte)
                   || t == typeof(Int16)
                   || t == typeof(Int32)
                   || t == typeof(Int64)
                   || t == typeof(SByte)
                   || t == typeof(UInt16)
                   || t == typeof(UInt32)
                   || t == typeof(UInt64)
                   || t == typeof(Decimal)
                   || t == typeof(Double)
                   || t == typeof(Single);
        }
        #endregion
        
    }
}

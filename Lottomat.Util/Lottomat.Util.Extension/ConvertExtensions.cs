using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Lottomat.Util.Extension.QueryableEx;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Formatting = Newtonsoft.Json.Formatting;

namespace Lottomat.Util.Extension
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

        #region 数据源转换

        #region DataTable转换成List集合
        /// <summary>
        /// DataTable转换成List集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> DataTableToList<T>(this DataTable dt) where T : new()
        {
            // 定义集合    
            IList<T> ts = new List<T>();

            // 获得此模型的类型   
            Type type = typeof(T);

            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                // 获得此模型的公共属性      
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    string tempName = pi.Name;

                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter      
                        if (!pi.CanWrite) continue;

                        object value = dr[tempName];
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                ts.Add(t);
            }
            return ts.CastTo<List<T>>();
        }
        #endregion

        #region DataTable转换成对象
        /// <summary>
        /// DataTable转换成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static T DataTableToObject<T>(this DataTable dt) where T : new()
        {
            T t = new T();
            foreach (DataRow dr in dt.Rows)
            {
                // 获得此模型的公共属性      
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    string tempName = pi.Name;

                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter      
                        if (!pi.CanWrite) continue;

                        object value = dr[tempName];
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
            }
            return t.CastTo<T>();
        } 
        #endregion

        #region List转换DataTable
        /// <summary>
        /// 将泛类型集合List类转换成DataTable
        /// </summary>
        /// <param name="entitys">泛类型集合</param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T>(this List<T> entitys)
        {
            //检查实体集合不能为空
            if (entitys == null || entitys.Count < 1)
            {
                throw new Exception("需转换的集合为空");
            }
            //取出第一个实体的所有Propertie
            Type entityType = entitys[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();

            //生成DataTable的structure
            //生产代码中，应将生成的DataTable结构Cache起来，此处略
            DataTable dt = new DataTable();
            for (int i = 0; i < entityProperties.Length; i++)
            {
                //dt.Columns.Add(entityProperties[i].Name, entityProperties[i].PropertyType);
                dt.Columns.Add(entityProperties[i].Name);
            }
            //将所有entity添加到DataTable中
            foreach (object entity in entitys)
            {
                //检查所有的的实体都为同一类型
                if (entity.GetType() != entityType)
                {
                    throw new Exception("要转换的集合元素类型不一致");
                }
                object[] entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    entityValues[i] = entityProperties[i].GetValue(entity, null);
                }
                dt.Rows.Add(entityValues);
            }
            return dt;
        }
        #endregion

        #region IList转成List<T>
        /// <summary>
        /// IList转成List<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<T> IListToList<T>(this IList list)
        {
            T[] array = new T[list.Count];
            list.CopyTo(array, 0);
            return new List<T>(array);
        }
        #endregion

        #region DataTable根据条件过滤表的内容
        /// <summary>
        /// 根据条件过滤表的内容
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static DataTable DataFilter(this DataTable dt, string condition)
        {
            if (IsExistRows(dt))
            {
                if (condition.Trim() == "")
                {
                    return dt;
                }
                else
                {
                    DataTable newdt = new DataTable();
                    newdt = dt.Clone();
                    DataRow[] dr = dt.Select(condition);
                    for (int i = 0; i < dr.Length; i++)
                    {
                        newdt.ImportRow((DataRow)dr[i]);
                    }
                    return newdt;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据条件过滤表的内容
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序字段</param>
        /// <returns></returns>
        public static DataTable DataFilter(this DataTable dt, string condition, string sort)
        {
            if (IsExistRows(dt))
            {
                DataTable newdt = dt.Clone();
                DataRow[] dr = dt.Select(condition, sort);
                for (int i = 0; i < dr.Length; i++)
                {
                    newdt.ImportRow((DataRow)dr[i]);
                }
                return newdt;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 检查DataTable 是否有数据行
        /// <summary>
        /// 检查DataTable 是否有数据行
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static bool IsExistRows(this DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
                return true;

            return false;
        }
        #endregion

        #region DataTable 转 DataTableToHashtable
        /// <summary>
        /// DataTable 转 DataTableToHashtable
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static Hashtable DataTableToHashtable(this DataTable dt)
        {
            Hashtable ht = new Hashtable();
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string key = dt.Columns[i].ColumnName;
                    ht[key] = dr[key];
                }
            }
            return ht;
        }
        #endregion

        #region DataTable/DataSet 转 XML
        /// <summary>
        /// DataTable 转 XML
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableToXml(this DataTable dt)
        {
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    System.IO.StringWriter writer = new System.IO.StringWriter();
                    dt.WriteXml(writer);
                    return writer.ToString();
                }
            }
            return String.Empty;
        }

        /// <summary>
        /// DataSet 转 XML
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static string DataSetToXml(this DataSet ds)
        {
            if (ds != null)
            {
                System.IO.StringWriter writer = new System.IO.StringWriter();
                ds.WriteXml(writer);
                return writer.ToString();
            }
            return String.Empty;
        }
        #endregion

        #region DataRow  转  HashTable
        /// <summary>
        /// DataRow  转  HashTable
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static Hashtable DataRowToHashTable(this DataRow dr)
        {
            Hashtable htReturn = new Hashtable(dr.ItemArray.Length);
            foreach (DataColumn dc in dr.Table.Columns)
                htReturn.Add(dc.ColumnName, dr[dc.ColumnName]);
            return htReturn;
        }
        #endregion

        #region 使用指定字符集将string转换成byte[]
        /// <summary>
        /// 使用指定字符集将string转换成byte[]
        /// </summary>
        /// <param name="text">要转换的字符串</param>
        /// <param name="encoding">字符编码</param>
        public static byte[] StringToBytes(this string text, Encoding encoding)
        {
            return encoding.GetBytes(text);
        }
        #endregion

        #region 使用指定字符集将byte[]转换成string
        /// <summary>
        /// 使用指定字符集将byte[]转换成string
        /// </summary>
        /// <param name="bytes">要转换的字节数组</param>
        /// <param name="encoding">字符编码</param>
        public static string BytesToString(this byte[] bytes, Encoding encoding)
        {
            return encoding.GetString(bytes);
        }
        #endregion

        #region 将byte[]转换成int
        /// <summary>
        /// 将byte[]转换成int
        /// </summary>
        /// <param name="data">需要转换成整数的byte数组</param>
        public static int BytesToInt32(this byte[] data)
        {
            //如果传入的字节数组长度小于4,则返回0
            if (data.Length < 4)
            {
                return 0;
            }

            //定义要返回的整数
            int num = 0;

            //如果传入的字节数组长度大于4,需要进行处理
            if (data.Length >= 4)
            {
                //创建一个临时缓冲区
                byte[] tempBuffer = new byte[4];

                //将传入的字节数组的前4个字节复制到临时缓冲区
                Buffer.BlockCopy(data, 0, tempBuffer, 0, 4);

                //将临时缓冲区的值转换成整数，并赋给num
                num = BitConverter.ToInt32(tempBuffer, 0);
            }

            //返回整数
            return num;
        }
        #endregion

        #region 将XML内容转换成目标对象实体集合
        /// <summary>
        /// 将XML内容转换成目标对象实体集合
        /// </summary>
        /// <typeparam name="T">目标对象实体</typeparam>
        /// <param name="fileName">完整文件名(根目录下只需文件名称)</param>
        /// <param name="wrapperNodeName"></param>
        /// <returns></returns>
        public static List<T> ConvertXMLToObject<T>(this string fileName, string wrapperNodeName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            List<T> result = new List<T>();
            var TType = typeof(T);
            XmlNodeList nodeList = doc.ChildNodes;
            if (!string.IsNullOrEmpty(wrapperNodeName))
            {
                foreach (XmlNode node in doc.ChildNodes)
                {
                    if (node.Name == wrapperNodeName)
                    {
                        nodeList = node.ChildNodes;
                        break;
                    }
                }
            }
            object oneT = null;
            foreach (XmlNode node in nodeList)
            {
                if (node.NodeType == XmlNodeType.Comment || node.NodeType == XmlNodeType.XmlDeclaration) continue;
                oneT = TType.Assembly.CreateInstance(TType.FullName);
                foreach (XmlNode item in node.ChildNodes)
                {
                    if (item.NodeType == XmlNodeType.Comment) continue;
                    var property = TType.GetProperty(item.Name);
                    if (property != null)
                        property.SetValue(oneT, Convert.ChangeType(item.InnerText, property.PropertyType), null);
                }
                result.Add((T)oneT);
            }
            return result;
        } 
        #endregion

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
        /// 对象序列化成Json字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="datetimeformats">时间格式化字符串</param>
        /// <param name="isIgnoreNullValue">是否忽略Null</param>
        /// <returns></returns>
        public static string ToJson(this object obj, string datetimeformats, bool isIgnoreNullValue = false)
        {
            JsonSerializerSettings jsetting = new JsonSerializerSettings();

            if (isIgnoreNullValue)
            {
                JsonConvert.DefaultSettings = () =>
                {
                    //日期类型默认格式化处理
                    jsetting.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
                    jsetting.DateFormatString = datetimeformats;

                    //空值处理,忽略值为NULL的属性
                    jsetting.NullValueHandling = NullValueHandling.Ignore;

                    return jsetting;
                };
            }
            else
            {
                JsonConvert.DefaultSettings = () =>
                {
                    //日期类型默认格式化处理
                    jsetting.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
                    jsetting.DateFormatString = datetimeformats;

                    //空值处理,不忽略值为NULL的属性
                    jsetting.NullValueHandling = NullValueHandling.Include;

                    return jsetting;
                };
            }

            string res = JsonConvert.SerializeObject(obj, Formatting.Indented, jsetting);

            return res;
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

        #region 把对象类型转换成指定的类型，转化失败时返回指定默认值
        /// <summary>
        /// 把对象类型转换成指定的类型，转化失败时返回指定默认值
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="value">要转换的原对象</param>
        /// <param name="detaultValue">转换失败时返回的默认值</param>
        /// <returns>转化后指定类型对象，转化失败时返回指定默认值</returns>
        public static T CastTo<T>(this object value, T detaultValue)
        {
            object result;
            Type t = typeof(T);
            try
            {
                result = t.IsEnum ? System.Enum.Parse(t, value.ToString()) : Convert.ChangeType(value, t);

            }
            catch (Exception)
            {
                return detaultValue;
            }

            return (T)result;
        }
        #endregion

        #region 把对象类型转换成指定的类型，转化失败时返回类型默认值
        /// <summary>
        /// 把对象类型转换成指定的类型，转化失败时返回类型默认值
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="value">要转换的原对象</param>
        /// <returns>转化后指定类型对象，转化失败时返回类型默认值</returns>
        public static T CastTo<T>(this object value)
        {
            object result;
            Type t = typeof(T);
            try
            {
                if (t.IsEnum)
                {
                    result = System.Enum.Parse(t, value.ToString());
                }
                else if (t == typeof(Guid))
                {
                    result = Guid.Parse(value.ToString());
                }
                else
                {
                    result = Convert.ChangeType(value, t);
                }

            }
            catch (Exception)
            {
                result = default(T);
            }

            return (T)result;
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

        #region 各进制数间转换
        /// <summary>
        /// 实现各进制数间的转换。ConvertBase("15",10,16)表示将十进制数15转换为16进制的数。
        /// </summary>
        /// <param name="value">要转换的值,即原值</param>
        /// <param name="from">原值的进制,只能是2,8,10,16四个值。</param>
        /// <param name="to">要转换到的目标进制，只能是2,8,10,16四个值。</param>
        public static string ConvertBase(this string value, int from, int to)
        {
            try
            {
                int intValue = Convert.ToInt32(value, from);  //先转成10进制
                string result = Convert.ToString(intValue, to);  //再转成目标进制
                if (to == 2)
                {
                    int resultLength = result.Length;  //获取二进制的长度
                    switch (resultLength)
                    {
                        case 7:
                            result = "0" + result;
                            break;
                        case 6:
                            result = "00" + result;
                            break;
                        case 5:
                            result = "000" + result;
                            break;
                        case 4:
                            result = "0000" + result;
                            break;
                        case 3:
                            result = "00000" + result;
                            break;
                    }
                }
                return result;
            }
            catch
            {

                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
                return "0";
            }
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

        #region 检验参数合法性，数值类型不小于0，引用类型不能为null，否则抛出异常
        /// <summary>
        /// 检验参数合法性，数值类型不小于0，引用类型不能为null，否则抛出异常
        /// </summary>
        /// <param name="arg">待检参数</param>
        /// <param name="argName">待检参数名称</param>
        /// <param name="canZero">数值类型是否可以为0</param>
        public static bool CheckArgument(this object arg, string argName, bool canZero = false)
        {
            try
            {
                if (arg == null)
                {
                    ArgumentNullException argumentNullException = new ArgumentNullException(argName);
                    throw new Exception(String.Format("参数{0}为空，引发异常", argName), argumentNullException);
                }

                Type t = arg.GetType();
                if (t.IsValueType && t.IsNumeric())
                {
                    bool flag = !canZero ? arg.CastTo(0.0) <= 0.0 : arg.CastTo(0.0) < 0.0;
                    if (flag)
                    {
                        ArgumentOutOfRangeException argumentOutOfRangeException = new ArgumentOutOfRangeException(argName);
                        throw new Exception(String.Format("参数{0}不在有效范围内，引发异常", argName), argumentOutOfRangeException);
                    }
                }
                if (t == typeof(Guid) && (Guid)arg == Guid.Empty)
                {
                    ArgumentNullException argumentNullException1 = new ArgumentNullException(argName);
                    throw new Exception(String.Format("参数{0}为空引发GUID异常", argName), argumentNullException1);
                }

                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }
        #endregion

        #region 从类型成员获取指定的Attribute T特性集合
        /// <summary>
        /// 从类型成员获取指定的Attribute T特性集合
        /// </summary>
        /// <typeparam name="T">Attribute特性类型集合</typeparam>
        /// <param name="memberinfo">实现了ICustomAttributeProvider接口的类实例</param>
        /// <param name="inherit">是否从继承中查找，默认不查找</param>
        /// <returns>存在则返回第一个，不存在返回null</returns>
        public static T GetAttribute<T>(this ICustomAttributeProvider memberinfo, bool inherit = false) where T : Attribute
        {
            return memberinfo.GetCustomAttributes(typeof(T), inherit).SingleOrDefault() as T;
        }

        /// <summary>
        /// 从类型成员获取指定的Attribute T特性集合
        /// </summary>
        /// <typeparam name="T[]">Attribute特性类型</typeparam>
        /// <param name="memberinfo">实现了ICustomAttributeProvider接口的类实例</param>
        /// <param name="inherit">是否从继承中查找，默认不查找</param>
        /// <returns>存在则返回所有特性集合，不存在返回null</returns>
        public static T[] GetAttributes<T>(this ICustomAttributeProvider memberinfo, bool inherit = false) where T : Attribute
        {
            return memberinfo.GetCustomAttributes(typeof(T), inherit).Cast<T>().ToArray();
        }

        #endregion

        #region 获取是否在此成员上定义一个或多个 attributeType 的实例
        /// <summary>
        /// 获取是否在此成员上定义一个或多个 attributeType 的实例
        /// </summary>
        /// <typeparam name="T">要检查的Attribute特性类</typeparam>
        /// <param name="memberinfo">要检查的类成员</param>
        /// <param name="inherit">是否从继承中查找，默认不查找</param>
        /// <returns>是否存在</returns>
        public static bool AttributeExists<T>(this ICustomAttributeProvider memberinfo, bool inherit = false)
            where T : Attribute
        {
            return memberinfo.IsDefined(typeof(T), inherit);
        }
        #endregion

        #region 得到成员元数据的Description特性描述信息
        /// <summary>
        /// 得到成员元数据的Description特性描述信息
        /// </summary>
        /// <param name="memberinfo">成员元数据对象</param>
        /// <param name="inherit">是否从继承中查找，默认不查找</param>
        /// <returns>存在则返回Description的描述信息，否则返回空</returns>
        public static string GetDescription(this ICustomAttributeProvider memberinfo, bool inherit = false)
        {
            DescriptionAttribute desc = memberinfo.GetAttribute<DescriptionAttribute>(inherit);
            return desc == null ? String.Empty : desc.Description;
        }
        #endregion

        #region 查询扩展方法

        #region IQueryable<T>的扩展方法

        #region 根据第三方条件是否为真是否执行指定条件的查询
        /// <summary>
        /// 根据第三方条件是否为真是否执行指定条件的查询
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="source">要查询的数据源</param>
        /// <param name="where">条件</param>
        /// <param name="condition">第三方条件</param>
        /// <returns>查询的结果</returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Expression<Func<T, bool>> where,
            bool condition)
        {
            if (where.CheckArgument("where"))
            {
                return condition ? source.Where(where) : source;
            }
            return null;
        }
        #endregion

        #region 把IQueryable<T>集合按照指定的属性与排序方式进行排序
        /// <summary>
        /// 把IQueryable集合按照指定的属性与排序方式进行排序
        /// </summary>
        /// <typeparam name="T">数据集类型</typeparam>
        /// <param name="source">要排序的数据集</param>
        /// <param name="propName">要排序的属性名称</param>
        /// <param name="listSort">排序方式</param>
        /// <returns>返回排序后的结果集</returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propName,
            ListSortDirection listSort = ListSortDirection.Ascending)
        {
            if (propName.CheckArgument("propName"))
            {
                return QueryableHelper<T>.OrderBy(source, propName, listSort);
            }
            return null;
        }
        #endregion

        #region 把IQueryable<T>集合按照指定的属性与排序方式进行排序
        /// <summary>
        /// 把IQueryable集合按照指定的属性与排序方式进行排序
        /// </summary>
        /// <typeparam name="T">数据集类型</typeparam>
        /// <param name="source">要排序的数据集</param>
        /// <param name="sortCondition">排序条件</param>
        /// <returns>返回排序后的结果集</returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, PropertySortCondition sortCondition)
        {
            if (sortCondition.CheckArgument("sortCondition"))
            {
                return source.OrderBy(sortCondition.PropertyName, sortCondition.ListSortDirection);
            }
            return null;
        }
        #endregion

        #region 把IOrderedQueryable<T>集合按照指定的属性与排序方式进行再排序
        /// <summary>
        /// 把IOrderedQueryable集合按照指定的属性与排序方式进行排序
        /// </summary>
        /// <typeparam name="T">数据集类型</typeparam>
        /// <param name="source">要排序的数据集</param>
        /// <param name="propName">要排序的属性名称</param>
        /// <param name="listSort">排序方式</param>
        /// <returns>返回排序后的结果集</returns>
        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string propName,
            ListSortDirection listSort = ListSortDirection.Ascending)
        {
            if (propName.CheckArgument("propName"))
            {
                return QueryableHelper<T>.ThenBy(source, propName, listSort);
            }
            return null;
        }
        #endregion

        #region 把IOrderedQueryable<T>集合按照指定的属性与排序方式进行再排序
        /// <summary>
        /// 把IOrderedQueryable集合按照指定的属性与排序方式进行排序
        /// </summary>
        /// <typeparam name="T">数据集类型</typeparam>
        /// <param name="source">要排序的数据集</param>
        /// <param name="sortCondition">排序条件</param>
        /// <returns>返回排序后的结果集</returns>
        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, PropertySortCondition sortCondition)
        {
            if (sortCondition.CheckArgument("sortCondition"))
            {
                return QueryableHelper<T>.ThenBy(source, sortCondition.PropertyName, sortCondition.ListSortDirection);
            }
            return null;
        }
        #endregion

        #region 多条件排序分页查询
        /// <summary>
        /// 把IQueryable集合按照指定的属性与排序方式进行排序后，再按照指定的条件提取指定页码指定条目数据
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="source">要排序的数据集</param>
        /// <param name="where">检索条件</param>
        /// <param name="pageIndex">索引</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="total">总页数</param>
        /// <param name="sortConditions">排序条件</param>
        /// <returns>子集</returns>
        public static IQueryable<T> WhereEx<T>(this IQueryable<T> source, Expression<Func<T, bool>> where, int pageIndex,
            int pageSize, out int total, params PropertySortCondition[] sortConditions) where T : class, new()
        {
            IQueryable<T> temp = null;

            int i = 0;
            if (source.CheckArgument("source")
                    && where.CheckArgument("where")
                    && pageIndex.CheckArgument("pageIndex")
                    && pageSize.CheckArgument("pageSize")
                    && sortConditions.CheckArgument("sortConditions"))
            {
                //判断是不是首个排序条件
                int count = 0;
                //得到满足条件的总记录数
                i = source.Count(where);
                //对数据源进行排序
                IOrderedQueryable<T> orderSource = null;
                foreach (PropertySortCondition sortCondition in sortConditions)
                {
                    orderSource = count == 0
                        ? source.OrderBy(sortCondition.PropertyName, sortCondition.ListSortDirection)
                        : orderSource.ThenBy(sortCondition.PropertyName, sortCondition.ListSortDirection);
                    count++;
                }
                source = orderSource;

                temp = source != null
                    ? source.Where(where).Skip((pageIndex - 1) * pageSize).Skip(pageSize)
                    : Enumerable.Empty<T>().AsQueryable();
            }

            total = i;

            return temp;
        }
        #endregion

        #region 多条件排序查询
        /// <summary>
        /// 多条件排序查询
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="source">要排序的数据集</param>
        /// <param name="where">检索条件</param>
        /// <param name="sortConditions">排序条件</param>
        /// <returns>子集</returns>
        public static IQueryable<T> WhereEx<T>(this IQueryable<T> source, Expression<Func<T, bool>> where, params PropertySortCondition[] sortConditions) where T : class, new()
        {
            IQueryable<T> temp = null;

            if (source.CheckArgument("source") && where.CheckArgument("where") && sortConditions.CheckArgument("sortConditions"))
            {
                //判断是不是首个排序条件
                int count = 0;
                //对数据源进行排序
                IOrderedQueryable<T> orderSource = null;
                foreach (PropertySortCondition sortCondition in sortConditions)
                {
                    orderSource = count == 0
                        ? source.OrderBy(sortCondition.PropertyName, sortCondition.ListSortDirection)
                        : orderSource.ThenBy(sortCondition.PropertyName, sortCondition.ListSortDirection);
                    count++;
                }
                source = orderSource;

                temp = source != null ? source.Where(where) : Enumerable.Empty<T>().AsQueryable();
            }
            return temp;
        }
        #endregion

        #endregion

        #region IEnumerable<T>的扩展方法

        #region 将集合展开分别转换成字符串，再以指定分隔字符串链接，拼成一个字符串返回
        /// <summary>
        /// 将集合展开分别转换成字符串，再以指定分隔字符串链接，拼成一个字符串返回
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="collection">要处理的集合</param>
        /// <param name="separator">分隔符</param>
        /// <returns>拼接后的字符串</returns>
        public static string ExpandAndToString<T>(this IEnumerable<T> collection, string separator)
        {
            List<T> source = collection as List<T> ?? collection.ToList();
            if (source.IsEmpty())
            {
                return "";
            }

            string result = source.Cast<object>()
                 .Aggregate<object, string>(null, (current, o) => current + string.Format("{0}{1}", o, separator));

            return result.Substring(0, result.LastIndexOf(separator, StringComparison.Ordinal));
        }
        #endregion

        #region 根据指定条件返回集合中不重复的元素
        /// <summary>
        /// 根据指定条件返回集合中不重复的元素
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <typeparam name="TKey">动态筛选条件类型</typeparam>
        /// <param name="source">要操作的数据源</param>
        /// <param name="keySelector">重复数据的筛选条件</param>
        /// <returns>不重复元素的集合</returns>
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
        {
            //取分组集合中每组的第一条数据
            return source.GroupBy(keySelector).Select(group => group.First());
        }
        #endregion

        #region 根据第三方条件是否为真是否执行指定条件的查询
        /// <summary>
        /// 根据第三方条件是否为真是否执行指定条件的查询
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="source">要查询的数据源</param>
        /// <param name="where">条件</param>
        /// <param name="condition">第三方条件</param>
        /// <returns>查询的结果</returns>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<T, bool> where, bool condition)
        {
            return condition ? source.Where(where) : source;
        }
        #endregion

        #endregion

        #region 其他
        /// <summary>
        /// 扩展Between 操作符
        /// 使用 var query = db.People.Between(person => person.Age, 18, 21);
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source">当前值</param>
        /// <param name="keySelector">拉姆达表达式</param>
        /// <param name="low">低</param>
        /// <param name="high">高</param>
        /// <returns></returns>
        public static IQueryable<TSource> Between<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, TKey low, TKey high) where TKey : IComparable<TKey>
        {
            Expression key = Expression.Invoke(keySelector, keySelector.Parameters.ToArray());

            Expression lowerBound = Expression.GreaterThanOrEqual(key, Expression.Constant(low));

            Expression upperBound = Expression.LessThanOrEqual(key, Expression.Constant(high));

            Expression and = Expression.AndAlso(lowerBound, upperBound);

            Expression<Func<TSource, bool>> lambda = Expression.Lambda<Func<TSource, bool>>(and, keySelector.Parameters);

            return source.Where(lambda);
        }

        /// <summary>
        /// In
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool In<T>(this T value, params T[] values)
        {
            return values.Contains(value);
        }

        /// <summary>
        /// Between
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="i"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static bool Between<T>(this T i, T start, T end) where T : IComparable<T>
        {
            return i.CompareTo(start) >= 0 && i.CompareTo(end) <= 0;
        }

        /// <summary>
        /// And
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool And(this bool left, bool right)
        {
            return left && right;
        }

        /// <summary>
        /// Or
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Or(this bool left, bool right)
        {
            return left || right;
        }

        #endregion

        #endregion
    }
}

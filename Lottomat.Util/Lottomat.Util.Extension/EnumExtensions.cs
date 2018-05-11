using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Lottomat.Util.Extension
{
    /// <summary>
    /// 枚举扩展方法
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// 缓存
        /// </summary>
        private static Dictionary<string, string> _dictionaryCache = new Dictionary<string, string>();
        
        /// <summary>
        /// 返回枚举项的描述信息。
        /// </summary>
        /// <param name="value">要获取描述信息的枚举项。</param>
        /// <returns>枚举想的描述信息。</returns>
        public static string GetEnumDescription(this Enum value)
        {
            Type enumType = value.GetType();
            // 获取枚举常数名称。
            string name = Enum.GetName(enumType, value);
            if (name != null)
            {
                // 获取枚举字段。
                FieldInfo fieldInfo = enumType.GetField(name);
                if (fieldInfo != null)
                {
                    // 获取描述的属性。
                    DescriptionAttribute attr = Attribute.GetCustomAttribute(fieldInfo,
                        typeof(DescriptionAttribute), false) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 获取枚举描述信息
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static string GetEnumText(this System.Enum en)
        {
            string enString = string.Empty;
            if (null == en)
                return enString;

            string key = en.ToString() + "__KEY__";

            if (_dictionaryCache.ContainsKey(key))
            {
                enString = _dictionaryCache[key];
            }
            else
            {
                string tableName = en.ToString();
                var fieldInfo = en.GetType().GetField(tableName);
                var attributes = (TextAttribute[])fieldInfo.GetCustomAttributes(typeof(TextAttribute), false);
                if (attributes.Length > 0)
                {
                    enString = attributes[0].Value;
                    if (!_dictionaryCache.ContainsKey(key))
                    {
                        _dictionaryCache.Add(key, enString);
                    }
                    else
                    {
                        _dictionaryCache[key] = enString;
                    }
                }
            }
            return enString;
        }

        /// <summary>
        /// 获取彩种对应数据库表名称
        /// </summary>
        /// <param name="value">彩种枚举类型</param>
        /// <returns></returns>
        public static string GetSCCLotteryTableName(this Enum value)
        {
            if (value == null)
            {
                throw new ArgumentException("value");
            }

            string tableName = String.Empty;

            var fieldInfo = value.GetType().GetField(value.ToString());
            var attributes = (TableNameAttribute[])fieldInfo.GetCustomAttributes(typeof(TableNameAttribute), false);
            if (attributes.Length > 0)
            {
                tableName = attributes[0].TableName;
            }

            //string key = "__" + tableName.ToUpper() + "__CACHE__KEY__";
            //if (!_dictionaryCache.ContainsKey(key))
            //{
            //    var fieldInfo = value.GetType().GetField(tableName);
            //    var attributes = (TableNameAttribute[])fieldInfo.GetCustomAttributes(typeof(TableNameAttribute), false);
            //    if (attributes.Length > 0)
            //    {
            //        tableName = attributes[0].TableName;
            //    }
            //    _dictionaryCache.Add(key, tableName);
            //}
            //else
            //{
            //    tableName = _dictionaryCache[key] ?? tableName;
            //}
            return tableName;
        }

        /// <summary>
        /// 获取彩种对应编码
        /// </summary>
        /// <param name="value">彩种枚举类型</param>
        /// <returns></returns>
        public static string GetLotteryCode(this Enum value)
        {
            if (value == null)
            {
                throw new ArgumentException("value");
            }
            string code = value.ToString();
            var fieldInfo = value.GetType().GetField(code);

            var attributes = (LotteryCodeAttribute[])fieldInfo.GetCustomAttributes(typeof(LotteryCodeAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                code = attributes[0].Code;
            }
            return code;
        }

        /// <summary>
        /// 遍历枚举对象的所有元素
        /// </summary>
        /// <typeparam name="T">枚举对象</typeparam>
        /// <returns>Dictionary：枚举值-描述</returns>
        public static Dictionary<int, string> GetEnumValues<T>()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            foreach (var code in System.Enum.GetValues(typeof(T)))
            {
                ////获取名称
                //string strName = System.Enum.GetName(typeof(T), code);

                object[] objAttrs = code.GetType().GetField(code.ToString()).GetCustomAttributes(typeof(TextAttribute), true);
                if (objAttrs.Length > 0)
                {
                    TextAttribute descAttr = objAttrs[0] as TextAttribute;
                    if (!dictionary.ContainsKey((int)code))
                    {
                        if (descAttr != null)
                            dictionary.Add((int)code, descAttr.Value);
                    }
                    //Console.WriteLine(string.Format("[{0}]", descAttr.Value));
                }
                //Console.WriteLine(string.Format("{0}={1}", code.ToString(), Convert.ToInt32(code)));
            }
            return dictionary;
        }
    }

    /// <summary>
    /// 自定义描述
    /// </summary>
    public class TextAttribute : Attribute
    {
        /// <summary>
        /// 描述信息
        /// </summary>
        public string Value { get; private set; }

        public TextAttribute(string value)
        {
            this.Value = value;
        }
    }

    /// <summary>
    /// 彩种表名称特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public sealed class TableNameAttribute : Attribute
    {
        private string _tablename;
        public string TableName { get { return _tablename; } }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="TableName">表名称</param>
        public TableNameAttribute(string TableName)
        {
            this._tablename = TableName;
        }
    }

    /// <summary>
    /// 彩种编码特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public sealed class LotteryCodeAttribute : Attribute
    {
        private string _code;
        public string Code { get { return _code; } }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="TableName">表名称</param>
        public LotteryCodeAttribute(string Code)
        {
            this._code = Code;
        }
    }
}
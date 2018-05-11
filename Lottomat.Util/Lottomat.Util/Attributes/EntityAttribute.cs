using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace Lottomat.Util.Attributes
{
    /// <summary>
    /// 获取实体类Attribute自定义属性
    /// </summary>
    public class EntityAttribute
    {
        /// <summary>
        ///  获取实体对象Key
        /// </summary>
        /// <returns></returns>
        public static string GetEntityKey<T>()
        {
            Type type = typeof(T);
            PropertyInfo[] props = type.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                foreach (System.Attribute attr in prop.GetCustomAttributes(true))
                {
                    KeyAttribute keyattribute = attr as KeyAttribute;
                    if (keyattribute != null)
                    {
                        return prop.Name;
                    }
                }
            }
            return null;
        }

        /// <summary>
        ///  获取实体对象表名
        /// </summary>
        /// <returns></returns>
        public static string GetEntityTable<T>()
        {
            Type objTye = typeof(T);
            string entityName = "";
            var tableAttribute = objTye.GetCustomAttributes(true).OfType<TableAttribute>();
            var descriptionAttributes = tableAttribute as TableAttribute[] ?? tableAttribute.ToArray();

            entityName = descriptionAttributes.Any() ? descriptionAttributes.ToList()[0].Name : objTye.Name;
            return entityName;
        }
    }
}

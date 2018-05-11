using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Lottomat.Utils.Date;

namespace Lottomat.Util
{
    /// <summary>
    /// 常用公共类
    /// </summary>
    public sealed class CommonHelper
    {
        #region 获取全局唯一值Guid
        /// <summary>
        /// 获取全局唯一值Guid
        /// </summary>
        public static string GetGuid()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        /// <summary>
        /// Guid
        /// </summary>
        public static Guid Guid => Guid.NewGuid();

        #endregion

        #region Stopwatch计时器
        /// <summary>
        /// 计时器开始
        /// </summary>
        /// <returns></returns>
        public static Stopwatch TimerStart()
        {
            Stopwatch watch = new Stopwatch();
            watch.Reset();
            watch.Start();
            return watch;
        }
        /// <summary>
        /// 计时器结束
        /// </summary>
        /// <param name="watch"></param>
        /// <returns></returns>
        public static string TimerEnd(Stopwatch watch)
        {
            watch.Stop();
            double costtime = watch.ElapsedMilliseconds;
            return costtime.ToString();
        }
        #endregion

        #region 删除数组中的重复项
        /// <summary>
        /// 删除数组中的重复项
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string[] RemoveDup(string[] values)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < values.Length; i++)//遍历数组成员
            {
                if (!list.Contains(values[i]))
                {
                    list.Add(values[i]);
                };
            }
            return list.ToArray();
        }
        #endregion

        #region 自动生成日期编号
        /// <summary>
        /// 自动生成编号  201008251145409865
        /// </summary>
        /// <returns></returns>
        public static string CreateNo()
        {
            Random random = new Random();
            string strRandom = random.Next(1000, 10000).ToString(); //生成编号 
            string code = DateTimeHelper.Now.ToString("yyyyMMddHHmmss") + strRandom;//形如
            return code;
        }
        #endregion

        #region 生成0-9随机数
        /// <summary>
        /// 生成0-9随机数
        /// </summary>
        /// <param name="codeNum">生成长度</param>
        /// <returns></returns>
        public static string RndNum(int codeNum)
        {
            StringBuilder sb = new StringBuilder(codeNum);
            Random rand = new Random();
            for (int i = 1; i < codeNum + 1; i++)
            {
                int t = rand.Next(9);
                sb.AppendFormat("{0}", t);
            }
            return sb.ToString();

        }
        #endregion

        /// <summary>
        /// 将值转换为type类型的值
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="type">目标类型</param>
        /// <returns></returns>
        public static object ChangeType(object value, Type type)
        {
            if (value != null)
            {
                var nullableType = Nullable.GetUnderlyingType(type);
                if (nullableType != null)//可空
                {
                    return Convert.ChangeType(value, nullableType);
                }
                if (Convert.IsDBNull(value))//特殊处理，由于数据库类型与项目中的类型定义不匹配
                    return type.IsValueType ? Activator.CreateInstance(type) : null;
                return Convert.ChangeType(value, type);
            }
            return null;
        }

    }
}

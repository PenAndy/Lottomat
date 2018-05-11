using System.Security.Cryptography;
using System.Text;

namespace Lottomat.Utils.Security
{
    /// <summary>
    /// MD5加密帮助类
    /// 版本：2.0
    /// <author>
    ///		<name>赵轶</name>
    ///		<date>2013.09.27</date>
    /// </author>
    /// </summary>
    public class Md5Helper
    {
        #region "MD5加密"
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input">加密字符</param>
        /// <param name="len">加密字符长度</param>
        /// <returns></returns>
        public static string MD5(string input,int len)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] encryptedBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(input));

            StringBuilder sb = new StringBuilder();

            if (len == 16)
            {
                foreach (byte t in encryptedBytes)
                {
                    sb.AppendFormat(t.ToString("X"));
                }
            }else if (len == 32)
            {
                foreach (byte t in encryptedBytes)
                {
                    sb.AppendFormat(t.ToString("X2"));
                }
            }

            return sb.ToString();
        }
        #endregion
    }
}

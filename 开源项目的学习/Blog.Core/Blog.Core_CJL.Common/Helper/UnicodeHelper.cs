using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blog.Core_CJL.Common.Helper
{
    /// <summary>
    /// Unicode帮助类
    /// </summary>
    public static class UnicodeHelper
    {
        /*
        Unicode是一种在计算机上使用的字符编码。它为每种语言中的每个字符设定了统一并且唯一的二进制编码，
        以满足跨语言、跨平台进行文本转换、处理的要求
         */
        /// <summary>
        /// 字符串转Unicode码
        /// </summary>
        /// <returns>The to unicode.</returns>
        /// <param name="value">Value.</param>
        public static string StringToUnicode(string value)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(value);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i += 2)
            {
                // 取两个字符，每个字符都是右对齐。
                stringBuilder.AppendFormat("u{0}{1}", bytes[i + 1].ToString("x").PadLeft(2, '0'), bytes[i].ToString("x").PadLeft(2, '0'));
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Unicode转字符串
        /// </summary>
        /// <returns>The to string.</returns>
        /// <param name="unicode">Unicode.</param>
        public static string UnicodeToString(string unicode)
        {
            unicode = unicode.Replace("%", "\\");

            return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(
                 unicode, x => string.Empty + Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)));
        }
    }
}

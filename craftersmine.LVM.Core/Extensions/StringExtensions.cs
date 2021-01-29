using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core.Extensions
{
    /// <summary>
    /// Contains extensions for <see cref="string"/> type
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Checks whether string is null, empty or whitespace
        /// </summary>
        /// <param name="str">String to check</param>
        /// <returns>true if it is null, empty or whitespace, otherwise false</returns>
        public static bool IsNullEmptyOrWhitespace(this string str)
        {
            if (str != null)
            {
                if (!string.IsNullOrEmpty(str) && !string.IsNullOrWhiteSpace(str))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Gets string byte array
        /// </summary>
        /// <param name="str">String to get byte array</param>
        /// <returns>Byte array of string</returns>
        public static byte[] GetBytes(this string str)
        {
            return Encoding.Default.GetBytes(str);
        }

        /// <summary>
        /// Gets string byte with specified encoding
        /// </summary>
        /// <param name="str">String to get byte array</param>
        /// <param name="encoding">Encoding to use for getting string byte array</param>
        /// <returns></returns>
        public static byte[] GetBytes(this string str, Encoding encoding)
        {
            return encoding.GetBytes(str);
        }
    }
}

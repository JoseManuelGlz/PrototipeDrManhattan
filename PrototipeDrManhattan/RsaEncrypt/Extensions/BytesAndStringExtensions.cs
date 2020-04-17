using System;
using System.Text;

namespace PrototipeDrManhattan.RsaEncrypt.Extensions
{
    internal static class BytesAndStringExtensions
    {
        /// <summary>
        /// byte to hex string extension
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        internal static string ToHexString(this byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("X2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// hex string to byte extension
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        internal static byte[] ToBytes(this string hex)
        {
            try
            {
                if (hex.Length == 0)
                {
                    return new byte[] { 0 };
                }
                if (hex.Length % 2 == 1)
                {
                    hex = "0" + hex;
                }
                byte[] result = new byte[hex.Length / 2];
                for (int i = 0; i < hex.Length / 2; i++)
                {
                    string val = hex.Substring(2 * i, 2);


                    result[i] = byte.Parse(hex.Substring(2 * i, 2), System.Globalization.NumberStyles.AllowHexSpecifier);

                }
                return result;
            }
            catch (FormatException ex)
            {
                byte[] base64 = Convert.FromBase64String(hex);

                //string newHex = Encoding.UTF8.GetString(base64);
                string newHex = System.Text.Encoding.GetEncoding("iso-8859-1").GetString(base64);

                if (newHex.Length == 0)
                {
                    return new byte[] { 0 };
                }
                if (newHex.Length % 2 == 1)
                {
                    newHex = "0" + newHex;
                }
                byte[] result = new byte[newHex.Length / 2];
                for (int i = 0; i < newHex.Length / 2; i++)
                {
                    //string val = hex.Substring(2 * i, 2);

                    result[i] = byte.Parse(newHex.Substring(2 * i, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                }
                return result;
               
                //return base64;
            }
        }
    }

}

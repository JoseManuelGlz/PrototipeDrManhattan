using System.IO;

namespace PrototipeDrManhattan.RsaEncrypt.Extensions
{
    internal static class StreamExtensions
    {
        /// <summary>
        /// Stream write all bytes 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="byts"></param>
        static public void WriteAll(this Stream stream, byte[] byts)
        {
            stream.Write(byts, 0, byts.Length);
        }
    }
}

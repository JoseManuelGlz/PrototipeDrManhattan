using System;
using Documents.Manager.Models.Models;
using System.Diagnostics.CodeAnalysis;

namespace Documents.Manager.Factories.ModelsFactories
{
    [ExcludeFromCodeCoverage]
    public static class FileTypeFactory
    {
        #region :: Properties ::

        /// <summary>
        /// 
        /// </summary>
        public readonly static int MAX_LENGTH = 250;

        #endregion

        #region :: Methods ::

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static FileType GetFileType()
        {
            var id = Guid.NewGuid();
            var name = Utils.GetAlphaNumericString(MAX_LENGTH);
            var mimeType = Utils.GetAlphaNumericString(MAX_LENGTH);
            var extension = Utils.GetAlphaNumericString(MAX_LENGTH);
            var status = Utils.GetAlphaNumericString(MAX_LENGTH);
            var minSize = Utils.GetLong(1, 100000);
            var maxSize = Utils.GetLong(1, 100000);

            return new FileType(id, name, mimeType, extension, minSize, maxSize, status);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static FileType GetFileTypeWithId(Guid id)
        {
            var name = Utils.GetAlphaNumericString(MAX_LENGTH);
            var mimeType = Utils.GetAlphaNumericString(MAX_LENGTH);
            var extension = Utils.GetAlphaNumericString(MAX_LENGTH);
            var status = Utils.GetAlphaNumericString(MAX_LENGTH);
            var minSize = Utils.GetLong(1, 100000);
            var maxSize = Utils.GetLong(1, 100000);

            return new FileType(id, name, mimeType, extension, minSize, maxSize, status);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static FileType GetFileTypeWithLength(int length)
        {
            var id = Guid.NewGuid();
            var name = Utils.GetAlphaNumericString(length);
            var mimeType = Utils.GetAlphaNumericString(length);
            var extension = Utils.GetAlphaNumericString(length);
            var status = Utils.GetAlphaNumericString(length);
            var minSize = Utils.GetLong(1, 100000);
            var maxSize = Utils.GetLong(1, 100000);

            return new FileType(id, name, mimeType, extension, minSize, maxSize, status);
        }

        #endregion
    }
}

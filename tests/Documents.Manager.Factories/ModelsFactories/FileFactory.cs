using System;
using System.Diagnostics.CodeAnalysis;
using Documents.Manager.Models.Models;

namespace Documents.Manager.Factories.ModelsFactories
{
    /// <summary>
    /// Factory to create instances of <see cref="T:Documents.Manager.Models.Models.File"/> 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class FileFactory
    {
        #region :: Properties ::

        /// <summary>
        /// Max length of varchar fields
        /// </summary>
        public readonly static int MAX_LENGTH = 250;

        #endregion

        #region :: Methods ::

        /// <summary>
        /// Get valid <see cref="T:Documents.Manager.Models.Models.File"/> without identifier.
        /// </summary>
        /// <returns>New <see cref="T:Documents.Manager.Models.Models.File"/></returns>
        public static File GetFile()
        {
            var id = Guid.NewGuid();
            var ownerId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var name = Utils.GetAlphaNumericString(MAX_LENGTH);
            var etag= Utils.GetAlphaNumericString(MAX_LENGTH);
            var version = Utils.GetAlphaNumericString(MAX_LENGTH);
            var processId = Guid.NewGuid();
            var fileTypeId = Guid.NewGuid();
            var fileClassificationId = Guid.NewGuid();
            var applicationId = Guid.NewGuid();
            return new File(id, ownerId, userId, name, etag, version, fileTypeId, processId, fileClassificationId, applicationId);
        }

        /// <summary>
        /// Get valid <see cref="T:Documents.Manager.Models.Models.File"/> without identifier.
        /// </summary>
        /// <returns>New <see cref="T:Documents.Manager.Models.Models.File"/></returns>
        public static File GetFileWithId(Guid id)
        {
            var ownerId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var name = Utils.GetAlphaNumericString(MAX_LENGTH);
            var etag = Utils.GetAlphaNumericString(MAX_LENGTH);
            var version = Utils.GetAlphaNumericString(MAX_LENGTH);
            var processId = Guid.NewGuid();
            var fileTypeId = Guid.NewGuid();
            var fileClassificationId = Guid.NewGuid();
            var applicationId = Guid.NewGuid();
            return new File(id, ownerId, userId, name, etag, version, fileTypeId, processId, fileClassificationId, applicationId);
        }

        /// <summary>
        /// Get valid <see cref="T:Documents.Manager.Models.Models.File"/> without identifier.
        /// </summary>
        /// <returns>New <see cref="T:Documents.Manager.Models.Models.File"/></returns>
        public static File GetFileWithLength(int length)
        {
            var id = Guid.NewGuid();
            var ownerId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var name = Utils.GetAlphaNumericString(length);
            var etag = Utils.GetAlphaNumericString(length);
            var version = Utils.GetAlphaNumericString(length);
            var processId = Guid.NewGuid();
            var fileTypeId = Guid.NewGuid();
            var fileClassificationId = Guid.NewGuid();
            var applicationId = Guid.NewGuid();
            return new File(id, ownerId, userId, name, etag, version, fileTypeId, processId, fileClassificationId, applicationId);
        }
        #endregion
    }
}
using System;
using System.Diagnostics.CodeAnalysis;
using Documents.Manager.Models.Models;

namespace Documents.Manager.Factories.ModelsFactories
{
    /// <summary>
    /// Factory to create instances of <see cref="T:Documents.Manager.Models.Models.Url"/> 
    /// </summary>
    [ExcludeFromCodeCoverage]
	public class UrlFactory
    {
        #region :: Properties ::

        /// <summary>
        /// Max length of varchar fields.
        /// </summary>
        public readonly static int MAX_LENGTH = 250;

        #endregion

        #region :: Methods ::

        /// <summary>
        /// Get valid <see cref="T:Documents.Manager.Models..Models.Url"/> with an identifier.
        /// </summary>
        /// <returns>New <see cref="T:Documents.Manager.Models..Models.Url"/></returns>
        public static Url GetUrl()
        {
            Guid id = Guid.NewGuid();
            var address = Utils.VALID_URL;
            var expiresAt = DateTime.Now;
            Guid fileId = Guid.NewGuid();
            return new Url(id, address, expiresAt, fileId);
        }

        /// <summary>
        /// Get valid <see cref="T:Documents.Manager.Models..Models.Url"/> with an identifier.
        /// </summary>
        /// <returns>New <see cref="T:Documents.Manager.Models..Models.Url"/></returns>
        public static Url GetUrlWithId(Guid id)
        {
            var address = Utils.VALID_URL;
            var expiresAt = DateTime.Now;
            Guid fileId = Guid.NewGuid();
            return new Url(id, address, expiresAt, fileId);
        }

        /// <summary>
        /// Get an invalid <see cref="T:Documents.Manager.Models.Models.Url"/> 
        /// </summary>
        /// <returns>New <see cref="T:Documents.Manager.Models.Models.Url"/></returns>
        public static Url GetUrlEmpty()
        {
            return new Url();
        }

        /// <summary>
        /// Get valid <see cref="T:Documents.Manager.Models..Models.Url"/> with an identifier.
        /// </summary>
        /// <returns>New <see cref="T:Documents.Manager.Models..Models.Url"/></returns>
        public static Url GetUrlWithInvalidAddress()
        {
            Guid id = Guid.NewGuid();
            var address = Utils.GetAlphaNumericString(MAX_LENGTH);
            var expiresAt = DateTime.Now;
            Guid fileId = Guid.NewGuid();
            return new Url(id, address, expiresAt, fileId);
        }
        #endregion
    }
}

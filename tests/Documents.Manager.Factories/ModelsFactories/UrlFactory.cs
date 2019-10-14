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
        /// <param name="userId">User identifier.</param>
        /// <returns>New <see cref="T:Documents.Manager.Models..Models.Url"/></returns>
        public static Url GetValid()
        {
            return new Url();
        }

        /// <summary>
        /// Get an invalid <see cref="T:Documents.Manager.Models.Models.Url"/> with fields longer than the specified length.
        /// </summary>
        /// <param name="userId">Identifier</param>
        /// <returns>New <see cref="T:Documents.Manager.Models.Models.Url"/></returns>
        public static Url GetAddedInvalidMaxLength(Guid userId, int length)
        {
            return new Url();
        }

        
        #endregion
    }
}

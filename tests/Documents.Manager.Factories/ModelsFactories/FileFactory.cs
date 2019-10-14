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
        /// Get valid <see cref="T:Documents.Manager.Models.Entities.File"/> without identifier.
        /// </summary>
        /// <returns>New <see cref="T:Documents.Manager.Models.Entities.File"/></returns>
        public static File GetValid()
        {
            return new File();
        }

        
        #endregion
    }
}
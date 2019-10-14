using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Documents.Manager.Business.Exceptions
{
    /// <summary>
    /// Secrets manager exception.
    /// </summary>
    [Serializable]
    public class SecretsManagerException : Exception
    {
        #region  Constructor 

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Documents.Manager.Service.Exceptions.SecretsManagerException"/> class.
        /// </summary>
        public SecretsManagerException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Documents.Manager.Service.Exceptions.SecretsManagerException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public SecretsManagerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Documents.Manager.Service.Exceptions.SecretsManagerException"/> class.
        /// </summary>
        /// <param name="info">Serialization information.</param>
        /// <param name="context">Streaming context.</param>
        [ExcludeFromCodeCoverage]
        protected SecretsManagerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}

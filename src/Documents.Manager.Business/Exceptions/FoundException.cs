using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Documents.Manager.Business.Exceptions
{
    [Serializable]
    public class FoundException : Exception
    {
        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Documents.Manager.Service.Exceptions.NotFoundException"/> class.
        /// </summary>
        public FoundException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Documents.Manager.Service.Exceptions.FoundException"/> class.
        /// </summary>
        /// <param name="info">Serialization information.</param>
        /// <param name="context">Streaming context.</param>
        [ExcludeFromCodeCoverage]
        protected FoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
        {
        }

        #endregion
    }
}

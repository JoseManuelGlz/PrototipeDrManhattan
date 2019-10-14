using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Documents.Manager.Business.Exceptions
{
    /// <summary>
    /// Not found exception.
    /// </summary>
    [Serializable]
    public class NotFoundException : Exception
    {
        #region  Constructor 

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Documents.Manager.Service.Exceptions.NotFoundException"/> class.
        /// </summary>
        public NotFoundException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Documents.Manager.Service.Exceptions.NotFoundException"/> class.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [ExcludeFromCodeCoverage]
        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        #endregion
    }
}
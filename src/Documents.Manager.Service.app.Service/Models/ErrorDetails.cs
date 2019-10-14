using System.Collections.Generic;

namespace Documents.Manager.Service.Models
{
    /// <summary>
    /// Error details.
    /// </summary>
    public class ErrorDetails
    {
        #region :: Properties ::

        /// <summary>
        /// Gets or sets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        public string objectType { get; set; }

        /// <summary>
        /// Gets or sets the response code.
        /// </summary>
        /// <value>The response code.</value>
        public string responseCode { get; set; }

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>The errors.</value>
        public List<Error> errors { get; set; }

        #endregion

        #region  Constructor 

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Documents.Manager.Service.Models.ErrorDetails"/> class.
        /// </summary>
        public ErrorDetails()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Documents.Manager.Service.Models.ErrorDetails"/> class.
        /// </summary>
        /// <param name="objectType">Object type.</param>
        /// <param name="responseCode">Response code.</param>
        /// <param name="errors">Errors.</param>
        public ErrorDetails(string objectType, string responseCode, List<Error> errors)
        {
            this.objectType = objectType;
            this.responseCode = responseCode;
            this.errors = errors;
        }

        #endregion
    }
}
using System.Collections.Generic;

namespace Documents.Manager.Service.Models
{
    /// <summary>
    /// Custom errors.
    /// </summary>
    public class CustomErrors
    {
        #region :: Properties ::

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>The errors.</value>
        public List<Errors> Errors { get; set; }

        #endregion
    }
}
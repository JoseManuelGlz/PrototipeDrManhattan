using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Documents.Manager.Factories.Entities
{
    /// <summary>
    /// Model validation results
    /// </summary>
    public class ModelResponse
    {
        #region :: Properties ::

        /// <summary>
        /// Validation results
        /// </summary>
        public List<ValidationResult> ValidationResults { get; set; }

        /// <summary>
        /// Passed
        /// </summary>
        public bool Passed { get; set; }

        #endregion
    }
}

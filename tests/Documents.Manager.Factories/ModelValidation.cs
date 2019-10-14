using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Documents.Manager.Factories.Entities;

namespace Documents.Manager.Factories
{
    /// <summary>
    /// Validates an instance.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ModelValidation
    {
        #region :: Methods ::

        /// <summary>
        /// Validates an instance and return if is valida or have errors.
        /// </summary>
        /// <typeparam name="T">Instance to validate.</typeparam>
        /// <param name="model">Instance to validate.</param>
        /// <returns>Success or error messages.</returns>
        public static ModelResponse GetValidationResults<T>(T model)
        {
            ModelResponse response = new ModelResponse();
            List<ValidationResult> validations = new List<ValidationResult>();
            response.Passed = Validator.TryValidateObject(model, new ValidationContext(model), validations, true);
            response.ValidationResults = validations;

            return response;
        }

        #endregion
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace Documents.Manager.Models.Validations
{
    [AttributeUsage(
        AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class NotEmptyAttribute : ValidationAttribute
    {
        #region :: Properties ::

        /// <summary>
        /// The default error message.
        /// </summary>
        private const string DefaultErrorMessage = "The {0} field must not be empty.";

        #endregion

        #region :: Constructor ::

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Documents.Manager.Models.Validations.NotEmptyAttribute"/> class.
        /// </summary>
        public NotEmptyAttribute() : base(DefaultErrorMessage) { }

        #endregion

        #region :: Methods ::

        /// <summary>
        /// Ises the valid.
        /// </summary>
        /// <returns><c>true</c>, if valid was ised, <c>false</c> otherwise.</returns>
        /// <param name="value">Value.</param>
        public override bool IsValid(object value)
        {
            var type = value.GetType();
            if (type.IsValueType)
            {
                var defaultValue = Activator.CreateInstance(type);
                return !value.Equals(defaultValue);
            }

            return true;
        }

        #endregion
    }
}

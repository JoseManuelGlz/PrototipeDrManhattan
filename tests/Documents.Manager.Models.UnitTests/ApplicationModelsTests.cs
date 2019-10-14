using System;
using Documents.Manager.Factories;
using Documents.Manager.Models.Models;
using Xunit;

namespace Documents.Manager.Models.UnitTests
{
    /// <summary>
    /// 
    /// </summary>
    [Collection("Sequential")]
    public static class ApplicationModelsTests
    {
        #region :: Methods ::

        /// <summary>
        /// Test a valid instance.
        /// </summary>
        [Fact]
        public static void Should_Be_Ok()
        {
            Application additionalInfo = ApplicationFactory.GetApplication();

            var response = ModelValidation.GetValidationResults(additionalInfo);

            Assert.True(response.Passed);
        }

        #endregion
    }
}
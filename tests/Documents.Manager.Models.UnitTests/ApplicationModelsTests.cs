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

        /// <summary>
        /// Test a valid instance.
        /// </summary>
        [Fact]
        public static void Should_Be_Fail_By_Length()
        {
            Application additionalInfo = ApplicationFactory.GetApplicationWithLength(ApplicationFactory.MAX_LENGTH + 1);

            var response = ModelValidation.GetValidationResults(additionalInfo);

            Assert.False(response.Passed);
        }

        /// <summary>
        /// Test a valid instance.
        /// </summary>
        [Fact]
        public static void Should_Be_Ok_With_Id()
        {
            Application additionalInfo = ApplicationFactory.GetApplicationWithId(Guid.NewGuid());

            var response = ModelValidation.GetValidationResults(additionalInfo);

            Assert.True(response.Passed);
        }
        #endregion
    }
}
using System;
using Documents.Manager.Factories;
using Documents.Manager.Factories.ModelsFactories;
using Documents.Manager.Models.Models;
using Xunit;

namespace Documents.Manager.Models.UnitTests
{
    /// <summary>
    /// Unit tests for <see cref="T:Documents.Manager.Models.Models.FileType"/>.
    /// </summary>
    [Collection("Sequential")]
    public static class FileTypeModelsTests
    {
        #region :: Methods ::

        /// <summary>
        /// Test a valid instance.
        /// </summary>
        [Fact]
        public static void Should_Be_Ok()
        {
            FileType fiscalInfo = FileTypeFactory.GetFileType();
            var response = ModelValidation.GetValidationResults(fiscalInfo);

            Assert.True(response.Passed);
        }

        #endregion
    }
}

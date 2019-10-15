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

        /// <summary>
        /// Test a valid instance.
        /// </summary>
        [Fact]
        public static void Should_Be_Fail()
        {
            FileType fiscalInfo = FileTypeFactory.GetFileTypeWithLength(FileClassificationFactory.MAX_LENGTH +1);
            var response = ModelValidation.GetValidationResults(fiscalInfo);

            Assert.False(response.Passed);
        }

        /// <summary>
        /// Test a valid instance.
        /// </summary>
        [Fact]
        public static void Should_Be_Ok_With_Id()
        {
            FileType fiscalInfo = FileTypeFactory.GetFileTypeWithId(Guid.NewGuid());
            var response = ModelValidation.GetValidationResults(fiscalInfo);

            Assert.True(response.Passed);
        }
        #endregion
    }
}

using System;
using Documents.Manager.Factories;
using Documents.Manager.Factories.ModelsFactories;
using Documents.Manager.Models.Models;
using Xunit;

namespace Documents.Manager.Models.UnitTests
{
    /// <summary>
    /// Unit tests to <see cref="T:Documents.Manager.Models.Models.FileClassification"/>. 
    /// </summary>
    [Collection("Sequential")]
    public static class FileClassificationModelTests
    {
        #region :: Methods ::

        /// <summary>
        /// Should be ok
        /// </summary>
        [Fact]
        public static void Should_Be_Ok()
        {
            FileClassification external = FileClassificationFactory.GetFileClassification();
            var res = ModelValidation.GetValidationResults(external);

            Assert.True(res.Passed);
        }

        /// <summary>
        /// Should be ok
        /// </summary>
        [Fact]
        public static void Should_Be_Fail_By_Length()
        {
            FileClassification external = FileClassificationFactory.GetFileClassificationWithLength(FileClassificationFactory.MAX_LENGTH +1);
            var res = ModelValidation.GetValidationResults(external);

            Assert.False(res.Passed);
        }

        /// <summary>
        /// Should be ok
        /// </summary>
        [Fact]
        public static void Should_Be_Ok_With_Id()
        {
            FileClassification external = FileClassificationFactory.GetFileClassificationWithId(Guid.NewGuid());
            var res = ModelValidation.GetValidationResults(external);

            Assert.True(res.Passed);
        }
        #endregion
    }
}

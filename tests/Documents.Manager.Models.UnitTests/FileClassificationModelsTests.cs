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

        #endregion
    }
}

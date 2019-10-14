using Documents.Manager.Factories;
using Documents.Manager.Factories.ModelsFactories;
using Documents.Manager.Models.Models;
using Xunit;

namespace Documents.Manager.Models.UnitTests
{
    /// <summary>
    /// 
    /// </summary>
    [Collection("Sequential")]
    public static class UrlModelsTests
    {
        #region :: Methods ::

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public static void Should_Be_ok()
        {
            Url url = UrlFactory.GetUrl();
            var result = ModelValidation.GetValidationResults(url);

            Assert.True(result.Passed);
        }

        #endregion
    }
}
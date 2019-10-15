using System;
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
        public static void Should_Be_Ok()
        {
            Url url = UrlFactory.GetUrl();
            var result = ModelValidation.GetValidationResults(url);

            Assert.True(result.Passed);
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public static void Should_Be_Ok_With_Id()
        {
            Url url = UrlFactory.GetUrlWithId(Guid.NewGuid());
            var result = ModelValidation.GetValidationResults(url);

            Assert.True(result.Passed);
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public static void Should_Be_Fail()
        {
            Url url = UrlFactory.GetUrlWithInvalidAddress();
            var result = ModelValidation.GetValidationResults(url);

            Assert.False(result.Passed);
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public static void Should_Be_Fail_By_Invalid_Lentgh()
        {
            Url url = UrlFactory.GetUrlEmpty();
            var result = ModelValidation.GetValidationResults(url);

            Assert.False(result.Passed);
        }

        #endregion
    }
}
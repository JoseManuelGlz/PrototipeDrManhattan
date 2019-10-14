using System;
using Documents.Manager.Factories;
using Documents.Manager.Factories.ModelsFactories;
using Documents.Manager.Models.Models;
using Xunit;

namespace Documents.Manager.Models.UnitTests
{
    /// <summary>
    /// Unit tests for <see cref="T:Documents.Manager.Models.Entities.File"/>.
    /// </summary>
    [Collection("Sequential")]
    public static class FileModelsTests
    {
        #region :: Methods ::

        /// <summary>
        /// Test a valid instance.
        /// </summary>
        [Fact]
        public static void Should_Be_Ok()
        {
            File file = FileFactory.GetValid();
            var response = ModelValidation.GetValidationResults(file);

            Assert.True(response.Passed);
        }

        #endregion
    }
}

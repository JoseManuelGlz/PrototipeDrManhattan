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
    public static class ProcessModelsTests
    {
        #region :: Methods ::

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public static void Should_Be_ok()
        {
            Process process = ProcessFactory.GetProcess();
            var result = ModelValidation.GetValidationResults(process);

            Assert.True(result.Passed);
        }

        #endregion
    }
}
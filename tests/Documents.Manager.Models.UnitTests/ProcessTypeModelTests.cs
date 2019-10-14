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
    public static class ProcessTypeModelsTests
    {
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public static void Should_Be_ok()
        {
            ProcessType processType = ProcessTypeFactory.GetProcessType();
            var result = ModelValidation.GetValidationResults(processType);

            Assert.True(result.Passed);
        }
    }
}
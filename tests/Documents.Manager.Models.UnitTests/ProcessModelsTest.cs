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


        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public static void Should_Be_Fail()
        {
            Process process = ProcessFactory.GetProcessWithLength(ProcessFactory.MAX_LENGTH + 1);
            var result = ModelValidation.GetValidationResults(process);

            Assert.False(result.Passed);
        }


        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public static void Should_Be_Ok_With_Id()
        {
            Process process = ProcessFactory.GetProcessWithId(Guid.NewGuid());
            var result = ModelValidation.GetValidationResults(process);

            Assert.True(result.Passed);
        }
        #endregion
    }
}
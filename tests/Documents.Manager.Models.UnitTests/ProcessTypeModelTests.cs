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
    public static class ProcessTypeModelsTests
    {
        #region :: Methods ::

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

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public static void Should_Be_Fail_By_Length()
        {
            ProcessType processType = ProcessTypeFactory.GetProcessTypeWithLength(ProcessTypeFactory.MAX_LENGTH + 1);
            var result = ModelValidation.GetValidationResults(processType);

            Assert.False(result.Passed);
        }


        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public static void Should_Be_Ok_With_Id()
        {
            ProcessType processType = ProcessTypeFactory.GetProcessTypeWithId(Guid.NewGuid());
            var result = ModelValidation.GetValidationResults(processType);

            Assert.True(result.Passed);
        }

        #endregion
    }
}
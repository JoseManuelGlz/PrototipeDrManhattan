using System;
using Documents.Manager.Models.Validations;
using Xunit;

namespace Documents.Manager.Models.UnitTests
{
    /// <summary>
    /// 
    /// </summary>
    [Collection("Sequential")]
    public static class NotEmptyAttributeTests
    {
        #region :: Methods ::

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public static void Should_Be_Ok_Value()
        {
            var value = Guid.NewGuid();
            var attrib = new NotEmptyAttribute();

            var result = attrib.IsValid(value);

            Assert.True(result);
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public static void Should_Be_Fail_Value()
        {
            var value = Guid.Empty;
            var attrib = new NotEmptyAttribute();

            var result = attrib.IsValid(value);

            Assert.False(result);
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public static void Should_Be_Ok_Object()
        {
            var value = new object();
            var attrib = new NotEmptyAttribute();

            var result = attrib.IsValid(value);

            Assert.True(result);
        }

        #endregion
    }
}
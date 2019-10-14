using Xunit;

namespace Documents.Manager.Models.UnitTests
{
    /// <summary>
    /// 
    /// </summary>
    [Collection("Sequential")]
    public static class ModelBuilderExtensionsTests
    {
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public static void Should_Be_Ok_Value()
        {
            var value = ModelBuilderExtensions.ToSnakeCase(string.Empty);
            Assert.Equal(string.Empty, value);
        }
    }
}

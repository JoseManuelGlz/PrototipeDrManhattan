using System.Text.RegularExpressions;

namespace Documents.Manager.Models
{
    /// <summary>
    /// Model builder extensions.
    /// </summary>
    public static class ModelBuilderExtensions
    {
        #region :: Methods ::

        /// <summary>
        /// Tos the snake case.
        /// </summary>
        /// <returns>The snake case.</returns>
        /// <param name="input">Input.</param>
        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            var startUnderscores = Regex.Match(input, @"^_+");
            return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
        }

        #endregion
    }
}
using System;
using System.Diagnostics.CodeAnalysis;
using Documents.Manager.Models.Models;

namespace Documents.Manager.Factories
{
    [ExcludeFromCodeCoverage]
    public static class ApplicationFactory
    {
        public readonly static int MAX_LENGTH = 250;
        public readonly static int MAX_LENGTH_DTO = 50;

        public static Application GetApplication()
        {
            var id = Guid.NewGuid();
            var name = Utils.GetAlphaNumericString(MAX_LENGTH);
            var description = Utils.GetAlphaNumericString(MAX_LENGTH);
            
            return new Application();
        }

        public static Application GetApplicationEmpty()
        {
            return new Application();
        }

        public static Application GetApplicationWithId(Guid id)
        {
            var name = Utils.GetAlphaNumericString(MAX_LENGTH);
            var description = Utils.GetAlphaNumericString(MAX_LENGTH);

            return new Application();
        }

        public static Application GetApplicationWithLength(int length)
        {
            var id = Guid.NewGuid();
            var name = Utils.GetAlphaNumericString(length);
            var description = Utils.GetAlphaNumericString(length);

            return new Application();
        }
    }
}
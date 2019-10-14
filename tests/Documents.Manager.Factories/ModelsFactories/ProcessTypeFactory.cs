using System;
using Documents.Manager.Models.Models;
using System.Diagnostics.CodeAnalysis;

namespace Documents.Manager.Factories.ModelsFactories
{
    [ExcludeFromCodeCoverage]
    public static class ProcessTypeFactory
    {
        public readonly static int MAX_LENGTH = 250;

        public static ProcessType GetProcessType()
        {
            var id = Guid.NewGuid();
            var name = Utils.GetAlphaNumericString(MAX_LENGTH);
            var description = Utils.GetAlphaNumericString(MAX_LENGTH);

            return new ProcessType();
        }

        public static ProcessType GetProcessTypeEmpty()
        {
            return new ProcessType();
        }

        public static ProcessType GetProcessTypeWithId(Guid id)
        {
            var name = Utils.GetAlphaNumericString(MAX_LENGTH);
            var description = Utils.GetAlphaNumericString(MAX_LENGTH);

            return new ProcessType();
        }

        public static ProcessType GetProcessTypeWithLength(int length)
        {
            var id = Guid.NewGuid();
            var name = Utils.GetAlphaNumericString(length);
            var description = Utils.GetAlphaNumericString(length);

            return new ProcessType();
        }
    }
}

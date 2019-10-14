using System;
using System.Diagnostics.CodeAnalysis;
using Documents.Manager.Models.Models;

namespace Documents.Manager.Factories.ModelsFactories
{
    [ExcludeFromCodeCoverage]
    public static class ProcessFactory
    {
        public static Process GetProcess()
        {
            return new Process(); 
        }
    }
}

using System;
using System.Diagnostics.CodeAnalysis;
using Documents.Manager.Models.Models;

namespace Documents.Manager.Factories.ModelsFactories
{
    [ExcludeFromCodeCoverage]
    public static class ProcessFactory
    {
        #region :: Properties ::

        /// <summary>
        /// 
        /// </summary>
        public readonly static int MAX_LENGTH = 250;

        #endregion

        #region :: Methods ::

        public static Process GetProcess()
        {
            return new Process(); 
        }

        #endregion
    }
}

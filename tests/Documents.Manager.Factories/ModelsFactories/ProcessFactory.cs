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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Process GetProcess()
        {
            var id = Guid.NewGuid();
            var name = Utils.GetAlphaNumericString(MAX_LENGTH);
            var description = Utils.GetAlphaNumericString(MAX_LENGTH);
            var nameBucket = Utils.GetAlphaNumericString(MAX_LENGTH);
            var proccessTypeId = Guid.NewGuid();

            return new Process(id, name, description, nameBucket, proccessTypeId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Process GetProcessWithId(Guid id)
        {
            var name = Utils.GetAlphaNumericString(MAX_LENGTH);
            var description = Utils.GetAlphaNumericString(MAX_LENGTH);
            var nameBucket = Utils.GetAlphaNumericString(MAX_LENGTH);
            var proccessTypeId = Guid.NewGuid();

            return new Process(id, name, description, nameBucket, proccessTypeId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Process GetProcessWithLength(int length)
        {
            var id = Guid.NewGuid();
            var name = Utils.GetAlphaNumericString(length);
            var description = Utils.GetAlphaNumericString(length);
            var nameBucket = Utils.GetAlphaNumericString(length);
            var proccessTypeId = Guid.NewGuid();

            return new Process(id, name, description, nameBucket, proccessTypeId);
        }
        #endregion
    }
}

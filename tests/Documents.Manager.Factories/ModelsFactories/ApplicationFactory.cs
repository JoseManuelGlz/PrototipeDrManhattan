using System;
using System.Diagnostics.CodeAnalysis;
using Documents.Manager.Models.Models;

namespace Documents.Manager.Factories
{
    [ExcludeFromCodeCoverage]
    public static class ApplicationFactory
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
        public static Application GetApplication()
        {
            var id = Guid.NewGuid();
            var name = Utils.GetAlphaNumericString(MAX_LENGTH);
            var description = Utils.GetAlphaNumericString(MAX_LENGTH);

            return new Application(id, name, description);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Application GetApplicationWithId(Guid id)
        {
            var name = Utils.GetAlphaNumericString(MAX_LENGTH);
            var description = Utils.GetAlphaNumericString(MAX_LENGTH);

            return new Application(id, name, description);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static Application GetApplicationWithLength(int length)
        {
            var id = Guid.NewGuid();
            var name = Utils.GetAlphaNumericString(length);
            var description = Utils.GetAlphaNumericString(length);

            return new Application(id, name, description);
        }

        #endregion
    }
}
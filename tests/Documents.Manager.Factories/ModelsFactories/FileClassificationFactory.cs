﻿using System;
using Documents.Manager.Models.Models;
using System.Diagnostics.CodeAnalysis;

namespace Documents.Manager.Factories.ModelsFactories
{
    [ExcludeFromCodeCoverage]
    public static class FileClassificationFactory
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
        public static FileClassification GetFileClassification()
        {
            var id = Guid.NewGuid();
            var name = Utils.GetAlphaNumericString(MAX_LENGTH);
            var description = Utils.GetAlphaNumericString(MAX_LENGTH);

            return new FileClassification();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static FileClassification GetFileClassificationEmpty()
        {
            return new FileClassification();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static FileClassification GetFileClassificationWithId(Guid id)
        {
            var name = Utils.GetAlphaNumericString(MAX_LENGTH);
            var description = Utils.GetAlphaNumericString(MAX_LENGTH);

            return new FileClassification();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static FileClassification GetFileClassificationWithLength(int length)
        {
            var id = Guid.NewGuid();
            var name = Utils.GetAlphaNumericString(length);
            var description = Utils.GetAlphaNumericString(length);

            return new FileClassification();
        }

        #endregion
    }
}

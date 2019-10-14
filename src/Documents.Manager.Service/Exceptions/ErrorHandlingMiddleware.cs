using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Documents.Manager.Business.Exceptions;
using Documents.Manager.Service.Models;

namespace Documents.Manager.Service.Exceptions
{
    /// <summary>
    /// Error handling middleware.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        #region :: Properties ::

        /// <summary>
        /// The custom errors.
        /// </summary>
        private readonly CustomErrors _customErrors;

        /// <summary>
        /// The next.
        /// </summary>
        private readonly RequestDelegate next;

        #endregion

        #region  Constructor 

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Documents.Manager.Service.Exceptions.ErrorHandlingMiddleware"/> class.
        /// </summary>
        /// <param name="next">Next.</param>
        /// <param name="options">Options.</param>
        public ErrorHandlingMiddleware(RequestDelegate next, IOptions<CustomErrors> options)
        {
            this.next = next;
            _customErrors = options.Value;
        }

        #endregion

        #region :: Methods ::

        /// <summary>
        /// Invoke the specified context.
        /// </summary>
        /// <returns>The invoke.</returns>
        /// <param name="context">Context.</param>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Builds the error details.
        /// </summary>
        /// <returns>The error details.</returns>
        /// <param name="code">Code.</param>
        /// <param name="exception">Exception.</param>
        public ErrorDetails BuildErrorDetails(HttpStatusCode code, Exception exception)
        {
            int _code = (int)code;

            var error = _customErrors.Errors.Find(item => item.httpStatuscode == _code);

            if (error != null)
            {
                return new ErrorDetails(error.type, _code.ToString(), new List<Error>
                {
                    new Error(exception.Source, code.ToString(), error.message)
                });
            }

            return null;
        }

        /// <summary>
        /// Handles the exception async.
        /// </summary>
        /// <returns>The exception async.</returns>
        /// <param name="context">Context.</param>
        /// <param name="exception">Exception.</param>
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            if (exception == null) { return; }

            // NotFound exception custom
            // NotFound for function Maybe when variable (HasValue == False)
            if (exception is NotFoundException
                || exception is InvalidOperationException)
            {
                code = HttpStatusCode.NotFound;
            }
            // NotFound for function Maybe when variable (HasValue == False)
            // Custom Found
            else if (exception is DbUpdateException
                || exception is FoundException)
            {
                code = HttpStatusCode.BadRequest;
            }

            await WriteExceptionAsync(context, exception, code).ConfigureAwait(false);
        }

        /// <summary>
        /// Writes the exception async.
        /// </summary>
        /// <returns>The exception async.</returns>
        /// <param name="context">Context.</param>
        /// <param name="exception">Exception.</param>
        /// <param name="code">Code.</param>
        private async Task WriteExceptionAsync(HttpContext context, Exception exception, HttpStatusCode code)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)code;

            await response.WriteAsync(JsonConvert.SerializeObject(
                BuildErrorDetails(code, exception)
            )).ConfigureAwait(false);
        }

        #endregion
    }
}

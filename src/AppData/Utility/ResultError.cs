using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDataModels.Utility
{
    /// <summary>
    /// Represents an error result with status code, message, and optional exception details.
    /// Used for standardized error handling with the result pattern.
    /// </summary>
    public class ResultError
    {
        /// <summary>
        /// Initializes a new instance of the ResultError class.
        /// </summary>
        /// <param name="statusCode">The status code indicating the type of error.</param>
        /// <param name="message">A descriptive message explaining the error.</param>
        /// <param name="exception">Optional. The exception associated with this error.</param>
        public ResultError(string statusCode, string message, Exception? exception = null)
        {
            StatusCode = statusCode;
            Message = message;
            Exception = exception;
        }

        /// <summary>
        /// Gets the status code indicating the type of error.
        /// </summary>
        public string StatusCode { get; }

        /// <summary>
        /// Gets the descriptive message explaining the error.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets or sets the exception associated with this error, if any.
        /// </summary>
        public Exception? Exception { get; set; }
    }
}

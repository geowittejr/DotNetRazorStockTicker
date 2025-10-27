using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDataModels.Utility
{
    public class ResultError
    {
        public ResultError(string statusCode, string message, Exception? exception = null)
        {
            StatusCode = statusCode;
            Message = message;
            Exception = exception;
        }
        public string StatusCode { get; }
        public string Message { get; }
        public Exception? Exception { get; set; }
    }
}

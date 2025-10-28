using AppDataModels.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Quotes.Utility
{
    public class HttpHelperMethods
    {
        public static ResultError? GetResultErrorForStatusCode(HttpStatusCode statusCode, Exception? ex = null)
        {
            return statusCode switch
            {
                HttpStatusCode.OK => null,
                HttpStatusCode.BadRequest => new ResultError("400", "Bad request made to the server", ex),
                HttpStatusCode.Unauthorized => new ResultError("401", "Unauthorized access to the resource", ex),
                HttpStatusCode.Forbidden => new ResultError("403", "Forbidden access to the resource", ex),
                HttpStatusCode.NotFound => new ResultError("404", "The resource was not found", ex),
                HttpStatusCode.TooManyRequests => new ResultError("429", "Too many requests", ex),
                _ => new ResultError("500", "Internal server error occurred", ex)
            };
        }
    }
}

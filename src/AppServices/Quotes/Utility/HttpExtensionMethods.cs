using AppDataModels.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Quotes.Utility
{
    public static class HttpExtensionMethods
    {
        public static async Task<T> DeserializeContentAsync<T>(this HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return obj!;
        }

        public static ResultError? GetResultErrorIfAny(this HttpResponseMessage response)
        {
            return response.StatusCode switch
            {
                HttpStatusCode.OK => null,
                HttpStatusCode.BadRequest => new ResultError("400", "Bad request made to the server"),
                HttpStatusCode.Unauthorized => new ResultError("401", "Unauthorized access to the resource"),
                HttpStatusCode.Forbidden => new ResultError("403", "Forbidden access to the resource"),
                HttpStatusCode.NotFound => new ResultError("404", "The resource was not found"),
                HttpStatusCode.TooManyRequests => new ResultError("429", "Too many requests"),                
                _ => new ResultError("500", "Internal server error occurred")
            };
        }
    }
}

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
            return HttpHelperMethods.GetResultErrorForStatusCode(response.StatusCode);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioService.API.Extensions
{
    public static class FunctionExtensions
    {
        public static async Task<T> GetJsonBodyFromRequest<T>(this HttpRequest req)
        {
            try
            {
                var stream = await new StreamReader(req.Body).ReadToEndAsync();
                var json = JsonConvert.DeserializeObject<T>(stream);
                return json;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Guid GetUserIdFromHeaders(this HttpRequest req)
        {
            var headers = req.Headers;
            Guid userId = Guid.Empty;
            if (headers.TryGetValue("user-id", out var userIdHeaderValue))
            {
                if (Guid.TryParse(userIdHeaderValue, out var id))
                {
                    userId = id;
                }
            }
            else
            {
                throw new NotImplementedException("missing user id");
            }

            return userId;
        }
    }
}

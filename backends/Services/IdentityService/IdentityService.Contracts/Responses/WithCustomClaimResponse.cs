using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace IdentityService.Contracts.Responses
{
    public class WithCustomClaimResponse
    {
        public const string ApiVersion = "1.0.0";

        public WithCustomClaimResponse()
        {
            this.version = WithCustomClaimResponse.ApiVersion;
            this.action = "Continue";
        }

        public WithCustomClaimResponse(string action, string userMessage)
        {
            this.version = WithCustomClaimResponse.ApiVersion;
            this.action = action;
            this.userMessage = userMessage;
            if (action == "ValidationError")
            {
                this.status = "400";
            }
        }


        public string version { get; }
        public string action { get; set; }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string userMessage { get; set; }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string status { get; set; }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string extension_7a7c4096a976419aa22d9e9a2bfb818a_AccountTier { get; set; }
    }
}

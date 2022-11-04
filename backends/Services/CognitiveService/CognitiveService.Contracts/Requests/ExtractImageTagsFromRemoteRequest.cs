using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Contracts.Requests
{
    public class ExtractImageTagsFromRemoteRequest
    {
        public string Url { get; set; }
        
    }
}

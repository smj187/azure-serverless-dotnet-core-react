using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.Services.Models
{
    public class AzureVisionCaptionRequest
    {
        public string Url { get; set; }

        public AzureVisionCaptionRequest(string url)
        {
            Url = url;
        }
    }
}

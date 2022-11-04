using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.Services.Models
{
    public class AzureVisionTagRequest
    {
        public string Url { get; set; }

        public AzureVisionTagRequest(string url)
        {
            Url = url;
        }
    }
}

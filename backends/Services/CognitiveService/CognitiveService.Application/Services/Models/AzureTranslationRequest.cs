using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.Services.Models
{
    public class AzureTranslationRequest
    {
        public string Text { get; set; }

        public AzureTranslationRequest(string text)
        {
            Text = text;
        }
    }

}

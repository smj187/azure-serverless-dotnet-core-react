using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Contracts.Requests
{
    public class TranslationValueRequest
    {
        public string Text { get; set; }
    }

    public class TranslationRequest
    {
        public string Locale { get; set; }
        public List<TranslationValueRequest> Translations { get; set; }
    }
}

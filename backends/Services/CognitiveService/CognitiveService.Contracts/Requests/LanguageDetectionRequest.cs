using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Contracts.Requests
{
    public class LanguageDetectionDocumentRequest
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }

    public class LanguageDetectionRequest
    {
        public List<LanguageDetectionDocumentRequest> Documents { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.DTOs
{
    public class KeyPhraseExtractionDocumentRequestDTO
    {
        public string Id { get; set; }
        public string Language { get; set; }
        public string Text { get; set; }
    }
}

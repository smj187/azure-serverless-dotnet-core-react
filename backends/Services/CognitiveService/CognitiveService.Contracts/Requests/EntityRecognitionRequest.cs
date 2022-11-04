using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Contracts.Requests
{
    public class EntityRecognitionDocumentRequest
    {
        public int Id { get; set; }
        public string Language { get; set; }
        public string Text { get; set; }
    }

    public class EntityRecognitionRequest
    {
        public List<EntityRecognitionDocumentRequest> Documents { get; set; }
    }
}

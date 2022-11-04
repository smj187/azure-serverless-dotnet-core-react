using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.Services.Models
{
    public class AzureEntityRecognitionRequest
    {
        public string Kind { get; init; }
        public AzureEntityRecognitionParameters Parameters { get; init; }
        public AzureEntityRecognitionAnalysisInput AnalysisInput { get; init; }

        public AzureEntityRecognitionRequest()
        {
            Kind = "EntityRecognition";
            Parameters = new AzureEntityRecognitionParameters
            {
                ModelVersion = "2021-06-01"
            };
            AnalysisInput = new AzureEntityRecognitionAnalysisInput
            {
                Documents = new List<AzureEntityRecognitionRequestDocument>()
            };
        }

        public void AddDocument(string id, string language, string text)
        {
            AnalysisInput.Documents.Add(new AzureEntityRecognitionRequestDocument(id, language, text));
        }
    }

    public class AzureEntityRecognitionAnalysisInput
    {
        public List<AzureEntityRecognitionRequestDocument> Documents { get; set; } = new();
    }

    public class AzureEntityRecognitionRequestDocument
    {
        public string Id { get; set; }
        public string Language { get; set; }
        public string Text { get; set; }

        public AzureEntityRecognitionRequestDocument(string id, string language, string text)
        {
            Id = id;
            Language = language;
            Text = text;
        }
    }

    public class AzureEntityRecognitionParameters
    {
        public string ModelVersion { get; set; } = default!;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.Services.Models
{
    public class AzureLanguageDetectionRequest
    {
        public string Kind { get; init; }
        public AzureLanguageDetectionParameters Parameters { get; init; }
        public AzureLanguageDetectionAnalysisInput AnalysisInput { get; init; }

        public AzureLanguageDetectionRequest()
        {
            Kind = "LanguageDetection";
            Parameters = new AzureLanguageDetectionParameters
            {
                ModelVersion = "2022-10-01"
            };
            AnalysisInput = new AzureLanguageDetectionAnalysisInput
            {
                Documents = new List<AzureLanguageDetectionRequestDocument>()
            };
        }

        public void AddDocument(string id, string text)
        {
            AnalysisInput.Documents.Add(new AzureLanguageDetectionRequestDocument(id, text));
        }
    }

    public class AzureLanguageDetectionAnalysisInput
    {
        public List<AzureLanguageDetectionRequestDocument> Documents { get; set; } = new();
    }

    public class AzureLanguageDetectionRequestDocument
    {
        public string Id { get; set; }
        public string Text { get; set; }

        public AzureLanguageDetectionRequestDocument(string id, string text)
        {
            Id = id;
            Text = text;
        }
    }

    public class AzureLanguageDetectionParameters
    {
        public string ModelVersion { get; set; } = default!;
    }
}

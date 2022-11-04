using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.Services.Models
{
    public class AzureKeyPhraseExtractionRequest
    {
        public string Kind { get; init; }
        public AzureKeyPhraseExtractionParameters Parameters { get; init; }
        public AzureKeyPhraseExtractionAnalysisInput AnalysisInput { get; init; }

        public AzureKeyPhraseExtractionRequest()
        {
            Kind = "KeyPhraseExtraction";
            Parameters = new AzureKeyPhraseExtractionParameters
            {
                ModelVersion = "2022-10-01"
            };
            AnalysisInput = new AzureKeyPhraseExtractionAnalysisInput
            {
                Documents = new List<AzureKeyPhraseExtractionRequestDocument>()
            };
        }

        public void AddDocument(string id, string language, string text)
        {
            AnalysisInput.Documents.Add(new AzureKeyPhraseExtractionRequestDocument(id, language, text));
        }
    }

    public class AzureKeyPhraseExtractionAnalysisInput
    {
        public List<AzureKeyPhraseExtractionRequestDocument> Documents { get; set; } = new();
    }

    public class AzureKeyPhraseExtractionRequestDocument
    {
        public string Id { get; set; }
        public string Language { get; set; }
        public string Text { get; set; }

        public AzureKeyPhraseExtractionRequestDocument(string id, string language, string text)
        {
            Id = id;
            Language = language;
            Text = text;
        }
    }

    public class AzureKeyPhraseExtractionParameters
    {
        public string ModelVersion { get; set; } = default!;
    }
}

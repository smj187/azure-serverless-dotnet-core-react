using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CognitiveService.Application.Services.Models
{
    public class AzureLanguageDetectionResponse
    {
        [JsonPropertyName("kind")]
        public string Kind { get; set; }

        [JsonPropertyName("results")]
        public AzureLanguageDetectionResults Results { get; set; }
    }

    public class AzureLanguageDetectionDetectedLanguage
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }


        [JsonPropertyName("iso6391Name")]
        public string Iso6391Name { get; set; }


        [JsonPropertyName("confidenceScore")]
        public double ConfidenceScore { get; set; }
    }

    public class AzureLanguageDetectionResponseDocument
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }


        [JsonPropertyName("detectedLanguage")]
        public AzureLanguageDetectionDetectedLanguage DetectedLanguage { get; set; }


        [JsonPropertyName("statistics")]
        public AzureLanguageDetectionStatistics Statistics { get; set; }


        [JsonPropertyName("warnings")]
        public List<object> Warnings { get; set; }
    }

    public class AzureLanguageDetectionResults
    {
        [JsonPropertyName("statistics")]
        public AzureLanguageDetectionStatistics Statistics { get; set; }


        [JsonPropertyName("documents")]
        public List<AzureLanguageDetectionResponseDocument> Documents { get; set; }


        [JsonPropertyName("errors")]
        public List<object> Errors { get; set; }


        [JsonPropertyName("modelVersion")]
        public string ModelVersion { get; set; }
    }

    public class AzureLanguageDetectionStatistics
    {
        [JsonPropertyName("documentsCount")]
        public int DocumentsCount { get; set; }


        [JsonPropertyName("validDocumentsCount")]
        public int ValidDocumentsCount { get; set; }


        [JsonPropertyName("erroneousDocumentsCount")]
        public int ErroneousDocumentsCount { get; set; }


        [JsonPropertyName("transactionsCount")]
        public int TransactionsCount { get; set; }


        [JsonPropertyName("charactersCount")]
        public int CharactersCount { get; set; }
    }

}

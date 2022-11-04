using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ThirdParty.Json.LitJson;

namespace CognitiveService.Application.Services.Models
{
    public class AzureEntityRecognitionResponse
    {
        [JsonPropertyName("kind")]
        public string Kind { get; set; }

        [JsonPropertyName("results")]
        public AzureEntityRecognitionResults Results { get; set; }
    }

    public class AzureEntityRecognitionResponseDocument
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("statistics")]
        public AzureEntityRecognitionStatistics Statistics { get; set; }

        [JsonPropertyName("entities")]
        public List<AzureEntityRecognitionEntity> Entities { get; set; }

        [JsonPropertyName("warnings")]
        public List<object> Warnings { get; set; }
    }

    public class AzureEntityRecognitionEntity
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("subcategory")]
        public string Subcategory { get; set; }

        [JsonPropertyName("offset")]
        public int Offset { get; set; }

        [JsonPropertyName("length")]
        public int Length { get; set; }

        [JsonPropertyName("confidenceScore")]
        public double ConfidenceScore { get; set; }
    }

    public class AzureEntityRecognitionResults
    {
        [JsonPropertyName("statistics")]
        public AzureEntityRecognitionStatistics Statistics { get; set; }

        [JsonPropertyName("documents")]
        public List<AzureEntityRecognitionResponseDocument> Documents { get; set; }

        [JsonPropertyName("errors")]
        public List<object> Errors { get; set; }

        [JsonPropertyName("modelVersion")]
        public string ModelVersion { get; set; }
    }
    public class AzureEntityRecognitionStatistics
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

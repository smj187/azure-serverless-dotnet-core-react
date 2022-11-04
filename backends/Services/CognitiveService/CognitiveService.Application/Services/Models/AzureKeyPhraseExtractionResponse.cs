using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CognitiveService.Application.Services.Models
{
    public class AzureKeyPhraseExtractionResponse
    {
        [JsonPropertyName("kind")]
        public string Kind { get; set; }

        [JsonPropertyName("results")]
        public AzureKeyPhraseExtractionResults Results { get; set; }
    }

    public class AzureKeyPhraseExtractionResponseDocument
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("keyPhrases")]
        public List<string> KeyPhrases { get; set; }

        [JsonPropertyName("statistics")]
        public AzureKeyPhraseExtractionStatistics Statistics { get; set; }

        [JsonPropertyName("warnings")]
        public List<object> Warnings { get; set; }
    }

    public class AzureKeyPhraseExtractionResults
    {
        [JsonPropertyName("statistics")]
        public AzureKeyPhraseExtractionStatistics Statistics { get; set; }

        [JsonPropertyName("documents")]
        public List<AzureKeyPhraseExtractionResponseDocument> Documents { get; set; }

        [JsonPropertyName("errors")]
        public List<object> Errors { get; set; }

        [JsonPropertyName("modelVersion")]
        public string ModelVersion { get; set; }
    }

    public class AzureKeyPhraseExtractionStatistics
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

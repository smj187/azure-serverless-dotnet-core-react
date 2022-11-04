using Azure.AI.TextAnalytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Transactions;

namespace CognitiveService.Application.Services.Models
{
    public class AzureTranslationResponse
    {
        [JsonPropertyName("detectedLanguage")]
        public AzureTranslationDetectedLanguage DetectedLanguage { get; set; }

        [JsonPropertyName("translations")]
        public List<AzureTranslationResponseResult> Translations { get; set; }
    }

    public class AzureTranslationResponseResult
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("to")]
        public string To { get; set; }
    }

    public class AzureTranslationDetectedLanguage
    {
        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("score")]
        public double Score { get; set; }
    }


}

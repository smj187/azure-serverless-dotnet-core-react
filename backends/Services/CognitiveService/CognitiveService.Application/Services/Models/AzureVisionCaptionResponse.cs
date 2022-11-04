using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CognitiveService.Application.Services.Models
{
    public class AzureVisionCaptionResponse
    {
        [JsonPropertyName("description")]
        public AzureVisionCaptionDescription Description { get; set; }

        [JsonPropertyName("requestId")]
        public string RequestId { get; set; }

        [JsonPropertyName("metadata")]
        public AzureVisionCaptionMetadata Metadata { get; set; }

        [JsonPropertyName("modelVersion")]
        public string ModelVersion { get; set; }
    }

    public class AzureVisionCaptionCaption
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }
    }

    public class AzureVisionCaptionDescription
    {
        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }

        [JsonPropertyName("captions")]
        public List<AzureVisionCaptionCaption> Captions { get; set; }
    }

    public class AzureVisionCaptionMetadata
    {
        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("format")]
        public string Format { get; set; }
    }



}

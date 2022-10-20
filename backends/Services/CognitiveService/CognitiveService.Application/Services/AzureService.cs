using CognitiveService.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.Services
{
    public class AzureService : IAzureService
    {
        private readonly RestClient _client;

        public AzureService(IConfiguration configuration)
        {
            var client = new RestClient($"https://westeurope.tts.speech.microsoft.com");
            client.AddDefaultHeader("Ocp-Apim-Subscription-Key", configuration.GetValue<string>("Azure:Cognitive:ApiKey"));
            client.AddDefaultHeader("User-Agent", "Dotnet");
            client.AddDefaultHeader("X-Microsoft-OutputFormat", "ogg-48khz-16bit-mono-opus");

            _client = client;
        }

        public async Task<Stream> TTS(string value, string language, string voice, string gender)
        {
            var text = $@"
                <speak version='1.0' xml:lang='{language}'>
                    <voice xml:lang='{language}' xml:gender='{gender}' name='{voice}'>
                         {value}
                    </voice>
                </speak>";



            var restRequest = new RestRequest("cognitiveservices/v1", Method.Post)
                .AddBody(text)
                .AddHeader("Content-Type", "application/ssml+xml");

            var restResponse = await _client.PostAsync(restRequest);
            if (restResponse.RawBytes == null)
            {
                throw new NotImplementedException();
            }

            var stream = new MemoryStream(restResponse.RawBytes);
            return stream;
        }
    }
}

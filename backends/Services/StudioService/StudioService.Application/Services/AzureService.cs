using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioService.Application.Services
{
    public class AzureService : IAzureService
    {
        private readonly string KEY = "f67126b813c14c2a96fcadc61adfa867";
        private readonly string REGION = "westeurope";

        private readonly IConfiguration _configuration;
        private readonly IBlobService _blobService;
        private readonly SpeechSynthesizer _speechSynthesizer;
        private readonly RestClient _client;

        public AzureService(IConfiguration configuration, IBlobService blobService)
        {
            _configuration = configuration;
            _blobService = blobService;
            var key = "f67126b813c14c2a96fcadc61adfa867";
            var region = "westeurope";

            var config = SpeechConfig.FromSubscription(key, region);
            config.SpeechSynthesisVoiceName = "en-US-JennyNeural";

            using var stream = AudioOutputStream.CreatePullStream();
            var audio = AudioConfig.FromStreamOutput(stream);

            _speechSynthesizer = new SpeechSynthesizer(config, audio);

            var client = new RestClient($"https://{REGION}.tts.speech.microsoft.com");
            //client.AddDefaultHeader("Authorization", TOKEN);
            client.AddDefaultHeader("Ocp-Apim-Subscription-Key", KEY);
            client.AddDefaultHeader("User-Agent", "Backend");

            _client = client;
        }

        public async Task<string> GernateAudio(string value)
        {
            var lang = "en"; // Specifies the language that you want the neural voice to speak.
            var locale = "en-US"; // required by lang
            var gender = "Male";
            var voice = "en-US-ChristopherNeural";

            //value = "This is my sentence";
            //value = "Hello Users, maybe my other car on wheels is not working.";
            //value= "After you collect and format your dataset.";
            //value= "Let's kick off a training run.";
            value= "initialize the audio processor which is used for feature extraction.";
            var name = "audio5.wav";

            // multiple voices is possible
            // https://learn.microsoft.com/en-us/azure/cognitive-services/speech-service/speech-synthesis-markup?tabs=csharp#use-multiple-voices
            var xml = $@"
                <speak version='1.0' xml:lang='{lang}'>
                    <voice xml:lang='{locale}' xml:gender='{gender}' name='{voice}'>
                         {value}
                    </voice>
                </speak>";


            var withStyle = $@"
                <speak version='1.0' xml:lang='{lang}' xmlns='http://www.w3.org/2001/10/synthesis' xmlns:mstts='http://www.w3.org/2001/mstts' xmlns:emo='http://www.w3.org/2009/10/emotionml'>
                    <voice xml:lang='{locale}' xml:gender='{gender}' name='{voice}'>
                        <mstts:express-as style='shouting'>{value}</mstts:express-as>
                    </voice>
                </speak>";


            // between 0ms and 5000ms
            var withBreak = $@"
                <speak version=""1.0"" xml:lang=""{lang}"" xmlns=""http://www.w3.org/2001/10/synthesis"" xmlns:mstts=""http://www.w3.org/2001/mstts"" xmlns:emo=""http://www.w3.org/2009/10/emotionml"">
                    <voice xml:lang=""{locale}"" xml:gender=""{gender}"" name=""{voice}"">
                        After further delays<break time=""1000ms"" />, the countdown began on January 21, 1968, with launch the following day.
                    </voice>
                </speak>";

            // pause between sentences
            var withPauses = $@"
                <speak version='1.0' xml:lang='{lang}' xmlns='http://www.w3.org/2001/10/synthesis' xmlns:mstts='http://www.w3.org/2001/mstts' xmlns:emo='http://www.w3.org/2009/10/emotionml'>
                    <voice xml:lang='{locale}' xml:gender='{gender}' name='{voice}'>
                        <mstts:silence  type='Sentenceboundary' value='1000ms'/>
                            To have fun along the way.
                            A good place to start
                    </voice>
                </speak>";

            var withDelay = $@"
                <speak version='1.0' xml:lang='{lang}' xmlns='http://www.w3.org/2001/10/synthesis' xmlns:mstts='http://www.w3.org/2001/mstts' xmlns:emo='http://www.w3.org/2009/10/emotionml'>
                    <voice xml:lang='{locale}' xml:gender='{gender}' name='{voice}'>
                        <mstts:silence  type='Leading' value='1000ms'/>
                        <mstts:express-as style='shouting'>A good place to start</mstts:express-as>
                    </voice>
                </speak>";

            // optional: paragraphs and sentences


            var withSpeed = $@"
                <speak version='1.0' xml:lang='{lang}' xmlns='http://www.w3.org/2001/10/synthesis' xmlns:mstts='http://www.w3.org/2001/mstts' xmlns:emo='http://www.w3.org/2009/10/emotionml'>
                    <voice xml:lang='{locale}' xml:gender='{gender}' name='{voice}'>
                        <prosody rate='-50.00%'>
                            Hello World
                        </prosody>
                        <prosody rate='+50.00%'>
                            Hello World
                        </prosody>
                    </voice>
                </speak>";

            

            var withVolumeAndPitch = $@"
                <speak version='1.0' xml:lang='{lang}' xmlns='http://www.w3.org/2001/10/synthesis' xmlns:mstts='http://www.w3.org/2001/mstts' xmlns:emo='http://www.w3.org/2009/10/emotionml'>
                    <voice xml:lang='{locale}' xml:gender='{gender}' name='{voice}'>
                        <prosody volume='+50%'>
                            Hello World
                        </prosody>
                        <prosody volume='-50%'>
                            Hello World
                        </prosody>
                        <prosody pitch='50%'>
                            Hello World
                        </prosody>
                        <prosody pitch='-50%'>
                            Hello World
                        </prosody>

                    </voice>
                </speak>";

            var withBookmark = $@"
                <speak version='1.0' xml:lang='{lang}' xmlns='http://www.w3.org/2001/10/synthesis' xmlns:mstts='http://www.w3.org/2001/mstts' xmlns:emo='http://www.w3.org/2009/10/emotionml'>
                    <voice xml:lang='{locale}' xml:gender='{gender}' name='{voice}'>
                        We are selling <bookmark mark='flower_1'/>roses and <bookmark mark='flower_2'/>daisies
                    </voice>
                </speak>";

            var request = new RestRequest("cognitiveservices/v1", Method.Post)
                .AddBody(xml)
                .AddHeader("Content-Type", "application/ssml+xml")
                .AddHeader("X-Microsoft-OutputFormat", "audio-16khz-128kbitrate-mono-mp3");

            var res = await _client.PostAsync(request);

            //var name = Path.GetRandomFileName() + "_" + DateTimeOffset.UtcNow.ToString("yy-mm-dd").Replace("/", "_") + ".mp3";
            //var name = DateTimeOffset.UtcNow.ToString("yy-MM-dd-HH-mm-ss").Replace("/", "_") + ".mp3";

            

            var url = await _blobService.UploadAudio("files", name, res.RawBytes);

            Console.WriteLine($"pls translate -> {value} :: {url}");

            return url;

        }
    }
}

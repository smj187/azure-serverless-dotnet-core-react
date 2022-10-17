using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StudioService.Application.Services;
using StudioService.API.Extensions;
using StudioService.Contracts.Requests.Project;

namespace StudioService.API.Functions
{
    public record CreateAwsRequest(string Value);

    public class TestFunctions
    {
        private readonly IGoogleService _googleService;
        private readonly IAwsService _awsService;

        public TestFunctions(IGoogleService googleService, IAwsService awsService)
        {
            _googleService = googleService;
            _awsService = awsService;
        }

        [FunctionName("create-aws-voice")]
        public async Task<IActionResult> CreateAwsVoiceAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var body = await req.GetJsonBodyFromRequest<CreateAwsRequest>();

            var res = await _awsService.GernateAudio(body.Value);

            return new OkObjectResult(res);
        }

        [FunctionName("list-google-voices")]
        public async Task<IActionResult> ListGoogleVoicesAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var data = await _googleService.ListGoogleVoicesAsync();

            return new OkObjectResult(data);
        }

        [FunctionName("create-google-text")]
        public async Task<IActionResult> CreateGoogleSynthesis(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var data = await _googleService.GernateAudio("test");

            return new OkObjectResult(data);
        }
    }
}

using Azure.Storage.Blobs;
using BuildingBlocks.Authentication.Extensions;
using BuildingBlocks.BlobStorage.Interfaces;
using BuildingBlocks.BlobStorage.Services;
using BuildingBlocks.Mongo.Extensions;
using CognitiveService.Application.Services;
using CognitiveService.Application.Services.Interfaces;
using CognitiveService.Core.Domain;
using CognitiveService.Infrastructure.BsonClassMapDefinitions;
using CognitiveService.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.Net.Http.Headers;
using System.Net.Http;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();


builder.Services.AddMongoDatabase(builder.Configuration).AddBsonClassMappings();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(Assembly.Load("CognitiveService.Application"));


builder.Services.AddSingleton<IVoiceRepository, VoiceRepository>();

builder.Services.AddSingleton(_ => new BlobServiceClient(builder.Configuration.GetValue<string>("BlobStorage:ConnectionString")));
builder.Services.AddSingleton<IBlobStorageService, BlobStorageService>();


builder.Services.AddAzureAdAuthentication(builder.Configuration);
builder.Services.AddAzureB2cAuthentication(builder.Configuration);

builder.Services.AddSingleton<IAmazonPollyService, AmazonPollyService>();

builder.Services.AddHttpClient<IAzureLanguageService, AzureLanguageService>(client =>
{
    client.BaseAddress = new Uri("https://myazurelanguagestudiodemo.cognitiveservices.azure.com");
    client.DefaultRequestHeaders.Clear();
    client.DefaultRequestHeaders.TryAddWithoutValidation("Ocp-Apim-Subscription-Key", "2a7ebd34d8834e83964add599fe95c1a");
});

builder.Services.AddHttpClient<IAzureTranslationService, AzureTranslationService>(client =>
{
    client.BaseAddress = new Uri("https://api.cognitive.microsofttranslator.com");
    client.DefaultRequestHeaders.Clear();
    client.DefaultRequestHeaders.TryAddWithoutValidation("Ocp-Apim-Subscription-Key", "d08973a89fe4479abc1df03dab81f111");
    client.DefaultRequestHeaders.TryAddWithoutValidation("Ocp-Apim-Subscription-Region", "westeurope");
    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
});

builder.Services.AddHttpClient<IAzureVisionService, AzureVisionService>(client =>
{
    client.BaseAddress = new Uri("https://westus2.api.cognitive.microsoft.com");
    client.DefaultRequestHeaders.Clear();
    client.DefaultRequestHeaders.TryAddWithoutValidation("Ocp-Apim-Subscription-Key", "06d57a3d35f5454cb8ace9c640f2b828");
    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
});

builder.Services.AddHttpClient<IAzureSpeechService, AzureSpeechService>(client =>
{
    client.BaseAddress = new Uri("https://westeurope.tts.speech.microsoft.com");
    client.DefaultRequestHeaders.Clear();
    client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Backend");
    client.DefaultRequestHeaders.TryAddWithoutValidation("Ocp-Apim-Subscription-Key", "f67126b813c14c2a96fcadc61adfa867");
    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/ssml+xml");
    client.DefaultRequestHeaders.TryAddWithoutValidation("X-Microsoft-OutputFormat", "ogg-48khz-16bit-mono-opus");
});

builder.Services.AddHttpClient<IGoogleSpeechService, GoogleSpeechService>(client =>
{
    client.BaseAddress = new Uri("https://texttospeech.googleapis.com");
    client.DefaultRequestHeaders.Clear();
});


var app = builder.Build();
app.UsePathBase(new PathString("/cognitive-service"));
app.UsePathBase(new PathString("/co"));
app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:3001", "https://localhost:3000"));
    app.UseSwagger();
    app.UseSwaggerUI(); 
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
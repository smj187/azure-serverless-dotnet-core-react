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



builder.Services.AddSingleton<IAwsService, AwsService>();
builder.Services.AddSingleton<IGoogleService, GoogleService>();
builder.Services.AddSingleton<IAzureService, AzureService>();


builder.Services.AddSingleton(_ => new BlobServiceClient(builder.Configuration.GetValue<string>("BlobStorage:ConnectionString")));
builder.Services.AddSingleton<IBlobStorageService, BlobStorageService>();


builder.Services.AddAzureAdAuthentication(builder.Configuration);
builder.Services.AddAzureB2cAuthentication(builder.Configuration);

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
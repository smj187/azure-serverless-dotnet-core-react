using Azure.Storage.Blobs;
using BuildingBlocks.BlobStorage.Interfaces;
using BuildingBlocks.BlobStorage.Services;
using BuildingBlocks.Cache.Extensions;
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

builder.Services.AddMongoDatabase(builder.Configuration).AddBsonClassMappings();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(Assembly.Load("CognitiveService.Application"));


builder.Services.AddSingleton<IVoiceRepository, VoiceRepository>();



builder.Services.AddSingleton<IAwsService, AwsService>();
builder.Services.AddSingleton<IGoogleService, GoogleService>();
builder.Services.AddSingleton<IAzureService, AzureService>();


builder.Services.AddSingleton(_ => new BlobServiceClient(builder.Configuration.GetValue<string>("Azure:BlobStorage:ConnectionString")));
builder.Services.AddSingleton<IBlobStorageService, BlobStorageService>();



builder.Services.AddCaching(builder.Configuration);
builder.Services.AddCors();

// Adds Microsoft Identity platform (AAD v2.0) support to protect this Api
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddMicrosoftIdentityWebApi(options =>
        {
            builder.Configuration.Bind("AzureAd", options);
            options.Events = new JwtBearerEvents();

            /// <summary>
            /// Below you can do extended token validation and check for additional claims, such as:
            ///
            /// - check if the caller's tenant is in the allowed tenants list via the 'tid' claim (for multi-tenant applications)
            /// - check if the caller's account is homed or guest via the 'acct' optional claim
            /// - check if the caller belongs to right roles or groups via the 'roles' or 'groups' claim, respectively
            ///
            /// Bear in mind that you can do any of the above checks within the individual routes and/or controllers as well.
            /// For more information, visit: https://docs.microsoft.com/azure/active-directory/develop/access-tokens#validate-the-user-has-permission-to-access-this-data
            /// </summary>
            /// 
            //options.TokenValidationParameters.NameClaimType = "name";

            //options.Events.OnTokenValidated = async context =>
            //{
            //    string[] allowedClientApps = { /* list of client ids to allow */ };

            //    string clientappId = context?.Principal?.Claims
            //        .FirstOrDefault(x => x.Type == "azp" || x.Type == "appid")?.Value;

            //    if (!allowedClientApps.Contains(clientappId))
            //    {
            //        throw new System.Exception("This client is not authorized");
            //    }
            //};
        }, options => { builder.Configuration.Bind("AzureAd", options); });

var app = builder.Build();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3001"));
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 

}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
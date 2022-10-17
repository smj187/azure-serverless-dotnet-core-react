using Azure.Storage.Blobs;
using BuildingBlocks.BlobStorage.Interfaces;
using BuildingBlocks.BlobStorage.Services;
using BuildingBlocks.Mongo.Extensions;
using MediatR;
using System.Reflection;
using WorkspaceService.Application.CommandHandlers;
using WorkspaceService.Core.Domain;
using WorkspaceService.Infrastructure.BsonClassMapDefinitions;
using WorkspaceService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMongoDatabase(builder.Configuration).AddBsonClassMappings();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(Assembly.Load("WorkspaceService.Application"));

builder.Services.AddSingleton(_ => new BlobServiceClient(builder.Configuration.GetValue<string>("Azure:BlobStorage:ConnectionString")));
builder.Services.AddSingleton<IBlobStorageService, BlobStorageService>();

builder.Services.AddSingleton<IProjectRepository, ProjectRepository>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

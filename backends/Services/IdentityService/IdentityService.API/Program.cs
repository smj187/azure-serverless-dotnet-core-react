using BuildingBlocks.Authentication.Extensions;
using BuildingBlocks.Mongo.Extensions;
using IdentityService.Application.Services;
using IdentityService.Application.Services.Interfaces;
using IdentityService.Core.Domain;
using IdentityService.Infrastructure.BsonClassMapDefinitions;
using IdentityService.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Logging;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

builder.Services.AddMongoDatabase(builder.Configuration).AddBsonClassMappings();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(Assembly.Load("IdentityService.Application"));


builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IGraphService, GraphService>();

builder.Services.AddAzureAdAuthentication(builder.Configuration);
builder.Services.AddAzureB2cAuthentication(builder.Configuration);

// The following flag can be used to get more descriptive errors in development environments
//IdentityModelEventSource.ShowPII = false;
//IdentityModelEventSource.ShowPII = true;


var app = builder.Build();
app.UsePathBase(new PathString("/identity-service"));
app.UsePathBase(new PathString("/id"));
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

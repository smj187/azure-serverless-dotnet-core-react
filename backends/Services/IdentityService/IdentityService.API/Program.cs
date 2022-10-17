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
builder.Services.AddCors(o => o.AddPolicy("default", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));


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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("default");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

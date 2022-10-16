using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(o => o.AddPolicy("default", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));


// allow Azure AD and Azure B2C to authenticate on the same API
// https://github.com/AzureAD/microsoft-identity-web/wiki/Web-APIs#using-multiple-authentication-schemes
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddMicrosoftIdentityWebApi(options =>
        {
            options.Events = new JwtBearerEvents();

            options.Events.OnTokenValidated = async context =>
            {
                string[] allowedClientApps = { 
                    // admin-app
                    "fce71c0c-5f69-4b6f-ac2c-79fd015cfe4e",
                };

                string clientappId = context?.Principal?.Claims.FirstOrDefault(x => x.Type == "azp" || x.Type == "appid")?.Value;

                var tid = context.Principal.Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/identity/claims/tenantid") ?? null;

                builder.Configuration.Bind("AzureAd", options);

                if (!allowedClientApps.Contains(clientappId))
                {
                    throw new System.Exception("This client is not authorized");
                }
            };
        },
        options =>
        {
            builder.Configuration.Bind("AzureAd", options);
        });

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder(
        JwtBearerDefaults.AuthenticationScheme,
        "B2CScheme")
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddAuthentication()
        .AddMicrosoftIdentityWebApi(options =>
        {
            options.Events = new JwtBearerEvents();

            options.Events.OnTokenValidated = async context =>
            {
                string[] allowedClientApps = { 
                    // customer-app
                    "df2a7179-a94f-4018-84e2-812ebdf7f148",
                };

                string clientappId = context?.Principal?.Claims.FirstOrDefault(x => x.Type == "azp" || x.Type == "appid")?.Value;


                builder.Configuration.Bind("AzureAdB2C", options);

                if (!allowedClientApps.Contains(clientappId))
                {
                    throw new System.Exception("This client is not authorized");
                }
            };
        },
        options =>
        {
            builder.Configuration.Bind("AzureAdB2C", options);
        }, "B2CScheme");



// The following flag can be used to get more descriptive errors in development environments
//IdentityModelEventSource.ShowPII = false;

var app = builder.Build();

// Configure the HTTP request pipeline.
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

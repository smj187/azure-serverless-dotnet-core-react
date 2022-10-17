using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Authentication.Extensions
{
    public static class ServiceExtensions
    {
        // allow Azure AD and Azure B2C to authenticate on the same API
        // https://github.com/AzureAD/microsoft-identity-web/wiki/Web-APIs#using-multiple-authentication-schemes
        public static IServiceCollection AddAzureAdAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(options =>
                {
                    options.Events = new JwtBearerEvents();

                    options.Events.OnTokenValidated = async context =>
                    {
                        string[] allowedClientApps = { configuration.GetValue<string>("AzureAd:AllowedWebClientId") };

                        string clientappId = context?.Principal?.Claims.FirstOrDefault(x => x.Type == "azp" || x.Type == "appid")?.Value;

                        var tid = context.Principal.Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/identity/claims/tenantid") ?? null;

                        configuration.Bind("AzureAd", options);

                        if (!allowedClientApps.Contains(clientappId))
                        {
                            throw new System.Exception("This client is not authorized");
                        }
                    };
                },
                options =>
                {
                    configuration.Bind("AzureAd", options);
                });

            return services;
        }

        public static IServiceCollection AddAzureB2cAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(
                    JwtBearerDefaults.AuthenticationScheme,
                    "B2CScheme")
                    .RequireAuthenticatedUser()
                    .Build();
            });

            services.AddAuthentication()
                .AddMicrosoftIdentityWebApi(options =>
                {
                    options.Events = new JwtBearerEvents();

                    options.Events.OnTokenValidated = async context =>
                    {
                        string[] allowedClientApps = { configuration.GetValue<string>("AzureAdB2C:AllowedWebClientId") };

                        string clientappId = context?.Principal?.Claims.FirstOrDefault(x => x.Type == "azp" || x.Type == "appid")?.Value;


                        configuration.Bind("AzureAdB2C", options);

                        if (!allowedClientApps.Contains(clientappId))
                        {
                            throw new System.Exception("This client is not authorized");
                        }
                    };
                },
                options =>
                {
                    configuration.Bind("AzureAdB2C", options);
                }, "B2CScheme");


            return services;
        }
    }
}

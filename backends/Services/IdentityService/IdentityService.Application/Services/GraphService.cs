using Azure.Identity;
using IdentityService.Application.Services.Interfaces;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IdentityService.Application.Services
{
    public class GraphService : IGraphService
    {
        private readonly GraphServiceClient _graphClient;

        public GraphService()
        {
            var tenantId = "99ab5d8c-b44b-4ac5-bf69-4a9d71743a64";
            var appId = "366a7c98-1da5-45da-bb7a-b7848b0f9ff1";
            var secret = "niQ8Q~IanRnqg-4sWilwMAJrzBsrphsjdf.WkbK6";

            var scopes = new[] { "https://graph.microsoft.com/.default" };
            var clientSecretCredential = new ClientSecretCredential(tenantId, appId, secret);
            _graphClient = new GraphServiceClient(clientSecretCredential, scopes);
        }

        public async Task<IReadOnlyCollection<User>> ListUsersAsync()
        {
            var users = await _graphClient.Users.Request().Select(x => new
            {
                x.DisplayName,
                x.Id,
                x.Identities,
                x.Mail
            }).GetAsync();

            return users.ToList();
        }
    }
}

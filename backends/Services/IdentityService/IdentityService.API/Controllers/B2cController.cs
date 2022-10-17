using IdentityService.Application.Commands;
using IdentityService.Application.Queries;
using IdentityService.Contracts.Requests;
using IdentityService.Contracts.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class B2cController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private readonly ILogger<B2cController> _logger;

        public B2cController(IMediator mediator, IConfiguration configuration, ILogger<B2cController> logger)
        {
            _mediator = mediator;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost]
        [Route("sign-up-connector")]
        public async Task<IActionResult> SignUpApiConnector([FromBody] WithCustomClaimRequest request)
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                _logger.LogWarning("Missing HTTP basic authentication header.");
                return new UnauthorizedResult();
            }

            var auth = Request.Headers["Authorization"].ToString();

            string encodedUsernamePassword = auth.Substring("Basic ".Length).Trim();
            Encoding encoding = Encoding.GetEncoding("iso-8859-1");
            string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

            int seperatorIndex = usernamePassword.IndexOf(':');

            var authUsername = usernamePassword.Substring(0, seperatorIndex);
            var authPassword = usernamePassword.Substring(seperatorIndex + 1);


            var username = _configuration.GetValue<string>("BASIC_AUTH_USERNAME");
            var password = _configuration.GetValue<string>("BASIC_AUTH_PASSWORD");

            if (string.IsNullOrEmpty(username))
            {
                _logger.LogInformation("HTTP basic authentication is not set.");
                return new UnauthorizedResult();
            }

            if (authUsername != username || authPassword != password)
            {
                return new UnauthorizedResult();
            }

            var command = new CreateUserCommand
            {
                ObjectId = request.ObjectId
            };

            var data = await _mediator.Send(command);
            if (data == null)
            {
                throw new NotImplementedException("cannot create account right now");
            }

            return new OkObjectResult(new WithCustomClaimResponse
            {
                extension_7a7c4096a976419aa22d9e9a2bfb818a_AccountTier = data.AccountTier.Description
            });
        }

        [HttpPost]
        [Route("sign-in-connector")]
        public async Task<IActionResult> SignInApiConnector([FromBody] WithCustomClaimRequest request)
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                _logger.LogWarning("Missing HTTP basic authentication header.");
                return new UnauthorizedResult();
            }

            var auth = Request.Headers["Authorization"].ToString();

            string encodedUsernamePassword = auth.Substring("Basic ".Length).Trim();
            Encoding encoding = Encoding.GetEncoding("iso-8859-1");
            string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

            int seperatorIndex = usernamePassword.IndexOf(':');

            var authUsername = usernamePassword.Substring(0, seperatorIndex);
            var authPassword = usernamePassword.Substring(seperatorIndex + 1);


            var username = _configuration.GetValue<string>("BASIC_AUTH_USERNAME");
            var password = _configuration.GetValue<string>("BASIC_AUTH_PASSWORD");

            if (string.IsNullOrEmpty(username))
            {
                _logger.LogInformation("HTTP basic authentication is not set.");
                return new UnauthorizedResult();
            }

            if (authUsername != username || authPassword != password)
            {
                return new UnauthorizedResult();
            }


            var query = new FindUserAccountTierQuery
            {
                UserId = Guid.Parse(request.ObjectId)
            };

            var data = await _mediator.Send(query);


            return new OkObjectResult(new WithCustomClaimResponse
            {
                extension_7a7c4096a976419aa22d9e9a2bfb818a_AccountTier = data.Description
            });

        }
    }
}

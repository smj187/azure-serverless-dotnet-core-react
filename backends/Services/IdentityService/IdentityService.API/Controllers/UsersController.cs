using AutoMapper;
using IdentityService.Application.Commands;
using IdentityService.Application.Queries;
using IdentityService.Contracts.Requests;
using IdentityService.Contracts.Responses;
using IdentityService.Core.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IMediator mediator, IUserRepository userRepository, IMapper mapper)
        {
            _mediator = mediator;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(_bearer_token);

            var oid = token.Claims.FirstOrDefault(c => c.Type == "oid");

            var query = new FindUserQuery
            {
                UserId = Guid.Parse(oid.Value)
            };

            var data = await _mediator.Send(query);

            return Ok(_mapper.Map<UserProfileResponse>(data));
        }

        [HttpPut]
        [Route("{userId:guid}")]
        public async Task<IActionResult> Test([FromRoute] Guid userId)
        {
            var u = await _userRepository.FindAsync(x => x.B2cObjectId == userId.ToString());
            u.AddEntryToCredit("TTS", "Hello This is a TEst");
            await _userRepository.PatchAsync(u);
            return Ok(u);
        }

        [HttpGet]
        [Route("find/{userid:guid}")]
        [RequiredScopeOrAppPermission(AcceptedScope = new string[] { "admin-privileges" })]
        public async Task<IActionResult> FindUserAsync([FromRoute] Guid userId)
        {
            var query = new FindUserQuery
            {
                UserId = userId
            };

            var data = await _mediator.Send(query);

            return Ok(data);
        }


        [HttpGet]
        [Route("list-users")]
        [RequiredScopeOrAppPermission(AcceptedScope = new string[] { "admin-privileges" })]
        public async Task<IActionResult> ListUsersAsync()
        {
            var query = new ListUserQuery();

            var data = await _mediator.Send(query);

            return Ok(data);
        }


        [HttpGet]
        [Route("list-tiers")]
        [RequiredScopeOrAppPermission(AcceptedScope = new string[] { "admin-privileges" })]
        public async Task<IActionResult> GetAccountTiers()
        {
            var query = new ListAccountTiersQuery();

            var data = await _mediator.Send(query);

            return Ok(data);
        }


        [HttpPatch]
        [Route("account-tier")]
        public async Task<IActionResult> PatchAccountTier([FromBody] PatchUserAccountTierRequest request)
        {
            var command = new PatchAccountTierCommand
            {
                UserId = request.UserId,
                Value = request.Value
            };

            var data = await _mediator.Send(command);

            return Ok(data);
        }


    }
}

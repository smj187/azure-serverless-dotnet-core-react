using IdentityService.Application.Commands;
using IdentityService.Core.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.CommandHandlers
{
    public class PatchAccountTierCommandHandler : IRequestHandler<PatchAccountTierCommand, User>
    {
        private readonly IUserRepository _userRepository;

        public PatchAccountTierCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(PatchAccountTierCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(x => x.B2cObjectId == request.UserId.ToString());
            if (user == null)
            {
                throw new NotImplementedException();
            }

            user.ChangeAccountTier(request.Value);

            await _userRepository.PatchAsync(user);

            return user;
        }
    }
}

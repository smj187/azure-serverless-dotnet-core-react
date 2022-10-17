using IdentityService.Application.Queries;
using IdentityService.Core.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.QueryHandlers
{
    public class FindUserAccountTierQueryHandler : IRequestHandler<FindUserAccountTierQuery, AccountTier>
    {
        private readonly IUserRepository _userRepository;

        public FindUserAccountTierQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AccountTier> Handle(FindUserAccountTierQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(x => x.B2cObjectId == request.UserId.ToString());
            if (user == null)
            {
                throw new NotImplementedException();
            }

            return user.AccountTier;
        }
    }
}

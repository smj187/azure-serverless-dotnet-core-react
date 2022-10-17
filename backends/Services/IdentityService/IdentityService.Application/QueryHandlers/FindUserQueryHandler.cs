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
    public class FindUserQueryHandler : IRequestHandler<FindUserQuery, User>
    {
        private readonly IUserRepository _userRepository;

        public FindUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(FindUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(x => x.B2cObjectId == request.UserId.ToString());
            if (user == null)
            {
                throw new NotImplementedException();
            }


            return user;
        }
    }
}

using IdentityService.Application.Queries;
using IdentityService.Application.Services.Interfaces;
using IdentityService.Core.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.QueryHandlers
{
    public class ListUserQueryHandler : IRequestHandler<ListUserQuery, IReadOnlyCollection<User>>
    {
        private readonly IUserRepository _userRepository;

        public ListUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IReadOnlyCollection<User>> Handle(ListUserQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.ListAsync();
        }
    }
}

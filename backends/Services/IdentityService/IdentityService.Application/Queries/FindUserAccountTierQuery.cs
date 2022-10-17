using IdentityService.Core.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Queries
{
    public class FindUserAccountTierQuery : IRequest<AccountTier>
    {
        public Guid UserId { get; set; }
    }
}

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
    public class ListAccountTiersQueryHandler : IRequestHandler<ListAccountTiersQuery, IReadOnlyCollection<AccountTier>>
    {
        public async Task<IReadOnlyCollection<AccountTier>> Handle(ListAccountTiersQuery request, CancellationToken cancellationToken)
        {
            var accountTiers = new List<AccountTier>
            {
                AccountTier.T0_Free,
                AccountTier.T1_Basic,
                AccountTier.T2_Premium
            };
            return accountTiers;
        }
    }
}

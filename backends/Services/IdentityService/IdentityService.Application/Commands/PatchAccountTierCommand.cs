using IdentityService.Core.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Commands
{
    public class PatchAccountTierCommand : IRequest<User>
    {
        public Guid UserId { get; set; }
        public int Value { get; set; }
    }
}

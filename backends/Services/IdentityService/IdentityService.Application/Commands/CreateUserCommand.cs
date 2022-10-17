using IdentityService.Core.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Commands
{
    public class CreateUserCommand : IRequest<User>
    {
        public string ObjectId { get; set; }
    }
}

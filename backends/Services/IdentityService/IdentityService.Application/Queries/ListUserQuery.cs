﻿using IdentityService.Core.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Queries
{
    public class ListUserQuery : IRequest<IReadOnlyCollection<User>>
    {

    }
}

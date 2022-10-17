using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Services.Interfaces
{
    public interface IGraphService
    {
        Task<IReadOnlyCollection<User>> ListUsersAsync();
    }
}

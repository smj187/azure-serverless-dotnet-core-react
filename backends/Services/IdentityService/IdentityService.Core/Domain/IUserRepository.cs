using BuildingBlocks.Mongo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Core.Domain
{
    public interface IUserRepository : IMongoRepository<User>
    {

    }
}

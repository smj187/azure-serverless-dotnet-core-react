using BuildingBlocks.Mongo.Repositories;
using IdentityService.Core.Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Infrastructure.Repositories
{
    public class UserRepository : MongoRepository<User>, IUserRepository
    {
        public UserRepository(IConfiguration configuration) :
            base(configuration)
        {

        }
    }
}

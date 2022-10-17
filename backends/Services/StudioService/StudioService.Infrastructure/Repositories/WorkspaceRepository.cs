using BuildingBlocks.Mongo.Repositories;
using Microsoft.Extensions.Configuration;
using StudioService.Core.Domain.Workspace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioService.Infrastructure.Repositories
{
    public class WorkspaceRepository : MongoRepository<Workspace>, IWorkspaceRepository
    {
        public WorkspaceRepository(IConfiguration configuration)
            : base(configuration)
        {

        }
    }
}

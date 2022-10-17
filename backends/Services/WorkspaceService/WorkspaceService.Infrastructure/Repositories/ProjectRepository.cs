using BuildingBlocks.Mongo.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkspaceService.Core.Domain;

namespace WorkspaceService.Infrastructure.Repositories
{
    public class ProjectRepository : MongoRepository<Project>, IProjectRepository
    {
        public ProjectRepository(IConfiguration configuration) 
            : base(configuration)
        {

        }
    }
}

using BuildingBlocks.Mongo.Repositories;
using Microsoft.Extensions.Configuration;
using StudioService.Core.Domain.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioService.Infrastructure.Repositories
{
    public class ProjectRepository : MongoRepository<Project>, IProjectRepository
    {
        public ProjectRepository(IConfiguration configuration) 
            : base(configuration)
        {

        }
    }
}

using BuildingBlocks.Mongo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkspaceService.Core.Domain
{
    public interface IProjectRepository : IMongoRepository<Project>
    {

    }
}

using BuildingBlocks.Mongo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioService.Core.Domain.Workspace
{
    public interface IWorkspaceRepository : IMongoRepository<Workspace>
    {

    }
}

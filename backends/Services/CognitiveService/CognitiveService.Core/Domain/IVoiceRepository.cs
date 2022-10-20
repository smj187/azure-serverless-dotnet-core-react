using BuildingBlocks.Mongo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Core.Domain
{
    public interface IVoiceRepository : IMongoRepository<Voice>
    {

    }
}

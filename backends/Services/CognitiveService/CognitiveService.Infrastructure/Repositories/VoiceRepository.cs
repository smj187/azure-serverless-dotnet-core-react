using BuildingBlocks.Mongo.Repositories;
using CognitiveService.Core.Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Infrastructure.Repositories
{
    public class VoiceRepository : MongoRepository<Voice>, IVoiceRepository
    {
        public VoiceRepository(IConfiguration configuration) 
            : base(configuration)
        {

        }
    }
}

using BuildingBlocks.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain
{
    public abstract class AggregateBase : IAggregateBase
    {
        public Guid Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? ModifiedAt { get; set; }
    }
}

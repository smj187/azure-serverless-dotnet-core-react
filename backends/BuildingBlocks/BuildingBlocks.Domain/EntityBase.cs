using BuildingBlocks.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain
{
    public class EntityBase : IEntityBase
    {
        public Guid Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? ModifiedAt { get; set; }
    }
}

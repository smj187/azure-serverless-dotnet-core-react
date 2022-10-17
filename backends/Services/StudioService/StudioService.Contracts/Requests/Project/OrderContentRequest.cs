using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioService.Contracts.Requests.Project
{
    public class OrderContentRequest
    {
        public Guid ContentId { get; set; }
        public int NewIndex { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Contracts.Requests
{
    public class PatchUserAccountTierRequest
    {
        public Guid UserId { get; set; }
        public int Value { get; set; }
    }
}

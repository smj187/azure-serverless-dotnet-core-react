using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Contracts.Requests
{
    public class WithCustomClaimRequest
    {
        public string Step { get; set; }
        public string Client_Id { get; set; }
        public string Ui_Locales { get; set; }
        public string Email { get; set; }
        public string ObjectId { get; set; }
        public string? DisplayName { get; set; }

    }
}

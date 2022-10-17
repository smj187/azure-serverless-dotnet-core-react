using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Contracts.Requests
{
    public class B2CSignUpRequest
    {
        public string step { get; set; }
        public string client_id { get; set; }
        public string ui_locales { get; set; }
        public string email { get; set; }
        public string objectId { get; set; }
    }
}

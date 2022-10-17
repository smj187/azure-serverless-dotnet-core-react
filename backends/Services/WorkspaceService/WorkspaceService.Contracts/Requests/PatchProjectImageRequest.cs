using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkspaceService.Contracts.Requests
{
    public class PatchProjectImageRequest
    {
        public IFormFile Image { get; set; }
    }
}

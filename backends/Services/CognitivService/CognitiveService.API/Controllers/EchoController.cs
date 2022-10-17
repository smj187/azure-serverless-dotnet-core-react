using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EchoController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok(new { Message = "success" });

        [HttpGet("2")]
        [Authorize]
        public IActionResult Get2() => Ok(new { Message = "secure" });
    }
}

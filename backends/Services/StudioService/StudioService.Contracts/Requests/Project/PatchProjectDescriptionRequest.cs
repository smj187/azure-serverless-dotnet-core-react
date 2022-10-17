﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioService.Contracts.Requests.Project
{
    public class PatchProjectDescriptionRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; } = null;
    }
}

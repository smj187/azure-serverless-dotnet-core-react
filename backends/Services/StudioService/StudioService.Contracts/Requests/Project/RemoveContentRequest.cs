﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioService.Contracts.Requests.Project
{
    public class RemoveContentRequest
    {
        public Guid ContentId { get; set; }
    }
}

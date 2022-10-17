﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkspaceService.Core.Domain;

namespace WorkspaceService.Application.Commands
{
    public class PatchAudioBlockIndexCommand : IRequest<AudioBlock>
    {
        public Guid ProjectId { get; set; }
        public Guid AudioBlockId { get; set; }
        public int Index { get; set; }
    }
}

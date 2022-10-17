using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkspaceService.Application.Commands
{
    public class RemoveAudioBlockCommand : IRequest<bool>
    {
        public Guid ProjectId { get; set; }
        public Guid AudioBlockId { get; set; }
    }
}

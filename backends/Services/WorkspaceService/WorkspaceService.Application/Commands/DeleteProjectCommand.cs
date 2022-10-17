using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkspaceService.Application.Commands
{
    public class DeleteProjectCommand : IRequest<bool>
    {
        public Guid ProjectId { get; set; }
    }
}

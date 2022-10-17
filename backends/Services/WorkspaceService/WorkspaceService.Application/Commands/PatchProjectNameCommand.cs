using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkspaceService.Core.Domain;

namespace WorkspaceService.Application.Commands
{
    public class PatchProjectNameCommand : IRequest<Project>
    {
        public Guid ProjectId { get; set; }
        public string Name { get; set; }
    }
}

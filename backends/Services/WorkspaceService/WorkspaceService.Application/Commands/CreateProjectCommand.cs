using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkspaceService.Core.Domain;

namespace WorkspaceService.Application.Commands
{
    public class CreateProjectCommand : IRequest<Project>
    {
        public ProjectType ProjectType { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; }
    }
}

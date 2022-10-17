using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkspaceService.Core.Domain;

namespace WorkspaceService.Application.Queries
{
    public class FindProjectQuery : IRequest<Project?>
    {
        public Guid ProjectId { get; set; }
    }
}

using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkspaceService.Core.Domain;

namespace WorkspaceService.Application.Commands
{
    public class PatchProjectImageCommand : IRequest<Project>
    {
        public Guid ProjectId { get; set; }
        public IFormFile Image { get; set; }
    }
}

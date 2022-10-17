using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkspaceService.Application.Commands;
using WorkspaceService.Core.Domain;

namespace WorkspaceService.Application.CommandHandlers
{
    public class PatchProjectNameCommandHandler : IRequestHandler<PatchProjectNameCommand, Project>
    {
        private readonly IProjectRepository _projectRepository;

        public PatchProjectNameCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Project> Handle(PatchProjectNameCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.FindAsync(request.ProjectId);
            if (project == null)
            {
                throw new NotImplementedException();
            }

            project.PatchName(request.Name);

            await _projectRepository.PatchAsync(project);

            return project;
        }
    }
}

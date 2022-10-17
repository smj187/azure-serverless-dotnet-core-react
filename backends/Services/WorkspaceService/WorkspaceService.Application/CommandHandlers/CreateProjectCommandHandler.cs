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
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Project>
    {
        private readonly IProjectRepository _projectRepository;

        public CreateProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Project> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project(request.ProjectType, request.Name, request.Description);

            await _projectRepository.AddAsync(project);

            return project;
        }
    }
}

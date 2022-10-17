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
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, bool>
    {
        private readonly IProjectRepository _projectRepository;

        public DeleteProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<bool> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.FindAsync(request.ProjectId);
            if (project == null)
            {
                throw new NotImplementedException();
            }

            return await _projectRepository.DeleteAsync(project);
        }
    }
}

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
    public class RemoveAudioBlockCommandHandler : IRequestHandler<RemoveAudioBlockCommand, bool>
    {
        private readonly IProjectRepository _projectRepository;

        public RemoveAudioBlockCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<bool> Handle(RemoveAudioBlockCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.FindAsync(request.ProjectId);
            if (project == null)
            {
                throw new NotImplementedException();
            }

            var isSuccess = project.RemoveAudioBlock(request.AudioBlockId);
            if (isSuccess == false)
            {
                throw new NotImplementedException();
            }

            await _projectRepository.PatchAsync(project);

            return isSuccess;
        }
    }
}

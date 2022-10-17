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
    public class PatchAudioBlockValueCommandHandler : IRequestHandler<PatchAudioBlockValueCommand, AudioBlock>
    {
        private readonly IProjectRepository _projectRepository;

        public PatchAudioBlockValueCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<AudioBlock> Handle(PatchAudioBlockValueCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.FindAsync(request.ProjectId);
            if (project == null)
            {
                throw new NotImplementedException();
            }

            var audioBlock = project.PatchValue(request.AudioBlockId, request.Value);

            await _projectRepository.PatchAsync(project);

            return audioBlock;
        }
    }
}

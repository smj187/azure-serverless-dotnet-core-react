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
    public class PatchAudioBlockVoiceCommandHandler : IRequestHandler<PatchAudioBlockVoiceCommand, AudioBlock>
    {
        private readonly IProjectRepository _projectRepository;

        public PatchAudioBlockVoiceCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<AudioBlock> Handle(PatchAudioBlockVoiceCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.FindAsync(request.ProjectId);
            if (project == null)
            {
                throw new NotImplementedException();
            }

            var block = project.PatchVoiceId(request.AudioBlockId, request.VoiceId);

            await _projectRepository.PatchAsync(project);

            return block;
        }
    }
}

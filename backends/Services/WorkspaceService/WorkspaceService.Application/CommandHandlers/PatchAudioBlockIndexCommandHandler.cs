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
    public class PatchAudioBlockIndexCommandHandler : IRequestHandler<PatchAudioBlockIndexCommand, AudioBlock>
    {
        private readonly IProjectRepository _projectRepository;

        public PatchAudioBlockIndexCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<AudioBlock> Handle(PatchAudioBlockIndexCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.FindAsync(request.ProjectId);
            if (project == null)
            {
                throw new NotImplementedException();
            }

            var block = project.PatchAudioBlockIndex(request.AudioBlockId, request.Index);
            await _projectRepository.PatchAsync(project);

            return block;

        }
    }
}

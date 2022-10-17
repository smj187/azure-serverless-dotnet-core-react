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
    public class AddAudioBlockCommandHandler : IRequestHandler<AddAudioBlockCommand, AudioBlock>
    {
        private readonly IProjectRepository _projectRepository;

        public AddAudioBlockCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<AudioBlock> Handle(AddAudioBlockCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.FindAsync(request.ProjectId);
            if (project == null)
            {
                throw new NotImplementedException();
            }

            var audioBlock = project.AddAudioBlock(request.Index);

            await _projectRepository.PatchAsync(project);

            return audioBlock;
        }
    }
}

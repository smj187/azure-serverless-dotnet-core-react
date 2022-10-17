using BuildingBlocks.BlobStorage.Interfaces;
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
    public class PatchProjectImageCommandHandler : IRequestHandler<PatchProjectImageCommand, Project>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IBlobStorageService _blobStorageService;

        public PatchProjectImageCommandHandler(IProjectRepository projectRepository, IBlobStorageService blobStorageService)
        {
            _projectRepository = projectRepository;
            _blobStorageService = blobStorageService;
        }

        public async Task<Project> Handle(PatchProjectImageCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.FindAsync(request.ProjectId);
            if (project == null)
            {
                throw new NotImplementedException();
            }

            var fileName = request.Image.Name;
            var contentType = request.Image.ContentType;
            Stream stream = new MemoryStream();
            await request.Image.CopyToAsync(stream);

            var m = new MemoryStream();
            request.Image.CopyTo(m);
            m.Position = 0;


            var url = await _blobStorageService.UploadStreamAsync(m, fileName, contentType);

            project.PatchImageUrl(url);
            await _projectRepository.PatchAsync(project);

            return project;
        }
    }
}

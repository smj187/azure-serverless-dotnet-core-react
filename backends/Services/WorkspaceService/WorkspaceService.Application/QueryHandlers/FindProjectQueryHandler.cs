using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkspaceService.Application.Queries;
using WorkspaceService.Core.Domain;

namespace WorkspaceService.Application.QueryHandlers
{
    public class FindProjectQueryHandler : IRequestHandler<FindProjectQuery, Project?>
    {
        private readonly IProjectRepository _projectRepository;

        public FindProjectQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Project?> Handle(FindProjectQuery request, CancellationToken cancellationToken)
        {
            return await _projectRepository.FindAsync(request.ProjectId);
        }
    }
}

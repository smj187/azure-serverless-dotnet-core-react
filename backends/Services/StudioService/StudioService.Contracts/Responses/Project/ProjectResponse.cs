using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioService.Contracts.Responses.Project
{
    public class AudioContentResponse
    {
        public Guid Id { get; set; }
        public int Index { get; set; }
        public string Value { get; set; }
    }
    public class ProjectResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        public List<AudioContentResponse> AudioContent { get; set; } = new List<AudioContentResponse>();

        public DateTimeOffset ModifiedAt { get; set; }
    }

    public class ReorderedAudioContentResponse
    {
        public Guid Id { get; set; }
        public int Index { get; set; }
    }
}

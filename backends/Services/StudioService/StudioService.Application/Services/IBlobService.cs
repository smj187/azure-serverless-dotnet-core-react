using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioService.Application.Services
{
    public interface IBlobService
    {
        Task<string> UploadFileAsStream(string containerName, string fileName, Stream fileStream, string contentType);
        Task<string> UploadAudio(string containerName, string fileName, byte[] file);
    }
}

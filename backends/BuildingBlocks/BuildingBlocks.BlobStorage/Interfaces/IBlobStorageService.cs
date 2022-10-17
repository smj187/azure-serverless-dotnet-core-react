using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.BlobStorage.Interfaces
{
    public interface IBlobStorageService
    {
        Task<string> UploadStreamAsync(Stream fileStream, string fileName, string contentType);
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.BlobStorage.Interfaces
{
    public interface IBlobStorageService
    {
        Task<string> UploadSpeechStreamAsync(Stream fileStream, string fileName, string contentType);
        Task<bool> DeleteSpeechBlobAsync(string fileName);


        Task<string> UploadVoicePreviewAsset(string fileName, IFormFile audio);
        Task<string> UploadVoiceAvatarAsset(string fileName, IFormFile audio);
        Task<bool> DeleteVoiceAssetBlob(string fileName);
    }
}

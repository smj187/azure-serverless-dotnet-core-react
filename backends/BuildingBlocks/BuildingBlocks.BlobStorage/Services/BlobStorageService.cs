using Azure.Core;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BuildingBlocks.BlobStorage.Extensions;
using BuildingBlocks.BlobStorage.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BuildingBlocks.BlobStorage.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobServiceClient _client;
        private readonly BlobContainerClient _voiceAssetsContainer;
        private readonly BlobContainerClient _speechSynthesisContainer;

        public BlobStorageService(BlobServiceClient client, IConfiguration configuration)
        {
            _client = client;

            _voiceAssetsContainer = _client.GetBlobContainerClient(configuration.GetValue<string>("BlobStorage:VoiceAssetsContainerName"));
            _speechSynthesisContainer = _client.GetBlobContainerClient(configuration.GetValue<string>("BlobStorage:SpeechSynthesisContainerName"));

            _voiceAssetsContainer.CreateIfNotExists();
            _voiceAssetsContainer.SetAccessPolicy(PublicAccessType.Blob);

            _speechSynthesisContainer.CreateIfNotExists();
            _speechSynthesisContainer.SetAccessPolicy(PublicAccessType.Blob);
        }

        public async Task<string> UploadVoiceAvatarAsset(string fileName, IFormFile image)
        {
            // check valid file type
            var validTypes = new List<string> { "image/jpeg", "image/jpg", "image/png", "image/gif", "image/webp" };
            if (!validTypes.Contains(image.ContentType))
            {
                throw new NotImplementedException($"{image.ContentType} is not supported");
            }

            // get stream
            var bytes = await image.GetBytes();
            using var stream = new MemoryStream(bytes);

            var blob = _voiceAssetsContainer.GetBlobClient(fileName);

            await blob.UploadAsync(stream, new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = image.ContentType,
                }
            });

            return blob.Uri.AbsoluteUri;
        }
       

        public async Task<string> UploadVoicePreviewAsset(string fileName, IFormFile audio)
        {
            // check valid file type
            var validTypes = new List<string> { "audio/wave", "audio/ogg" };
            if (!validTypes.Contains(audio.ContentType))
            {
                throw new NotImplementedException($"{audio.ContentType} is not supported");
            }

            // get stream
            var bytes = await audio.GetBytes();
            using var stream = new MemoryStream(bytes);

            var blob = _voiceAssetsContainer.GetBlobClient(fileName);

            await blob.UploadAsync(stream, new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = audio.ContentType,
                }
            });

            return blob.Uri.AbsoluteUri;
        }

        public async Task<bool> DeleteVoiceAssetBlob(string fileName)
        {
            var blob = _voiceAssetsContainer.GetBlobClient(fileName);
            var res = await blob.DeleteIfExistsAsync();
            return res.Value;
        }





        public async Task<string> UploadSpeechStreamAsync(Stream fileStream, string fileName, string contentType)
        {
            var blob = _speechSynthesisContainer.GetBlobClient(fileName);

            var res = await blob.UploadAsync(fileStream, new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = contentType
                }
            });

            return blob.Uri.AbsoluteUri;
        }

    

        public async Task<bool> DeleteSpeechBlobAsync(string fileName)
        {
            var blob = _speechSynthesisContainer.GetBlobClient(fileName);
            var res = await blob.DeleteIfExistsAsync();
            return res.Value;
        }
    }
}

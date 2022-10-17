using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using MongoDB.Driver.Core.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioService.Application.Services
{
    public class BlobService : IBlobService
    {
        private readonly string _str = "DefaultEndpointsProtocol=https;AccountName=testaudiofiles23;AccountKey=oiKuCKVcXvfhZ2D2YK0Ng5cpAFgeF+xQMCtR1h4mFTFrjRXlUEsuOhef7y0vZc4WGS3k4dF2oRvs+AStNElYEg==;EndpointSuffix=core.windows.net";
        
        public async Task<string> UploadFileAsStream(string containerName, string fileName, Stream fileStream, string contentType)
        {
            // create container reference
            var container = new BlobContainerClient(_str, containerName);
            await container.CreateIfNotExistsAsync();
            await container.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

            // upload file
            var blob = container.GetBlobClient(fileName);

            var blobHttpHeader = new BlobHttpHeaders
            {
                ContentType = contentType
            };

            await blob.UploadAsync(fileStream, new BlobUploadOptions
            {
                HttpHeaders = blobHttpHeader,
            });

            return blob.Uri.AbsoluteUri;
        }

        public async Task<string> UploadAudio(string containerName, string fileName, byte[] file)
        {
            // create container reference
            var container = new BlobContainerClient(_str, containerName);
            await container.CreateIfNotExistsAsync();
            await container.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

            // upload file
            var blob = container.GetBlobClient(fileName);

            // convert files byte array to stream
            var stream = new MemoryStream(file);

            var blobHttpHeader = new BlobHttpHeaders
            {
                ContentType = "audio/wav"
            };

            await blob.UploadAsync(stream, new BlobUploadOptions
            {
                HttpHeaders = blobHttpHeader,
            });

            return blob.Uri.AbsoluteUri;
        }
    }
}

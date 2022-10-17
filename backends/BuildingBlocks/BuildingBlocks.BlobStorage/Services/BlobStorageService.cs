using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BuildingBlocks.BlobStorage.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.BlobStorage.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobServiceClient _client;
        private readonly BlobContainerClient _containerClient;

        public BlobStorageService(BlobServiceClient client, IConfiguration configuration)
        {
            _client = client;

            var containerName = configuration.GetValue<string>("Azure:BlobStorage:ContainerName");

            var container = _client.GetBlobContainerClient(containerName);
            container.CreateIfNotExists();
            container.SetAccessPolicy(PublicAccessType.Blob);
            _containerClient = container;
        }


        public async Task<string> UploadStreamAsync(Stream fileStream, string fileName, string contentType)
        {
            var blob = _containerClient.GetBlobClient(fileName);

            var res = await blob.UploadAsync(fileStream, new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = contentType
                }
            });

            return blob.Uri.AbsoluteUri;
        }
    }
}

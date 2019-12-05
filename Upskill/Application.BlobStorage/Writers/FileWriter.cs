﻿using System.Threading.Tasks;
using Application.BlobStorage.Providers;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Application.BlobStorage.Writers
{
    public class FileWriter : IFileWriter
    {
        private readonly CloudBlobClient _blobClient;

        public FileWriter(IBlobClientProvider blobClientProvider)
        {
            _blobClient = blobClientProvider.Get();
        }

        public async Task<string> Save(string containerName, byte[] content, string contentType, string fileName, string extension)
        {
            var container = _blobClient.GetContainerReference(containerName);
            await container.CreateIfNotExistsAsync();

            var blob = container.GetBlockBlobReference($"{fileName}.{extension}");
            blob.Properties.ContentType = contentType;

            await blob.UploadFromByteArrayAsync(content, 0, content.Length);

            return blob.Uri.AbsoluteUri;
        }
    }
}
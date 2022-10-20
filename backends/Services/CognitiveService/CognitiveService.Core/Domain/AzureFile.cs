using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Core.Domain
{
    public class AzureFile : ValueObject
    {
        public AzureFile(string url, string name, string contentType, long size)
        {
            Url = url; 
            Name = name;
            ContentType = contentType; 
            Size = size;
        }

        public string Url { get; set; }
        public string ContentType { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioService.Application.Services
{
    public interface IAzureService
    {
        Task<string> GernateAudio(string value);
    }
}

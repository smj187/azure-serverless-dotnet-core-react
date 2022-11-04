using CognitiveService.Application.DTOs;
using CognitiveService.Application.Services.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.Queries
{
    public class FindKeyPhrasesQuery : IRequest<AzureKeyPhraseExtractionResponse>
    {
        public List<KeyPhraseExtractionDocumentRequestDTO> Documents { get; set; }
    }
}

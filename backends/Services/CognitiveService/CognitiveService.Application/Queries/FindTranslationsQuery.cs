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
    public class FindTranslationsQuery : IRequest<IReadOnlyCollection<AzureTranslationResponse>>
    {
        public string Locale { get; set; }
        public List<TranslationRequestDTO> Translations { get; set; }
    }
}

﻿using CognitiveService.Application.Services.Models;
using MediatR;
using Microsoft.CognitiveServices.Speech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.Queries
{
    public class FindAzureVoicesQuery : IRequest<IReadOnlyCollection<AzureSpeechVoiceResponse>>
    {

    }
}

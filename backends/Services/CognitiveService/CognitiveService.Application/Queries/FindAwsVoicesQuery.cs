using CognitiveService.Infrastructure.ProviderResponses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.Queries
{
    public class FindAwsVoicesQuery : IRequest<IReadOnlyCollection<AwsVoiceResponse>>
    {

    }
}

using CognitiveService.Core.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Application.Queries
{
    public class ListVoicesQuery : IRequest<IReadOnlyCollection<Voice>>
    {

    }
}

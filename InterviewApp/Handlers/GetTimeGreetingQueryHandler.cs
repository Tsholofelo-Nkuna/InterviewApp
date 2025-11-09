using InterviewApp.Constants;
using InterviewApp.Models;
using InterviewApp.Queries;
using InterviewApp.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewApp.Handlers
{
    public class GetTimeGreetingQueryHandler(IGreetingService greetingService) : IRequestHandler<GetTimeGreetingQuery, string>
    {
        public Task<string> Handle(GetTimeGreetingQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(greetingService.Run());
        }
    }
}

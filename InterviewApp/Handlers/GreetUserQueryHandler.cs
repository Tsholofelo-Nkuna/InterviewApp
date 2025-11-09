using InterviewApp.Queries;
using InterviewApp.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewApp.Handlers
{
    public class GreetUserQueryHandler(IGreetingService greetingService) : IRequestHandler<GreetUserQuery, string>
    {
        public Task<string> Handle(GreetUserQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(greetingService.Run());
        }
    }
}

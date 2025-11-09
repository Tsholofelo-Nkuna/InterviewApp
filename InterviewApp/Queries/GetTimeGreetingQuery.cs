using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewApp.Queries
{
    public class GetTimeGreetingQuery: IRequest<string>
    {
    }
}

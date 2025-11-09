using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewApp.Services
{
    public interface ITimeGreetingService : IGreetingService
    {
        string GetTimeOfDay(TimeOnly currentTime);
    }
}

using InterviewApp.Constants;
using InterviewApp.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace InterviewApp.Services
{
    public class TimeGreetingService : GreetingService, ITimeGreetingService
    {
        private readonly LanugageSetting _options;
        private readonly ILogger<GreetingService> _logger;
        public TimeGreetingService(IOptions<LanugageSetting> options, ILogger<GreetingService> logger) : base(options, logger, null)
        {
            _logger = logger;
            _options = options.Value;
        }

        public override string Run()
        {
            var currentLanguage = !LanguageIsSupported(_options.Language) ? _options.DefaultLanguage : _options.Language;
            var resManager = new ResourceManager($"{languageResourcePrefix}.{currentLanguage}", Assembly.GetExecutingAssembly());
           
            if (currentLanguage == _options.DefaultLanguage && !LanguageIsSupported(currentLanguage))
            {
                _logger.LogError("DefaultLanguage not set.\n");
                return string.Empty;
            }
            else
            {
               
                var timeOfDay = GetTimeOfDay(TimeOnly.FromDateTime(DateTime.Now));
                if (!string.IsNullOrWhiteSpace(timeOfDay))
                {
                    return $"{resManager.GetString(timeOfDay)}";
                }
                else
                {
                    _logger.LogError("Current time is not within the range of supported times");
                    return string.Empty;
                }
                    
            }
        }

        public string GetTimeOfDay(TimeOnly currentTime)
        {
            if(currentTime.IsBetween(TimeOnly.FromDateTime(new DateTime(2025,1,1,6,0,0)), TimeOnly.FromDateTime(new DateTime(2025, 1, 1, 12, 0, 0))))
            {
                return LanguageKey.Morning;
            }
            else if (currentTime.IsBetween(TimeOnly.FromDateTime(new DateTime(2025, 1, 1, 12, 0, 1)), TimeOnly.FromDateTime(new DateTime(2025, 1, 1, 17, 0, 0))))
            {
                return LanguageKey.Afternoon;
            }
            else if (currentTime.IsBetween(TimeOnly.FromDateTime(new DateTime(2025, 1, 1, 17, 0, 1)), TimeOnly.FromDateTime(new DateTime(2025, 1, 1, 19, 0, 0))))
            {
                return LanguageKey.Evening;
            }
            else if (currentTime.IsBetween(TimeOnly.FromDateTime(new DateTime(2025, 1, 1, 19, 0, 1)), TimeOnly.FromDateTime(new DateTime(2025, 1, 1, 23, 0, 59))))
            {
                return LanguageKey.Night;
            }
            else
            {
                return string.Empty;
            }


        }
    }
}

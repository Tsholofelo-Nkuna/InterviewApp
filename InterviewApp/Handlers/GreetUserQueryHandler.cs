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
    public class GreetUserQueryHandler(IOptions<LanguageSetting> _langSettingsOptions, IGreetingService greetingService,ILogger<GreetUserQueryHandler> logger) : IRequestHandler<GreetUserQuery, string>
    {
        public Task<string> Handle(GreetUserQuery request, CancellationToken cancellationToken)
        {
            var currentLanguage = !greetingService.LanguageIsSupported(_langSettingsOptions.Value.Language) ? _langSettingsOptions.Value.DefaultLanguage : _langSettingsOptions.Value.Language;
            if (!string.IsNullOrWhiteSpace(currentLanguage)) {

                var resManager = new ResourceManager($"InterviewApp.Languages.{currentLanguage}", Assembly.GetExecutingAssembly());
                return Task.FromResult(resManager.GetString(LanguageKey.Morning));
            }
            else
            {
                logger.LogError("DefaultLanguage not set.\n");   
                return Task.FromResult(string.Empty);
            }
        }
    }
}

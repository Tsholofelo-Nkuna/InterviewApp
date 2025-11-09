using System;
using Microsoft.Extensions.Options;
using InterviewApp.Models;
using System.Resources;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;
using InterviewApp.Constants;

namespace InterviewApp.Services
{
    public class GreetingService(IOptions<LanugageSetting> options, ILogger<GreetingService> logger, ITimeGreetingService timeGreetingService) : IGreetingService
    {
        private readonly LanugageSetting _options = options.Value;
        private readonly ILogger<GreetingService> _logger = logger;
        protected string languageResourcePrefix = "InterviewApp.Languages";
        
        public IEnumerable<string> GetSupportedLanguages()
        {
            var languages = Directory.GetFiles("./Languages")
                .Select(absoluteFileName => Regex.Match(absoluteFileName,@".+(?=\.)").Value)
                .Select(x => x.Split(@"\",StringSplitOptions.RemoveEmptyEntries).LastOrDefault());
            return languages;
        }
   
        public virtual string Run()
        {
            try
            {
               
                _logger.LogInformation($"{nameof(GreetingService)} started.\n");
                _logger.LogInformation($"Supported languages: {string.Join(", ", GetSupportedLanguages())}\n");
             
                var activeLanguage = _options.Language?.ToLower();
                var defaultLanguage = _options.DefaultLanguage?.ToLower();
                string message = string.Empty;
                ResourceManager resManager = null;
                if (LanguageIsSupported(activeLanguage))
                {
                    resManager = new ResourceManager($"{languageResourcePrefix}.{activeLanguage}", Assembly.GetExecutingAssembly());
                    message = resManager.GetString(LanguageKey.Messsage);
                   
                }
                else 
                {
                    _logger.LogInformation($"Specified language ({_options.Language}) not found. Defaulting to {_options.DefaultLanguage}\n");
                    if (string.IsNullOrWhiteSpace(defaultLanguage))
                    {
                        _logger.LogError("DefaultLanguage not set.\n");
                    }
                    else
                    {
                        resManager = new ResourceManager($"{languageResourcePrefix}.{defaultLanguage}", Assembly.GetExecutingAssembly());
                        message = resManager.GetString(LanguageKey.Messsage);
                    }
                }
                var combinedMessage = $"{timeGreetingService.Run()}, {message}";
                 _logger.LogInformation($"Display message: {combinedMessage}\n");
                Console.WriteLine(combinedMessage);
                  return message;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Error occured: {ex.Message}");
                return string.Empty;
            }
               
        }

        protected bool LanguageIsSupported(string language)
        {
            if(language is not null)
            {
                return GetSupportedLanguages().Any(langStr => langStr.Equals(language, StringComparison.OrdinalIgnoreCase));
            }
            else
            {
                return false;
            }
        }
    }
}
using System.Collections.Generic;

namespace InterviewApp.Services;

public interface IGreetingService
{
    string Run();
    IEnumerable<string> GetSupportedLanguages();
    public bool LanguageIsSupported(string language);


}
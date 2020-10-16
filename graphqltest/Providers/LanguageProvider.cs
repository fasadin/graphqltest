using System.Threading.Tasks;
using graphqltest.Common.Data;

namespace graphqltest.Providers
{
    public class LanguageProvider : ILanguageProvider
    {
        public Task<Language> GetLanguage(string language)
        {
            throw new System.NotImplementedException();
        }
    }
}
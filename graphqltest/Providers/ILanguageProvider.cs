using System.Threading.Tasks;
using graphqltest.Common.Data;

namespace graphqltest.Providers
{
    public interface ILanguageProvider
    {
        Task<Language> GetLanguage(string language);
    }
}
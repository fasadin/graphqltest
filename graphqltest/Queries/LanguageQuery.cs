using System;
using System.Net.Http;
using System.Threading.Tasks;
using graphqltest.Common.Data;
using graphqltest.Data.Inputs;
using HotChocolate.Types;
using Newtonsoft.Json;

namespace graphqltest.Queries
{
    [ExtendObjectType(Name = "Query")]
    public class LanguageQuery
    {
        private readonly HttpClient _httpClient;

        public LanguageQuery(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Language> GetLanguage(LanguageInput languageInput)
        {
            var result = await _httpClient.GetAsync(
                $"language/get?language={languageInput.Indicator}"
                );

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception(result.StatusCode.ToString());
            }
            var content = await result.Content.ReadAsStringAsync();

            var language = JsonConvert.DeserializeObject<Language>(content);

            return language;
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using graphqltest.Data;
using graphqltest.Providers;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;

namespace graphqltest.Queries
{
    [ExtendObjectType(Name = "Query")]
    public class PostQuery
    {
        [Authorize]
        public async Task<List<Post>> GetPosts([Service] IPostProvider postProvider)
        {
            var result = await postProvider.GetPosts();
            return result;
        }
    }
}
using System;
using System.Threading.Tasks;
using graphqltest.Data.Inputs;
using graphqltest.Data.Responses;
using graphqltest.Providers;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;

namespace graphqltest.Mutations
{
    
    [ExtendObjectType(Name = "Mutation")]
    public class PostMutation
    {
        [Authorize]
        public async Task<PostResponse> CreateNewPost(
            PostInput postInput,
            [Service] IPostProvider postProvider)
        {
            var READMEFROMTOKEN = new Guid();
            var result = await postProvider.CreatePost(postInput, READMEFROMTOKEN);
            return result;
        }
    }
}
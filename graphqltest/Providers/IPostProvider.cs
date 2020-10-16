using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using graphqltest.Data;
using graphqltest.Data.Inputs;
using graphqltest.Data.Responses;

namespace graphqltest.Providers
{
    public interface IPostProvider
    {
        public Task<List<Post>> GetPosts();
        public Task<PostResponse> CreatePost(PostInput postInput, Guid performUserId);
    }
}
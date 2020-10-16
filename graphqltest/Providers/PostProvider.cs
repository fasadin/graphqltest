using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using graphqltest.Data;
using graphqltest.Data.Inputs;
using graphqltest.Data.Responses;
using Microsoft.EntityFrameworkCore;

namespace graphqltest.Providers
{
    public class PostProvider : IPostProvider
    {
        private readonly graphqltestContext _graphqltestContext;

        public PostProvider(graphqltestContext graphqltestContext)
        {
            _graphqltestContext = graphqltestContext;
        }


        public async Task<PostResponse> CreatePost(PostInput postInput, Guid performUserId)
        {
            var newPost = new Post
            {
                Text = postInput.Text, OwnerId = performUserId
            };

            await _graphqltestContext.Posts.AddAsync(newPost);
            await _graphqltestContext.SaveChangesAsync();

            return new PostResponse
            {
                Id = newPost.Id
            };
        }

        public async Task<List<Post>> GetPosts()
        {
            var result = await _graphqltestContext.Posts.ToListAsync();
            return result;
        }
    }
}
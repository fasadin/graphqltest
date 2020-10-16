using System;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace graphqltest.Data
{
    public class Post
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Text { get; set; } = "";
    }
}
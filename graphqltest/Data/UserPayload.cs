using System;

namespace graphqltest.Common.Data
{
    public class UserPayload
    {
        public UserPayload(Guid id, string token)
        {
            Id = id;
            Token = token;
        }
        public Guid Id { get; }
        public string Token { get; }
    }
}
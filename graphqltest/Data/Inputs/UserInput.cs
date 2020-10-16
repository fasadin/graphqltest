using System;

namespace graphqltest.Data.Inputs
{
    public class UserInput
    {
        public UserInput(string nickname, Guid id)
        {
            Nickname = nickname;
            Id = id;
        }
        public string? Nickname { get; }
        public Guid? Id { get; }
    }
}
using System;
using System.Threading.Tasks;
using graphqltest.Common.Data;
using graphqltest.Data.Inputs;

namespace graphqltest.Providers
{
    public interface IUserProvider
    {
        Task ChangeNickname(UserInput userInput, Guid userId);
        Task<User> GetUserDetails(Guid userId);
    }
}
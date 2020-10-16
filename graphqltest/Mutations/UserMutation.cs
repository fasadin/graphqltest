using System;
using System.Threading.Tasks;
using graphqltest.Data.Inputs;
using graphqltest.Providers;
using HotChocolate;

namespace graphqltest.Mutations
{
    //[Authorize]
    //[ExtendObjectType(Name = "Mutation")]
    public class UserMutation
    {
        public async Task ChangeNickname(
            UserInput userInput,
            [Service]IUserProvider userProvider)
        {
            string fromTokenId = "hereReadFromTokenDetailsAndGetId";
            await userProvider.ChangeNickname(userInput, Guid.Parse(fromTokenId));
            //TODO: Might be that all mutation has to return something to be working
        }
    }
}
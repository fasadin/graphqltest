using System;
using System.Linq;
using System.Threading.Tasks;
using graphqltest.Common.Data;
using graphqltest.Data;
using graphqltest.Data.Inputs;
using Microsoft.EntityFrameworkCore;

namespace graphqltest.Providers
{
    public class UserProvider :IUserProvider
    {
        private readonly graphqltestContext _context;

        public UserProvider(graphqltestContext context)
        {
            _context = context;
        }

        public async Task ChangeNickname(UserInput userInput, Guid userId)
        {
            var userDetails = await _context.Users.Where(user => user.Id == userId).SingleAsync();
            userDetails.Nickname = userInput.Nickname;
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserDetails(Guid userId)
        {
            var userDetails = await _context.Users.Where(user => user.Id == userId).SingleAsync();

            return userDetails;
        }
    }
}
using System;

namespace graphqltest.Common.Data
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Nickname { get; set; }
        public string? Password { get; set; }
        public DateTime? PasswordValidTill { get; set; }
        public string PhoneNumber { get; set; }
    }
}
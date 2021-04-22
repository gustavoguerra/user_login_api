using Dapper.Contrib.Extensions;

namespace Login.Domain.Model

{
    [Table("UserConfiguration")]
    public class UserLoginDomain
    {
        [Write(false)]
        public int UserId { get; set; }
        public int SystemId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SocialNumber { get; set; }
        public string CellPhoneNumber { get; set; }
        public string Email { get; set; }
        public string UserPassword { get; set; }
        public bool Active { get; set; }
        
    }
}

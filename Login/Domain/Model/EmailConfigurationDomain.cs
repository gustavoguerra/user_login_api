using Dapper.Contrib.Extensions;

namespace Login.Domain.Model
{

    [Table("EmailConfiguration")]
    public class EmailConfigurationDomain
    {
        public string SmtpClient { get; set; }
        public int Port { get; set; }
        public string EmailCredencial { get; set; }
        public string PasswordCredencial { get; set; }
        public bool EnableSsl { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public bool SetDefault { get; set; }
    }
}

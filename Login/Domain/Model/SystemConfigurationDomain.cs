using Dapper.Contrib.Extensions;

namespace Login.Domain.Model
{
    [Table("SystemConfiguration")]
    public class SystemConfigurationDomain
    {        
        public int SystemId { get; set; }
        public string SystemName { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ExpirationInMinuts { get; set; }
        public string KeyHash { get; set; }
    }
}

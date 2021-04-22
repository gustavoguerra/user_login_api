using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Domain
{
    public sealed class TokenConfiguration
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ExpirationInMinuts { get; set; }
        public string Hash { get; set; }
    }
}

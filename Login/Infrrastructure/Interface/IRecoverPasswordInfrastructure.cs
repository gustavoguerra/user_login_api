using Login.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Infrrastructure.Interface
{
    public interface IRecoverPasswordInfrastructure
    {
        void SendNewPasswordToEmail(UserLoginDomain user, EmailConfigurationDomain credencials);

    }
}

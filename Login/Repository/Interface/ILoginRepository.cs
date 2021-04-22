using Login.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Repository.Interface
{
    public interface ILoginRepository
    {
        SystemConfigurationDomain GetSystemConfigurationById(int SystemId);

        EmailConfigurationDomain GetEmailConfigurationBySystemId(int SystemId);

    }
}

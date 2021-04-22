using Login.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Repository.Interface
{
    public interface IUserLoginRepository
    {
        void CreateUser(UserLoginDomain user);

        UserLoginDomain GetUsuarioByUsername(string username, int SystemId);

        UserLoginDomain GetUserByToken(string token);

        void UpdateNewPassword(UserLoginDomain user);
    }
}

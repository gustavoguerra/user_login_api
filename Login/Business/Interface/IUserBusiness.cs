using Login.Domain.DTO;
using Login.Domain.Model;

namespace Login.Business.Interface
{
    public interface IUserBusiness
    {
        void CreateUser(UserLoginDomain user);

        void RecoverPassword(RecoverPasswordDTO recover);
    }
}

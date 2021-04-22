using Login.Domain.Model;
using Login.DTO;

namespace Login.Business.Interface
{
    public interface ILoginBusiness
    {
        string Auth(LoginDTO login);

        void ValidateToken(string token);

        string RefreshToken(string token);
    }
}

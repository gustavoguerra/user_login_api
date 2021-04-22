using Login.Business.Interface;
using Login.Domain;
using Login.DTO;
using Login.Helpers;
using Login.Repository.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Login.Business
{
    public class LoginBusiness : ILoginBusiness
    {
        protected readonly IUserLoginRepository _repository;
        protected readonly ILoginRepository _loginRepository;

        public LoginBusiness (IUserLoginRepository repository, ILoginRepository loginRepository)
        {
            _repository = repository;
            _loginRepository = loginRepository;
        }

        public string Auth(LoginDTO login)
        {
            login.Password = SegurancaBusiness.CriarHashSenha(login.Password);

            if (!login.UserName.Contains('@'))
            {
                login.UserName = login.UserName.Replace(".", "").Replace("-", "");
            }
            var user = _repository.GetUsuarioByUsername(login.UserName, login.SystemId);

            DomainException.When(user == null || user.UserPassword != login.Password, ErrorMessages.USUARIO_ERRO);

            var tokenconfiguration = GetTokenConfiguration(user.SystemId);

            var token = SegurancaBusiness.GerarToken(tokenconfiguration, user);

            return token;
        }

        public string RefreshToken(string token)
        {
            ValidateToken(token);

            var user = _repository.GetUserByToken(token);

            var tokenconfiguration = GetTokenConfiguration(user.SystemId);

            var Token = SegurancaBusiness.GerarToken(tokenconfiguration, user);

            return Token;
        }

        public void ValidateToken(string token)
        {
            var user = _repository.GetUserByToken(token);

            DomainException.When(user == null, ErrorMessages.TOKEN_ERRO);

            var tokenconfiguration = GetTokenConfiguration(user.SystemId);

            SegurancaBusiness.VerificarToken(token,tokenconfiguration);
        }

        private TokenConfiguration GetTokenConfiguration(int SystemId)
        {
            var systemconfiguration = _loginRepository.GetSystemConfigurationById(SystemId);
            var tokenconfiguration = new TokenConfiguration();

            tokenconfiguration.Audience = systemconfiguration.Audience;
            tokenconfiguration.Issuer = systemconfiguration.Issuer;
            tokenconfiguration.ExpirationInMinuts = systemconfiguration.ExpirationInMinuts;
            tokenconfiguration.Hash = systemconfiguration.KeyHash;

            return tokenconfiguration;
        }
    }
}

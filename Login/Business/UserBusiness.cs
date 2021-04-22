using Login.Business.Interface;
using Login.Domain.DTO;
using Login.Domain.Model;
using Login.Helpers;
using Login.Infrrastructure.Interface;
using Login.Repository.Interface;

namespace Login.Business
{
    public class UserBusiness : IUserBusiness
    {
        protected readonly IUserLoginRepository _repository;
        protected readonly IRecoverPasswordInfrastructure _infrastructure;
        protected readonly ILoginRepository _loginrepository;

        public UserBusiness (IUserLoginRepository repository, IRecoverPasswordInfrastructure infrastructure, ILoginRepository loginrepository)
        {
            _repository = repository;
            _infrastructure = infrastructure;
            _loginrepository = loginrepository;
        }

        public void CreateUser(UserLoginDomain user)
        {
            ValidateUser(user);
            user.SocialNumber = user.SocialNumber.Replace(".", "").Replace("-", "");

            var UserReturn = _repository.GetUsuarioByUsername(user.SocialNumber, user.SystemId);

            DomainException.When(UserReturn != null, ErrorMessages.CPF_CNPJ_CADASTRADA);

            user.UserPassword = SegurancaBusiness.CriarHashSenha(user.UserPassword);

            _repository.CreateUser(user);
        }

        public void RecoverPassword(RecoverPasswordDTO recover)
        {
            var user = _repository.GetUsuarioByUsername(recover.Email, recover.SystemId);

            if (user != null)
            {
                var newpassword = SegurancaBusiness.GeraSenhaRandon();

                user.UserPassword = SegurancaBusiness.CriarHashSenha(newpassword);

                _repository.UpdateNewPassword(user);

                user.UserPassword = newpassword;

                var credencials = _loginrepository.GetEmailConfigurationBySystemId(user.SystemId);

                _infrastructure.SendNewPasswordToEmail(user, credencials);
            }
        }

        private void ValidateUser(UserLoginDomain user)
        {
            DomainException.When(string.IsNullOrEmpty(user.FirstName), ErrorMessages.ERROR_FIRST_NAME);
            DomainException.When(string.IsNullOrEmpty(user.LastName), ErrorMessages.ERROR_LAST_NAME);
            DomainException.When(string.IsNullOrEmpty(user.SocialNumber), ErrorMessages.ERROR_SOCIAL_NUMBER);
            DomainException.When(string.IsNullOrEmpty(user.CellPhoneNumber), ErrorMessages.ERROR_CELLPHONE);
            DomainException.When(string.IsNullOrEmpty(user.Email), ErrorMessages.ERROR_EMAIL_NULL);
            DomainException.When(!user.Email.Contains("@"), ErrorMessages.ERROR_EMAIL);
            DomainException.When(string.IsNullOrEmpty(user.UserPassword), ErrorMessages.ERROR_PASSWORD);
            DomainException.When(user.UserPassword.Length < 8, ErrorMessages.ERROR_LENGTH_PASSWORD);
        }
    }
}

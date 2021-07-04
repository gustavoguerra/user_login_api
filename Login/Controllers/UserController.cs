using Login.Business.Interface;
using Login.Domain.DTO;
using Login.Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Login.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        protected readonly IUserBusiness _business;

        public UserController(IUserBusiness business)
        {
            _business = business;
        }

        [HttpPost("createuser")]
        public IActionResult CreateUser([FromBody] UserLoginDomain user)
        {
             _business.CreateUser(user);

            return Ok();
        }

        [HttpPost("recoverpassword")]
        public IActionResult RecoverPassword([FromBody] RecoverPasswordDTO recover)
        {
            _business.RecoverPassword(recover);

            return Ok();
        }
    }
}

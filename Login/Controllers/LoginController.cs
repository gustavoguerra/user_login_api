using Login.Business.Interface;
using Login.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Login.Controllers
{
    [Route("/")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        protected readonly ILoginBusiness _business;

        public LoginController(ILoginBusiness business)
        {
            _business = business;
        }

        [HttpPost("Auth")]
        public IActionResult Auth([FromBody] LoginDTO user)
        {
            var result = _business.Auth(user);

            return Ok(result);
        }

        [HttpGet("validatetoken/{token}")]
        public IActionResult ValidateToken(string token)
        {
            _business.ValidateToken(token);

            return Ok();
        }

        [HttpGet("refreshtoken/{token}")]
        public IActionResult RefreshToken(string token)
        {
            var resultToken = _business.RefreshToken(token);

            return Ok(resultToken);
        }
    }
}

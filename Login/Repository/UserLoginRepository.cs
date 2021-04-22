using Dapper;
using Dapper.Contrib.Extensions;
using Login.Domain.Model;
using Login.Repository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Repository
{
    public class UserLoginRepository : IUserLoginRepository
    {
        private readonly IConfiguration _config;
        protected IDbConnection Conn => new SqlConnection(_config.GetConnectionString("SqlConnection"));

        public UserLoginRepository(IConfiguration config)
        {
            _config = config;
        }

        public void CreateUser(UserLoginDomain user)
        {
            Conn.Insert(user);
        }

        public UserLoginDomain GetUsuarioByUsername(string username, int SystemId)
        {
            var query = @"SELECT * FROM UserConfiguration WHERE (SocialNumber = @CPF OR Email = @EMAIL) AND SystemId = @SYSTEMID";

            var parameter = new DynamicParameters();

            parameter.Add("@EMAIL", username);
            parameter.Add("@CPF", username);
            parameter.Add("@SYSTEMID", SystemId);

            return Conn.QueryFirstOrDefault<UserLoginDomain>(query, parameter);
        }

        public UserLoginDomain GetUserByToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            var idUser = jwtToken.Claims.FirstOrDefault(a => a.Type.Equals("id")).Value;

            var query = @"SELECT * FROM UserConfiguration WHERE UserId = @USERID";

            var parameter = new DynamicParameters();

            parameter.Add("@USERID", Convert.ToInt32(idUser));

            return Conn.QueryFirstOrDefault<UserLoginDomain>(query, parameter);
        }

        public void UpdateNewPassword(UserLoginDomain user)
        {
            var query = @"UPDATE UserConfiguration set UserPassword = @PASSWORD WHERE UserId = @USERID";

            var parameter = new DynamicParameters();

            parameter.Add("@PASSWORD", user.UserPassword);
            parameter.Add("@USERID", user.UserId);

            Conn.Query(query, parameter);
        }
    }
}

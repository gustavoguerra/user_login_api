using Dapper;
using Dapper.Contrib.Extensions;
using Login.Domain.Model;
using Login.Repository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IConfiguration _config;
        protected IDbConnection Conn => new SqlConnection(_config.GetConnectionString("SqlConnection"));

        public LoginRepository(IConfiguration config)
        {
            _config = config;
        }

        public SystemConfigurationDomain GetSystemConfigurationById(int SystemId)
        {
            var query = @"select * from SystemConfiguration where SystemId = @SystemId";

            var parameter = new DynamicParameters();

            parameter.Add("@SystemId", SystemId);

            return Conn.QueryFirstOrDefault<SystemConfigurationDomain>(query, parameter);      
        }

        public EmailConfigurationDomain GetEmailConfigurationBySystemId(int SystemId)
        {
            var query = @"select * from EmailConfiguration where SystemId = @SystemId AND SetDefault = 1";

            var parameter = new DynamicParameters();

            parameter.Add("@SystemId", SystemId);

            return Conn.QueryFirstOrDefault<EmailConfigurationDomain>(query, parameter);
        }
    }
}

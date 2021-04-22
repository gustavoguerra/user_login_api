using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace Login.Extencions
{
    public static class SweggerExtendions
    {
        public static void SwaggerServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "User & Login",
                    Version = "v1",
                    Description = "User & Login",
                    Contact = new OpenApiContact
                    {
                        Name = "Only R - Only Research",
                        Email = "luisgustavoguerra@gmail.com",
                        Url = new Uri("https://github.com/gustavoguerra/GestaoBackEndAPI")
                    }
                });
                var caminhoAplicacao =
                PlatformServices.Default.Application.ApplicationBasePath;
                var caminhoXmlDoc =
                    Path.Combine(caminhoAplicacao, $"Login.xml");
            });
        }

        public static void SwaggerApplication(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Login User");
                c.RoutePrefix = "api/docs";
            });
        }
    }
}

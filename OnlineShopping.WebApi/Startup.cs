using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineShopping.Business.DependencyResolvers.Microsoft;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using OnlineShopping.WebApi.Filters;

namespace OnlineShopping.WebApi
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(config =>
            {
                config.Filters.Add<SecurityExceptionFilter>();
                config.Filters.Add<ValidationExceptionFilter>();
            });
            services.AddBusinessModule();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, config =>
            {
                config.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateActor = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    SaveSigninToken = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("eXVzdWYgYWtiYcWf")),
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                try
                {
                    string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                    JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                    byte[] key = Encoding.UTF8.GetBytes("eXVzdWYgYWtiYcWf");
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);
                    JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;
                    context.User = new ClaimsPrincipal(new ClaimsIdentity(jwtToken.Claims));
                    Thread.CurrentPrincipal = context.User;
                }
                catch
                {
                }
                await next();
            });

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

    }
}

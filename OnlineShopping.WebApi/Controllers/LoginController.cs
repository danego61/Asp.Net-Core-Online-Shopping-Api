using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineShopping.Business.Abstract;
using OnlineShopping.Core.Utils;
using OnlineShopping.Entities.Concrete;
using OnlineShopping.WebApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShopping.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IUserService userService;

        public LoginController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public IActionResult Login(UserModel model)
        {
            if (model.Email == "danego61@gmail.com" && model.Password == "parola123")
            {
                return Ok(GetToken(0, "Admin"));
            }
            User user = userService.GetEmployee(model.Email);
            user ??= userService.GetCustomer(model.Email);
            if (user != null)
            {
                if (!user.IsEmailVerified)
                    return Ok("Pleasa verify mail address!");
                if (PasswordHashUtils.IsPasswordMatch(model.Password, user.Password))
                {

                    return Ok(GetToken(user.UserID, user is Employee _ ? "Employee" : "Customer"));
                }
                else
                {
                    return BadRequest("Username or password is incorrect!");
                }

            }
            return BadRequest();
        }

        private string GetToken(int userID, string role)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, userID.ToString()),
                new Claim(ClaimTypes.Role, role)
            };
            JwtSecurityToken token = new(
                expires: DateTime.Now.AddDays(3),
                notBefore: DateTime.Now,
                claims: claims,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("eXVzdWYgYWtiYcWf")), SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

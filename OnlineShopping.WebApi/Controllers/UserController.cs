using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Business.Abstract;
using OnlineShopping.Entities.Concrete;
using OnlineShopping.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("{userID:int}")]
        public IActionResult GetUser(int userID)
        {
            Customer customer = userService.GetCustomer(userID);
            if (customer == null)
                return BadRequest();
            if (!customer.IsEmailVerified)
                return Ok("Please verify mail address!");
            return Ok(new UserModel
            {
                UserID = customer.UserID,
                Name = customer.Name,
                Surname = customer.Surname,
                Email = customer.Email
            });
        }

        [HttpGet("Verify/{guid}")]
        public IActionResult VerifyUserEmail(Guid guid)
        {
            if (userService.VerifyUserEmail(guid))
                return Ok();
            return NotFound();
        }

        [HttpPost]
        public IActionResult CreateUser(UserModel model)
        {
            Customer customer = userService.CreateCustomer(new()
            {
                Email = model.Email,
                Name = model.Name,
                Surname = model.Surname,
                Password = model.Password,
                IsEmailVerified = false,
                IsPasswordChange = false
            });
            model.UserID = customer.UserID;
            return CreatedAtAction(nameof(GetUser), new { userID = customer.UserID }, model);
        }

        [HttpPut]
        public IActionResult UpdateUser(UserModel model)
        {
            userService.UpdateCustomer(new()
            {
                UserID = model.UserID,
                Name = model.Name,
                Surname = model.Surname,
                Password = model.Password
            });
            return Ok(model);
        }

        [HttpGet("Employee/{userID:int}")]
        public IActionResult GetEmployee(int userID)
        {
            Employee employee = userService.GetEmployee(userID);
            if (employee == null)
                return BadRequest();
            if (employee.IsPasswordChange)
                return Ok(new UserModel
                {
                    UserID = employee.UserID,
                    IsPasswordChange = true
                });
            return Ok(new EmployeeModel
            {
                UserID = employee.UserID,
                Name = employee.Name,
                Surname = employee.Surname,
                Email = employee.Email,
                EmployeeType = employee.EmployeeType.ToString(),
                IsWorking = employee.EmployeeStatus == Entities.EmployeeStatus.Working
            });
        }

        [HttpPost("Employee")]
        public IActionResult CreateEmployee(EmployeeModel model)
        {
            Employee employee = userService.CreateEmployee(new()
            {
                Email = model.Email,
                Name = model.Name,
                Surname = model.Surname,
                Password =model.Password,
                IsEmailVerified = true,
                IsPasswordChange = true,
                EmployeeStatus = Entities.EmployeeStatus.Working,
                EmployeeType = Enum.Parse<Entities.EmployeeType>(model.EmployeeType)
            });
            return CreatedAtAction(nameof(GetEmployee), new { userID = employee.UserID }, model);
        }

        [HttpPut("Employee")]
        public IActionResult UpdateEmployee(EmployeeModel model)
        {
            userService.UpdateEmployee(new()
            {
                UserID = model.UserID,
                Name = model.Name,
                Surname = model.Surname,
                Password = model.Password,
                EmployeeStatus = model.IsWorking ? Entities.EmployeeStatus.Working : Entities.EmployeeStatus.Leave
            });
            return Ok(model);
        }

    }

}

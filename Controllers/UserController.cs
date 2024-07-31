using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using Restul_Web_Assessment.Interfaces;
using Restul_Web_Assessment.Repository.PostModels;

namespace Restul_Web_Assessment.Controllers
{
    public class UserController: ControllerBase 
    {
        private readonly IUser _login;
        public UserController(IUser login) 
        {
            _login = login;
        }


        [HttpGet("Login")]
        public IActionResult Login(string idNumber, string password)
        {
            var token = _login.LoginAuthorize(idNumber, password);
            if(token == string.Empty)
            {
                return NotFound("Invalid login credentials, or user does not exist");
            }
            return Ok(token);
        }

        [HttpPost("RegisterUser")]
        public IActionResult RegisterUser(UserDTO user)
        {
            var results = _login.PostUser(user);
            if (results != null)
                return Ok(results);
            else
                return BadRequest();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using GameBackend.Models;
using System.Linq;
using System.Collections.Generic;

namespace GameBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private static List<User> _users = new List<User>();  //? List static will remove when restart server then if dont want to delete just use real database

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            if (_users.Any(u => u.Username == request.Username))
            {
                return BadRequest("Username already exists.");
            }

            _users.Add(new User { Username = request.Username, Password = request.Password });
            return Ok("Registration successful");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var user = _users.FirstOrDefault(u => u.Username == request.Username && u.Password == request.Password);
            if (user == null) return Unauthorized("Invalid username or password");

            return Ok("Login successful");
        }
    }
}

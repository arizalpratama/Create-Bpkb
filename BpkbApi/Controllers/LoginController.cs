using BpkbApi.Data;
using BpkbApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BpkbApi.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly BpkbDbContext _context;

        public LoginController(BpkbDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Login([FromBody] MsUser user)
        {
            var loginUser = _context.MsUsers.FirstOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
            if (loginUser == null || !loginUser.IsActive)
            {
                return Unauthorized("Invalid credentials");
            }

            return Ok(new { Username = loginUser.UserName });
        }
    }
}

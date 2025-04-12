using book_diplom2.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace book_diplom2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Конструктор с внедрением зависимости
        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginRequest request)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Username == request.Username && u.UserPassword == request.UserPassword);

            if (user == null)
            {
                return Unauthorized("Неверный логин или пароль");
            }

            return Ok(new { Message = "Успешный вход", UserId = user.UserId });
        }

        public class LoginRequest
        {
            public string Username { get; set; }
            public string UserPassword { get; set; }
        }

        [HttpGet("user/{id}")]
        public ActionResult GetUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null) return NotFound();
            return Ok(new { username = user.Username });
        }
    }
}
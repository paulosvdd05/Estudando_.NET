using Microsoft.AspNetCore.Mvc;
using PrimeiraAPI.Application.Services;

namespace PrimeiraAPI.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : Controller
    {
        [HttpPost]
        public IActionResult Auth(string username, string password)
        {
            if (username == "admin" && password == "1234")
            {
                var token = TokenService.GenerateToken(new Domain.Model.Employee());
                return Ok(token);
            }
            return BadRequest("Usuario ou senha incorreto.");
        }
    }
}

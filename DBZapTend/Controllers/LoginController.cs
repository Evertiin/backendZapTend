using DBZapTend.Models;
using DBZapTend.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DBZapTend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public LoginController(IUserRepository repository)
        {
            _repository = repository;
        }
        [HttpPost]
        public ActionResult<User> Login(User user)
        {
            if (user.Email == "admin" && user.Password == "admin" )
            {
                return Ok(new {token = ""});
            }
            return BadRequest();
        }

    }
}

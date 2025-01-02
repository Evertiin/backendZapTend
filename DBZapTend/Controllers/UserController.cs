using DBZapTend.Models;
using DBZapTend.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DBZapTend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var category = await _repository.GetUsers();
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUsers(User user)
        {
            if (user is null)
            {
                return BadRequest("Dados inválidos");
            }
            var createdUser = await _repository.CreateUser(user);


            return Ok(createdUser);
        }

        [HttpGet("{id:int}")]

        public async Task<ActionResult<User>> GetUserId(int id)
        {
            var user = await _repository.GetUser(id);

            if (user == null)
            {
                return NotFound("Usuário não encontrado");
            }


            return Ok(user);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<User>> UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest("Dados inválidos");
            }

            var updateUser = await _repository.UpdateUser(user);


            return Ok(updateUser);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<User>> DeleteCategoryy(int id)
        {
            var user = _repository.DeleteUser(id);

            if (user == null)
            {
                return NotFound("Usuário não encontrada");
            }
            var deleteUser = await _repository.DeleteUser(id);


            return Ok(deleteUser);
        }
    }
}

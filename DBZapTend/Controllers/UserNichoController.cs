using DBZapTend.Models;
using DBZapTend.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DBZapTend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserNichoController : Controller
    {
        private readonly IUserNichoRepository _repository;

        public UserNichoController(IUserNichoRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserNicho>>> GetUserNichos()
        {
            var userNicho = await _repository.GetUserNichos();
            return Ok(userNicho);
        }

        [HttpPost]
        public async Task<ActionResult<UserNicho>> CreateUserNicho(UserNicho userNicho)
        {
            if (userNicho is null)
            {
                return BadRequest("Dados inválidos");
            }
            var createdUserNicho = await _repository.CreateUserNicho(userNicho);


            return Ok(createdUserNicho);
        }

        [HttpGet("{id:int}")]

        public async Task<ActionResult<UserNicho>> GetUserNichoId(int id)
        {
            var userNicho = await _repository.GetUserNicho(id);

            if (userNicho == null)
            {
                return NotFound("Não encontrado");
            }


            return Ok(userNicho);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<UserNicho>> UpdateUserNichos(int id, UserNicho userNicho)
        {
            if (id != userNicho.IdUserNichos)
            {
                return BadRequest("Dados inválidos");
            }

            var updateUserNicho = await _repository.CreateUserNicho(userNicho);


            return Ok(updateUserNicho);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<UserNicho>> DeleteUserNichos(int id)
        {
            var userNicho = _repository.GetUserNicho(id);

            if (userNicho == null)
            {
                return NotFound("Não encontrada");
            }
            var deleteUserNicho = await _repository.DeleteUserNicho(id);


            return Ok(deleteUserNicho);
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using DemoService.Ef.Repositories.Users;
using DemoService.Ef.UOF;
using DemoService.Models;

namespace DemoService.Controllers {

    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IUsersRepository _repository;

        public DemoController(IUnitOfWork unitOfWork, IUsersRepository repository) {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllAsync() =>
            Ok(await _repository.GetAllAsync());

        [HttpPost("users")]
        public async Task<IActionResult> AddAsync([FromBody] CreateUser user) {

            await _repository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateAsync([BindRequired, FromRoute] int id, [FromBody] UpdateUser users) {

            var user = await _repository.GetAsync(id);
            if (user == null)
                return NotFound($"User id [{id}] not found.");

            user.Name = users.Name;

            await _repository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteAsync([BindRequired, FromRoute] int id) {

            var user = await _repository.GetAsync(id);
            if (user == null)
                return NotFound($"User id [{id}] not found.");

            await _repository.DeleteAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

    }
}
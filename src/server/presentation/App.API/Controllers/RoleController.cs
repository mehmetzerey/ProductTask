using App.Application.ModelView.RoleView;
using App.Application.Repositories.Role;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IWriteRoleRepository _roleRepository;

        public RoleController(IWriteRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        // POST api/<RoleController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoleRequestViewModel createRoleRequest)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _roleRepository.AddRoleAsync(createRoleRequest.Role));
        }
    }
}

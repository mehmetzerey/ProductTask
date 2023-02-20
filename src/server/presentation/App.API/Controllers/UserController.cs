using App.Application.ModelView.UserView;
using App.Application.Repositories.User;
using App.Domain.Enums;
using App.Domain.Models;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class UserController : ControllerBase
    {
        private readonly IWriteUserRepository _writeUserRepository;
        private readonly IReadUserRepository _readUserRepository;

        public UserController(IWriteUserRepository writeUserRepository, IReadUserRepository readUserRepository)
        {
            _writeUserRepository = writeUserRepository;
            _readUserRepository = readUserRepository;
        }

        // GET: api/<UserController>
        [HttpGet]
        [Route("GetAll")]
        public async Task<List<GetUserViewModel>> GetAll(int role)
        {
            var result = await _readUserRepository.GetUserByRole(role);

            return result.Select(x => new GetUserViewModel
            {
                Email = x.Email,
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname
            }).ToList();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByEmail(int id)
        {
            var user = await _readUserRepository.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            var response = new GetUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname
            };

            return Ok(response);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post(CreateUserViewModel model)
        {
            #region Validate model
            if (!ModelState.IsValid)
            {
                var responseViewModel = new ServiceResponse();
                responseViewModel.IsSuccess = false;
                responseViewModel.Message = "Bilgileriniz eksik, bazı alanlar gönderilmemiş. Lütfen tüm alanları doldurunuz.";

                return BadRequest(responseViewModel);
            }
            #endregion

            var applicationUserModel = new ApplicationUser();
            applicationUserModel.Email = model.Email;
            applicationUserModel.Name = model.Name;
            applicationUserModel.Surname = model.Surname;
            applicationUserModel.UserName = model.Email;

            var userRole = RoleEnum.From(model.Role);

            var result = await _writeUserRepository.AddUserAsync(applicationUserModel, model.Password, userRole);
            return Ok(result);
        }

        // PUT api/<UserController>/5
        [HttpPut]
        public async Task<IActionResult> Put(UpdateUserViewModel model)
        {
            var responseViewModel = new ServiceResponse();

            #region Validate model
            if (!ModelState.IsValid)
            {
                responseViewModel.IsSuccess = false;
                responseViewModel.Message = "Bilgileriniz eksik, bazı alanlar gönderilmemiş. Lütfen tüm alanları doldurunuz.";

                return BadRequest(responseViewModel);
            }
            #endregion

            var user = await _readUserRepository.FindByEmail(model.Email);
            if (user == null)
            {
                responseViewModel.IsSuccess = false;
                responseViewModel.Message = "Kullanıcı bulunamadı.";
                return NotFound(responseViewModel);
            }
            else
            {
                user.Name = model.Name;
                user.Surname = model.Surname;

                var response = await _writeUserRepository.UpdateUserAsync(user);
                return Ok(response);
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var responseViewModel = new ServiceResponse();
            var user = await _readUserRepository.FindByIdAsync(id);

            if (user == null)
            {
                responseViewModel.IsSuccess = false;
                responseViewModel.Error = "Kullanıcı bulunamadı";
                return NotFound(responseViewModel);
            }

            var result = await _writeUserRepository.DeleteUserAsync(user);
            return Ok(result);
        }
    }
}

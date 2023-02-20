using App.Application.Authentication;
using App.Application.ModelView;
using App.Application.Repositories.User;
using App.Domain.Models;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using App.Domain.Enums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IWriteUserRepository _writeUserRepository;
        private readonly IAuthService _authService;

        public AuthController(IWriteUserRepository writeUserRepository, IAuthService authService)
        {
            _writeUserRepository = writeUserRepository;
            _authService = authService;
        }

        // POST: api/<AccountController>
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
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
            var result = await _writeUserRepository.AddUserAsync(applicationUserModel, model.Password, RoleEnum.Member);
            return Ok(result);

        }
        // POST: api/<AccountController>
        [HttpPost]
        [Route("RegisterWithRole")]
        public async Task<IActionResult> Register(RegisterViewModel model, int role = 2)
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

            var roleEnum = RoleEnum.From(role);

            var applicationUserModel = new ApplicationUser();
            applicationUserModel.Email = model.Email;
            applicationUserModel.Name = model.Name;
            applicationUserModel.Surname = model.Surname;
            applicationUserModel.UserName = model.Email;
            var result = await _writeUserRepository.AddUserAsync(applicationUserModel, model.Password, roleEnum);
            return Ok(result);

        }

        // POST api/<AccountController>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            #region Validate

            if (ModelState.IsValid == false)
            {
                var responseViewModel = new ServiceResponse();
                responseViewModel.IsSuccess = false;
                responseViewModel.Message = "Bilgileriniz eksik, bazı alanlar gönderilmemiş. Lütfen tüm alanları doldurunuz.";
                return BadRequest(responseViewModel);
            }
            #endregion
            var result = await _authService.SignInAsync(model.Email, model.Password, false, false);
            return Ok(result);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Registration.Application.Services.Register;
using Registration.Domein.Entities.DTOs;
using Registration.Domein.Entities.Models;

namespace Registration.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RegisterCRUD : ControllerBase
    {
        private readonly IRegisterService _service;

        public RegisterCRUD(IRegisterService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<string> CreateEmail(SignUp signUp)
        {
            return await _service.CreateEmailAsync(signUp);
        }

        [HttpPost("checkEmail")]
        public Task<string> CheckEmailAsync(LoginDTO login)
        {
            return _service.CheckEmailAsync(login);
        }
        [HttpPost("password")]
        public Task<string> CheckCodeAsync(int num)
        {
            return _service.CheckCodeAsync(num);
        }

    }
}

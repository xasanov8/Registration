using Registration.Domein.Entities.DTOs;
using Registration.Domein.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.Application.Services.Register
{
    public interface IRegisterService
    {
        public Task<string> CreateEmailAsync(SignUp signUp);
        public Task<string> CheckEmailAsync(LoginDTO login);
        public Task<string> CheckCodeAsync(int num);
    }
}

using Microsoft.EntityFrameworkCore;
using Registration.Application.Services.EmailService;
using Registration.Domein.Entities.DTOs;
using Registration.Domein.Entities.Models;
using Registration.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.Application.Services.Register
{
    public class RegisterService : IRegisterService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmileService _emileService;

        public RegisterService(ApplicationDbContext context, IEmileService emileService)
        {
            _context = context;
            _emileService = emileService;
        }

        public async Task<string> CreateEmailAsync(SignUp signUp)
        {
            if (signUp != null)
            {
                var model = await _context.Logins.Select(x => x.Email).FirstOrDefaultAsync(x => x == signUp.Email);
                if (model == null)
                {
                    if (signUp.Password == signUp.ConfirmPassword)
                    {
                        _context.Logins.AddAsync(new Login
                        {
                            Email = signUp.Email,
                            Password = signUp.Password,
                        });
                        await _context.SaveChangesAsync();
                        return "Accepted";
                    }
                    else
                    {
                        return "Error ConfirmPassword";
                    }
                }
                else
                {
                    return "Error Dublikate Email";
                }
            }
            else
            {
                return "Sign Up NULL";
            }
        }

        public async Task<string> CheckEmailAsync(LoginDTO login)
        {
            if (login != null)
            {
                var model = await _context.Logins.FirstOrDefaultAsync(x => x.Email == login.Email);
                if (model != null)
                {
                    if (model.Password == login.Password)
                    {
                        Random random = new Random();
                        int num = random.Next(10000, 99999);

                        var result = await _context.Codes.FirstOrDefaultAsync(x => x.Id == 1);
                        if (result == null)
                        {
                            await _context.Codes.AddAsync(new Code
                            {
                                code = num
                            });
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            result.code = num;
                            await _context.SaveChangesAsync();
                        }

                        SendEmail(new EmailModel
                        {
                            To = login.Email,
                            Subject = "Your Code",
                            Body = $"<h1>{ num.ToString() }</h1>"
                        }) ;
                        return "Code has been sent to your email";
                    }
                    else
                    {
                        return "Error Password";
                    }
                }
                else
                {
                    return "No such email address";
                }
            }
            else
            {
                return "Login NULL";
            }
        }
        private void SendEmail(EmailModel emailModel)
        {
            _emileService.SendEmailAsync(emailModel);
        }

        public async Task<string> CheckCodeAsync(int num)
        {
            var result = await _context.Codes.FirstOrDefaultAsync(x => x.Id == 1);
            if (num == result.code)
            {
                return "Accepted";
            }
            else
            {
                return "Error code";
            }
        }
    }
}

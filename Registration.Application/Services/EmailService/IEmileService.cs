using Registration.Domein.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.Application.Services.EmailService
{
    public interface IEmileService
    {
        public Task SendEmailAsync(EmailModel model);
    }
}

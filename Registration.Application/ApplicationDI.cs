using Microsoft.Extensions.DependencyInjection;
using Registration.Application.Services.EmailService;
using Registration.Application.Services.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.Application
{
    public static class ApplicationDI
    {
        public static IServiceCollection AddServicees(this IServiceCollection services)
        {
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<IEmileService, EmailService>();
            return services;
        }
    }
}

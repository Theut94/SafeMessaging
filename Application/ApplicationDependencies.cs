using Application.Interface;
using Application.Services;
using Application.Util;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ApplicationDependencies
    {
        public static void AddDependencies(IServiceCollection services)
        {
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<ICredentialService, CredentialService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEncryptionUtil, EncryptionUtil>();

        }
    }
}

using Data.Repo;
using Data.Repo.Context;
using Data.Repo.Interface;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public static class DataDependencies
    {
        public static void AddDependencies(IServiceCollection services, string connectionstring)
        {
            services.AddDbContextFactory<Context>(opt => opt.UseNpgsql(connectionstring));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
        }
    }
}

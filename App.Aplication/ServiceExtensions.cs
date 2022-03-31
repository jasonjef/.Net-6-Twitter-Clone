using App.Application.Interfaces;
using App.Application.Services;
using App.Application.Services.Mail;
using App.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Application
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                //var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
                //.UseLazyLoadingProxies()
            });

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddTransient<PostService>();
            services.AddTransient<SearchService>();
            services.AddTransient<UserService>();

            services.AddTransient<IEmailService, SmtpEmailService>();

            services.AddHttpContextAccessor();

            return services;
        }
    }
}

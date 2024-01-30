using Contracts;
using LoggerService;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service;
using ServiceContracts;


namespace CompanyEmployee.Extentions
{
    public static class ServiceExtentions
    {
        public static void ConfigurIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {

            });
        
        public static void ConfigureLoggerService(this IServiceCollection services)=>
            services.AddSingleton<ILoggerManager,LoggerManger>();

        public static void ConfiguerCors(this IServiceCollection services) =>
            services.AddCors(options =>
            options.AddPolicy("Cors Policy", builder =>
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            ));

        public static void ConfigureRepositoryManger(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManger, RepositoryManger>();
        public static void ConfigureServiceManger(this IServiceCollection services) =>
            services.AddScoped<IserviceManager, ServiceManger>();
        public static void ConfigureSqlContext(this IServiceCollection services,
IConfiguration configuration) =>
services.AddDbContext<ApplicationDbContext>(opts =>
opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
       public delegate int Delegatename(int x,int y);

    }
}

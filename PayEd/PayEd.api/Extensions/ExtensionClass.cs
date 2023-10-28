using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PayEd.Core.Implementation;
using PayEd.Core.Services;
using PayEd.Data.AppContext;
using PayEd.Data.Models;

namespace PayEd.api.Extensions
{
    public static class ExtentionClass
    {
        public static void AddSomeExtentionMethods(this IServiceCollection services, IConfiguration configiration)
        {
 
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configiration.GetConnectionString("Default"));

               // options.UseNpgsql(configiration.GetConnectionString("DefaultConnection"));

            });

            var keysDirectory = Path.Combine(Directory.GetCurrentDirectory(), "keys");
#pragma warning disable CA1416 // Validate platform compatibility
            services
                .AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(keysDirectory))
                .ProtectKeysWithDpapi();
#pragma warning restore CA1416 // Validate platform compatibility


            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBudgetRepository, BudgetRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<IIncomeRepository, IncomeRepository>();
            services.AddScoped<ISchoolRepository, SchoolRepository>();
            services.AddScoped<IStreamRepository, StreamsRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }

}

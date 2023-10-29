using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PayEd.Core.Implementation;
using PayEd.Core.Services;
using PayEd.Data.AppContext;
using PayEd.Data.Models;
using Supabase;
using Supabase.Interfaces;

namespace PayEd.api.Extensions
{
    public static class ExtentionClass
    {
        public static void AddSomeExtentionMethods(this IServiceCollection services, IConfiguration configiration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                //options.UseSqlServer(configiration.GetConnectionString("Default"));
                options.UseNpgsql(configiration.GetConnectionString("Default"));
            });

            services.AddScoped<Supabase.Client>(_ =>
                new Supabase.Client(
                    configiration["SupabaseUrl"],
                    configiration["SupabaseKey"],
                    new SupabaseOptions
                    {
                        AutoRefreshToken = true,
                        AutoConnectRealtime = true,
                    }
            ));

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

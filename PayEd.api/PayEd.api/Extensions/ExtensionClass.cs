using Microsoft.EntityFrameworkCore;
using PayEd.Data.AppContext;

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
        }
    }

}

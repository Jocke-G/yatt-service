using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YattService.Common.RepositoryInterfaces;
using YattService.Persistance.Repositories;

namespace YattService.Persistance
{
    public static class DatabaseInitialization
    {
        public static void AddPostgreSql(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<YattDbContext>(options =>
                options.UseNpgsql(connectionString)
            );

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IToDoItemRepository, ToDoItemRepository>();
        }
    }
}

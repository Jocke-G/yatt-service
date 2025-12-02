using Microsoft.EntityFrameworkCore;
using YattService.Common.Entities;

namespace YattService.Persistance
{
    public class YattDbContext(DbContextOptions<YattDbContext> options) : DbContext(options)
    {
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<ToDoItemEntity> TodoItems => Set<ToDoItemEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(YattDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}

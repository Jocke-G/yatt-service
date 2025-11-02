using Microsoft.EntityFrameworkCore;
using Yatt_Service.Entities;

namespace Yatt_Service
{
    public class YattDbContext : DbContext
    {
        public YattDbContext(DbContextOptions<YattDbContext> options)
        : base(options)
        {
        }

        public DbSet<ToDoItemEntity> TodoItems => Set<ToDoItemEntity>();
    }
}

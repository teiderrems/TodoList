using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TodoList.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public DbSet<TodoList.Models.TaskModel> TodoList { get; set; } = default!;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<TodoList.Models.TaskModel>()
                .HasOne(t => t.Owner)
                .WithMany();
        }
    }
}

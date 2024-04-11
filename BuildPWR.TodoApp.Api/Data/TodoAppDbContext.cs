using BuildPWR.TodoApp.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace BuildPWR.TodoApp.Api.Data
{
    public class TodoAppDbContext : DbContext
    {
        public TodoAppDbContext(DbContextOptions<TodoAppDbContext> options) : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}

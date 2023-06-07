using Microsoft.EntityFrameworkCore;
using ProjectSkeleton.Core.Entities;

namespace ProjectSkeleton.Data
{
    public class TodoDb : DbContext
    {
     
        public TodoDb(DbContextOptions<TodoDb> options) : base(options)
        {
            
        }

        public DbSet<Todo> Todos => Set<Todo>();
    }
}

using Microsoft.EntityFrameworkCore;

namespace ToDoAPI
{
    public class ToDoDb : DbContext
    {
        public ToDoDb(DbContextOptions<ToDoDb> options) : base(options) { }

        public DbSet<ToDoItem> ToDos { get; set; }

    }
}

using Microsoft.EntityFrameworkCore;
using ToDo.Models;

namespace ToDo.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<ToDoItem> ToDoItems { get; set; } = null;

}
using Microsoft.EntityFrameworkCore;
using CloudTodoList.API.Models;

namespace CloudTodoList.API.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
    : base(options) { }

    public DbSet<Todo> Todos { get; set; }
}
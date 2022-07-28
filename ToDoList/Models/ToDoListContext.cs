using Microsoft.EntityFrameworkCore;

namespace ToDoList.Models
{
  public class ToDoListContext : DbContext
  {
    public DbSet<Category> Categories { get; set; }//adds a Category DbSet to ToDoListContext.cs
    public DbSet<Item> Items { get; set; }

    public ToDoListContext(DbContextOptions options) : base(options) { }
    
    //below OnConfiguing enables lazy-loading
    protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}
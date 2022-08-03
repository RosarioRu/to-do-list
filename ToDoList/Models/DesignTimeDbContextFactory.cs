using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

//this code makes it so our application can properly find our database context for migrations. it's called DesignTime because running migrations is something that is done when designing an application, not when we are actually running it. 
namespace ToDoList.Models
{
  public class ToDoListContextFactory : IDesignTimeDbContextFactory<ToDoListContext>
  {

    ToDoListContext IDesignTimeDbContextFactory<ToDoListContext>.CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();

      var builder = new DbContextOptionsBuilder<ToDoListContext>();

      builder.UseMySql(configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(configuration["ConnectionStrings:DefaultConnection"]));

      return new ToDoListContext(builder.Options);
    }
  }
}
using ToDoList.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;//gives startup class access to identity
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace ToDoList
{
  public class Startup
  {
    public Startup(IWebHostEnvironment env)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json");
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; set; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();
      //below in AddDbContext method is a 'dependency injection' that allows us to set the value of _db to ToDoListContext in our ItemsController file.
      services.AddEntityFrameworkMySql()
        .AddDbContext<ToDoListContext>(options => options 
        .UseMySql(Configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(Configuration["ConnectionStrings:DefaultConnection"])));
      
      //tells Identity what we want to use as a model for our user with this line below:
      services.AddIdentity<ApplicationUser, IdentityRole>() 
                .AddEntityFrameworkStores<ToDoListContext>()
                .AddDefaultTokenProviders();
    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseDeveloperExceptionPage();
      app.UseAuthentication();
      app.UseRouting();
      app.UseAuthorization();

      app.UseEndpoints(routes =>
      {
        routes.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
      });

      app.UseStaticFiles(); //THIS IS NEW AND CONFIGURES APP TO USE STATIC CONTENT

      app.Run(async (context) =>
      {
        await context.Response.WriteAsync("Oops!");
      });

      
    }

  }
}
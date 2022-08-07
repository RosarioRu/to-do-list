using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq; //this allows us to use ToList() method below
using ToDoList.Models;
using Microsoft.EntityFrameworkCore; //need this for using .Include()


namespace ToDoList.Controllers
{
  public class CategoriesController : Controller
  {
    //a private and readonly field of type 'ToDoListContext' named _db
    private readonly ToDoListContext _db;
    //below constructor sets value of _db property to ToDoListContext. we can do this bc of a 'dependency injection' we set up in startup.cs
    public CategoriesController(ToDoListContext db) 
    {
      _db = db;
    }

    //below we are able to access all Item(s) in list form by usign LINQ ToList()
    //_db's value is db, which is an instance of DbContext class. It holds reference to te database.
    public ActionResult Index()
    {
      return View(_db.Categories.ToList()); 
    } 

    [HttpGet]
    public ActionResult Create() // where a form for new categories will be
    {
      return View();
    }

    [HttpPost] //creates new category after form submission and redirects to index view
    public ActionResult Create(Category category)
    {
      _db.Categories.Add(category);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpGet]
    public ActionResult Details(int id)
    {
      Category categoryToDisplay = _db.Categories
        .Include(category => category.JoinEntities)//loads all CategoryItem(s) this category is a part of!
        .ThenInclude(join=> join.Item) //loads the .Item property of each of those CategoryItem(s)
        .FirstOrDefault(category => category.CategoryId == id); //finally loads the CategoryItem's CategoryId(s) for all the CategoryItem(s) matching... so.. will return the item passed into this controller as a parameter but also include it's Item(s)?...
      return View(categoryToDisplay);
    }


  }
}


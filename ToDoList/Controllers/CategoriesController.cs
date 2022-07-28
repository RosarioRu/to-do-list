using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq; //this allows us to use ToList() method below
using ToDoList.Models;



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
      List<Category> model = _db.Categories.ToList(); //once in database, it looks for object named "Categories" which is the DbSet declaired in ToDoListContext.cs, a property of ->(ToDoListContext : DbContext). LINQ turns this DbSet into a list using the ToList() method
      return View(model); //this list is what is returned and is the model we'll use for the Index view!
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
      Category categoryToDisplay = _db.Categories.FirstOrDefault(category => category.CategoryId == id);
      return View(categoryToDisplay);
    }

  }
}

// using System.Collections.Generic;
// using System;
// using Microsoft.AspNetCore.Mvc;
// using ToDoList.Models;

// namespace ToDoList.Controllers
// {
//   public class CategoriesController : Controller
//   {

//     [HttpGet("/categories")]
//     public ActionResult Index()
//     {
//       List<Category> allCategories = Category.GetAll();
//       return View(allCategories);
//     }

//     [HttpGet("/categories/new")]
//     public ActionResult New()
//     {
//       return View();
//     }

//     [HttpPost("/categories")]
//     public ActionResult Create(string categoryName)
//     {
//       Category newCategory = new Category(categoryName);
//       return RedirectToAction("Index");
//     }

//     [HttpGet("/categories/{id}")]
//     public ActionResult Show(int id)
//     {
//       Dictionary<string, object> model = new Dictionary<string, object>();
//       Category selectedCategory = Category.Find(id);
//       List<Item> categoryItems = selectedCategory.Items;
//       model.Add("category", selectedCategory);
//       model.Add("items", categoryItems);
//       return View(model);
//     }

//     // This one creates new Items within a given Category, not new Categories:
//     [HttpPost("/categories/{categoryId}/items")]
//     public ActionResult Create(int categoryId, string itemDescription)
//     {
//       Dictionary<string, object> model = new Dictionary<string, object>();
//       Category foundCategory = Category.Find(categoryId);
//       Item newItem = new Item(itemDescription);
//       newItem.Save();    // New code
//       foundCategory.AddItem(newItem);
//       List<Item> categoryItems = foundCategory.Items;
//       model.Add("items", categoryItems);
//       model.Add("category", foundCategory);
//       return View("Show", model);
//     }

//   }
// }
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Collections.Generic;
using System.Linq; //this allows us to use ToList() method below
using Microsoft.EntityFrameworkCore; //so we can use EntityState to modify Item(s).


namespace ToDoList.Controllers
{
  public class ItemsController : Controller
  {
    //a private and readonly field of type 'ToDoListContext' named _db
    private readonly ToDoListContext _db;
    //below constructor sets value of _db property to ToDoListContext. we can due this bc of a 'dependency injection' we set up in startup.cs
    public ItemsController(ToDoListContext db) 
    {
      _db = db;
    }

    //below we are able to access all Item(s) in list form by usign LINQ ToList()
    //_db's value is db, which is an instance of DbContext class. It holds reference to te database.
    public ActionResult Index()
    {
      List<Item> model = _db.Items.ToList(); //once in database, it looks for object named "Items" which is the DbSet declaired in ToDoListContext.cs, a property of ->(ToDoListContext : DbContext). LINQ turns this DbSet into a list using the ToList() method
      return View(model); //this list is what is returned and is the model we'll use for the Index view!
    }

    public ActionResult Create() //GET route that was previously, before Entity, called New(). This is where our form for new items lives, I think?
    {
      return View();
    } 

    //below Post request takes item as argument (presumably from the form user submits) and adds it to _db.Items, then saves changes to database object (_db) and routes to Index() page/view for Items.
    [HttpPost]
    public ActionResult Create(Item item)
    {
      _db.Items.Add(item);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpGet]
    public ActionResult Details(int id)
    {
      
      Item itemToDisplay = _db.Items.FirstOrDefault(item => item.ItemId == id);
      return View(itemToDisplay);
    }

    [HttpGet] //this will go to a form that will allow editing of item. User will get here by clicking a link to edit a specific item when it that item's details page/view.
    public ActionResult Edit(int id)
    {
      var thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
      return View(thisItem);
    }

    [HttpPost] //POST method will actually edit the item then take user to index of items view.
    public ActionResult Edit(Item item)
    {
      _db.Entry(item).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    //Vieow to Form to delete individual item below. Link to this is in Details view for each item.
    [HttpGet]
    public ActionResult Delete(int id)
    {
      var thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
      return View(thisItem);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
      _db.Items.Remove(thisItem);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }




  }
}
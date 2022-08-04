using Microsoft.AspNetCore.Mvc.Rendering; //need this for SelectList.

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
      return View(_db.Items.ToList());
    }

    [HttpGet]
    public ActionResult Create()
    {
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Item item, int CategoryId)
    {
      _db.Items.Add(item);
      _db.SaveChanges();
      if (CategoryId != 0)
      {
        _db.CategoryItems.Add(new CategoryItem() { CategoryId = CategoryId, ItemId = item.ItemId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    [HttpGet]
    public ActionResult Details(int id)
    { //below we first say thisItem is a list of all Item(s) in database, then we 'load' the joinEntities of items by saying .Include the item(s) property called JoinEntities (list of relationships of items and their categories), then load the categories by .ThenInclude the join.Category. This will return list of items with the Categories of the CatgoryItem(s).  Finally we say the one we want is the the item with the ItemId equalling id.... I think?... so we return the item along with associated categories object(s).
      var thisItem = _db.Items
        .Include(item => item.JoinEntities)
        .ThenInclude(join => join.Category)
        .FirstOrDefault(item => item.ItemId == id);
      return View(thisItem);
    }

    [HttpGet] 
    public ActionResult Edit(int id)
    {
      var thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
      return View(thisItem);
    }

    [HttpPost] //POST method will actually edit the item then take user to index of items view.
    public ActionResult Edit(Item item, int CategoryId)
    {
      if (CategoryId != 0)
      {
        _db.CategoryItems.Add(new CategoryItem() { CategoryId = CategoryId, ItemId = item.ItemId});
      }
      _db.Entry(item).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    

  }
}











//     //Vieow to Form to delete individual item below. Link to this is in Details view for each item.
//     [HttpGet]
//     public ActionResult Delete(int id)
//     {
//       var thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
//       return View(thisItem);
//     }

//     [HttpPost, ActionName("Delete")]
//     public ActionResult DeleteConfirmed(int id)
//     {
//       var thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
//       _db.Items.Remove(thisItem);
//       _db.SaveChanges();
//       return RedirectToAction("Index");
//     }




//   }
// }
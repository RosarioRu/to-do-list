using System.Collections.Generic;

namespace ToDoList.Models
{
  public class Category
    {
        public Category()
        {
            this.JoinEntities = new HashSet<CategoryItem>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CategoryItem> JoinEntities { get; set; } //this is a 'collection navigation property' as it contains a reference to many related items. This reference allows us to access related Item(s) in controllers and views.
    }
}
using System.Collections.Generic;
// using MySql.Data.MySqlClient;

using System.Collections.Generic;

namespace ToDoList.Models
{
    public class Item
    {
        public Item()
        {
            this.JoinEntities = new HashSet<CategoryItem>();
            //JointEntities is a COLLECTION NAVIGATION PROPERTY and holds the list of relationships this Item is a part of - this is how we find its related Categories.
        }

        public int ItemId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<CategoryItem> JoinEntities { get;} //JointEntities is a COLLECTION NAVIGATION PROPERTY and holds the list of relationships this Item is a part of - this is how we find its related Categories.
    }
}
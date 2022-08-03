namespace ToDoList.Models
{
  public class CategoryItem
    {       
        public int CategoryItemId { get; set; }
        public int ItemId { get; set; }
        public int CategoryId { get; set; }
        public virtual Item Item { get; set; } //Item and Category below are included as objects!
        public virtual Category Category { get; set; }
    }
}
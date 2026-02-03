namespace Project_MVC.Models
{
    public class Category
    {
        public int id { get; set; }
        public string categoryName { get; set; }
        public string  desc { get; set; }
        public List<Element> elements { get; set; }

        // public string entertainment { get; set; }
    }
}
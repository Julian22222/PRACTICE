namespace Project_MVC.Models.Domain
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }

        // tags can have multiple Icons
        public ICollection<Icon> Icons { get; set; }




    }
}
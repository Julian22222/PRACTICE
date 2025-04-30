namespace Project_MVC.Models
{
    public class Element
    {
        public int id { get; set; }
        public string name { get; set; }
        public string shortDesc { get; set; }
        public string longDesc { get; set; }
        public string img { get; set; }
        // ushort -> price can't be negative = -13
        public ushort price { get; set; }
        public bool isFavourite { get; set; }
        public bool available { get; set; }
        public int categoryID { get; set; }


        public virtual Category category{ get; set; }
    }
}
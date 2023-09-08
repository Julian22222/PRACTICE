using System.Security.Cryptography.X509Certificates;
namespace Project_MVC.Models.Domain
{
    public class Icon
    {

        // Guid - unique identifier
        // all this proporties can't be null - must have any value, otherwise it will throw an error
        //if you want to have a null value for a property, or it can be null in the database -> (as example)->  public string? Heading { get; set; }
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle {get;set;}
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }

        // Icon can have multiple tags
        public ICollection<Tag> Tags { get; set; }

    }
}
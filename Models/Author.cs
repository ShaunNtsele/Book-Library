using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace  Book_Library.Models
{
       public class Author
    {
        public Guid AuthorId {get;set;}

        [Required]
        public string? AuthorName { get;set;}

        [Required]
        public DateTime ActiveFrom {get;set;}

        [Required]
        public DateTime ActiveTo {get;set;}
        
        [JsonIgnore]
        public ICollection<Book>? Books {get;set;}
        //To do: get name of user currently login in using userManager
        public Guid CreatedBy {get; set;}
            
    }
}
using System.ComponentModel.DataAnnotations;

namespace  Book_Library.Models
{
    public class Book
    {
        public Guid bookId {get;set;}

        [Required]
        public string? BookName { get;set;}

        [Required]
        public string? Publisher {get;set;}

        [Required]
        public DateTime DatePublished {get;set;}
        
        [Required]
        public int copiesSold {get;set;}

        public Author? Author {get;set;}

        //To do: get name of user currently login in using userManager
       public Guid CreatedBy {get; set;}
    }
}
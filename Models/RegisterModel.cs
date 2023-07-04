using System.ComponentModel.DataAnnotations;

namespace Book_Library.Models 
{

   public class RegisterModel
    {
        public Guid UserID {get;set;}
        //Enter error messages if required 
        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
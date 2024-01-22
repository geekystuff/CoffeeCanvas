using System.ComponentModel.DataAnnotations;

namespace CoffeeCanvas.Models
{
    public class ApplicationUser
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage ="This field is required.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        public string FirstName { get; set; }
       
        public string LastName { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
        ErrorMessage = "Please enter correct email address")]

        public string Email { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CoffeeCanvas.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Please enter your username")]
        public string Username { get; set; }
        [Required(ErrorMessage ="Please enter you password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

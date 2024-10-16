using System.ComponentModel.DataAnnotations;

namespace LibraryMangementSystem.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="User Name is required")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me !!!")]
        public bool RememberMe { get; set; }   
    }
}

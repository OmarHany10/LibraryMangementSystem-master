using System.ComponentModel.DataAnnotations;

namespace LibraryMangementSystem.ViewModels
{
    public class RegisterViewModel
    {
        public string UserName { get; set; }
        
        public string? Address { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}

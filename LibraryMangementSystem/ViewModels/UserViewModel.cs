using System.ComponentModel.DataAnnotations;

namespace LibraryMangementSystem.ViewModels
{
    public class UserViewModel
    {
        [Required]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

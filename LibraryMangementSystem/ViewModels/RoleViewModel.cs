using System.ComponentModel.DataAnnotations;

namespace LibraryMangementSystem.ViewModels
{
    public class RoleViewModel
    {
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}

namespace LibraryMangementSystem.ViewModels
{
    public class AssignViewModel
    {
        public string UserName { get; set; }
        public List<RoleSelection> Roles { get; set; }
        public List<string> AssignedRoles { get; set; }
    }

    public class RoleSelection
    {
        public string RoleName { get; set; }
    }
}

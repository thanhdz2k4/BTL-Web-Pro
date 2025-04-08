using System.ComponentModel.DataAnnotations;

namespace BTL_LTW_PRO.Models
{
    public class Roles
    {
        [Key]
        public int RoleID { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}

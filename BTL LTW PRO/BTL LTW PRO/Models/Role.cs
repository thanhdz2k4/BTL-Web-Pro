using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;  // Import thư viện này

namespace BTL_LTW_PRO.Models
{
    [Table("Roles")]  // Ánh xạ đến bảng "Roles" trong DB
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        [Required]
        public string RoleName { get; set; } = "";
    }
}

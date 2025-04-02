using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BTL_LTW_PRO.Models
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; } = new User();

        [ForeignKey("Course")]
        public int CourseID { get; set; }
        public Course Course { get; set; } = new Course();

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(50)]
        public string Status { get; set; }
    }
}

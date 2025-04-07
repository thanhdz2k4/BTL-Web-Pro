using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BTL_LTW_PRO.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        [ForeignKey("Course")]
        public String CourseID { get; set; } = "";

        public String Status { get; set; } = "Pending";

        public DateTime EnrolledAt { get; set; } = DateTime.Now;

        public User User { get; set; }  
        public virtual Course Course { get; set; }
    }
}

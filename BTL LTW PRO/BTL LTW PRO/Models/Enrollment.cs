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
        public User User { get; set; } = new User();

        [ForeignKey("Course")]
        public int CourseID { get; set; }
        public Course Course { get; set; } = new Course();

        public DateTime EnrolledAt { get; set; } = DateTime.Now;
    }
}

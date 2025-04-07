using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BTL_LTW_PRO.Models
{
    public class Course
    {
        [Key]
        public string CourseID { get; set; } = "";

        [Required]
        [MaxLength(200)]
        public string CourseName { get; set; } = "";

        public string Description { get; set; } = "";

        [ForeignKey("Instructor")]
        public int InstructorID { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }

    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BTL_LTW_PRO.Models
{
    public class Lesson
    {
        [Key]
        public int LessonID { get; set; }

        [ForeignKey("Course")]
        public int CourseID { get; set; }
        public Course Course { get; set; } = new Course();

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = "";

        public string Content { get; set; } = "";
        public string VideoURL { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}

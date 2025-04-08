using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL_LTW_PRO.Models
{
    public class Lesson
    {
        [Key]
        public int LessonID { get; set; }

        [ForeignKey("Course")]
        public int CourseID { get; set; }
        public Course Course { get; set; } // No need to initialize here

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;
        public string VideoURL { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}

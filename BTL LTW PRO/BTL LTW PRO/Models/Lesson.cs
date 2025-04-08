using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL_LTW_PRO.Models
{
    public class Lesson
    {
        [Key]
        public int LessonID { get; set; }

        [Required]
        public string CourseID { get; set; }

        [ForeignKey("CourseID")]
        public Course? Course { get; set; } 

        [Required]
        public string Title { get; set; }

        public string Content { get; set; }
        public string VideoURL { get; set; }
        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
    }

}

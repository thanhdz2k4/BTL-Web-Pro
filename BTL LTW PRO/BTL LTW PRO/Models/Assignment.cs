using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL_LTW_PRO.Models
{
    public class Assignment
    {
        [Key]
        public int AssignmentID { get; set; }

        [ForeignKey("Lesson")]
        public int LessonID { get; set; }
        public Lesson lesson { get; set; } = new Lesson();


        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = "";
        public string? Description;
        public DateTime? DueDate { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL_LTW_PRO.Models
{
    public class Assignment
    {
        [Key]
        public int AssignmentID { get; set; }

        [ForeignKey("Lesson")]
        [Display(Name = "Bài học")]
        [Required(ErrorMessage = "Phải chọn bài học")]
        public int LessonID { get; set; }

        [Required(ErrorMessage = "Tiêu đề là bắt buộc")]
        [StringLength(200, ErrorMessage = "Tiêu đề không được vượt quá 200 ký tự")]
        [Display(Name = "Tiêu đề bài tập")]
        public string Title { get; set; }

        [Display(Name = "Mô tả")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Ngày hết hạn là bắt buộc")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày hết hạn")]
        public DateTime DueDate { get; set; }

        // Navigation property
        public Lesson? Lesson { get; set; }
    }
}

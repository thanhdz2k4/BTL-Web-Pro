using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BTL_LTW_PRO.Models
{
    public class Submission
    {
        [Key]
        public int SubmissionID { get; set; }

        [ForeignKey("Assignment")]
        public int AssignmentID { get; set; }
        public Assignment? Assignment { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
    
        public string FileURL { get; set; } = "";
        public DateTime SubmittedAt { get; set; } = DateTime.Now;
    }
}

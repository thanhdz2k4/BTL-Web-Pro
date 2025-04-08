using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BTL_LTW_PRO.Models; 

public class Submission
{
    public int SubmissionID { get; set; }

    public int AssignmentID { get; set; }
    public Assignment Assignment { get; set; }

    public int UserID { get; set; }
    public User User { get; set; }

    public string FileURL { get; set; }

    public DateTime SubmittedAt { get; set; } = DateTime.Now;
}

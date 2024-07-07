using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Data.Models;

public partial class InterviewsInfo
{
    public int InterviewinfoId { get; set; }

    public DateTime Time { get; set; }

    public string Location { get; set; } = null!;

    public string? Result { get; set; }

    public string Position { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string Content { get; set; } = null!;

    public string? InterviewMode { get; set; }

    public string? Feedback { get; set; }
   
    public int InternId { get; set; }
    public int MentorId { get; set; }

    public virtual Intern Intern { get; set; } = null!;

    public virtual Mentor Mentor { get; set; } = null!;
}

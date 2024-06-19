using System;
using System.Collections.Generic;

namespace IMS.Data.Models;

public partial class WorkingResult
{
    public int ResultId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public double? Rating { get; set; }

    public string? Note { get; set; }

    public DateTime DateCompleted { get; set; }

    public string? Status { get; set; }

    public long HoursWorked { get; set; }

    public int MentorId { get; set; }

    public int TaskId { get; set; }

    public int InternId { get; set; }

    public virtual Intern Intern { get; set; } = null!;

    public virtual Mentor Mentor { get; set; } = null!;

    public virtual Task Task { get; set; } = null!;
}

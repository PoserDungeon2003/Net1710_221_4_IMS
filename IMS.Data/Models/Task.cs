using System;
using System.Collections.Generic;

namespace IMS.Data.Models;

public partial class Task
{
    public int TaskId { get; set; }

    public string Description { get; set; } = null!;

    public int InternId { get; set; }

    public string? Name { get; set; }

    public int MentorId { get; set; }

    public virtual Intern Intern { get; set; } = null!;

    public virtual Mentor Mentor { get; set; } = null!;

    public virtual ICollection<WorkingResult> WorkingResults { get; set; } = new List<WorkingResult>();
}

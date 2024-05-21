using System;
using System.Collections.Generic;

namespace IMS.Data.Models;

public partial class Mentor
{
    public int MentorId { get; set; }

    public string FullName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string JobPosition { get; set; } = null!;

    public int? CompanyId { get; set; }

    public virtual Company? Company { get; set; }

    public virtual ICollection<Intern> Interns { get; set; } = new List<Intern>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual ICollection<WorkingResult> WorkingResults { get; set; } = new List<WorkingResult>();
}

using System;
using System.Collections.Generic;

namespace IMS.Data.Models;

public partial class Intern
{
    public int InternId { get; set; }

    public string University { get; set; } = null!;

    public string Major { get; set; } = null!;

    public string JobPosition { get; set; } = null!;

    public string? EducationBackground { get; set; }

    public string? Experiences { get; set; }

    public string WorkingTasks { get; set; } = null!;

    public int MentorId { get; set; }

    public string Name { get; set; } = null!;

    public int CompanyId { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<InterviewsInfo> InterviewsInfos { get; set; } = new List<InterviewsInfo>();

    public virtual Mentor Mentor { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual ICollection<WorkingResult> WorkingResults { get; set; } = new List<WorkingResult>();

    public class InternSearchCriteria // Search criteria for interns
    {
        public string SearchTerm { get; set; } // Search by name, email, or skills
    }
}

using System;
using System.Collections.Generic;

namespace IMS.Data.Models;

public partial class Company
{
    public int CompanyId { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Phone { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Intern> Interns { get; set; } = new List<Intern>();

    public virtual ICollection<Mentor> Mentors { get; set; } = new List<Mentor>();
}

using System;
using System.Collections.Generic;

namespace IMS.Data.Models;

public partial class InterviewsInfo
{
    public int InterviewinfoId { get; set; }

    public DateTime Time { get; set; }

    public string Location { get; set; } = null!;

    public string Result { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Content { get; set; } = null!;

    public int InternId { get; set; }

    public virtual Intern Intern { get; set; } = null!;
}

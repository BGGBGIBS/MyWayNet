using System;
using System.Collections.Generic;

namespace MyWayNet.Models;

public partial class Grade
{
    public int GradeId { get; set; }

    public string GradeName { get; set; } = null!;

    public int GradeValue { get; set; }

    public int? ScaleId { get; set; }

    public virtual ICollection<Record> Records { get; set; } = new List<Record>();

    public virtual Scale? Scale { get; set; }
}

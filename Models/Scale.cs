using System;
using System.Collections.Generic;

namespace MyWayNet.Models;

public partial class Scale
{
    public int ScaleId { get; set; }

    public int ScaleMin { get; set; }

    public int ScaleMax { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}

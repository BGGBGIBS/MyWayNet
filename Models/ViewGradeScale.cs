using System;
using System.Collections.Generic;

namespace MyWayNet.Models;

public partial class ViewGradeScale
{
    public int ScaleMin { get; set; }

    public int ScaleMax { get; set; }

    public string GradeName { get; set; } = null!;

    public int GradeValue { get; set; }
}

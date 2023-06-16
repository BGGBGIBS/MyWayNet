using System;
using System.Collections.Generic;

namespace MyWayNet.Models;

public partial class ViewUserRecord
{
    public long RecordId { get; set; }

    public string UserFirstname { get; set; } = null!;

    public string UserLastname { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public string OccupationName { get; set; } = null!;

    public string? OccupationDescription { get; set; }

    public DateTime EventBegin { get; set; }

    public DateTime EventEnd { get; set; }

    public string InstitutionName { get; set; } = null!;

    public string InstitutionType { get; set; } = null!;

    public string InstitutionAddress { get; set; } = null!;

    public int ScaleMin { get; set; }

    public int ScaleMax { get; set; }

    public string GradeName { get; set; } = null!;

    public int GradeValue { get; set; }
}

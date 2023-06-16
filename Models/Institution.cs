using System;
using System.Collections.Generic;

namespace MyWayNet.Models;

public partial class Institution
{
    public long InstitutionId { get; set; }

    public string InstitutionName { get; set; } = null!;

    public string InstitutionType { get; set; } = null!;

    public string InstitutionAddress { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}

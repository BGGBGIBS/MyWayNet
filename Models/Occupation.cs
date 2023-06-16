using System;
using System.Collections.Generic;

namespace MyWayNet.Models;

public partial class Occupation
{
    public long OccupationId { get; set; }

    public string OccupationName { get; set; } = null!;

    public string? OccupationDescription { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}

using System;
using System.Collections.Generic;

namespace MyWayNet.Models;

public partial class Event
{
    public long EventId { get; set; }

    public DateTime EventBegin { get; set; }

    public DateTime EventEnd { get; set; }

    public long OccupationId { get; set; }

    public long InstitutionId { get; set; }

    public bool EventType { get; set; }

    public virtual Institution Institution { get; set; } = null!;

    public virtual Occupation Occupation { get; set; } = null!;

    public virtual ICollection<Record> Records { get; set; } = new List<Record>();
}

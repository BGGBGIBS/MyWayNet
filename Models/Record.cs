using System;
using System.Collections.Generic;

namespace MyWayNet.Models;

public partial class Record
{
    public long RecordId { get; set; }

    public long UserId { get; set; }

    public long EventId { get; set; }

    public long SkillId { get; set; }

    public int GradeId { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual Grade Grade { get; set; } = null!;

    public virtual Skill Skill { get; set; } = null!;
}

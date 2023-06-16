using System;
using System.Collections.Generic;

namespace MyWayNet.Models;

public partial class Skill
{
    public long SkillId { get; set; }

    public string SkillName { get; set; } = null!;

    public string SkillDescription { get; set; } = null!;

    public bool SkillType { get; set; }

    public virtual ICollection<Record> Records { get; set; } = new List<Record>();
}

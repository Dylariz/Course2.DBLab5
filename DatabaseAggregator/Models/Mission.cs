using System;
using System.Collections.Generic;

namespace DatabaseAggregator.Models;

public partial class Mission
{
    public int Id { get; set; }

    public string Employer { get; set; } = null!;

    public string Objective { get; set; } = null!;

    public string Place { get; set; } = null!;

    public int AwardUsd { get; set; }

    public bool Completed { get; set; }

    public virtual ICollection<Regiment> Regiments { get; set; } = new List<Regiment>();
}

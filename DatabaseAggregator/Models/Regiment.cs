using System;
using System.Collections.Generic;

namespace DatabaseAggregator.Models;

public partial class Regiment
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Population { get; set; }

    public int SquadCount { get; set; }

    public int CommanderId { get; set; }

    /// <summary>
    /// null - нет задания
    /// </summary>
    public int? MissionId { get; set; }

    public virtual Staff Commander { get; set; } = null!;

    public virtual Mission? Mission { get; set; }

    public virtual ICollection<RegimentEquipment> RegimentEquipments { get; set; } = new List<RegimentEquipment>();

    public virtual ICollection<Squad> Squads { get; set; } = new List<Squad>();

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}

using System;
using System.Collections.Generic;

namespace DatabaseAggregator.Models;

/// <summary>
/// Служащие
/// </summary>
public partial class Staff
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public DateOnly EntryDate { get; set; }

    public int Salary { get; set; }

    /// <summary>
    /// null - не прикреплён
    /// </summary>
    public int? RegimentId { get; set; }

    /// <summary>
    /// null - не прикреплён
    /// </summary>
    public int? SquadId { get; set; }

    public string Position { get; set; } = null!;

    /// <summary>
    /// 1 - в резерве
    /// </summary>
    public bool InReserve { get; set; }

    public virtual ICollection<PersonalEquipment> PersonalEquipments { get; set; } = new List<PersonalEquipment>();

    public virtual Position PositionNavigation { get; set; } = null!;

    public virtual Regiment? Regiment { get; set; }

    public virtual ICollection<Regiment> Regiments { get; set; } = new List<Regiment>();

    public virtual Squad? Squad { get; set; }

    public virtual ICollection<Squad> Squads { get; set; } = new List<Squad>();
}

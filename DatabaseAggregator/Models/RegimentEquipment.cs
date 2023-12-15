namespace DatabaseAggregator.Models;

public class RegimentEquipment
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Count { get; set; }

    /// <summary>
    /// null - в резерве	
    /// </summary>
    public int? RegimentId { get; set; }

    /// <summary>
    /// null - нет прикрепления к отдельной бригаде
    /// </summary>
    public int? SquadId { get; set; }

    public virtual Regiment? Regiment { get; set; }

    public virtual Squad? Squad { get; set; }
}
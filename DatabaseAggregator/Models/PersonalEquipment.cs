namespace DatabaseAggregator.Models;

public class PersonalEquipment
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Count { get; set; }

    /// <summary>
    /// null - в резерве	
    /// </summary>
    public int? StaffId { get; set; }

    public virtual Staff? Staff { get; set; }
}

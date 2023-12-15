namespace DatabaseAggregator.Models;

public class Squad
{
    public int Id { get; set; }

    public int RegimentId { get; set; }

    public string Name { get; set; } = null!;

    public int Population { get; set; }

    public int CommanderId { get; set; }

    public virtual Staff Commander { get; set; } = null!;

    public virtual Regiment Regiment { get; set; } = null!;

    public virtual ICollection<RegimentEquipment> RegimentEquipments { get; set; } = new List<RegimentEquipment>();

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}

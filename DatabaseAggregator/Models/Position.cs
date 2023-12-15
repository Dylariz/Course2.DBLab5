namespace DatabaseAggregator.Models;

public class Position
{
    public string Name { get; set; } = null!;

    public bool IsOfficer { get; set; }

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}

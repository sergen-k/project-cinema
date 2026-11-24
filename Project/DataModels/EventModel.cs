
public class EventModel
{
    public EventModel(long id, string name, string description, double cost)
    {
        Id = id;
        Name = name;
        Description = description;
        Cost = cost;
    }

    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Cost { get; set; }
}
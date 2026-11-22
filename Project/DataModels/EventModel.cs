public class EventModel
{
    public EventModel(Int64 id, string name, string description, double cost)
    {
        Id = id;
        Name = name;
        Description = description;
        Cost = cost;
    }

    public Int64 Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Cost { get; set; }
}
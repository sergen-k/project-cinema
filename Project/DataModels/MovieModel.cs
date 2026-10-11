
    public class MovieModel {
    public MovieModel(long id, string name, string description, string director, long ageLimit, string duration)
    {
        Id = id;
        Name = name;
        Description = description;
        Director = director;
        AgeLimit = ageLimit;
        Duration = duration;
    }

    public  Int64   Id {get; set;}
    public  String   Name {get; set;}
    public  String   Description {get; set;}
    public  String   Director {get; set;}
    public  Int64   AgeLimit {get; set;}
    public  String   Duration {get; set;}
 }
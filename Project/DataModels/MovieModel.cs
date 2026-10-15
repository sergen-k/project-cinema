
    public class MovieModel {
    public MovieModel(long id, string title, string description, string director, long ageLimit, double duration)
    {
        Id = id;
        Title = title;
        Description = description;
        Director = director;
        AgeLimit = ageLimit;
        Duration = duration;
    }

    public  Int64   Id {get; set;}
    public  String   Title {get; set;}
    public  String   Description {get; set;}
    public  String   Director {get; set;}
    public  Int64   AgeLimit {get; set;}
    public  double   Duration {get; set;}
 }
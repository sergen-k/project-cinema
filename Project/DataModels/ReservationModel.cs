
    public class ReservationModel {
    public ReservationModel(long id, long movieShowingId, long userId)
    {
        Id = id;
        MovieShowingId = movieShowingId;
        UserId = userId;
    }

    public  Int64   Id {get; set;}
    public  Int64   MovieShowingId {get; set;}
    public  Int64   UserId {get; set;}
 }
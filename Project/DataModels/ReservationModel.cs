
    public class ReservationModel {
    public ReservationModel(long id, long movieShowingId, long userId, long peaple)
    {
        Id = id;
        MovieShowingId = movieShowingId;
        UserId = userId;
        Peaple = peaple;

    }

    public  Int64   Id {get; set;}
    public  Int64   MovieShowingId {get; set;}
    public  Int64   UserId {get; set;}

    public Int64    Peaple{get; set;}
 }
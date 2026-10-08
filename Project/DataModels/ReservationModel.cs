public class Reservation
{
    public Reservation(Int64 id,  Int64 movieShowingId,  Int64 userId)
    {
        Id = id;
        MovieShowingId = movieShowingId;
        UserId = userId;
    }

    public Int64 Id { get; set; }

    public Int64 MovieShowingId {get; set;} 

    public Int64 UserId {get; set;}

}




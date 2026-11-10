
    public class ReservationModel {
    public ReservationModel(long id, long movieShowingId, long userId, long people)
    {
        Id = id;
        MovieShowingId = movieShowingId;
        UserId = userId;
        People = people;
    }

    public ReservationModel() { }

    public long Id { get; set; }
    public long MovieShowingId { get; set; }
    public long UserId { get; set; }
    public long People { get; set; }
 }
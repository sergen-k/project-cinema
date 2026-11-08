
    public class ReservationModel {
    public ReservationModel(long id, long movieShowingId, long userId)
    {
        Id = id;
        MovieShowingId = movieShowingId;
        UserId = userId;
    }

    public ReservationModel() { }

    public long Id { get; set; }
    public long MovieShowingId { get; set; }
    public long UserId { get; set; }
    public long Peaple { get; set; }
 }
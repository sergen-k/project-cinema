
public class TimetableModel
{

    public TimetableModel(Int64 id, Int64 showtime, Int64 movieId, Int64 roomNumber)
    {
        Id = id;
        UnixShowTime = showtime;
        MovieId = movieId;
        RoomNumber = roomNumber;
    }

    public Int64 Id { get; set; }

    public Int64 UnixShowTime { get; set; }
    public DateTime Showtime
    {
        get
        {
            // Source: https://stackoverflow.com/questions/249760/how-can-i-convert-a-unix-timestamp-to-datetime-and-vice-versa
            DateTime toDate = DateTimeOffset.FromUnixTimeSeconds(UnixShowTime).LocalDateTime;

            return toDate;
        }
        set
        {
            // https://stackoverflow.com/questions/17632584/how-to-get-the-unix-timestamp-in-c-sharp
            UnixShowTime = ((DateTimeOffset) value).ToUnixTimeSeconds();
            field = value;
        }
    }

    public Int64 MovieId { get; set; }
    public Int64 RoomNumber { get; set; }
}

public class TimetableModel
{

    public TimetableModel(Int64 id, Int64 startTime, Int64 movieId, Int64 roomNumber)
    {
        Id = id;
        StartTime = startTime;
        MovieId = movieId;
        RoomNumber = roomNumber;
    }

    public Int64 Id { get; set; }

    public Int64 StartTime { get; set; }
    public DateTime HumanReadableShowtime
    {
        get
        {
            // Source: https://stackoverflow.com/questions/249760/how-can-i-convert-a-unix-timestamp-to-datetime-and-vice-versa
            DateTime toDate = DateTimeOffset.FromUnixTimeSeconds(StartTime).LocalDateTime;

            return toDate;
        }
        set
        {
            // https://stackoverflow.com/questions/17632584/how-to-get-the-unix-timestamp-in-c-sharp
            StartTime = ((DateTimeOffset) value).ToUnixTimeSeconds();
            field = value;
        }
    }

    public Int64 MovieId { get; set; }
    public Int64 RoomNumber { get; set; }
}
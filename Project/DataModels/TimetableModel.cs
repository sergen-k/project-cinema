public class TimetableModel
{
    public TimetableModel() { }

    public TimetableModel(Int64 id, Int64 startTime, Int64 endTime, Int64 movieId, Int64 roomNumber)
    {
        Id = id;
        StartTime = startTime;
        EndTime = endTime;
        MovieId = movieId;
        RoomNumber = roomNumber;
    }

    public Int64 Id { get; set; }

    public Int64 StartTime { get; set; }
    public Int64 EndTime { get; set; }

    public DateTime HumanReadableShowtime
    {
        get => DateTimeOffset.FromUnixTimeSeconds(StartTime).LocalDateTime;
        set => StartTime = ((DateTimeOffset)value).ToUnixTimeSeconds();
    }

    public DateTime HumanReadableEndtime
    {
        get => DateTimeOffset.FromUnixTimeSeconds(EndTime).LocalDateTime;
        set => EndTime = ((DateTimeOffset)value).ToUnixTimeSeconds();
    }

    public Int64 MovieId { get; set; }
    public Int64 RoomNumber { get; set; }
}
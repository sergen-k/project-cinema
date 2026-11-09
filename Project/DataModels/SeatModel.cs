
    public class SeatModel: IEquatable<SeatModel>{
    public SeatModel(long id, long roomNumber, long seatType, double seatCost)
    {
        Id = id;
        RoomNumber = roomNumber;
        SeatType = seatType;
        SeatCost = seatCost;
    }

    public  Int64   Id {get; set;}
    public  Int64 RoomNumber {get; set;}
    public  Int64 SeatType {get; set;}
    public double SeatCost {get; set;}

    public bool Equals(SeatModel? other)
    {
        if (other is null) return false;
        return this.Id == other.Id;
    }
}
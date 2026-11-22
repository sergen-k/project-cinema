public abstract class MovieRoom
{

    public abstract SeatModel[][] seatArrangements { set; get; }
    public abstract void ConstructAuditoriumRoom();

    public abstract (int, int)? GetSeatInfo(SeatModel model);

    public abstract  (int, int)[] yClamps {get;}

    public abstract int[] pattern  {get;}

}
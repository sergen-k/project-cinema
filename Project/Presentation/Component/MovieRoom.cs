public abstract class MovieRoom
{
    public abstract List<List<SeatModel>> seatArrangements {get; set;}
    
    protected MovieRoom()
    {
        ConstructAuditoriumRoom();
    }

    public abstract void ConstructAuditoriumRoom();


}



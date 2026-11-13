public class SeatRoomA : MovieRoom
{
    private static readonly SeatLogic _seatLogic = new();

    public override List<List<SeatModel>> seatArrangements { set; get; } = [];

    public static readonly (int, int)[] yClamps = [
        (3, 10),
        (1, 11),
        (0, 13),
        (0, 13),
        (0, 13),
        (0, 13),
        (0, 13),
        (0, 13),
        (0, 13),
        (0, 13),
        (1, 11),
        (3, 10)
    ];
    

    public static readonly int[] pattern = [
        2,
        1,
        1,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        1,
        2,
        2
    ];


    public override void ConstructAuditoriumRoom()
    {
        List<SeatModel> seatModels = _seatLogic.SelectByRoom(1);
        Queue<SeatModel> ordered = new(seatModels);
    
        int[] num_seats = [8, 10, 10, 12, 12, 12, 12, 12, 12, 12, 12, 10, 8, 8];
        List<List<SeatModel>> result = new();

        foreach (int row in num_seats)
        {
            List<SeatModel> seat_row = new();

            for (int i = 0; i < row; i++)
            {
                seat_row.Add(ordered.Dequeue());
            }

            result.Add(seat_row);
        }

        seatArrangements = result;
    }

    public (int, int)? GetSeatInfo(SeatModel model)
    {
        for (int row = 0; row < seatArrangements.Count; row++)
        {
            for (int j = 0; j < seatArrangements[row].Count; j++)
            {
                if (seatArrangements[row][j].Id == model.Id) return (14-row, j+1); 
            }
        }

        return null;
    }

}
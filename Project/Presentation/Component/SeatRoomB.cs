public  class SeatRoomB: MovieRoom 
{
    private static readonly SeatLogic _seatLogic = new();

    public  override SeatModel[][] seatArrangements { set; get; } = [];

    public override  (int, int)[] yClamps {get;} = [
        (6, 10),
        (0, 13),
        (0, 16),
        (0, 18),
        (0, 18),
        (0, 18),
        (0, 18),
        (0, 18),
        (0, 18),
        (0, 18),
        (0, 18),
        (0, 18),
        (0, 18),
        (0, 18),
        (0, 18),
        (0, 16),
        (0, 13),
        (6, 10),
    ];

    public  SeatRoomB()
    {
        ConstructAuditoriumRoom();
    }


    public override int[] pattern {get;} = [
        1,
        1, 
        1, 
        1, 
        1,
        1,
        0,
        0,
        0,
        0,
        0,
        1,
        1,
        1,
        2,
        2,
        2,
        3,
        3
    ];


    public override void ConstructAuditoriumRoom()
    {
        List<SeatModel> seatModels = _seatLogic.SelectByRoom(2);
        Queue<SeatModel> ordered = new(seatModels);

        int[] num_seats = [16, 16, 16, 16, 16, 16, 18, 18, 18, 18, 18, 16, 16, 16, 14, 14, 14, 12, 12];
        List<SeatModel[]> result = new();

        foreach (int row in num_seats)
        {
            List<SeatModel> seat_row = new();

            for (int i = 0; i < row; i++)
            {
                seat_row.Add(ordered.Dequeue());
            }

            result.Add(seat_row.ToArray());
        }

        seatArrangements = result.ToArray();
    }

    public override (int, int)? GetSeatInfo(SeatModel model)
    {
        for (int row = 0; row < seatArrangements.Length; row++)
        {
            for (int j = 0; j < seatArrangements[row].Length; j++)
            {
                if (seatArrangements[row][j].Id == model.Id) return (19 - row, j + 1);
            }
        }

        return null;
    }

}
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
        int seatsAdded = 0;
        for(int i = 0; i < 14; i++) {
            int final = 12-pattern[i]*2;
            int start_offset = final + seatsAdded;

            List<SeatModel> row = seatModels[seatsAdded..start_offset];

            seatArrangements.Add(row);

            seatsAdded += row.Count;
        }


    }

}
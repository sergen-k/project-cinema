public class SeatSelection
{

    const string Red = "\u001b[31m";
    const string Green = "\u001b[32m";
    const string Grey = "\u001b[90m";
    const string Reset = "\u001b[0m";

    const string Yellow = "\u001b[33m";

    private static SeatLogic _seatLogic = new();

    private int cursorX = 1;
    private int cursorY = 0;


    public List<SeatModel> Start(TimetableModel timetableModel, int amountToReserve)
    {


        SeatRoomA roomA = new();
        List<SeatModel> filledSeats = _seatLogic.SelectOrderedSeatsForViewing(timetableModel);
        List<SeatModel> toReserve = [];


        while (true)
        {
            // TODO: make cursor X a field 
            Console.WriteLine(DrawAuditorium(roomA, filledSeats, toReserve));
            Console.WriteLine($"{toReserve.Count} / {amountToReserve}");
            var keypress = Console.ReadKey().Key;

            // values to clamp the cursor in
            int rowOffset = SeatRoomA.pattern[cursorY];
            int amountOfSeatsInRow = roomA.seatArrangements[cursorY].Count;
            (int yMin, int yMax) = SeatRoomA.yClamps[cursorX];

            // TODO: move this code for the cusor into the SeatRoomA class an move auditorium there
            if (keypress == ConsoleKey.RightArrow)
            {
                cursorX = Math.Clamp(cursorX + 1, rowOffset, rowOffset + amountOfSeatsInRow - 1);
            }
            else if (keypress == ConsoleKey.LeftArrow)
            {
                cursorX = Math.Clamp(cursorX - 1, rowOffset, amountOfSeatsInRow);
            }
            else if (keypress == ConsoleKey.UpArrow)
            {
                cursorY = Math.Clamp(cursorY - 1, yMin, yMax);
            }
            else if (keypress == ConsoleKey.DownArrow)
            {
                cursorY = Math.Clamp(cursorY + 1, yMin, yMax);
            }
            else if (keypress == ConsoleKey.Enter)
            {
                SeatModel? model = getSeat(roomA.seatArrangements);
                if (model is SeatModel model1 && !filledSeats.Any(x => x.Id == model1.Id))
                {
                    toReserve.Add(model1);
                }
            }else if (keypress == ConsoleKey.Backspace)
            {
                SeatModel? model = getSeat(roomA.seatArrangements);
                if (model is SeatModel model1 && !filledSeats.Any(x => x.Id == model1.Id))
                {
                    toReserve.Remove(model1);
                }
            }
            if (toReserve.Count == amountToReserve)
            {
                Console.WriteLine("You have reached the desire amount of seats. Do you want to continue? (y/n)");
                string input = Console.ReadLine();
                if (input.ToLower() == "y")
                {
                    break;
                } 
            }


            Console.Clear();

        }

        return toReserve;
    }

    public string DrawAuditorium(SeatRoomA roomA, List<SeatModel> filledSeats, List<SeatModel> toReserve)
    {
        string finalResult = "";
        int rowNumber = 0;
        foreach (List<SeatModel> row in roomA.seatArrangements)
        {
            string mainSeat = "";
            string chair = "";

            int rowOffset = SeatRoomA.pattern[rowNumber];


            for (int i = 0; i < rowOffset; i++) mainSeat += "     ";
            for (int i = 0; i < rowOffset; i++) chair += "     ";

            int seatNumber = 0;
            foreach (SeatModel seatModel in row)
            {

                if (filledSeats.Any(x => x.Id == seatModel.Id))
                {
                    mainSeat += $"  {Red}█{Reset}  ";
                    chair += $"  {Red}-{Reset}  ";

                }
                else if (toReserve.Any(x => x.Id == seatModel.Id))
                {
                    mainSeat += $"  {Yellow}█{Reset}  ";
                    chair += $"  {Yellow}-{Reset}  ";
                }
                else if (seatNumber == cursorX - rowOffset && rowNumber == cursorY)
                {
                    mainSeat += $"  {Grey}█{Reset}  ";
                    chair += $"  {Grey}-{Reset}  ";

                }
                else
                {
                    mainSeat += $"  {Green}█{Reset}  ";
                    chair += $"  {Green}-{Reset}  ";
                }

                seatNumber++;
            }

            finalResult += mainSeat + "\n";
            finalResult += chair + "\n";

            rowNumber++;
        }




        return finalResult;
    }

    private SeatModel? getSeat(List<List<SeatModel>> seats)
    {
        try
        {
            return seats[cursorY][cursorX - SeatRoomA.pattern[cursorY]];
        }
        catch (ArgumentOutOfRangeException)
        {
            return null;
        }
    }

}
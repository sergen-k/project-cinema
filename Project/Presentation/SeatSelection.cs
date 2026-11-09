public class SeatSelection
{

    const string Red = "\u001b[31m";
    const string Normal = "\u001b[38;2;0;100;0m";
    const string Premium = "\u001b[38;2;20;160;0m";

    const string Deluxe = "\u001b[38;2;50;255;0m";
    const string Grey = "\u001b[90m";

    const string Reset = "\u001b[0m";

    const string Yellow = "\u001b[33m";


    private static SeatLogic _seatLogic = new();

    private int cursorX = 3;
    private int cursorY = 1;


    public List<SeatModel> Start(TimetableModel timetableModel, int amountToReserve)
    {


        SeatRoomA roomA = new();
        List<SeatModel> filledSeats = _seatLogic.SelectOrderedSeatsForViewing(timetableModel);
        List<SeatModel> toReserve = [];


        while (true)
        {
            Console.WriteLine("Please select the correct amount of seats.");
            Console.WriteLine("Use arrow keys to navigate through the menu");
            Console.WriteLine("When the correct amount of seats has been selected press y to continu");

            Console.WriteLine(DrawAuditorium(roomA, filledSeats, toReserve));

            double totalPrice = toReserve.Sum(x => x.SeatCost);
            Console.WriteLine($"Total price: {totalPrice}");
            Console.WriteLine($"{toReserve.Count} / {amountToReserve}");
            var keypress = Console.ReadKey().Key;

            // values to clamp the cursor in
            int rowOffset = SeatRoomA.pattern[cursorY];
            int amountOfSeatsInRow = roomA.seatArrangements[cursorY].Count;
            (int yMin, int yMax) = SeatRoomA.yClamps[cursorX];

            // TODO: move this code for the cusor into the SeatRoomA class an move auditorium there
            SeatModel? model = getSeat(roomA.seatArrangements);
            switch (keypress)
            {
                case ConsoleKey.RightArrow:
                    cursorX = Math.Clamp(cursorX + 1, rowOffset, rowOffset + amountOfSeatsInRow - 1);
                    break;
                case ConsoleKey.LeftArrow:
                    cursorX = Math.Clamp(cursorX - 1, rowOffset, amountOfSeatsInRow);
                    break;
                case ConsoleKey.UpArrow:
                    cursorY = Math.Clamp(cursorY - 1, yMin, yMax);
                    break;
                case ConsoleKey.DownArrow:
                    cursorY = Math.Clamp(cursorY + 1, yMin, yMax);
                    break;
                case ConsoleKey.Enter:
                        if (model is SeatModel model1 && !filledSeats.Any(x => x.Id == model1.Id) && !toReserve.Any(x => x.Id == model1.Id) && toReserve.Count < amountToReserve)
                        {
                            toReserve.Add(model1);
                        }

                    break;
                case ConsoleKey.Backspace:
                    if (model is SeatModel model2 && !filledSeats.Any(x => x.Id == model2.Id))
                    {
                        toReserve.Remove(model2);
                    }
                    break;
                case ConsoleKey.Y:
                    if (toReserve.Count == amountToReserve)
                    {
                       return toReserve;
                    }
                break;
            }


            Console.Clear();

        }
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
                    switch (seatModel.SeatType)
                    {
                        case 1:
                            mainSeat += $"  {Normal}█{Reset}  ";
                            chair += $"  {Normal}-{Reset}  ";
                            break;
                        case 2:
                            mainSeat += $"  {Premium}█{Reset}  ";
                            chair += $"  {Premium}-{Reset}  ";
                            break;
                        case 3:
                            mainSeat += $"  {Deluxe}█{Reset}  ";
                            chair += $"  {Deluxe}-{Reset}  ";
                            break;
                    }
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
namespace UnitTests;

[TestClass]
public class CinemaroomATest
{

    [TestMethod]
    public void CinemaroomACount()
    {
        int count = 0;
        foreach (var row in SeatRoomA.seatArrangements)
        {
            foreach (var seat in row){
               count++; 
            }
        }
 
        Assert.AreEqual(count, 150);

    }

    [TestMethod]
    public void CinemaroomASelectSeat()
    {
        SeatModel seatModel = new(1, 1, 1, 10);
        (int, int)? test = SeatRoomA.GetSeatInfo(seatModel);
        (int row, int col) = ((int, int))test!;

        Assert.AreEqual(row, 14);
        Assert.AreEqual(col, 1);


        SeatModel seatModel2 = new(150, 1, 1, 10);
        (int, int)? test2 = SeatRoomA.GetSeatInfo(seatModel2);
        (int row2, int col2) = ((int, int))test2!;

        Assert.AreEqual(row2, 1);
        Assert.AreEqual(col2, 8);


        SeatModel seatModel3 = new(151, 1, 1, 10);
        (int, int)? test3 = SeatRoomA.GetSeatInfo(seatModel3);
        Assert.IsNull(test3);



    
    }
}
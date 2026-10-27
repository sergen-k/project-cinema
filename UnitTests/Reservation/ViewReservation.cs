namespace UnitTests;


[TestClass]
public sealed class ViewReservation
{


    [TestMethod]
    public void TestCorrectReservations()
    {
    using (StringWriter sw = new StringWriter())
    {
        Console.SetOut(sw);

        

        string expected = string.Format("Ploeh{0}", Environment.NewLine);
        Assert.AreEqual<string>(expected, sw.ToString());
    }


    }


}

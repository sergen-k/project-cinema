namespace UnitTests;

[TestClass]
public class MovieTest
{
    [DataTestMethod]
    [DataRow(-1, "Invalid age")]
    [DataRow(12, "PG")]
    [DataRow(13, "PG-13")]
    [DataRow(16, "R")]
    public void testAgeRange(int age, string expected)
    {
        string result = MovieLogic.AgeToCategory(age);
        Assert.AreEqual(expected, result);
    }


    [TestMethod]
    public void GetAllMovies()
    {
        MovieLogic movieLogic = new();
        List<MovieModel> MovieModels = movieLogic.SelectAll();

        Assert.AreEqual(MovieModels.Count, 15);
    }



    [DataTestMethod]
    [DataRow(0, "The Lord of the Rings: Fellowship of the ring")]
    [DataRow(14, "Shaun of the Dead")]
    public void GetMovie(int id, string title)
    {
        MovieLogic movieLogic = new();
        MovieModel MovieModel = movieLogic.Select(id);

        Assert.AreEqual(MovieModel.Title, title);
    }

    [DataTestMethod]
    [DataRow( "",  "genre",  "director",  "description", 16,  "120",  false)]
    [DataRow( "title",  "",  "director",  "description", 16,  "120",  false)]
    [DataRow( "title",  "Genre",  "",  "description", 16,  "120",  false)]
    [DataRow( "title",  "Genre",  "director",  "", 16,  "120",  false)]
    [DataRow( "title",  "genre",  "director",  "description", -16,  "120",  false)]
    [DataRow( "title",  "genre",  "director",  "description", 16,  "",  false)]
    [DataRow( "title",  "genre",  "director",  "description", 16,  "120",  true)]
    public void CreateMovieAdmin(string title, string genre, string director, string description, long ageLimit, string duration, bool success)
    {
        MovieLogic movieLogic = new();

        UserModel userModel = new();
        userModel.Role = "Admin";

        bool MovieModel = movieLogic.AddMovie(userModel,title, genre, director, description, ageLimit, duration);

        Assert.AreEqual(MovieModel, success);
    }    


    [TestMethod]
    public void TestAgeCheck()
    {
        MovieModel movieModel = new(1, "test", "blah", "ddf", "test", 16, 120.0);

        UserModel adult = new();
        adult.DateOfBirth = ((DateTimeOffset)DateTime.Today.AddYears(-19)).ToUnixTimeSeconds();

        UserLogic.ChangeAccount(adult);

        bool result = MovieLogic.IsOfAge(movieModel);
        Assert.AreEqual(true, result);

        UserModel child = new();
        child.DateOfBirth = ((DateTimeOffset)DateTime.Today.AddYears(-10)).ToUnixTimeSeconds();

        UserLogic.ChangeAccount(child);

        bool result2 = MovieLogic.IsOfAge(movieModel);
        Assert.AreEqual(false, result2);
    }

}
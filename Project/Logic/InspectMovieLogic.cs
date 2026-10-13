public class InspectMovieLogic
{
    public static bool IsLoggedIn() => Menu.User == null ? false : true;

    public static bool IsOfAge(MovieModel movie)
    {
        if (IsLoggedIn())
        {
            // replace with calculation to determine age and if its actually age appropriate
            return true;
        }
        else
        {
            return false;
        }
    }
}
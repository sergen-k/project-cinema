public class InspectMovieLogic
{
    public static bool IsLoggedIn() => AccountsLogic.CurrentAccount != null;

    public static bool IsOfAge(MovieModel movie)
    {
        if (IsLoggedIn())
        {
            DateTime today = DateTime.Today;
            DateTime birthdate = AccountsLogic.CurrentAccount.HumanReadableDateOfBirth;

            int age = DateTime.Today.Year-birthdate.Year;
            if (today < birthdate.AddYears(age)) {age--;}
            
            if (age >= movie.AgeLimit){return true;}
        }
        return false;
    }
}
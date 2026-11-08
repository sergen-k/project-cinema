public static class InspectMovie
{
    public static void Start(MovieModel movie)
    {
        bool exit = false;
        do
        {
            DisplayInfo(movie);
            string option = Options(InspectMovieLogic.IsOfAge(movie), movie);
            if (option == "1")
            {
                return;
            }
            else
            {
                int succes = MakeReservation.Start(movie);
                Console.Clear();
                if (succes == 1)
                {
                    exit = true;
                } 
            }

        } while (exit == false);
    }

    private static void DisplayInfo(MovieModel movie)
    {
        Console.WriteLine(movie.Title);

        Console.WriteLine($"Genre: {movie.Genre}");

        Console.WriteLine($"Directed by: {movie.Director}");
        Console.WriteLine($"Must be at least {movie.AgeLimit} years old");
        Console.WriteLine($"Duration: {movie.Duration} minutes");
        Console.WriteLine();
        Console.WriteLine(movie.Description);
        Console.WriteLine();
    }

    private static string Options(bool isOfAge, MovieModel movie)
    {
        string answer;
        do
        {
            TimetableAccess timetableAccess = new();
            List<TimetableModel> timetables = timetableAccess.SelectByMovieId(movie);
            Console.WriteLine("1. Return to Menu");
            if (isOfAge && timetables.Count > 0)
                Console.WriteLine("2. Reserve Movie");
            answer = Console.ReadLine()!;
        } while (!(answer == "1" || (answer == "2" && isOfAge)));
        return answer;
    }

}
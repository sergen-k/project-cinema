public static class InspectMovie
{
    public static void Start(MovieModel movie)
    {
        DisplayInfo(movie);
        string option = Options(InspectMovieLogic.IsOfAge(movie));
        if(option == "1")
        {
            return;
        }
        else
        {
            MakeReservation.Start(movie);
        }

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

    private static string Options(bool isOfAge)
    {
        string answer;
        do
        {
            Console.WriteLine("1. Return to Menu");
            if (isOfAge)
                Console.WriteLine("2. Reserve Movie");
            answer = Console.ReadLine()!;
        } while (!(answer == "1" || (answer == "2" && isOfAge)));
        return answer;
    }

}
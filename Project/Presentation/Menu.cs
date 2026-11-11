static class Menu
{
    const string Banner = """
        ══════════════════════════════════════════════════════════════════════════════

        ██████╗  ██████╗ ████████╗████████╗███████╗██████╗ ██████╗  █████╗ ███╗   ███╗
        ██╔══██╗██╔═══██╗╚══██╔══╝╚══██╔══╝██╔════╝██╔══██╗██╔══██╗██╔══██╗████╗ ████║
        ██████╔╝██║   ██║   ██║      ██║   █████╗  ██████╔╝██║  ██║███████║██╔████╔██║
        ██╔══██╗██║   ██║   ██║      ██║   ██╔══╝  ██╔══██╗██║  ██║██╔══██║██║╚██╔╝██║
        ██║  ██║╚██████╔╝   ██║      ██║   ███████╗██║  ██║██████╔╝██║  ██║██║ ╚═╝ ██║
        ╚═╝  ╚═╝ ╚═════╝    ╚═╝      ╚═╝   ╚══════╝╚═╝  ╚═╝╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝

                     ██████╗██╗███╗   ██╗███████╗███╗   ███╗ █████╗
                    ██╔════╝██║████╗  ██║██╔════╝████╗ ████║██╔══██╗
                    ██║     ██║██╔██╗ ██║█████╗  ██╔████╔██║███████║
                    ██║     ██║██║╚██╗██║██╔══╝  ██║╚██╔╝██║██╔══██║
                    ╚██████╗██║██║ ╚████║███████╗██║ ╚═╝ ██║██║  ██║
                    ╚═════╝╚═╝╚═╝  ╚═══╝╚══════╝╚═╝     ╚═╝╚═╝  ╚═╝

        ══════════════════════════════════════════════════════════════════════════════
        
        """;
    public static void Start()
    {
        while (true)
        {
            ConsoleMenu menu = new();
            menu.AddOption(new InfoOption(Banner));
            menu.PopulateOptions(GetOptions());

            string chosenOption = menu.SelectOption();

            Console.Clear();
            switch (chosenOption) // Got to give each option a body
            {
                case "About Us":
                    break;
                case "Browse Movies":
                    BrowseMovies.Start();
                    break;
                case "Register":
                    Register.Start();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
                case "Log In":
                    UserLogin.Start();
                    break;
                case "Log Out":
                    AccountsLogic.LogOut();
                    break;
                case "View Reservations":
                    Console.CursorVisible = true;
                    ViewReservation viewReservation = new();
                    viewReservation.Start();
                    Console.CursorVisible = false;
                    break;
                case "Edit Profile":
                    Console.CursorVisible = true;
                    EditProfile.Start();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.CursorVisible = false;
                    break;
                case "Add Movie":
                    AdminAddMovie.Start();
                    break;

                case "Auto Log (Test)":
                    UserAccess userAccess = new();
                    AccountsLogic.ChangeAccount(userAccess.Select(1));
                    break;
            }
        }
    }

    private static List<string> GetOptions()
    {
        List<string> options = [];
        options.Add("Browse Movies");
        if (AccountsLogic.CurrentAccount == null)
        {
            options.Add("Log In");
            options.Add("Register");
        }
        else
        {
            options.Add("Log Out");
            options.Add("View Reservations");
            options.Add("Edit Profile");

            if (AccountsLogic.CurrentAccount?.Role == "admin")
            {
                options.Add("Add Movie");
            }
        }
        options.Add("About Us");
        if (!InspectMovieLogic.IsLoggedIn())
        {
        options.Add("Auto Log (Test)");
        }
        return options;
    }
}
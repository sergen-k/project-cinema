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
    private static List<string> _options = new();
    public static void Start()
    {
        while (true)
        {
            LoadOptions(); 
            ConsoleMenu menu = new(Banner);
            string chosenMenu = menu.SelectOption(_options);

            Console.Clear();
            switch (chosenMenu) // Got to give each option a body
            {
                case "About Us":
                    break;
                case "Browse Movies":
                    BrowseMovies.Start();
                    break;
                case "Register":
                    Register.Start();
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
                case "Add Movie":
                    AdminAddMovie.Start();
                    break;

                case "change users settings":
                     AdminChangeUserSettings.Start();
                     break;    


            }
            // Console.WriteLine("Press any key to continue...");
            // Console.ReadKey();

            Console.Clear();
        }
    }

    private static void LoadOptions()
    {
        _options.Clear();
        int indexer = 0;
        _options.Add("Browse Movies");
        if (AccountsLogic.CurrentAccount == null)
        {
            _options.Add("Log In");
            _options.Add("Register");
        }
        else
        {
            _options.Add("Log Out");
            _options.Add("View Reservations");

            if (AccountsLogic.CurrentAccount?.Role == "Admin")
            {
                _options.Add("Add Movie");
                _options.Add("change users settings");
            }
        }
        _options.Add("About Us");
        if (!InspectMovieLogic.IsLoggedIn())
        {
        _options.Add("Auto Log (Test)");
        }
    }
}
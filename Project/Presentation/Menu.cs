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
            _options.Add("Edit Profile");

            if (AccountsLogic.CurrentAccount?.Role == "admin")
            {
                _options.Add("Add Movie");
            }
        }
        _options.Add("About Us");
        if (!InspectMovieLogic.IsLoggedIn())
        {
        _options.Add("Auto Log (Test)");
        }
    }
}
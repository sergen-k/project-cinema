static class MainMenu
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
            menu.IsMainMenu = true;
            menu.AddOption(new InfoOption(Banner));
            menu.PopulateOptions(GetOptions());

            string chosenOption = menu.SelectOption();

            Console.Clear();
            switch (chosenOption)
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
                    Console.CursorVisible = true;
                    UserLogin.Start();
                    Console.CursorVisible = false;
                    break;

                case "Log Out":
                    UserLogic.LogOut();
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
                    Console.CursorVisible = false;
                    break;

                case "Add Movie":
                    AdminAddMovie.Start();
                    break;

                case "░▒▓ ADMIN PANEL ▓▒░":
                    AdminPanel.Start();
                    break;

                case "Auto Log (Temporary)":
                    UserAccess userAccess = new();
                    UserLogic.ChangeAccount(userAccess.SelectAllById(1));
                    break;

                case null:
                    break;
            }
            if (chosenOption == null)
            {
                break;
            }
        }
    }

    private static List<string> GetOptions()
    {
        List<string> options = [];

        if (UserLogic.CurrentAccount?.Role == "Admin")
        {
            options.Add("░▒▓ ADMIN PANEL ▓▒░");
        }

        options.Add("Browse Movies");
        if (UserLogic.CurrentAccount == null)
        {
            options.Add("Log In");
            options.Add("Register");
        }
        else
        {
            options.Add("Log Out");
            options.Add("View Reservations");
            options.Add("Edit Profile");
        }

        options.Add("About Us");

        if (!UserLogic.IsLoggedIn())
        {
            options.Add("Auto Log (Temporary)");
        }

        return options;
    }
}
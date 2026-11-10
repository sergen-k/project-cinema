
public class ConsoleMenu<T>
{
    public string Header { get; set; }

    public ConsoleMenu(string header) => Header = header;
    public ConsoleMenu() => Header = null;

    public T SelectOption(List<T> options)
    {
        if (options == null || options.Count == 0)
        {
            return default;
        }

        int selectedIndex = 0;

        while (true)
        {
            Console.Clear();

            if (Header != null)
                Console.WriteLine(Header);

            for (int i = 0; i < options.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;

                    Console.WriteLine($"> {options[i]}");

                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {options[i]}");
                }
            }

            ConsoleKey key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex =
                        (selectedIndex - 1 + options.Count) % options.Count;
                    break;

                case ConsoleKey.DownArrow:
                    selectedIndex =
                        (selectedIndex + 1) % options.Count;
                    break;

                case ConsoleKey.Enter:
                    return options[selectedIndex];

                case ConsoleKey.Escape:
                    return default;
            }
        }
    }
}

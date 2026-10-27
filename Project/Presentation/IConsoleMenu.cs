using System.Collections.Generic;

public interface IConsoleMenu<T>
{
    int SelectOption (List<T> options);
}
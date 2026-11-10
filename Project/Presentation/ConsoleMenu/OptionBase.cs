public abstract class OptionBase<T>
{
    public abstract string Label {get; set;}
    public abstract T ReturnInfo {get; set;}
    public abstract bool IsSelectable {get;}
    public abstract bool IsSearchable {get;}
    public virtual void PrintOut(bool isSelected)
    {
        if (isSelected)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write($"> {Label} ");
            Console.ResetColor();
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine($"  {Label} ");
        }
    }
    
}
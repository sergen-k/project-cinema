public abstract class OptionBase
{
    public abstract string Label {get; set;}
    public virtual bool IsSelectable => false;
    public virtual bool IsSearchable => false;
    public virtual bool IsScrollable => false;
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


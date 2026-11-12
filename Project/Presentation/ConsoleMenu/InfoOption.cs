public class InfoOption<T> : OptionBase<T>
{
    
    public override string Label {get; set;}
    public override T ReturnInfo {get => default ; set;}
    public override bool IsSelectable {get;} = false;
    public override bool IsSearchable {get;} = false;

    public InfoOption(string label) => Label = label;
    public override void PrintOut(bool isSelected)
    {
        Console.WriteLine(Label);
    }
}

public class InfoOption : InfoOption<string>
{
    public InfoOption(string label) : base(label){}
}
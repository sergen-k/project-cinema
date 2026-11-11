public class SelectOption<T> : OptionBase<T>
{
    
    public override string Label {get; set;}
    public override T ReturnInfo {get; set;}
    public override bool IsSelectable {get;} = true;
    public override bool IsSearchable {get;} = false;

    public SelectOption(string label, T returnInfo)
    {
        Label = label;
        ReturnInfo = returnInfo;
    }
}

public class SelectOption : SelectOption<string>
{
    public SelectOption(string label) : base(label, label){}
}
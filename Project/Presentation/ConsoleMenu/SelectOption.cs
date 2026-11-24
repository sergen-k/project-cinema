public class SelectOption : OptionBase
{
    
    public override string Label {get; set;}
    public override bool IsSelectable => true;
    public override bool IsSearchable => false;
    public override bool IsScrollable => false;

    public SelectOption(string label)
    {
        Label = label;
    }
}
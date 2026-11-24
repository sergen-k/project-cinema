public class InfoOption : OptionBase
{
    
    public override string Label {get; set;}
    public override bool IsSelectable => false;
    public override bool IsSearchable => false;
    public override bool IsScrollable => false;

    public InfoOption(string label) => Label = label;
    public override void PrintOut(bool isSelected) => Console.WriteLine(Label);
}
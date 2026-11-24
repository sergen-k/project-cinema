public class ScrollOption : SelectOption
{
    public override bool IsScrollable => true;
    public ScrollOption(string label) : base(label){}
}
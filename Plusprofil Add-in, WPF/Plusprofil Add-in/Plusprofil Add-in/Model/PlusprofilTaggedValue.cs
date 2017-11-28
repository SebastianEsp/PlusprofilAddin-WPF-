namespace PlusprofilAddin.Model
{
    public class PlusprofilTaggedValue
    {
        public string Title { get; set; }
        public string Value { get; set; }

        public PlusprofilTaggedValue(string title, string value)
        {
            Title = title;
            Value = value;
        }
    }
}

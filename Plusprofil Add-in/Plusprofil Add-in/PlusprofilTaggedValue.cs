namespace PlusprofilAddin.Model
{
    public class PlusprofilTaggedValue
    {
        public string Name { get; }
        public string Definition { get; }
        public bool HasMemoField { get; }
        public bool ManyMultiplicity { get; }

        public PlusprofilTaggedValue(string name, string definition, bool hasMemoField, bool manyMultiplicity)
        {
            Name = name;
            Definition = definition;
            HasMemoField = hasMemoField;
            ManyMultiplicity = manyMultiplicity;
        }
    }
}
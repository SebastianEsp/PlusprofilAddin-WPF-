namespace PlusprofilAddin
{
	public class PlusprofilTaggedValue
	{
		public PlusprofilTaggedValue(string name, bool hasMemoField, bool manyMultiplicity)
		{
			Name = name;
			HasMemoField = hasMemoField;
			ManyMultiplicity = manyMultiplicity;
		}

		public string Name { get; }
		public bool HasMemoField { get; }
		public bool ManyMultiplicity { get; }
	}
}
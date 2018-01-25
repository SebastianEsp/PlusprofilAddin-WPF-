using System.Windows;

namespace PlusprofilAddin
{
	public class PlusprofilTaggedValue
	{
		public PlusprofilTaggedValue(string resourceKey, string name, bool hasMemoField, bool manyMultiplicity)
		{
			ResourceKey = resourceKey;
			Name = name;
			HasMemoField = hasMemoField;
			ManyMultiplicity = manyMultiplicity;
		}

		public string ResourceKey { get; }
		public string Name { get; }
		public bool HasMemoField { get; }
		public bool ManyMultiplicity { get; }
	}
}
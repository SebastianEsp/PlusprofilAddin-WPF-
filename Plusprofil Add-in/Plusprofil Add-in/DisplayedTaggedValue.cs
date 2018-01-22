using System.Windows;
using EA;

namespace PlusprofilAddin
{
	class DisplayedTaggedValue
	{
		//TODO: Refactor any methods calling this constructor to instead create new tagged value
		public DisplayedTaggedValue(string name, string value)
		{
			Name = name;
			Value = value;
		}

		public DisplayedTaggedValue(dynamic taggedValue)
		{
			TaggedValue = taggedValue;
			ObjectType = (ObjectType) taggedValue.ObjectType; // Note: taggedValue.ObjectType has type "short"

			// Set Name field based on ObjectType
			switch (ObjectType)
			{
				case ObjectType.otTaggedValue:
				case ObjectType.otAttribute:
				{
					Name = TaggedValue.Name;
					break;
				}
				case ObjectType.otConnectorEnd:
				{
					Name = TaggedValue.Tag;
					break;
				}
			}

			// Set PlusprofilTaggedValue field based on Name
			PlusprofilTaggedValue = PlusprofilTaggedValueDefinitions.Definitions.Find(ptv => ptv.Name == Name);

			// Set Value based on ObjectType and PlusprofilTaggedValue

			switch (ObjectType)
			{
				case ObjectType.otTaggedValue:
				case ObjectType.otAttribute:
				{
					Value = PlusprofilTaggedValue.HasMemoField ? TaggedValue.Notes : TaggedValue.Value;
					break;
				}
				case ObjectType.otConnectorEnd:
				{
					if (PlusprofilTaggedValue.HasMemoField)
					{
						// TODO: Use regex to set DisplayTaggedValue.Value to substring after $ea_notes=
					}
					else
					{
						// TODO: Use regex to set DisplayTaggedValue.Value to substring before $ea_notes=
					}
					break;
				}
			}
		}

		public void UpdateTaggedValueValue()
		{
			switch (ObjectType)
			{
				case ObjectType.otAttributeTag:
				case ObjectType.otTaggedValue:
					if (PlusprofilTaggedValue.HasMemoField) TaggedValue.Notes = Value;
					else TaggedValue.Value = Value;
					break;

				case ObjectType.otRoleTag:
					// TODO: Use regex to add DisplayTaggedValue.Value before or after $ea_notes=
					break;
			}
			TaggedValue.Update();
		}

		public dynamic TaggedValue { get; }
		public string Name { get; set; }
		public string Value { get; set; }
		public ObjectType ObjectType { get; set; }
		public PlusprofilTaggedValue PlusprofilTaggedValue { get; }
		
		public override string ToString()
		{
			return $"DisplayTaggedValue with name \"{Name}\" and value \"{Value}\"";
		}
	}
}
﻿using EA;

namespace PlusprofilAddin
{
	public class DisplayedTaggedValue
	{
		public DisplayedTaggedValue(string name)
		{
			Name = name;
			Value = "";
			PlusprofilTaggedValue = PlusprofilTaggedValueDefinitions.Definitions.Find(ptv => ptv.Name == Name);
		}

		public DisplayedTaggedValue(dynamic taggedValue)
		{
			TaggedValue = taggedValue;
			ObjectType = (ObjectType) TaggedValue.ObjectType;

			// Set Name field based on ObjectType
			switch (ObjectType)
			{
				case ObjectType.otTaggedValue:
				case ObjectType.otAttributeTag:
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
				case ObjectType.otAttributeTag:
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
				case ObjectType.otTaggedValue:
				case ObjectType.otAttributeTag:
					if (PlusprofilTaggedValue.HasMemoField) TaggedValue.Notes = Value;
					else TaggedValue.Value = Value;
					break;

				case ObjectType.otRoleTag:
					// TODO: Use regex to add DisplayTaggedValue.Value before or after $ea_notes=
					break;
			}
			TaggedValue.Update();
		}

		public void AddTaggedValue(Collection taggedValues)
		{
			dynamic taggedValue = taggedValues.AddNew(Name, "");
			TaggedValue = taggedValue;
			ObjectType = (ObjectType) TaggedValue.ObjectType;
		}

		public void DeleteTaggedValue(Collection taggedValues)
		{
			// TODO: Find alternative to Collection traversal
			for (short i = 0; i < taggedValues.Count; i++)
			{
				switch (ObjectType)
				{
					case ObjectType.otTaggedValue:
					case ObjectType.otRoleTag:
						if(taggedValues.GetAt(i).PropertyGUID == TaggedValue.PropertyGUID) taggedValues.Delete(i);
						break;
					case ObjectType.otAttributeTag:
						if(taggedValues.GetAt(i).TagGUID == TaggedValue.TagGUID) taggedValues.Delete(i);
						break;
				}
				taggedValues.Refresh();
			}
		}

		public dynamic TaggedValue { get; set; }
		public string Name { get; set; }
		public string Value { get; set; }
		public ObjectType ObjectType { get; set; }
		public PlusprofilTaggedValue PlusprofilTaggedValue { get; set; }

		public override string ToString()
		{
			return $"DisplayTaggedValue with name \"{Name}\"\nValue: {Value}\nObjectType: {ObjectType}";
		}
	}
}
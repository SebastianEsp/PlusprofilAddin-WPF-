using System;
using System.Windows.Forms;
using EA;

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
				case ObjectType.otRoleTag:
				{
					Name = TaggedValue.Tag;
					break;
				}
			}

			// Set PlusprofilTaggedValue field based on Name
			PlusprofilTaggedValue = PlusprofilTaggedValueDefinitions.Definitions.Find(ptv => ptv.Name == Name);

			// TODO: Create property "DisplayName" and set it to the StringsResource equivalent to Name

			// Set Value based on ObjectType and PlusprofilTaggedValue
			switch (ObjectType)
			{
				case ObjectType.otTaggedValue:
				case ObjectType.otAttributeTag:
				{
					Value = PlusprofilTaggedValue.HasMemoField ? TaggedValue.Notes : TaggedValue.Value;
					break;
				}
				case ObjectType.otRoleTag:
				{
					// Ensure that TaggedValue.Value has format "{value}$ea_notes={notes}",
					// tokenize the string, then set Value = {notes} if HasMemoField, otherwise Value = {value}
					if (!TaggedValue.Value.Contains("$ea_notes")) {TaggedValue.Value = String.Concat(TaggedValue.Value, "$ea_notes=");
						TaggedValue.Update();
					}
					string[] tokens = TaggedValue.Value.Split(new[] {"$ea_notes="}, StringSplitOptions.None);
					Value = PlusprofilTaggedValue.HasMemoField ? tokens[1] : tokens[0];
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
					// Ensure that TaggedValue.Value has format "{value}$ea_notes={notes}",
					// tokenize the string, then set Value = {notes} if HasMemoField, otherwise Value = {value}
					// "$ea_notes=" check may be superfluous, requires additional testing
					if (!TaggedValue.Value.Contains("$ea_notes")) TaggedValue.Value = String.Concat(TaggedValue.Value, "$ea_notes=");
					string[] tokens = TaggedValue.Value.Split(new[] {"$ea_notes="}, StringSplitOptions.None);
					TaggedValue.Value = PlusprofilTaggedValue.HasMemoField ? $"{tokens[0]}$ea_notes={Value}" : $"{Value}$ea_notes={tokens[1]}";
					break;
			}
			TaggedValue.Update();
		}

		public void AddTaggedValue(Collection taggedValues)
		{
			MessageBox.Show("In AddTaggedValue");
			dynamic taggedValue = taggedValues.AddNew(Name, "");
			MessageBox.Show("Setting TaggedValue = taggedValue");
			TaggedValue = taggedValue;
			MessageBox.Show($"Setting ObjectType = {TaggedValue.ObjectType}");
			ObjectType = (ObjectType) TaggedValue.ObjectType;
			MessageBox.Show("Exiting AddTaggedValue");
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
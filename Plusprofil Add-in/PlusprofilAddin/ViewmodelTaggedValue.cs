using System;
using System.Windows;
using EA;

namespace PlusprofilAddin
{
	/// <summary>
	/// Class used to represent a single <c>EA.TaggedValue</c>, <c>EA.RoleTag</c> or <c>EA.AttributeTag</c> in the add-in.
	/// View classes retrieve information from the properties of this class. Likewise, <c>SaveCommand</c> uses the information stored in this class to update the corresponding tagged value in Sparwx Enterprise Architect.
	/// </summary>
	public class ViewmodelTaggedValue
	{

		/// <summary>
		/// Creates a new <c>ViewmodelTaggedValue</c> to represent an <c>EA.TaggedValue</c>, <c>EA.RoleTag</c> or <c>EA.AttributeTag</c> which is unknown or has not yet been created.
		/// </summary>
		/// <param name="key"></param>
		public ViewmodelTaggedValue(string key)
		{
			Key = key;
			PlusprofilTaggedValue = PlusprofilTaggedValueDefinitions.Definitions.Find(ptv => ptv.Key == Key);
			Name = PlusprofilTaggedValue.Name;
			Value = "";
			DisplayedName = "";
		}

		/// <summary>
		/// Creates a new <c>ViewmodelTaggedValue</c> to represent an <c>EA.TaggedValue</c>, <c>EA.RoleTag</c> or <c>EA.AttributeTag</c> passed as a parameter.
		/// </summary>
		/// <param name="taggedValue"></param>
		public ViewmodelTaggedValue(dynamic taggedValue)
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
		}

		/// <summary>
		/// String to identify what <c>PlusprofilTaggedValue</c> the <c>ViewmodelTaggedValue</c> corresponds to.
		/// </summary>
		public string Key { get; set; }
		
		/// <summary>
		/// String used to identify the name of the tagged value in Sparx Enterprise Architect.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// String used as the name displayed in the view, based on <c>Key</c> and the language selected in <c>MainClass</c>.
		/// </summary>
		public string DisplayedName { get; set; }

		/// <summary>
		/// String representing the value of the tagged value as shown in the view.
		/// Saving the value correctly in Sparx Enterprise Architect is handled in the <c>ViewmodelTaggedValue.UpdateTaggedValue()</c> method, rather than here.
		/// </summary>
		public string Value { get; set; }

		/// <summary>
		/// The <c>EA.TaggedValue</c>, <c>EA.RoleTag</c> or <c>EA.AttributeTag</c> in Sparx Enterprise Architect that this <c>ViewmodelTaggedValue</c> represents.
		/// </summary>
		public dynamic TaggedValue { get; set; }

		/// <summary>
		/// The <c>EA.ObjectType</c> of <c>ViewmodelTaggedValue.TaggedValue</c>
		/// Always has type <c>EA.ObjectType.otTaggedValue</c>, <c>EA.ObjectType.otRoleTag</c> or <c>EA.ObjectType.otAttributeTag</c>.
		/// Note that when retrieving the value from Sparx Enterprise Architect, it has type <c>short</c>, and thus must be casted to <c>EA.ObjectType</c>
		/// </summary>
		public ObjectType ObjectType { get; set; }

		/// <summary>
		/// The <c>PlusprofilTaggedValue</c> that corresponds to the tagged value retrieved from Enterprise Architect.
		/// </summary>
		public PlusprofilTaggedValue PlusprofilTaggedValue { get; set; }

		/// <summary>
		/// The <c>ResourceDictionary</c> used for the view.
		/// Used to set <c>ViewmodelTaggedValue.DisplayedName</c> based on <c>ViewmodelTaggedValue.Key</c>.
		/// </summary>
		public ResourceDictionary ResourceDictionary { get; set; }


		/// <summary>
		/// Used to initialize properties which cannot be initialized during creation of the <c>ViewmodelTaggedValue</c> due to missing information.
		/// </summary>
		public void Initialize()
		{
			// Set PlusprofilTaggedValue field based on Key
			PlusprofilTaggedValue = PlusprofilTaggedValueDefinitions.Definitions.Find(ptv => ptv.Key == Key);

			// Set DisplayedNamed by retrieving localized string from ResourceDictionary using PlusprofilTaggedValue.Key
			DisplayedName = (string) ResourceDictionary[PlusprofilTaggedValue.Key];


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
					if (!TaggedValue.Value.Contains("$ea_notes")) {TaggedValue.Value = string.Concat(TaggedValue.Value, "$ea_notes=");
						TaggedValue.Update();
					}
					string[] tokens = TaggedValue.Value.Split(new[] {"$ea_notes="}, StringSplitOptions.None);
					Value = PlusprofilTaggedValue.HasMemoField ? tokens[1] : tokens[0];
					break;
				}
			}
		}

		/// <summary>
		/// Updates the <c>Value</c> property of the <c>ViewmodelTaggedValue.TaggedValue</c>, effectively saving changes made to the value in the add-in.
		/// </summary>
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
					// TODO: "$ea_notes=" check may be superfluous, requires additional testing
					if (!TaggedValue.Value.Contains("$ea_notes")) TaggedValue.Value = String.Concat(TaggedValue.Value, "$ea_notes=");
					string[] tokens = TaggedValue.Value.Split(new[] {"$ea_notes="}, StringSplitOptions.None);
					TaggedValue.Value = PlusprofilTaggedValue.HasMemoField ? $"{tokens[0]}$ea_notes={Value}" : $"{Value}$ea_notes={tokens[1]}";
					break;
			}
			TaggedValue.Update();
		}

		/// <summary>
		/// Given a collection of tagged values, adds a new tagged value with information corresponding to that given in the <c>ViewmodelTaggedValue</c>.
		/// <c>ViewmodelTaggedValue.UpdateTaggedValueValue()</c> is used to update information and ensure that <c>EA.TaggedValue.Update()</c> is called or similar for classes <c>EA.RoleTag</c> or <c>EA.AttributeTag</c>.
		/// </summary>
		/// <param name="taggedValues">The <c>EA.Collection</c> to add the tagged value to.</param>
		public void AddTaggedValue(Collection taggedValues)
		{
			dynamic taggedValue = taggedValues.AddNew(Name, "");
			TaggedValue = taggedValue;
			ObjectType = (ObjectType) TaggedValue.ObjectType;
			UpdateTaggedValueValue();
		}


		/// <summary>
		/// Given a collection of tagged values, removes the tagged value with a GUID matching the GUID of the <c>ViewmodelTaggedValue.Value</c>.
		/// As <c>EA.Collection.GetByName(string Name)</c> does not support classes <c>EA.Attribute</c> and <c>EA.ConnectorEnd</c>, the <c>EA.Collection</c> must be iterated through using <c>EA.Collection.GetAt(short index)</c>, comparing the GUID at each index.
		/// </summary>
		/// <param name="taggedValues">The <c>Collection</c> to delete the tagged value from.</param>
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

		public override string ToString()
		{
			return $"Type: {GetType()}\n" +
				   $"Key: {Key}\n" +
			       $"Name: {Name}\n" +
			       $"Value: {Value}\n" +
			       $"ObjectType: {ObjectType}\n" +
			       $"PlusprofilTaggedValue: {PlusprofilTaggedValue.Name}\n" +
			       $"ResourceDictionary: {ResourceDictionary}";
		}
	}
}
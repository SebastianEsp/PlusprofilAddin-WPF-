using System.Collections.Generic;

namespace PlusprofilAddin
{
	/// <summary>
	/// Class used to define how a tagged value retrieved from Enterprise Architect should be presented
	/// </summary>
	public class PlusprofilTaggedValue
	{
		/// <summary>
		/// Constructs a <c>PlusprofilTaggedValue</c> with the given key, name and values on expected tagged value behaviour
		/// </summary>
		/// <param name="key"></param>
		/// <param name="name"></param>
		/// <param name="hasMemoField"></param>
		/// <param name="manyMultiplicity"></param>
        /// <param name="isChild"></param>
		public PlusprofilTaggedValue(string key, string name, bool hasMemoField, bool manyMultiplicity, bool isChild)
		{
			Key = key;
			Name = name;
			HasMemoField = hasMemoField;
			ManyMultiplicity = manyMultiplicity;
            IsChild = isChild;
		}
		
		/// <summary>
		/// Gets the key used to identify this <c>PlusprofilTaggedValue</c>
		/// Used as a unique ID when referenced in other classes, and to retrieve the relevant localized string with the same key
		/// </summary>
		public string Key { get; }
		
		/// <summary>
		/// Gets the expected name of the tagged value in Enterprise Architect and as defined in Plusprofilen
		/// Used to retrieve tagged values from Enterprise Architect
		/// </summary>
		public string Name { get; }
		
		/// <summary>
		/// Gets a bool representing whether the tagged value is expected to store its value in <c>TaggedValue.Value</c> or <c>TaggedValue.Notes</c>, or similar for the <c>RoleTag</c> class
		/// Used to change how the value is shown in View, and how the tagged value is updated when <c>SaveCommand.Execute(object parameter)</c> is called
		/// </summary>
		public bool HasMemoField { get; }
		
		/// <summary>
		/// Gets a bool representing whether it should be allowed to have more than one of the tagged value
		/// Used to enable or disable the "Add" button in View
		/// </summary>
		public bool ManyMultiplicity { get; }

        /// <summary>
        /// Gets a bool representing whether a tagged value is a child of another tagged value
        /// Used to enable or disable the "Remove" button in View
        /// </summary>
        public bool IsChild { get; set; }
    }

		/// <summary>
	/// Definitions of tagged values defined in Plusprofilen.
	/// Used to define which tagged values can be retrieved from Enterprise Architect, and how they should be presented according to the implementation of the <c>PlusprofilTaggedValue</c> class.
	/// In a future iteration, these definitions could be loaded from a file.
	/// </summary>
	public static class PlusprofilTaggedValueDefinitions
	{
		/// <summary>
		/// List of all definitions used for setting the property <c>ViewmodelTaggedValue.PlusprofilTaggedValue</c>
		/// Using a <c>HashMap</c> will improve performance. However, a <c>List</c> is used for simplicity.
		/// </summary>
		public static List<PlusprofilTaggedValue> Definitions = new List<PlusprofilTaggedValue>
		{
			new PlusprofilTaggedValue("AltLabelDa", "altLabel (da)", false, true, false),
			new PlusprofilTaggedValue("AltLabelEn", "altLabel (en)", false, true, false),
			new PlusprofilTaggedValue("PrefLabelDa", "prefLabel (da)", false, false, false),
			new PlusprofilTaggedValue("PrefLabelEn", "prefLabel (en)", false, false, false),
			new PlusprofilTaggedValue("DeprecatedLabelDa", "deprecatedLabel (da)", false, true, false),
			new PlusprofilTaggedValue("DeprecatedLabelEn", "deprecatedLabel (en)", false, true, false),
			new PlusprofilTaggedValue("DefinitionDa", "definition (da)", true, false, false),
			new PlusprofilTaggedValue("DefinitionEn", "definition (en)", true, false, false),
			new PlusprofilTaggedValue("ApplicationNoteDa", "applicationNote (da)", true, true, false),
			new PlusprofilTaggedValue("ApplicationNoteEn", "applicationNote (en)", true, true, false),
			new PlusprofilTaggedValue("ExampleDa", "example (da)", true, true, false),
			new PlusprofilTaggedValue("ExampleEn", "example (en)", true, true, false),
			new PlusprofilTaggedValue("CommentDa", "comment (da)", true, true, false),
			new PlusprofilTaggedValue("CommentEn", "comment (en)", true, true, false),
			new PlusprofilTaggedValue("LegalSource", "legalSource", false, true, false),
			new PlusprofilTaggedValue("LegalSourcePackage", "legalSource", true, true, false),
			new PlusprofilTaggedValue("Source", "source", false, true, false),
			new PlusprofilTaggedValue("SourcePackage", "source", true, true, false),
			new PlusprofilTaggedValue("IsDefinedBy", "isDefinedBy", false, true, false),
			new PlusprofilTaggedValue("WasDerivedFrom", "wasDerivedFrom", false, true, false),
			new PlusprofilTaggedValue("EquivalentClass", "equivalentClass", false, true, false),
			new PlusprofilTaggedValue("Range", "range", false, false, false),
			new PlusprofilTaggedValue("RangeConnectorEnd", "range", false, true, false),
			new PlusprofilTaggedValue("Domain", "domain", false, false, false),
			new PlusprofilTaggedValue("DomainConnectorEnd", "domain", false, true, false),
			new PlusprofilTaggedValue("SubPropertyOf", "subPropertyOf", false, true, false),
			new PlusprofilTaggedValue("EquivalentProperty", "equivalentProperty", false, true, false),
			new PlusprofilTaggedValue("FunctionalProperty", "functionalProperty", false, false, false),
			new PlusprofilTaggedValue("InverseOf", "inverseOf", false, false, false),
			new PlusprofilTaggedValue("InverseFunctionalProperty", "inverseFunctionalProperty", false, false, false),
			new PlusprofilTaggedValue("TransitiveProperty", "transitiveProperty", false, false, false),
			new PlusprofilTaggedValue("SymmetricProperty", "symmetricProperty", false, false, false),
			new PlusprofilTaggedValue("ApprovalStatus", "approvalStatus", false, false, false),
			new PlusprofilTaggedValue("ModelStatus", "modelStatus", false, false, false),
			new PlusprofilTaggedValue("Modified", "modified", false, false, false),
			new PlusprofilTaggedValue("Namespace", "namespace", false, false, false),
			new PlusprofilTaggedValue("NamespacePrefix", "namespacePrefix", false, false, false),
			new PlusprofilTaggedValue("Publisher", "publisher", false, false, false),
			new PlusprofilTaggedValue("Theme", "theme", false, false, false),
			new PlusprofilTaggedValue("VersionInfo", "versionInfo", false, false, false),
			new PlusprofilTaggedValue("SubClassOf", "subClassOf", false, true, false),
			new PlusprofilTaggedValue("SameAs", "sameAs", false, true, false),
			new PlusprofilTaggedValue("Type", "type", false, false, false),
			new PlusprofilTaggedValue("LabelDa", "label (da)", false, true, false),
			new PlusprofilTaggedValue("LabelEn", "label (en)", true, true, false),
			new PlusprofilTaggedValue("URI", "URI", false, false, false)
		};
	}
}
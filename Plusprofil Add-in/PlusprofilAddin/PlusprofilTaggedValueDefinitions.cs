using System.Collections.Generic;

namespace PlusprofilAddin
{
	public static class PlusprofilTaggedValueDefinitions
	{
		/*
	     * Definitions of tagged values defined in Plusprofil
	     * Used when retrieving tagged values from Enterprise Architect to present them correctly
	     * by defining the name of the tagged value (PlusprofilTaggedValue.Name),
		 * if the value or notes should be edited (PlusprofilTaggedValue.HasMemoField) 
	     * and if adding multiple tagged values is allowed (PlusprofilTaggedValue.ManyMultiplicity)
		 * In a future iteration, these definitions could be loaded from a file.
	     */
		public static readonly PlusprofilTaggedValue AltLabelDa = new PlusprofilTaggedValue("AltLabel", "altLabel (da)", false, true);
		public static readonly PlusprofilTaggedValue AltLabelEn = new PlusprofilTaggedValue("AltLabel", "altLabel (en)", false, true);
		public static readonly PlusprofilTaggedValue PrefLabelDa = new PlusprofilTaggedValue("PrefLabel", "prefLabel (da)", false, false);
		public static readonly PlusprofilTaggedValue PrefLabelEn = new PlusprofilTaggedValue("PrefLabel", "prefLabel (en)", false, false);
		public static readonly PlusprofilTaggedValue DeprecatedLabelDa = new PlusprofilTaggedValue("DeprecatedLabel", "deprecatedLabel (da)", false, true);
		public static readonly PlusprofilTaggedValue DeprecatedLabelEn = new PlusprofilTaggedValue("DeprecatedLabel", "deprecatedLabel (en)", false, true);
		public static readonly PlusprofilTaggedValue DefinitionDa = new PlusprofilTaggedValue("Definition", "definition (da)", true, false);
		public static readonly PlusprofilTaggedValue DefinitionEn = new PlusprofilTaggedValue("Definition", "definition (en)", true, false);
		public static readonly PlusprofilTaggedValue ApplicationNoteDa = new PlusprofilTaggedValue("ApplicationNote", "applicationNote (da)", true, true);
		public static readonly PlusprofilTaggedValue ApplicationNoteEn = new PlusprofilTaggedValue("ApplicationNote", "applicationNote (en)", true, true);
		public static readonly PlusprofilTaggedValue ExampleDa = new PlusprofilTaggedValue("Example", "example (da)", true, true);
		public static readonly PlusprofilTaggedValue ExampleEn = new PlusprofilTaggedValue("Example", "example (en)", true, true);
		public static readonly PlusprofilTaggedValue CommentDa = new PlusprofilTaggedValue("Comment", "comment (da)", true, true);
		public static readonly PlusprofilTaggedValue CommentEn = new PlusprofilTaggedValue("Comment", "comment (en)", true, true);
		public static readonly PlusprofilTaggedValue LegalSource = new PlusprofilTaggedValue("LegalSource", "legalSource", true, true);
		public static readonly PlusprofilTaggedValue Source = new PlusprofilTaggedValue("Source", "source", true, true);
		public static readonly PlusprofilTaggedValue IsDefinedBy = new PlusprofilTaggedValue("IsDefinedBy", "isDefinedBy", false, true);
		public static readonly PlusprofilTaggedValue WasDerivedFrom = new PlusprofilTaggedValue("WasDerivedFrom", "wasDerivedFrom", false, true);
		public static readonly PlusprofilTaggedValue EquivalentClass = new PlusprofilTaggedValue("EquivalentClass", "equivalentClass", false, true);
		public static readonly PlusprofilTaggedValue Range = new PlusprofilTaggedValue("Range", "range", false, false);
		public static readonly PlusprofilTaggedValue Domain = new PlusprofilTaggedValue("Domain", "domain", false, false);
		public static readonly PlusprofilTaggedValue SubPropertyOf = new PlusprofilTaggedValue("SubPropertyOf", "subPropertyOf", false, true);
		public static readonly PlusprofilTaggedValue EquivalentProperty = new PlusprofilTaggedValue("EquivalentProperty", "equivalentProperty", false, true);
		public static readonly PlusprofilTaggedValue FunctionalProperty = new PlusprofilTaggedValue("FunctionalProperty", "functionalProperty", false, false);
		public static readonly PlusprofilTaggedValue InverseOf = new PlusprofilTaggedValue("InverseOf", "inverseOf", false, false);
		public static readonly PlusprofilTaggedValue InverseFunctionalProperty = new PlusprofilTaggedValue("InverseFunctionalProperty", "inverseFunctionalProperty", false, false);
		public static readonly PlusprofilTaggedValue TransitiveProperty = new PlusprofilTaggedValue("TransitiveProperty", "transitiveProperty", false, false);
		public static readonly PlusprofilTaggedValue SymmetricProperty = new PlusprofilTaggedValue("SymmetricProperty", "symmetricProperty", false, false);
		public static readonly PlusprofilTaggedValue ApprovalStatus = new PlusprofilTaggedValue("ApprovalStatus", "approvalStatus", false, false);
		public static readonly PlusprofilTaggedValue ModelStatus = new PlusprofilTaggedValue("ModelStatus", "modelStatus", false, false);
		public static readonly PlusprofilTaggedValue Modified = new PlusprofilTaggedValue("Modified", "modified", false, false);
		public static readonly PlusprofilTaggedValue Namespace = new PlusprofilTaggedValue("Namespace", "namespace", false, false);
		public static readonly PlusprofilTaggedValue NamespacePrefix = new PlusprofilTaggedValue("NamespacePrefix", "namespacePrefix", false, false);
		public static readonly PlusprofilTaggedValue Publisher = new PlusprofilTaggedValue("Publisher", "publisher", false, false);
		public static readonly PlusprofilTaggedValue Theme = new PlusprofilTaggedValue("Theme", "theme", false, false);
		public static readonly PlusprofilTaggedValue VersionInfo = new PlusprofilTaggedValue("VersionInfo", "versionInfo", false, false);
		public static readonly PlusprofilTaggedValue SubClassOf = new PlusprofilTaggedValue("SubClassOf", "subClassOf", false, true);
		public static readonly PlusprofilTaggedValue SameAs = new PlusprofilTaggedValue("SameAs", "sameAs", false, true);
		public static readonly PlusprofilTaggedValue Type = new PlusprofilTaggedValue("Type", "type", false, false);
		public static readonly PlusprofilTaggedValue LabelDa = new PlusprofilTaggedValue("LabelDa", "label (da)", false, true);
		public static readonly PlusprofilTaggedValue LabelEn = new PlusprofilTaggedValue("LabelEn", "label (en)", false, true);
		public static readonly PlusprofilTaggedValue URI = new PlusprofilTaggedValue("URI", "URI", false, false);

		public static List<PlusprofilTaggedValue> Definitions = new List<PlusprofilTaggedValue>
		{
			AltLabelDa,
			AltLabelEn,
			PrefLabelDa,
			PrefLabelEn,
			DeprecatedLabelDa,
			DeprecatedLabelEn,
			DefinitionDa,
			DefinitionEn,
			ApplicationNoteDa,
			ApplicationNoteEn,
			ExampleDa,
			ExampleEn,
			CommentDa,
			CommentEn,
			LegalSource,
			Source,
			IsDefinedBy,
			WasDerivedFrom,
			EquivalentClass,
			Range,
			Domain,
			SubPropertyOf,
			EquivalentProperty,
			FunctionalProperty,
			InverseOf,
			InverseFunctionalProperty,
			TransitiveProperty,
			SymmetricProperty,
			ApprovalStatus,
			ModelStatus,
			Modified,
			Namespace,
			NamespacePrefix,
			Publisher,
			Theme,
			VersionInfo,
			SubClassOf,
			SameAs,
			Type,
			LabelDa,
			LabelEn,
			URI
		};
	}
}
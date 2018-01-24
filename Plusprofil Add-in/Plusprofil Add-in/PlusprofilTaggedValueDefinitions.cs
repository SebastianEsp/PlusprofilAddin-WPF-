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
		public static readonly PlusprofilTaggedValue AltLabelDa = new PlusprofilTaggedValue("altLabel (da)", false, true);
		public static readonly PlusprofilTaggedValue AltLabelEn = new PlusprofilTaggedValue("altLabel (en)", false, true);
		public static readonly PlusprofilTaggedValue PrefLabelDa = new PlusprofilTaggedValue("prefLabel (da)", false, false);
		public static readonly PlusprofilTaggedValue PrefLabelEn = new PlusprofilTaggedValue("prefLabel (en)", false, false);
		public static readonly PlusprofilTaggedValue DeprecatedLabelDa = new PlusprofilTaggedValue("deprecatedLabel (da)", false, true);
		public static readonly PlusprofilTaggedValue DeprecatedLabelEn = new PlusprofilTaggedValue("deprecatedLabel (en)", false, true);
		public static readonly PlusprofilTaggedValue DefinitionDa = new PlusprofilTaggedValue("definition (da)", true, false);
		public static readonly PlusprofilTaggedValue DefinitionEn = new PlusprofilTaggedValue("definition (en)", true, false);
		public static readonly PlusprofilTaggedValue ApplicationNoteDa = new PlusprofilTaggedValue("applicationNote (da)", true, true);
		public static readonly PlusprofilTaggedValue ApplicationNoteEn = new PlusprofilTaggedValue("applicationNote (en)", true, true);
		public static readonly PlusprofilTaggedValue ExampleDa = new PlusprofilTaggedValue("example (da)", true, true);
		public static readonly PlusprofilTaggedValue ExampleEn = new PlusprofilTaggedValue("example (en)", true, true);
		public static readonly PlusprofilTaggedValue CommentDa = new PlusprofilTaggedValue("comment (da)", true, true);
		public static readonly PlusprofilTaggedValue CommentEn = new PlusprofilTaggedValue("comment (en)", true, true);
		public static readonly PlusprofilTaggedValue LegalSource = new PlusprofilTaggedValue("legalSource", true, true);
		public static readonly PlusprofilTaggedValue Source = new PlusprofilTaggedValue("source", true, true);
		public static readonly PlusprofilTaggedValue IsDefinedBy = new PlusprofilTaggedValue("isDefinedBy", false, true);
		public static readonly PlusprofilTaggedValue WasDerivedFrom = new PlusprofilTaggedValue("wasDerivedFrom", false, true);
		public static readonly PlusprofilTaggedValue EquivalentClass = new PlusprofilTaggedValue("equivalentClass", false, true);
		public static readonly PlusprofilTaggedValue Range = new PlusprofilTaggedValue("range", false, false);
		public static readonly PlusprofilTaggedValue Domain = new PlusprofilTaggedValue("domain", false, false);
		public static readonly PlusprofilTaggedValue SubPropertyOf = new PlusprofilTaggedValue("subPropertyOf", false, true);
		public static readonly PlusprofilTaggedValue EquivalentProperty = new PlusprofilTaggedValue("equivalentProperty", false, true);
		public static readonly PlusprofilTaggedValue FunctionalProperty = new PlusprofilTaggedValue("functionalProperty", false, false);
		public static readonly PlusprofilTaggedValue InverseOf = new PlusprofilTaggedValue("inverseOf", false, false);
		public static readonly PlusprofilTaggedValue InverseFunctionalProperty = new PlusprofilTaggedValue("inverseFunctionalProperty", false, false);
		public static readonly PlusprofilTaggedValue TransitiveProperty = new PlusprofilTaggedValue("transitiveProperty", false, false);
		public static readonly PlusprofilTaggedValue SymmetricProperty = new PlusprofilTaggedValue("symmetricProperty", false, false);
		public static readonly PlusprofilTaggedValue ApprovalStatus = new PlusprofilTaggedValue("approvalStatus", false, false);
		public static readonly PlusprofilTaggedValue ModelStatus = new PlusprofilTaggedValue("modelStatus", false, false);
		public static readonly PlusprofilTaggedValue Modified = new PlusprofilTaggedValue("modified", false, false);
		public static readonly PlusprofilTaggedValue Namespace = new PlusprofilTaggedValue("namespace", false, false);
		public static readonly PlusprofilTaggedValue NamespacePrefix = new PlusprofilTaggedValue("namespacePrefix", false, false);
		public static readonly PlusprofilTaggedValue Publisher = new PlusprofilTaggedValue("publisher", false, false);
		public static readonly PlusprofilTaggedValue Theme = new PlusprofilTaggedValue("theme", false, false);
		public static readonly PlusprofilTaggedValue VersionInfo = new PlusprofilTaggedValue("versionInfo", false, false);
		public static readonly PlusprofilTaggedValue SubClassOf = new PlusprofilTaggedValue("subClassOf", false, true);
		public static readonly PlusprofilTaggedValue SameAs = new PlusprofilTaggedValue("sameAs", false, true);
		public static readonly PlusprofilTaggedValue Type = new PlusprofilTaggedValue("type", false, false);
		public static readonly PlusprofilTaggedValue LabelDa = new PlusprofilTaggedValue("label (da)", false, true);
		public static readonly PlusprofilTaggedValue LabelEn = new PlusprofilTaggedValue("label (en)", false, true);

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
			LabelEn
		};
	}
}
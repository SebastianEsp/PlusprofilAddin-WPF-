using PlusprofilAddin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlusprofilAddin
{
    static class PlusprofilTaggedValueDefinitions
    {
        /*
         * Definitions of tagged values defined in Plusprofil
         * Used to retrieve tagged values from Enterprise Architect and present them correctly
         * by defining the name of the tagged value (PlusprofilTaggedValue.Name), the definition to show on hover
         * (PlusprofilTaggedValue.Definition), if the value or notes should be edited (PlusprofilTaggedValue.HasMemoField) 
         * and if adding multiple tagged values is allowed (PlusprofilTaggedValue.ManyMultiplicity)
		 * In a future iteration, these definitions could be loaded from a file.
         */
        public readonly static PlusprofilTaggedValue altLabelDa = new PlusprofilTaggedValue("altLabel (da)", "", false, true);
        public readonly static PlusprofilTaggedValue altLabelEn = new PlusprofilTaggedValue("altLabel (en)", "", false, true);
        public readonly static PlusprofilTaggedValue prefLabelDa = new PlusprofilTaggedValue("prefLabel (da)", "", false, false);
        public readonly static PlusprofilTaggedValue prefLabelEn = new PlusprofilTaggedValue("prefLabel (en)", "", false, false);
        public readonly static PlusprofilTaggedValue deprecatedLabelDa = new PlusprofilTaggedValue("deprecatedLabel (da)", "", false, true);
        public readonly static PlusprofilTaggedValue deprecatedLabelEn = new PlusprofilTaggedValue("deprecatedLabel (en)", "", false, true);
        public readonly static PlusprofilTaggedValue definitionDa = new PlusprofilTaggedValue("definition (da)", "", true, false);
        public readonly static PlusprofilTaggedValue definitionEn = new PlusprofilTaggedValue("definition (en)", "", true, false);
        public readonly static PlusprofilTaggedValue applicationNoteDa = new PlusprofilTaggedValue("applicationNote (da)", "", true, true);
        public readonly static PlusprofilTaggedValue applicationNoteEn = new PlusprofilTaggedValue("applicationNote (en)", "", true, true);
        public readonly static PlusprofilTaggedValue exampleDa = new PlusprofilTaggedValue("example (da)", "", true, true);
        public readonly static PlusprofilTaggedValue exampleEn = new PlusprofilTaggedValue("example (en)", "", true, true);
        public readonly static PlusprofilTaggedValue commentDa = new PlusprofilTaggedValue("comment (da)", "", true, true);
        public readonly static PlusprofilTaggedValue commentEn = new PlusprofilTaggedValue("comment (en)", "", true, true);
        public readonly static PlusprofilTaggedValue legalSource = new PlusprofilTaggedValue("legalSource", "", true, true);
        public readonly static PlusprofilTaggedValue source = new PlusprofilTaggedValue("source", "", true, true);
        public readonly static PlusprofilTaggedValue isDefinedBy = new PlusprofilTaggedValue("isDefinedBy", "", false, true);
        public readonly static PlusprofilTaggedValue wasDerivedFrom = new PlusprofilTaggedValue("wasDerivedFrom", "", false, true);
        public readonly static PlusprofilTaggedValue equivalentClass = new PlusprofilTaggedValue("equivalentClass", "", false, true);
        public readonly static PlusprofilTaggedValue range = new PlusprofilTaggedValue("range", "", false, false);
        public readonly static PlusprofilTaggedValue domain = new PlusprofilTaggedValue("domain", "", false, false);
        public readonly static PlusprofilTaggedValue subPropertyOf = new PlusprofilTaggedValue("subPropertyOf", "", false, true);
        public readonly static PlusprofilTaggedValue equivalentProperty = new PlusprofilTaggedValue("equivalentProperty", "", false, true);
        public readonly static PlusprofilTaggedValue functionalProperty = new PlusprofilTaggedValue("functionalProperty", "", false, false);
        public readonly static PlusprofilTaggedValue inverseOf = new PlusprofilTaggedValue("inverseOf", "", false, false);
        public readonly static PlusprofilTaggedValue inverseOfFunctionalProperty = new PlusprofilTaggedValue("inverseOfFunctionalProperty", "", false, false);
        public readonly static PlusprofilTaggedValue transitiveProperty = new PlusprofilTaggedValue("transitiveProperty", "", false, false);
        public readonly static PlusprofilTaggedValue symmetricProperty = new PlusprofilTaggedValue("symmetricproperty", "", false, false);
        public readonly static PlusprofilTaggedValue approvalStatus = new PlusprofilTaggedValue("approvalStatus", "", false, false);
        public readonly static PlusprofilTaggedValue modelStatus = new PlusprofilTaggedValue("modelStatus", "", false, false);
        public readonly static PlusprofilTaggedValue modified = new PlusprofilTaggedValue("modified", "", false, false);
        public readonly static PlusprofilTaggedValue namespace_ = new PlusprofilTaggedValue("namespace", "", false, false); //Underscore added as namespace is a reserved keyword
        public readonly static PlusprofilTaggedValue namespacePrefix = new PlusprofilTaggedValue("namespacePrefix", "", false, false);
        public readonly static PlusprofilTaggedValue publisher = new PlusprofilTaggedValue("publisher", "", false, false);
        public readonly static PlusprofilTaggedValue theme = new PlusprofilTaggedValue("theme", "", false, false);
        public readonly static PlusprofilTaggedValue versionInfo = new PlusprofilTaggedValue("versionInfo", "", false, false);
        public readonly static PlusprofilTaggedValue subClassOf = new PlusprofilTaggedValue("subClassOf", "", false, true);
        public readonly static PlusprofilTaggedValue sameAs = new PlusprofilTaggedValue("sameAs", "", false, true);
        public readonly static PlusprofilTaggedValue type = new PlusprofilTaggedValue("type", "", false, false);
        public readonly static PlusprofilTaggedValue labelDa = new PlusprofilTaggedValue("label (da)", "", false, true);
        public readonly static PlusprofilTaggedValue labelEn = new PlusprofilTaggedValue("label (en)", "", false, true);
    }
}

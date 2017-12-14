using PlusprofilAddin.Model;
using System.Collections.ObjectModel;
using EA;
using PlusprofilAddin.ViewModels;
using System.Collections.Generic;
using PDefinitions = PlusprofilAddin.PlusprofilTaggedValueDefinitions;

namespace PlusprofilAddin
{
    class AttributeDialogViewModel : DialogViewModel
    {
		protected List<PlusprofilTaggedValue> ToAddStereotypeTaggedValues = new List<PlusprofilTaggedValue>
		{
			PDefinitions.range,
			PDefinitions.domain,
			PDefinitions.subPropertyOf,
			PDefinitions.equivalentProperty
		};

		public Attribute Attribute { get; set; }

        public string URIText { get; set; }
        public string UMLText { get; set; }
        public string AliasText { get; set; }
        public string DatatypeText { get; set; }
        public string URIValue { get; set; }
        public string UMLValue { get; set; }
        public string AliasValue { get; set; }
        public string DatatypeValue { get; set; }

        public ObservableCollection<PlusprofilTaggedValue> TaggedValues { get; set; }
        public ObservableCollection<PlusprofilTaggedValue> DanishTaggedValues { get; set; }
        public ObservableCollection<PlusprofilTaggedValue> EnglishTaggedValues { get; set; }
        public ObservableCollection<PlusprofilTaggedValue> ProvenanceTaggedValues { get; set; }
        public ObservableCollection<PlusprofilTaggedValue> StereotypeTaggedValues { get; set; }

        public AttributeDialogViewModel()
        {
            DanishTaggedValues = new ObservableCollection<PlusprofilTaggedValue>();
            EnglishTaggedValues = new ObservableCollection<PlusprofilTaggedValue>();
            ProvenanceTaggedValues = new ObservableCollection<PlusprofilTaggedValue>();
            StereotypeTaggedValues = new ObservableCollection<PlusprofilTaggedValue>();

            URIText = "URI";
            UMLText = "UML";
            AliasText = "Alias";
            DatatypeText = "";
            URIValue = "";
            UMLValue = "";
            AliasValue = "";
            DatatypeValue = "";
        }

        public override void Initialize()
        {
            Attribute = Repository.GetContextObject() as Attribute;

			//Finalize list of stereotype tags to add
			if (Attribute.Stereotype == "DatatypeProperty")
			{
				ToAddStereotypeTaggedValues.Add(PDefinitions.functionalProperty);
				//TODO _defaultAttributeType?
			}
		}
    }
}
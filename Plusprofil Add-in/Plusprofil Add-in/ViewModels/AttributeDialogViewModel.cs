using System.Collections.Generic;
using System.Collections.ObjectModel;
using EA;
using PDefinitions = PlusprofilAddin.PlusprofilTaggedValueDefinitions;

namespace PlusprofilAddin.ViewModels
{
	internal class AttributeDialogViewModel : DialogViewModel
	{
		protected List<PlusprofilTaggedValue> ToAddStereotypeTaggedValues = new List<PlusprofilTaggedValue>
		{
			PDefinitions.Range,
			PDefinitions.Domain,
			PDefinitions.SubPropertyOf,
			PDefinitions.EquivalentProperty
		};

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
		public List<DisplayedTaggedValue> DeleteTaggedValues { get; set; }

		public override void Initialize()
		{
			Attribute = Repository.GetContextObject() as Attribute;

			//Finalize list of stereotype tags to add
			if (Attribute.Stereotype == "DatatypeProperty") ToAddStereotypeTaggedValues.Add(PDefinitions.FunctionalProperty);
		}
	}
}
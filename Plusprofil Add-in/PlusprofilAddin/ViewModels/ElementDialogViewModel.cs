using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using EA;
using PlusprofilAddin.Commands;
using static PlusprofilAddin.PlusprofilTaggedValueDefinitions;
using PTVD = PlusprofilAddin.PlusprofilTaggedValueDefinitions;

namespace PlusprofilAddin.ViewModels
{
	public class ElementDialogViewModel : DialogViewModel
	{
		public List<dynamic> TaggedValuesList;
		
		private readonly List<PlusprofilTaggedValue> _toAddDanishTaggedValues = new List<PlusprofilTaggedValue>
		{
			Definitions.Find(ptv => ptv.Key == "PrefLabelDa"),
			Definitions.Find(ptv => ptv.Key == "AltLabelDa"),
			Definitions.Find(ptv => ptv.Key == "DeprecatedLabelDa"),
			Definitions.Find(ptv => ptv.Key == "DefinitionDa"),
			Definitions.Find(ptv => ptv.Key == "CommentDa"),
			Definitions.Find(ptv => ptv.Key == "ApplicationNoteDa"),
		};

		private readonly List<PlusprofilTaggedValue> _toAddEnglishTaggedValues = new List<PlusprofilTaggedValue>
		{
			Definitions.Find(ptv => ptv.Key == "PrefLabelEn"),
			Definitions.Find(ptv => ptv.Key == "AltLabelEn"),
			Definitions.Find(ptv => ptv.Key == "DeprecatedLabelEn"),
			Definitions.Find(ptv => ptv.Key == "DefinitionEn"),
			Definitions.Find(ptv => ptv.Key == "CommentEn"),
			Definitions.Find(ptv => ptv.Key == "ApplicationNoteEn"),
		};

		private readonly List<PlusprofilTaggedValue> _toAddProvenanceTaggedValues = new List<PlusprofilTaggedValue>
		{
			Definitions.Find(ptv => ptv.Key == "LegalSource"),
			Definitions.Find(ptv => ptv.Key == "Source"),
			Definitions.Find(ptv => ptv.Key == "IsDefinedBy"),
			Definitions.Find(ptv => ptv.Key == "WasDerivedFrom"),
		};

		private readonly List<PlusprofilTaggedValue> _toAddStereotypeTaggedValues = new List<PlusprofilTaggedValue>();

		public ElementDialogViewModel()
		{
			DanishViewmodelTaggedValues = new ObservableCollection<ObservableCollection<ViewmodelTaggedValue>>();
			EnglishViewmodelTaggedValues = new ObservableCollection<ObservableCollection<ViewmodelTaggedValue>>();
			ProvenanceViewmodelTaggedValues = new ObservableCollection<ObservableCollection<ViewmodelTaggedValue>>();
			StereotypeViewmodelTaggedValues = new ObservableCollection<ObservableCollection<ViewmodelTaggedValue>>();
			TaggedValuesList = new List<dynamic>();
			DeleteTaggedValues = new List<ViewmodelTaggedValue>();

			URIValue = "";
			UMLNameValue = "";
			AliasValue = "";
		}

		public Element Element { get; set; }
		public Collection TaggedValues { get; set; }

		public string URIValue { get; set; }
		public string UMLNameValue { get; set; }
		public string AliasValue { get; set; }

		public ViewmodelTaggedValue URIViewmodelTaggedValue { get; set; }
		public ObservableCollection<ObservableCollection<ViewmodelTaggedValue>> ProvenanceViewmodelTaggedValues { get; set; }
		public ObservableCollection<ObservableCollection<ViewmodelTaggedValue>> StereotypeViewmodelTaggedValues { get; set; }

		public override void Initialize()
		{
			SaveCommand = new SaveCommand();
			CancelCommand = new CancelCommand();
			AddCommand = new AddCommand();
			RemoveCommand = new RemoveCommand();

			Element = Repository.GetContextObject();
			UMLNameValue = Element.Name;
			AliasValue = Element.Alias;
			TaggedValues = Element.TaggedValues;

			//Finalize list of stereotype tags to add
			switch (Element.Stereotype)
			{
				case "OwlClass":
				case "RdfsClass":
					_toAddStereotypeTaggedValues.Add(Definitions.Find(ptv => ptv.Key == "EquivalentClass"));
					_toAddStereotypeTaggedValues.Add(Definitions.Find(ptv => ptv.Key == "SubClassOf"));
					break;
				case "Individual":
					_toAddStereotypeTaggedValues.Add(Definitions.Find(ptv => ptv.Key == "SameAs"));
					_toAddStereotypeTaggedValues.Add(Definitions.Find(ptv => ptv.Key == "Type"));
					break;
			}

			//Retrieve all tagged values and store them in a list
			//Tagged values are stored in a list to avoid iterating Collections multiple times, which is very costly
			//In a future iteration of the addin, avoid iterating the collection even once, instead using Repository.SQLQuery to retrieve
			//an XML-formatted list of every Tagged Value where the owner ID is Element.ElementID
			for (int i = 0; i < TaggedValues.Count; i++)
			{
				TaggedValue tv = TaggedValues.GetAt((short) i);
				TaggedValuesList.Add(tv);
			}
			
			// Retrieve URI tagged value and save it in URIViewmodelTaggedValue
			var result = RetrieveTaggedValues(TaggedValuesList, "URI");
			URIViewmodelTaggedValue = new ViewmodelTaggedValue(result.First())
			{
				ResourceDictionary = ResourceDictionary,
				Key = Definitions.Find(ptv => ptv.Key == "URI").Key
			};
			URIValue = URIViewmodelTaggedValue.Value;

			// Add tagged values to list of ViewmodelTaggedValues
			AddTaggedValuesToViewmodelTaggedValues(_toAddDanishTaggedValues, TaggedValuesList, DanishViewmodelTaggedValues);
			AddTaggedValuesToViewmodelTaggedValues(_toAddEnglishTaggedValues, TaggedValuesList, EnglishViewmodelTaggedValues);
			AddTaggedValuesToViewmodelTaggedValues(_toAddProvenanceTaggedValues, TaggedValuesList, ProvenanceViewmodelTaggedValues);
			AddTaggedValuesToViewmodelTaggedValues(_toAddStereotypeTaggedValues, TaggedValuesList, StereotypeViewmodelTaggedValues);
		}
	}
}
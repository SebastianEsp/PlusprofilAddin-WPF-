using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EA;
using PlusprofilAddin.ViewModels.Commands;
using static PlusprofilAddin.PlusprofilTaggedValueDefinitions;
using Attribute = EA.Attribute;

namespace PlusprofilAddin.ViewModels
{
	public class AttributeDialogViewModel : DialogViewModel
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


		public AttributeDialogViewModel()
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
			DatatypeValue = "";
		}

		public Attribute Attribute { get; set; }		
		public Collection TaggedValues { get; set; }


		public string URIValue { get; set; }
		public string UMLNameValue { get; set; }
		public string AliasValue { get; set; }
		public string DatatypeValue { get; set; }

		public ViewmodelTaggedValue URIViewmodelTaggedValue { get; set; }
		public ObservableCollection<ObservableCollection<ViewmodelTaggedValue>> ProvenanceViewmodelTaggedValues { get; set; }
		public ObservableCollection<ObservableCollection<ViewmodelTaggedValue>> StereotypeViewmodelTaggedValues { get; set; }
		

		public override void Initialize()
		{
			SaveCommand = new SaveCommand();
			CancelCommand = new CancelCommand();
			AddCommand = new AddCommand();
			RemoveCommand = new RemoveCommand();

			Attribute = Repository.GetContextObject();
			UMLNameValue = Attribute.Name;
			AliasValue = Attribute.Alias;
			DatatypeValue = Attribute.Type;
			TaggedValues = Attribute.TaggedValues;
			

			//Finalize list of stereotype tags to add
			if (Attribute.Stereotype == "DatatypeProperty") _toAddStereotypeTaggedValues.Add(Definitions.Find(ptv => ptv.Key == "FunctionalProperty"));
			if (Attribute.Stereotype == "RdfsProperty" || Attribute.Stereotype == "DatatypeProperty")
			{
				_toAddStereotypeTaggedValues.Add(Definitions.Find(ptv => ptv.Key == "Range"));
				_toAddStereotypeTaggedValues.Add(Definitions.Find(ptv => ptv.Key == "Domain"));
				_toAddStereotypeTaggedValues.Add(Definitions.Find(ptv => ptv.Key == "SubPropertyOf"));
				_toAddStereotypeTaggedValues.Add(Definitions.Find(ptv => ptv.Key == "EquivalentProperty"));
			}

			//Retrieve all tagged values and store them in a list
			//Tagged values are stored in a list to avoid iterating Collections multiple times, which is very costly
			//In a future iteration of the addin, avoid iterating the collection even once, instead using Repository.SQLQuery to retrieve
			//an XML-formatted list of every Tagged Value where the owner ID is Element.ElementID
			for (short i = 0; i < TaggedValues.Count; i++)
			{
				dynamic tv = TaggedValues.GetAt(i);
				TaggedValuesList.Add(tv);
			}
			
			// Retrieve URI tagged value and save it in URIViewmodelTaggedValue
			var result = RetrieveTaggedValues(TaggedValuesList, "URI");
			URIViewmodelTaggedValue = new ViewmodelTaggedValue(result.First())
			{
				ResourceDictionary = ResourceDictionary,
				Key = Definitions.Find(ptv => ptv.Key == "URI").Key
			};
			
			URIViewmodelTaggedValue.Initialize();
			URIValue = URIViewmodelTaggedValue.Value;

			// Add tagged values to list of ViewmodelTaggedValues
			AddTaggedValuesToViewmodelTaggedValues(_toAddDanishTaggedValues, TaggedValuesList, DanishViewmodelTaggedValues);
			AddTaggedValuesToViewmodelTaggedValues(_toAddEnglishTaggedValues, TaggedValuesList, EnglishViewmodelTaggedValues);
			AddTaggedValuesToViewmodelTaggedValues(_toAddProvenanceTaggedValues, TaggedValuesList, ProvenanceViewmodelTaggedValues);
			AddTaggedValuesToViewmodelTaggedValues(_toAddStereotypeTaggedValues, TaggedValuesList, StereotypeViewmodelTaggedValues);
		}
	}
}
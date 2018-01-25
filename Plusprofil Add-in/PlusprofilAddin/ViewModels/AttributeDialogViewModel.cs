using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EA;
using PlusprofilAddin.Commands;
using static PlusprofilAddin.PlusprofilTaggedValueDefinitions;
using Attribute = EA.Attribute;

namespace PlusprofilAddin.ViewModels
{
	public class AttributeDialogViewModel : DialogViewModel
	{
		public List<dynamic> TaggedValuesList;
		
		private readonly List<PlusprofilTaggedValue> _toAddDanishTaggedValues = new List<PlusprofilTaggedValue>
		{
			PrefLabelDa,
			AltLabelDa,
			DeprecatedLabelDa,
			DefinitionDa,
			ExampleDa,
			CommentDa,
			ApplicationNoteDa
		};

		private readonly List<PlusprofilTaggedValue> _toAddEnglishTaggedValues = new List<PlusprofilTaggedValue>
		{
			PrefLabelEn,
			AltLabelEn,
			DeprecatedLabelEn,
			DefinitionEn,
			ExampleEn,
			CommentEn,
			ApplicationNoteEn
		};

		private readonly List<PlusprofilTaggedValue> _toAddProvenanceTaggedValues = new List<PlusprofilTaggedValue>
		{
			LegalSource,
			Source,
			IsDefinedBy,
			WasDerivedFrom
		};

		private readonly List<PlusprofilTaggedValue> _toAddStereotypeTaggedValues = new List<PlusprofilTaggedValue>();


		public AttributeDialogViewModel()
		{
			DanishTaggedValues = new ObservableCollection<ObservableCollection<ViewmodelTaggedValue>>();
			EnglishTaggedValues = new ObservableCollection<ObservableCollection<ViewmodelTaggedValue>>();
			ProvenanceTaggedValues = new ObservableCollection<ObservableCollection<ViewmodelTaggedValue>>();
			StereotypeTaggedValues = new ObservableCollection<ObservableCollection<ViewmodelTaggedValue>>();
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
		public ObservableCollection<ObservableCollection<ViewmodelTaggedValue>> ProvenanceTaggedValues { get; set; }
		public ObservableCollection<ObservableCollection<ViewmodelTaggedValue>> StereotypeTaggedValues { get; set; }
		

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
			if (Attribute.Stereotype == "DatatypeProperty") _toAddStereotypeTaggedValues.Add(FunctionalProperty);
			if (Attribute.Stereotype == "RdfsProperty" || Attribute.Stereotype == "DatatypeProperty")
			{
				_toAddStereotypeTaggedValues.Add(Range);
				_toAddStereotypeTaggedValues.Add(Domain);
				_toAddStereotypeTaggedValues.Add(SubPropertyOf);
				_toAddStereotypeTaggedValues.Add(EquivalentProperty);
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
			URIViewmodelTaggedValue = new ViewmodelTaggedValue(result.First(), ResourceDictionary);
			URIValue = URIViewmodelTaggedValue.Value;

			//Add all Danish tagged values to list
			foreach (PlusprofilTaggedValue ptv in _toAddDanishTaggedValues)
			{
				result = RetrieveTaggedValues(TaggedValuesList, ptv.Name);
				var resultList = new ObservableCollection<ViewmodelTaggedValue>();
				foreach (AttributeTag attributeTag in result) resultList.Add(new ViewmodelTaggedValue(attributeTag, ResourceDictionary));
				DanishTaggedValues.Add(resultList);
			}

			//Add all English tagged values to list
			foreach (PlusprofilTaggedValue ptv in _toAddEnglishTaggedValues)
			{
				result = RetrieveTaggedValues(TaggedValuesList, ptv.Name);
				var resultList = new ObservableCollection<ViewmodelTaggedValue>();
				foreach (AttributeTag attributeTag in result) resultList.Add(new ViewmodelTaggedValue(attributeTag, ResourceDictionary));
				EnglishTaggedValues.Add(resultList);
			}

			//Add all provenance tagged values to list
			foreach (PlusprofilTaggedValue ptv in _toAddProvenanceTaggedValues)
			{
				result = RetrieveTaggedValues(TaggedValuesList, ptv.Name);
				var resultList = new ObservableCollection<ViewmodelTaggedValue>();
				foreach (AttributeTag attributeTag in result) resultList.Add(new ViewmodelTaggedValue(attributeTag, ResourceDictionary));
				ProvenanceTaggedValues.Add(resultList);
			}

			//Add all stereotype-specific tagged values to list
			foreach (PlusprofilTaggedValue ptv in _toAddStereotypeTaggedValues)
			{
				result = RetrieveTaggedValues(TaggedValuesList, ptv.Name);
				var resultList = new ObservableCollection<ViewmodelTaggedValue>();
				foreach (AttributeTag attributeTag in result) resultList.Add(new ViewmodelTaggedValue(attributeTag, ResourceDictionary));
				StereotypeTaggedValues.Add(resultList);
			}
		}
	}
}
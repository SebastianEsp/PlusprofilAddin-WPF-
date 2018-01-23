using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using EA;
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
			DanishTaggedValues = new ObservableCollection<ObservableCollection<DisplayedTaggedValue>>();
			EnglishTaggedValues = new ObservableCollection<ObservableCollection<DisplayedTaggedValue>>();
			ProvenanceTaggedValues = new ObservableCollection<ObservableCollection<DisplayedTaggedValue>>();
			StereotypeTaggedValues = new ObservableCollection<ObservableCollection<DisplayedTaggedValue>>();
			TaggedValuesList = new List<dynamic>();
			DeleteTaggedValues = new List<DisplayedTaggedValue>();

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

		public ObservableCollection<ObservableCollection<DisplayedTaggedValue>> DanishTaggedValues { get; set; }
		public ObservableCollection<ObservableCollection<DisplayedTaggedValue>> EnglishTaggedValues { get; set; }
		public ObservableCollection<ObservableCollection<DisplayedTaggedValue>> ProvenanceTaggedValues { get; set; }
		public ObservableCollection<ObservableCollection<DisplayedTaggedValue>> StereotypeTaggedValues { get; set; }
		public List<DisplayedTaggedValue> DeleteTaggedValues { get; set; }

		public override void Initialize()
		{
			Attribute = Repository.GetContextObject();

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
			for (int i = 0; i < TaggedValues.Count; i++)
			{
				dynamic tv = TaggedValues.GetAt((short) i);
				TaggedValuesList.Add(tv);
			}
			
			//Declare List to hold result of list lookups
			List<dynamic> result;

			try
			{
				result = RetrieveTaggedValues(TaggedValuesList, "URI");
				URIValue = result.First().Value;
			}
			catch (ArgumentException e)
			{
				MessageBox.Show(e.Message);
			}

			//Add all Danish tagged values to list
			foreach (PlusprofilTaggedValue ptv in _toAddDanishTaggedValues)
			{
				result = RetrieveTaggedValues(TaggedValuesList, ptv.Name);
				ObservableCollection<DisplayedTaggedValue> resultList = new ObservableCollection<DisplayedTaggedValue>();
				foreach (dynamic tv in result) resultList.Add(new DisplayedTaggedValue(tv));
				DanishTaggedValues.Add(resultList);
			}

			//Add all English tagged values to list
			foreach (PlusprofilTaggedValue ptv in _toAddEnglishTaggedValues)
			{
				result = RetrieveTaggedValues(TaggedValuesList, ptv.Name);
				ObservableCollection<DisplayedTaggedValue> resultList = new ObservableCollection<DisplayedTaggedValue>();
				foreach (dynamic tv in result) resultList.Add(new DisplayedTaggedValue(tv));
				EnglishTaggedValues.Add(resultList);
			}

			//Add all provenance tagged values to list
			foreach (PlusprofilTaggedValue ptv in _toAddProvenanceTaggedValues)
			{
				result = RetrieveTaggedValues(TaggedValuesList, ptv.Name);
				ObservableCollection<DisplayedTaggedValue> resultList = new ObservableCollection<DisplayedTaggedValue>();
				foreach (dynamic tv in result) resultList.Add(new DisplayedTaggedValue(tv));
				ProvenanceTaggedValues.Add(resultList);
			}

			//Add all stereotype-specific tagged values to list
			foreach (PlusprofilTaggedValue ptv in _toAddStereotypeTaggedValues)
			{
				result = RetrieveTaggedValues(TaggedValuesList, ptv.Name);
				ObservableCollection<DisplayedTaggedValue> resultList = new ObservableCollection<DisplayedTaggedValue>();
				foreach (dynamic tv in result) resultList.Add(new DisplayedTaggedValue(tv));
				StereotypeTaggedValues.Add(resultList);
			}
		}
	}
}
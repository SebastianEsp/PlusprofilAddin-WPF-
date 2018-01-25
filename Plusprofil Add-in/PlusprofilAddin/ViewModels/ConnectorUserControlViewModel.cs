﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EA;
using static PlusprofilAddin.PlusprofilTaggedValueDefinitions;

namespace PlusprofilAddin.ViewModels
{
	public class ConnectorUserControlViewModel : DialogViewModel
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

		public ConnectorUserControlViewModel()
		{
			DanishTaggedValues = new ObservableCollection<ObservableCollection<ViewmodelTaggedValue>>();
			EnglishTaggedValues = new ObservableCollection<ObservableCollection<ViewmodelTaggedValue>>();
			ProvenanceTaggedValues = new ObservableCollection<ObservableCollection<ViewmodelTaggedValue>>();
			StereotypeTaggedValues = new ObservableCollection<ObservableCollection<ViewmodelTaggedValue>>();
			TaggedValuesList = new List<dynamic>();
			DeleteTaggedValues = new List<ViewmodelTaggedValue>();

			ElementNameValue = "";
			UMLNameValue = "";
			URIValue = "";
			AliasValue = "";
			MultiplicityValue = "";
		}

		
		public ConnectorEnd ConnectorEnd { get; set; }
		public Collection TaggedValues { get; set; }

		public string ElementNameValue { get; set; }
		public string ConnectorEndType { get; set; }
		public string UMLNameValue { get; set; }
		public string URIValue { get; set; }
		public string AliasValue { get; set; }
		public string MultiplicityValue { get; set; }

		public ViewmodelTaggedValue URIViewmodelTaggedValue { get; set; }
		public ObservableCollection<ObservableCollection<ViewmodelTaggedValue>> ProvenanceTaggedValues { get; set; }
		public ObservableCollection<ObservableCollection<ViewmodelTaggedValue>> StereotypeTaggedValues { get; set; }

		public override void Initialize()
		{
			ConnectorEndType = (ConnectorEnd.End == "Supplier") ? "Source" : "Target";
			UMLNameValue = ConnectorEnd.Role;
			AliasValue = ConnectorEnd.Alias;
			MultiplicityValue = ConnectorEnd.Cardinality;
			TaggedValues = ConnectorEnd.TaggedValues;

			//Finalize list of stereotype tags to add
			if (ConnectorEnd.Stereotype == "ObjectProperty")
			{
				_toAddStereotypeTaggedValues.Add(InverseOf);
				_toAddStereotypeTaggedValues.Add(FunctionalProperty);
				_toAddStereotypeTaggedValues.Add(InverseFunctionalProperty);
				_toAddStereotypeTaggedValues.Add(SymmetricProperty);
				_toAddStereotypeTaggedValues.Add(TransitiveProperty);
			}
			if (ConnectorEnd.Stereotype == "RdfsProperty" || ConnectorEnd.Stereotype == "ObjectProperty")
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
				RoleTag tv = TaggedValues.GetAt((short) i);
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
				foreach (RoleTag rt in result) resultList.Add(new ViewmodelTaggedValue(rt, ResourceDictionary));
				DanishTaggedValues.Add(resultList);
			}

			//Add all English tagged values to list
			foreach (PlusprofilTaggedValue ptv in _toAddEnglishTaggedValues)
			{
				result = RetrieveTaggedValues(TaggedValuesList, ptv.Name);
				var resultList = new ObservableCollection<ViewmodelTaggedValue>();
				foreach (RoleTag rt in result) resultList.Add(new ViewmodelTaggedValue(rt, ResourceDictionary));
				EnglishTaggedValues.Add(resultList);
			}

			//Add all provenance tagged values to list
			foreach (PlusprofilTaggedValue ptv in _toAddProvenanceTaggedValues)
			{
				result = RetrieveTaggedValues(TaggedValuesList, ptv.Name);
				var resultList = new ObservableCollection<ViewmodelTaggedValue>();
				foreach (RoleTag rt in result) resultList.Add(new ViewmodelTaggedValue(rt, ResourceDictionary));
				ProvenanceTaggedValues.Add(resultList);
			}

			//Add all stereotype-specific tagged values to list
			foreach (PlusprofilTaggedValue ptv in _toAddStereotypeTaggedValues)
			{
				result = RetrieveTaggedValues(TaggedValuesList, ptv.Name);
				var resultList = new ObservableCollection<ViewmodelTaggedValue>();
				foreach (RoleTag rt in result) resultList.Add(new ViewmodelTaggedValue(rt, ResourceDictionary));
				StereotypeTaggedValues.Add(resultList);
			}
		}
	}
}
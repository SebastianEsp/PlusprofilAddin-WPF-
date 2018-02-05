using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EA;
using static PlusprofilAddin.PlusprofilTaggedValueDefinitions;

namespace PlusprofilAddin.ViewModels
{
	/// <summary>
	/// ViewModel used to update View (Window) state and retrieve user input to update Model (Sparx Systems Enterprise Architect) state.
	/// </summary>
	/// <inheritdoc />
	public class ConnectorUserControlViewModel : DialogViewModel
	{
		private readonly List<dynamic> _taggedValuesList;

		private readonly List<PlusprofilTaggedValue> _toAddDanishTaggedValues = new List<PlusprofilTaggedValue>
		{
			Definitions.Find(ptv => ptv.Key == "PrefLabelDa"),
			Definitions.Find(ptv => ptv.Key == "AltLabelDa"),
			Definitions.Find(ptv => ptv.Key == "DeprecatedLabelDa"),
			Definitions.Find(ptv => ptv.Key == "DefinitionDa"),
			Definitions.Find(ptv => ptv.Key == "CommentDa"),
			Definitions.Find(ptv => ptv.Key == "ApplicationNoteDa")
		};

		private readonly List<PlusprofilTaggedValue> _toAddEnglishTaggedValues = new List<PlusprofilTaggedValue>
		{
			Definitions.Find(ptv => ptv.Key == "PrefLabelEn"),
			Definitions.Find(ptv => ptv.Key == "AltLabelEn"),
			Definitions.Find(ptv => ptv.Key == "DeprecatedLabelEn"),
			Definitions.Find(ptv => ptv.Key == "DefinitionEn"),
			Definitions.Find(ptv => ptv.Key == "CommentEn"),
			Definitions.Find(ptv => ptv.Key == "ApplicationNoteEn")
		};

		private readonly List<PlusprofilTaggedValue> _toAddProvenanceTaggedValues = new List<PlusprofilTaggedValue>
		{
			Definitions.Find(ptv => ptv.Key == "LegalSource"),
			Definitions.Find(ptv => ptv.Key == "Source"),
			Definitions.Find(ptv => ptv.Key == "IsDefinedBy"),
			Definitions.Find(ptv => ptv.Key == "WasDerivedFrom")
		};

		private readonly List<PlusprofilTaggedValue> _toAddStereotypeTaggedValues = new List<PlusprofilTaggedValue>();

		/// <summary>
		/// 
		/// </summary>
		/// <inheritdoc />
		public ConnectorUserControlViewModel()
		{
			DanishViewmodelTaggedValues = new ObservableCollection<ObservableCollection<ViewModelTaggedValue>>();
			EnglishViewmodelTaggedValues = new ObservableCollection<ObservableCollection<ViewModelTaggedValue>>();
			ProvenanceViewmodelTaggedValues = new ObservableCollection<ObservableCollection<ViewModelTaggedValue>>();
			StereotypeViewmodelTaggedValues = new ObservableCollection<ObservableCollection<ViewModelTaggedValue>>();
			_taggedValuesList = new List<dynamic>();
			DeleteTaggedValues = new List<ViewModelTaggedValue>();

			ElementNameValue = "";
			UMLNameValue = "";
			URIValue = "";
			AliasValue = "";
			MultiplicityValue = "";
		}

		
		/// <summary>Sparx Systems Enterprise Architect object representing a ConnectorEnd of the Connector selected when the add-in is opened.</summary>
		public ConnectorEnd ConnectorEnd { get; set; }

		/// <summary>Sparx Systems Enterprise Architect object holding the tagged values of the ConnectorEnd, i.e. <c>EA.ConnectorEnd.TaggedValues</c>.</summary>
		public Collection TaggedValues { get; set; }

		/// <summary>String representing the name of the element associated with the <c>ConnectorEnd</c></summary>
		public string ElementNameValue { get; set; }
		
		/// <summary>String representing the type of <c>ConnectorEnd</c>.</summary>
		public string ConnectorEndType { get; set; }
		
		/// <summary>String representing <c>EA.ConnectorEnd.Role</c>.</summary>
		public string UMLNameValue { get; set; }
		
		/// <summary>String representing <c>EA.RoleTag.Value</c> (after <c>RoleTag</c>-specific filtering) of the <c>EA.RoleTag</c> with Name "URI"</summary>
		public string URIValue { get; set; }
		
		/// <summary>String representing <c>EA.ConnectorEnd.Alias</c>.</summary>
		public string AliasValue { get; set; }
		
		/// <summary>String representing <c>EA.ConnectorEnd.Cardinality</c></summary>
		public string MultiplicityValue { get; set; }

		/// <summary>String representing <c>EA.Element.Stereotype</c>.</summary>
		public string StereotypeString { get; set; }

		/// <summary><c>ViewModelTaggedValue</c> representing the <c>EA.RoleTag</c> with Name "URI"</summary>
		public ViewModelTaggedValue URIViewmodelTaggedValue { get; set; }

		/// <summary>Collection of collection of <c>ViewModelTaggedValue</c>s that should be grouped in a single UI element.</summary>
		public ObservableCollection<ObservableCollection<ViewModelTaggedValue>> ProvenanceViewmodelTaggedValues { get; set; }

		/// <summary>Collection of collection of <c>ViewModelTaggedValue</c>s that should be grouped in a single UI element.</summary>
		public ObservableCollection<ObservableCollection<ViewModelTaggedValue>> StereotypeViewmodelTaggedValues { get; set; }

		/// <inheritdoc />
		public override void Initialize()
		{
			ConnectorEndType = ConnectorEnd.End == "Supplier" ? "Source" : "Target";
			UMLNameValue = ConnectorEnd.Role;
			AliasValue = ConnectorEnd.Alias;
			MultiplicityValue = ConnectorEnd.Cardinality;
			TaggedValues = ConnectorEnd.TaggedValues;

			//Finalize list of stereotype tags to add
			switch (ConnectorEnd.Stereotype)
			{
				case "RdfsProperty":
					_toAddStereotypeTaggedValues.Add(Definitions.Find(ptv => ptv.Key == "RangeConnectorEnd"));
					_toAddStereotypeTaggedValues.Add(Definitions.Find(ptv => ptv.Key == "DomainConnectorEnd"));
					_toAddStereotypeTaggedValues.Add(Definitions.Find(ptv => ptv.Key == "SubPropertyOf"));
					_toAddStereotypeTaggedValues.Add(Definitions.Find(ptv => ptv.Key == "EquivalentProperty"));
					StereotypeString = (string) ResourceDictionary["RdfsPropertyCharacteristics"];
					break;

				case "ObjectProperty":
					_toAddStereotypeTaggedValues.Add(Definitions.Find(ptv => ptv.Key == "RangeConnectorEnd"));
					_toAddStereotypeTaggedValues.Add(Definitions.Find(ptv => ptv.Key == "DomainConnectorEnd"));
					_toAddStereotypeTaggedValues.Add(Definitions.Find(ptv => ptv.Key == "SubPropertyOf"));
					_toAddStereotypeTaggedValues.Add(Definitions.Find(ptv => ptv.Key == "EquivalentProperty"));
					_toAddStereotypeTaggedValues.Add(Definitions.Find(ptv => ptv.Key == "InverseOf"));
					_toAddStereotypeTaggedValues.Add(Definitions.Find(ptv => ptv.Key == "FunctionalProperty"));
					_toAddStereotypeTaggedValues.Add(Definitions.Find(ptv => ptv.Key == "InverseFunctionalProperty"));
					_toAddStereotypeTaggedValues.Add(Definitions.Find(ptv => ptv.Key == "SymmetricProperty"));
					_toAddStereotypeTaggedValues.Add(Definitions.Find(ptv => ptv.Key == "TransitiveProperty"));
					StereotypeString = (string) ResourceDictionary["ObjectPropertyCharacteristics"];
					break;
			}

			//Retrieve all tagged values and store them in a list
			//Tagged values are stored in a list to avoid iterating Collections multiple times, which is very costly
			//In a future iteration of the addin, avoid iterating the collection even once, instead using Repository.SQLQuery to retrieve
			//an XML-formatted list of every Tagged Value where the owner ID is Element.ElementID
			for (short i = 0; i < TaggedValues.Count; i++)
			{
				RoleTag tv = TaggedValues.GetAt(i);
				_taggedValuesList.Add(tv);
			}
			
			// Retrieve URI tagged value and save it in URIViewmodelTaggedValue
			try
			{
				var result = RetrieveTaggedValues(_taggedValuesList, "URI");
				URIViewmodelTaggedValue = new ViewModelTaggedValue(result.First())
				{
					ResourceDictionary = ResourceDictionary,
					Key = Definitions.Find(ptv => ptv.Key == "URI").Key
				};
				URIViewmodelTaggedValue.Initialize();
				URIValue = URIViewmodelTaggedValue.Value;
			}
			catch (ArgumentException)
			{
				//URI does not exist: Do nothing
			}

			// Add tagged values to list of ViewmodelTaggedValues
			DanishViewmodelTaggedValues = AddTaggedValuesToViewmodelTaggedValues(_toAddDanishTaggedValues, _taggedValuesList);
			EnglishViewmodelTaggedValues = AddTaggedValuesToViewmodelTaggedValues(_toAddEnglishTaggedValues, _taggedValuesList);
			ProvenanceViewmodelTaggedValues = AddTaggedValuesToViewmodelTaggedValues(_toAddProvenanceTaggedValues, _taggedValuesList);
			StereotypeViewmodelTaggedValues = AddTaggedValuesToViewmodelTaggedValues(_toAddStereotypeTaggedValues, _taggedValuesList);
		}
	}
}
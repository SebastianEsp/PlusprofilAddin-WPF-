using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using EA;
using PlusprofilAddin.ViewModels.Commands;
using static PlusprofilAddin.PlusprofilTaggedValueDefinitions;

namespace PlusprofilAddin.ViewModels
{

	/// <summary>
	/// ViewModel used to update View (Window) state and retrieve user input to update Model (Sparx Systems Enterprise Architect) state.
	/// </summary>
	/// <inheritdoc />
	public class ElementDialogViewModel : DialogViewModel
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

		/// <inheritdoc/>
		public ElementDialogViewModel()
		{
			DanishViewmodelTaggedValues = new ObservableCollection<ObservableCollection<ViewModelTaggedValue>>();
			EnglishViewmodelTaggedValues = new ObservableCollection<ObservableCollection<ViewModelTaggedValue>>();
			ProvenanceViewmodelTaggedValues = new ObservableCollection<ObservableCollection<ViewModelTaggedValue>>();
			StereotypeViewmodelTaggedValues = new ObservableCollection<ObservableCollection<ViewModelTaggedValue>>();
			_taggedValuesList = new List<dynamic>();
			DeleteTaggedValues = new List<ViewModelTaggedValue>();

			URIValue = "";
			UMLNameValue = "";
			AliasValue = "";
		}

		/// <summary>Sparx Systems Enterprise Architect object representing the element selected when the add-in is opened.</summary>
		public Element Element { get; set; }

		/// <summary>Sparx Systems Enterprise Architect object holding the tagged values of the element, i.e. <c>EA.Element.TaggedValues</c>.</summary>
		public Collection TaggedValues { get; set; }

		/// <summary>String representing <c>EA.Element.Name</c>.</summary>
		public string UMLNameValue { get; set; }

		/// <summary>String representing <c>EA.TaggedValue.Value</c> of the <c>EA.TaggedValue</c> with Name "URI"</summary>
		public string URIValue { get; set; }

		/// <summary>String representing <c>EA.Element.Alias</c>.</summary>
		public string AliasValue { get; set; }

		/// <summary>String representing <c>EA.Element.Stereotype</c>.</summary>
		public string StereotypeString { get; set; }

		/// <summary><c>ViewModelTaggedValue</c> representing the <c>EA.TaggedValue</c> with Name "URI"</summary>
		public ViewModelTaggedValue URIViewmodelTaggedValue { get; set; }

		/// <summary>Collection of collections of <c>ViewModelTaggedValue</c>s that should be grouped in a single UI element.</summary>
		public ObservableCollection<ObservableCollection<ViewModelTaggedValue>> ProvenanceViewmodelTaggedValues { get; set; }

		/// <summary>Collection of collections of <c>ViewModelTaggedValue</c>s that should be grouped in a single UI element.</summary>
		public ObservableCollection<ObservableCollection<ViewModelTaggedValue>> StereotypeViewmodelTaggedValues { get; set; }

		/// <inheritdoc />
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
					_toAddStereotypeTaggedValues.Add(Definitions.Find(ptv => ptv.Key == "EquivalentClass"));
					_toAddStereotypeTaggedValues.Add(Definitions.Find(ptv => ptv.Key == "SubClassOf"));
					StereotypeString = (string) ResourceDictionary["OwlClassCharacteristics"];
					break;
				case "RdfsClass":
					_toAddStereotypeTaggedValues.Add(Definitions.Find(ptv => ptv.Key == "EquivalentClass"));
					_toAddStereotypeTaggedValues.Add(Definitions.Find(ptv => ptv.Key == "SubClassOf"));
					StereotypeString = (string) ResourceDictionary["RdfsClassCharacteristics"];
					break;
				case "Individual":
					_toAddStereotypeTaggedValues.Add(Definitions.Find(ptv => ptv.Key == "SameAs"));
					_toAddStereotypeTaggedValues.Add(Definitions.Find(ptv => ptv.Key == "Type"));
					StereotypeString = (string) ResourceDictionary["IndividualCharacteristics"];
					break;
			}

			//Retrieve all tagged values and store them in a list
			//Tagged values are stored in a list to avoid iterating Collections multiple times, which is very costly
			//In a future iteration of the addin, avoid iterating the collection even once, instead using Repository.SQLQuery to retrieve
			//an XML-formatted list of every Tagged Value where the owner ID is Element.ElementID
			for (short i = 0; i < TaggedValues.Count; i++)
			{
				TaggedValue tv = TaggedValues.GetAt(i);
				_taggedValuesList.Add(tv);
			}
			
			// Retrieve URI tagged value and save it in URIViewmodelTaggedValue
			var result = RetrieveTaggedValues(_taggedValuesList, "URI");
			URIViewmodelTaggedValue = new ViewModelTaggedValue(result.First())
			{
				ResourceDictionary = ResourceDictionary,
				Key = Definitions.Find(ptv => ptv.Key == "URI").Key
			};
			URIViewmodelTaggedValue.Initialize();
			URIValue = URIViewmodelTaggedValue.Value;

			// Add tagged values to list of ViewmodelTaggedValues
			DanishViewmodelTaggedValues = AddTaggedValuesToViewmodelTaggedValues(_toAddDanishTaggedValues, _taggedValuesList);
			EnglishViewmodelTaggedValues = AddTaggedValuesToViewmodelTaggedValues(_toAddEnglishTaggedValues, _taggedValuesList);
			ProvenanceViewmodelTaggedValues = AddTaggedValuesToViewmodelTaggedValues(_toAddProvenanceTaggedValues, _taggedValuesList);
			StereotypeViewmodelTaggedValues = AddTaggedValuesToViewmodelTaggedValues(_toAddStereotypeTaggedValues, _taggedValuesList);
		}
	}
}
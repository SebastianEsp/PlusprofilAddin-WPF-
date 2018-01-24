using System.Collections.Generic;
using System.Collections.ObjectModel;
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
			DanishTaggedValues = new ObservableCollection<ObservableCollection<DisplayedTaggedValue>>();
			EnglishTaggedValues = new ObservableCollection<ObservableCollection<DisplayedTaggedValue>>();
			ProvenanceTaggedValues = new ObservableCollection<ObservableCollection<DisplayedTaggedValue>>();
			StereotypeTaggedValues = new ObservableCollection<ObservableCollection<DisplayedTaggedValue>>();
			TaggedValuesList = new List<dynamic>();
			DeleteTaggedValues = new List<DisplayedTaggedValue>();

			UMLNameValue = "Placeholder1";
			URIValue = "Placeholder2";
			AliasValue = "Placeholder3";
			MultiplicityValue = "";
		}

		public ConnectorEnd ConnectorEnd { get; set; }
		public Collection TaggedValues { get; set; }

		public string UMLNameValue { get; set; }
		public string URIValue { get; set; }
		public string AliasValue { get; set; }
		public string MultiplicityValue { get; set; }

		public ObservableCollection<ObservableCollection<DisplayedTaggedValue>> DanishTaggedValues { get; set; }
		public ObservableCollection<ObservableCollection<DisplayedTaggedValue>> EnglishTaggedValues { get; set; }
		public ObservableCollection<ObservableCollection<DisplayedTaggedValue>> ProvenanceTaggedValues { get; set; }
		public ObservableCollection<ObservableCollection<DisplayedTaggedValue>> StereotypeTaggedValues { get; set; }
		public List<DisplayedTaggedValue> DeleteTaggedValues { get; set; }

		public override void Initialize()
		{
		}
	}
}